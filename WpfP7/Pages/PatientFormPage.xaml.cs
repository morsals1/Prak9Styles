using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfP7.Pages
{
    public partial class PatientFormPage : Page, INotifyPropertyChanged
    {
        private Patient _currentPatient;
        private Patient? _originalPatient;
        private ObservableCollection<Patient> _patients;
        private bool _isEditMode;

        public Patient CurrentPatient
        {
            get => _currentPatient;
            set
            {
                _currentPatient = value;
                OnPropertyChanged(nameof(CurrentPatient));
            }
        }

        public PatientFormPage(ObservableCollection<Patient> patients, Patient? patientToEdit = null)
        {
            _patients = patients;
            _isEditMode = patientToEdit != null;
            _originalPatient = patientToEdit;
            if (_isEditMode && patientToEdit != null)
            {
                _currentPatient = new Patient
                {
                    Id = patientToEdit.Id,
                    Name = patientToEdit.Name,
                    LastName = patientToEdit.LastName,
                    MiddleName = patientToEdit.MiddleName,
                    Birthday = patientToEdit.Birthday,
                    PhoneNumber = patientToEdit.PhoneNumber,
                    AppointmentStories = patientToEdit.AppointmentStories
                };
            }
            else
            {
                _currentPatient = new Patient();
            }

            InitializeComponent();
            DataContext = this;
        }

        private int GeneratePatientId()
        {
            Random rnd = new Random();
            int maxAttempts = 10000;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                int newId = rnd.Next(1000000, 9999999);
                string fileName = $"P_{newId}.json";

                if (!File.Exists(fileName))
                {
                    return newId;
                }
                attempts++;
            }
            throw new InvalidOperationException("Не удалось сгенерировать уникальный ID пациента");
        }

        private void SavePatientToFile(Patient patient)
        {
            string fileName = $"P_{patient.Id}.json";
            string jsonString = JsonSerializer.Serialize(patient, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isEditMode && _originalPatient != null)
            {
                _originalPatient.Name = _currentPatient.Name;
                _originalPatient.LastName = _currentPatient.LastName;
                _originalPatient.MiddleName = _currentPatient.MiddleName;
                _originalPatient.Birthday = _currentPatient.Birthday;
                _originalPatient.PhoneNumber = _currentPatient.PhoneNumber;

                SavePatientToFile(_originalPatient);

                var index = _patients.IndexOf(_originalPatient);
                if (index >= 0)
                {
                    _patients[index] = _originalPatient;
                }

                MessageBox.Show("Данные пациента обновлены");
            }
            else
            {
                _currentPatient.Id = GeneratePatientId();
                SavePatientToFile(_currentPatient);
                _patients.Add(_currentPatient);

                MessageBox.Show($"Пациент создан с ID: {_currentPatient.Id}");
            }

            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}