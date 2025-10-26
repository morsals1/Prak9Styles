using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WpfP7.Pages
{
    public partial class MainUserPage : Page
    {
        public Doctor CurrentDoctor { get; set; }
        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public Patient? SelectedPatient { get; set; }

        public MainUserPage(Doctor doctor)
        {
            CurrentDoctor = doctor;
            InitializeComponent();
            DataContext = this;
            LoadPatients();
        }

        private void LoadPatients()
        {
            Patients.Clear();
            var patientFiles = Directory.GetFiles(".", "P_*.json");
            foreach (var file in patientFiles)
            {
                string jsonString = File.ReadAllText(file);
                Patient patient = JsonSerializer.Deserialize<Patient>(jsonString);
                if (patient != null)
                {
                    Patients.Add(patient);
                }
            }
        }

        private void CreatePatientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientFormPage(Patients));
        }

        private void StartAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                NavigationService.Navigate(new AppointmentPage(CurrentDoctor, SelectedPatient, Patients));
            }
            else
            {
                MessageBox.Show("Выберите пациента для приема");
            }
        }

        private void EditInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient != null)
            {
                NavigationService.Navigate(new PatientFormPage(Patients, SelectedPatient));
            }
            else
            {
                MessageBox.Show("Выберите пациента для редактирования");
            }
        }

        private void PatientList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedPatient != null)
            {
                NavigationService.Navigate(new PatientFormPage(Patients, SelectedPatient));
            }
        }
    }
}