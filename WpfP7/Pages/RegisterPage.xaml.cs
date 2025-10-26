using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfP7.Pages
{
    public partial class RegisterPage : Page
    {
        private Doctor _currentDoctor = new Doctor();

        public RegisterPage()
        {
            InitializeComponent();
            DataContext = _currentDoctor;
        }

        private int GenerateDoctorId()
        {
            Random rnd = new Random();
            int maxAttempts = 1000;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                int newId = rnd.Next(10000, 99999);
                string fileName = $"D_{newId}.json";

                if (!File.Exists(fileName))
                {
                    return newId;
                }
                attempts++;
            }
            throw new InvalidOperationException("Не удалось сгенерировать уникальный ID доктора");
        }

        private void SaveDoctorToFile(Doctor doctor)
        {
            string fileName = $"D_{doctor.Id}.json";
            string jsonString = JsonSerializer.Serialize(doctor, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            _currentDoctor.Id = GenerateDoctorId();
            _currentDoctor.Password = PasswordBox.Password;
            SaveDoctorToFile(_currentDoctor);

            MessageBox.Show($"Доктор зарегистрирован с ID: {_currentDoctor.Id}");
            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}