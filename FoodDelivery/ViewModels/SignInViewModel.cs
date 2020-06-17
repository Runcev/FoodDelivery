using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;

namespace FoodDelivery.ViewModels
{
    class SignInViewModel : PropertyChangedVM
    {
        private string _login;
        private string _password;

        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _signUpCommand;
        private RelayCommand<object> _closeCommand;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SignInCommand =>
            _signInCommand ??= new RelayCommand<object>(o => SignInImplementation(), o => CanExecuteCommand());

        public RelayCommand<Object> SignUpCommand =>
            _signUpCommand ??= new RelayCommand<object>(o => SignUpImplementation());

        public RelayCommand<Object> CloseCommand =>
            _closeCommand ??= new RelayCommand<object>(o => Environment.Exit(0));

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void SignUpImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.SignUp);
            Clear();
        }

        private async void SignInImplementation()
        {
            var passwordDb = StationManager.DataStorage.GetPassword(Login);

            // hashing
            var hashPassword = Password;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(hashPassword);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            hashPassword = System.Text.Encoding.ASCII.GetString(data);

            var result = await Task.Run(() =>
            {
                Thread.Sleep(1000);
                User currentUser;
                try
                {
                    currentUser = StationManager.DataStorage.GetUser(_login);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed for user {_login}. Reason:{Environment.NewLine}{ex.Message}");
                    return false;
                }
                if (currentUser == null)
                {
                    MessageBox.Show($"Sign In failed for user {_login}. Reason:{Environment.NewLine}User does not exist.");
                    return false;
                }
                if (passwordDb != hashPassword)
                {
                    MessageBox.Show($"Sign In failed for user {_login}. Reason:{Environment.NewLine}Wrong Password.");
                    return false;
                }

                StationManager.CurrentUser = currentUser;
                MessageBox.Show($"Sign In successful for user {_login}.");
                return true;
            });
            if (result) {
                NavigationManager.Instance.Navigate(ViewType.Profile);
            }
            Clear();
        }

        private void Clear()
        {
            Login = Password = "";
        }
    }
}
