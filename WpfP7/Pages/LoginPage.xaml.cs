using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfP7.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DoctorIdTextBox.Text, out int doctorId))
            {
                string fileName = $"D_{doctorId}.json";
                if (File.Exists(fileName))
                {
                    string jsonString = File.ReadAllText(fileName);
                    Doctor loadedDoctor = JsonSerializer.Deserialize<Doctor>(jsonString);

                    if (loadedDoctor != null && loadedDoctor.Password == PasswordBox.Password)
                    {
                        NavigationService.Navigate(new MainUserPage(loadedDoctor));
                        return;
                    }
                }
            }

            MessageBox.Show("Неверный ID или пароль");
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}