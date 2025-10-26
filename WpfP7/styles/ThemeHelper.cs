using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfP7.styles
{
    public static class ThemeHelper
    {
        private static readonly string[] _themePaths = {
            "styles/DefaultColors.xaml",
            "styles/DarkTheme.xaml"
        };

        private static readonly string _settingsFile = "theme.config";

        public static string CurrentTheme
        {
            get
            {
                try
                {
                    if (File.Exists(_settingsFile))
                    {
                        string savedTheme = File.ReadAllText(_settingsFile).Trim();
                        if (_themePaths.Contains(savedTheme))
                        {
                            return savedTheme;
                        }
                    }
                }
                catch { }
                return _themePaths[0];
            }
            set
            {
                try
                {
                    File.WriteAllText(_settingsFile, value);
                }
                catch { }
            }
        }

        public static void Apply(string themePath)
        {
            try
            {
                if (string.IsNullOrEmpty(themePath))
                {
                    themePath = _themePaths[0];
                }

                var newTheme = new ResourceDictionary
                {
                    Source = new Uri(themePath, UriKind.Relative)
                };

                var oldTheme = Application.Current.Resources.MergedDictionaries
                    .FirstOrDefault(d => _themePaths.Any(path =>
                        d.Source != null && d.Source.OriginalString == path));

                if (oldTheme != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                    Application.Current.Resources.MergedDictionaries[index] = newTheme;
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(newTheme);
                }

                CurrentTheme = themePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки темы: {ex.Message}");
                if (themePath != _themePaths[0])
                {
                    Apply(_themePaths[0]);
                }
            }
        }

        public static void ApplySaved()
        {
            Apply(CurrentTheme);
        }

        public static void Toggle()
        {
            var newTheme = CurrentTheme == _themePaths[0] ? _themePaths[1] : _themePaths[0];
            Apply(newTheme);
        }
    }
}