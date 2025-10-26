using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfP7.Pages;

namespace WpfP7
{
    public class Patient : INotifyPropertyChanged
    {
        private int _id = 0;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "Имя пациента";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lastName = "Фамилия пациента";
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

        private string _middleName = "Отчество пациента";
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

        private string _birthday = "01.01.1990";
        public string Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _phoneNumber = "+79001112233";
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Appointment> _appointmentStories = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> AppointmentStories
        {
            get => _appointmentStories;
            set
            {
                if (_appointmentStories != value)
                {
                    _appointmentStories = value;
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