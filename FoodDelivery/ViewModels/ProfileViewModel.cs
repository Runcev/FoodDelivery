using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;

namespace FoodDelivery.ViewModels
{
    class ProfileViewModel : PropertyChangedVM
    {
        public User _user;

        public ProfileViewModel(string login)
        { 
            _user = StationManager.DataStorage.GetUser(login);
            Orders = StationManager.DataStorage.GetUserOrders(MyUser.UserID);
        }

        private string _login; 
        private string _name;
        private string _surname;    
        private string _patronymic;
        private string _email;
        private string _phone;
        private string _city;
        private string _street;
        private string _building;
        private string _flat;
        private string _createdat;


        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _logoutCommand;
        private RelayCommand<object> _mainCommand;
        private RelayCommand<object> _doubleclickCommand;
        private RelayCommand<object> _toAdminCommand;



        private ObservableCollection<Order> _orders;

        public Order SelectedOrder { get; set; }

        private bool _hideVisibility;

        public bool HideVisibility
        {
            get => _hideVisibility = StationManager.CurrentUser.Role == "Admin";
            set
            {
                _hideVisibility = value;
                _hideVisibility = _hideVisibility = StationManager.CurrentUser.Role == "Admin";
                OnPropertyChanged(nameof(HideVisibility));
            }
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public User MyUser
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => MyUser.Login;

            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => MyUser.Name;

            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => MyUser.Surname;

            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Patronymic
        {
            get => MyUser.Patronymic;

            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => MyUser.Email;

            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get => MyUser.Phone;

            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        public string City
        {
            get => MyUser.City;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }
        public string Street
        {
            get => MyUser.Street;
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }
        public string Building
        {
            get => MyUser.Building;
            set
            {
                _building = value;
                OnPropertyChanged();
            }
        }
        public string Flat
        {
            get => MyUser.Flat;
            set
            {
                _flat = value;
                OnPropertyChanged();
            }
        }
        public string CreatedAt
        {
            get => MyUser.Created_at;
            set
            {
                _createdat = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> EditCommand =>
            _editCommand ??= new RelayCommand<object>(o => EditImplementation(), o => CanExecuteCommand() );
        public RelayCommand<object> LogoutCommand =>
            _logoutCommand ??= new RelayCommand<object>(o => LogoutImplementation());
        public RelayCommand<object> MainCommand =>
            _mainCommand ??= new RelayCommand<object>(o => MainImplementation());
        public RelayCommand<object> ToOrderDetails =>
            _doubleclickCommand ??= new RelayCommand<object>(o => ToOrderDetailsImplementation());

        public RelayCommand<object> ToAdminPanelCommand =>
            _toAdminCommand ??= new RelayCommand<object>(o => ToAdminPanelImplementation());

        private void ToAdminPanelImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.AdminPanel);
        }


        private void ToOrderDetailsImplementation()
        {
            StationManager.CurrentOrder = SelectedOrder;
            NavigationManager.Instance.Navigate(ViewType.OrderDetails);
        }

        private bool EditImplementation()
        {
            try
            {
                StationManager.DataStorage.UpdateUser(MyUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Edit failed for user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                return false;
            }

            MessageBox.Show($"Profile edited for user {_login}.");

            return true;
        }
        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(Login) &&
                   !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Surname) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Phone) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(Building);
        }

        private void LogoutImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }
        private void MainImplementation()
        {
           NavigationManager.Instance.Navigate(ViewType.Main);
        }
    }
}
