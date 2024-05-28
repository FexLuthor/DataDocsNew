using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WpfApp3.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "a", "a" },
            { "Justus", "password1" },
            { "Felix", "password2" },
            { "Ahmad", "password3" }
        };

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            if (_users.ContainsKey(Username) && _users[Username] == Password)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();

                    // Schließen Sie das aktuelle Fenster
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(Views.LoginWindow))
                        {
                            window.Close();
                            break;
                        }
                    }
                });
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        }
    }
}
