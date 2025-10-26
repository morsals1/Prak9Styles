using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Authentication;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfP7.Pages
{
    public partial class AppointmentPage : Page, INotifyPropertyChanged
    {
        public Doctor CurrentDoctor { get; set; }
        public Patient CurrentPatient { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public Appointment NewAppointment { get; set; } = new Appointment();

        public AppointmentPage(Doctor doctor, Patient patient, ObservableCollection<Patient> patients)
        {
            CurrentDoctor = doctor;
            CurrentPatient = patient;
            Patients = patients;       
            InitializeComponent();
            DataContext = this;
            NewAppointment.Date = DateTime.Now.ToString("dd.MM.yyyy");
            NewAppointment.DoctorId = doctor.Id;
        }

        private void SaveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NewAppointment.Date) || 
                string.IsNullOrEmpty(NewAppointment.Diagnosis))
            {
                MessageBox.Show("Заполните обязательные поля: Дата и Диагноз");
                return;
            }

            CurrentPatient.AppointmentStories.Add(new Appointment
            {
                Date = NewAppointment.Date,
                DoctorId = CurrentDoctor.Id,
                Diagnosis = NewAppointment.Diagnosis,  
                FullName = $"{CurrentDoctor.Name} {CurrentDoctor.MiddleName} {CurrentDoctor.LastName}",
                Recommendations = NewAppointment.Recommendations
            });


            SavePatientToFile(CurrentPatient);

            var index = Patients.IndexOf(CurrentPatient);
            if (index >= 0)
            {
                Patients[index] = CurrentPatient;
            }

            NewAppointment = new Appointment 
            { 
                Date = DateTime.Now.ToString("dd.MM.yyyy"),
                DoctorId = CurrentDoctor.Id
            };
            OnPropertyChanged(nameof(NewAppointment));

            MessageBox.Show("Прием сохранен");
        }

        private void SavePatientToFile(Patient patient)
        {
            string fileName = $"P_{patient.Id}.json";
            string jsonString = JsonSerializer.Serialize(patient, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }

        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientFormPage(Patients, CurrentPatient));
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