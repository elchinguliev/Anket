using Microsoft.Win32;
using Newtonsoft.Json;
using Praktika;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Person person = new Person();
        Person person2 = new Person();
        List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();
            var files = Directory.GetFiles(".");
            foreach (var item in files)
            {
                if (item.EndsWith(".json"))
                {
                    var obj = JsonConvert.DeserializeObject<Person>(File.ReadAllText(item));
                    persons.Add(obj);
                }
            }
            return persons;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            person = new Person();
            person.Name = nameTxb.Text;
            person.Surname = surnameTxb.Text;
            person.Email = emailTxb.Text;
            person.Number = monilePhoneTxb.Text;
            person.Birthdate=birthdateTxb.Text;
            listBox.DisplayMemberPath=nameof(person.Name);
            listBox.Items.Add(person);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var human = listBox.SelectedItem as Person;
            nameTxb.Text = human.Name;
            surnameTxb.Text = human.Surname;
            emailTxb.Text = human.Email;
            monilePhoneTxb.Text = human.Number;
            birthdateTxb.Text = human.Birthdate;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Write(person);
            nameTxb.Text = "";
            surnameTxb.Text = "";
            emailTxb.Text = "";
            monilePhoneTxb.Text = "";
            birthdateTxb.Text = "";

        }


        public void Write(Person newPerson)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(newPerson.Name+".json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, newPerson);
                }
            }
        }


        public Person Read(string filename)
        {
            Person person = new Person();
            try
            {
                var context = File.ReadAllText(filename);
                person = JsonConvert.DeserializeObject<Person>(context);
            }
            catch (Exception)
            {

            }
            return person;
        }

        public void WriteListBox(ListBox humans)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter("humans.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, humans);
                }
            }
        }
        private void chhangeBtn(object sender, RoutedEventArgs e)
        {
    
            person.Name = nameTxb.Text;
            person.Surname = surnameTxb.Text;
            person.Email = emailTxb.Text;
            person.Number = monilePhoneTxb.Text;
            person.Birthdate=birthdateTxb.Text;
            listBox.DisplayMemberPath=nameof(person.Name);
        

        }



        private void loaddBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fileNameTxb.Text == "")
                {
                    var persons = GetAllPersons();
                    listBox.Items.Clear();
                    listBox.Items.Add(persons.ToArray());
                    listBox.DisplayMemberPath = nameof(Person.Name);
                }
                var path = Directory.GetCurrentDirectory() + "\\" + fileNameTxb.Text;

                if (fileNameTxb.Text != "" && !fileNameTxb.Text.Contains(".json"))
                {
                    fileNameTxb.Text += ".json";
                }
                if (File.Exists(fileNameTxb.Text))
                {
                    person2 = Read(fileNameTxb.Text);
                    nameTxb.Text = person2.Name;
                    surnameTxb.Text = person2.Surname;
                    emailTxb.Text = person2.Email;
                    monilePhoneTxb.Text = person2.Number;
                    birthdateTxb.Text = person2.Birthdate;
                }
               
            }
            catch { }
        }
    }
}












