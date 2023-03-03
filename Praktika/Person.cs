using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Praktika
{
    public class Person:INotifyPropertyChanged
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; } = null;
        public string Birthdate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public override string ToString()
        {
            return $"{Name}-{Surname}-{Email}-{Number}-{Birthdate}";
        }

    }
}
