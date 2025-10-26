using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfP7.Pages;

namespace WpfP7
{
    public class Doctor : INotifyPropertyChanged
    {
        private int _id = 0;
        public int Id 
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "Имя пользователя";
        public string Name 
        { 
            get => _name;
            set 
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName = "Фамилия пользователя";
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _middleName = "Отчество пользователя";
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _specification = "Спецификация пользователя";
        public string Specification
        {
            get => _specification;
            set
            {
                if (_specification != value)
                {
                    _specification = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password = "пароль пользователя";
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
