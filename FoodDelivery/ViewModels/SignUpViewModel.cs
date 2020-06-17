using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;

namespace FoodDelivery.ViewModels
{
    class SignUpViewModel : PropertyChangedVM
    {

        private string _login;
        private string _password;
        private string  _name;
        private string _surname;
        private string _patronymic;
        private string _email;
        private string _phone;
        private string _city;
        private string _street;
        private string _building;
        private string _flat;


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
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }
        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }
        public string Building
        {
            get => _building;
            set
            {
                _building = value;
                OnPropertyChanged();
            }
        }

        public string Flat
        {
            get => _flat;
            set
            {
                _flat = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SignInCommand =>
            _signInCommand ??= new RelayCommand<object>(o => BackToSignInImplementation());
        public RelayCommand<object> SignUpCommand =>
            _signUpCommand ??= new RelayCommand<object>(o => SignUpImplementation(), o => CanExecuteCommand());
        public RelayCommand<object> CloseCommand =>
            _closeCommand ??= new RelayCommand<object>(o => Environment.Exit(0));


        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(Login) &&
                   !string.IsNullOrWhiteSpace(Password) && 
                   !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Surname) &&
                   !string.IsNullOrWhiteSpace(Email) && 
                   !string.IsNullOrWhiteSpace(Phone) &&
                   !string.IsNullOrWhiteSpace(City) && 
                   !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(Building);
        }

        private void BackToSignInImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.SignIn);
            Clear();
        }

        private void Clear()
        {
            Login = Password = Name = Surname = Email = Phone = Patronymic = City = Street = Building = Flat = "";
        }

        private async void SignUpImplementation()
        {
            string hash_password = Password;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(hash_password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            hash_password = System.Text.Encoding.ASCII.GetString(data);


            var result = await Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    if (!new EmailAddressAttribute().IsValid(_email))
                    {
                        MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} Email {_email} is not valid.");
                        return false;
                    }

                    string patern = @"[0-9]{9}";
                    if (!Regex.IsMatch(_phone, patern))
                    {
                        MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} Phone {_phone} is not valid. Format: 10 numbers");
                        return false;
                    }
                    if (StationManager.DataStorage.UserExists(_login))
                    {
                        MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} User with such login already exists.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                try
                {
                    StationManager.DataStorage.InsertNewUser(Login, hash_password, Name, Surname, Patronymic, Email, Phone, City, Street, Building, Flat);

                    StationManager.CurrentUser = StationManager.DataStorage.GetUser(_login);


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                MessageBox.Show($"User {_login} was successfully created.");
                return true;
            });
            if (result)
            {
                NavigationManager.Instance.Navigate(ViewType.Profile);
            }
        }
    }
}
