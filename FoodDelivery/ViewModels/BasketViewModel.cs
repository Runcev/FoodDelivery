using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;

namespace FoodDelivery.ViewModels
{
    class BasketViewModel : PropertyChangedVM
    {
        private RelayCommand<object> _myProfileCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _backToDishesCommand;
        private RelayCommand<object> _orderCommand;


        private double _totalprice;
        public Dish SelectedDish { get; set; }

        public ObservableCollection<Dish> Dishes => StationManager.CurrentBasket.GetDishes();

        public double TotalPrice
        {
            get { return _totalprice = StationManager.CurrentBasket.GetPrice(); }
            set
            {
                _totalprice = value;
                OnPropertyChanged(nameof(StationManager.CurrentBasket.GetDishes));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public string Comment { get; set; }

        public RelayCommand<object> DeleteDish =>
            _deleteCommand ??= new RelayCommand<object>(o => DeleteImplementation(), o => CanExecuteCategory());

        public RelayCommand<object> BackToDishesCommand =>
            _backToDishesCommand ??= new RelayCommand<object>(o => ToDishesImplementation());

        public RelayCommand<object> ProfileCommand =>
            _myProfileCommand ??= new RelayCommand<object>(o => MyProfileImplementation());

        public RelayCommand<object> OrderDish =>
            _orderCommand ??= new RelayCommand<object>(o => OrderImplementation());

        private bool CanExecuteCategory() => SelectedDish != null;

        private void ToDishesImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Dish);
        }
        private void MyProfileImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Profile);
        }

        private void DeleteImplementation()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete dish", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                StationManager.CurrentBasket.Delete(SelectedDish);
                TotalPrice = StationManager.CurrentBasket.GetPrice();
            }
        }

        private void OrderImplementation()
        {
            Order order = StationManager.DataStorage.InsertNewOrder(Convert.ToDouble(TotalPrice), Comment, StationManager.CurrentUser.UserID);

            StationManager.DataStorage.InsertNewDishAndOrder(order.OrderID, StationManager.CurrentBasket.DishAmount());

            NavigationManager.Instance.Navigate(ViewType.Profile);

            StationManager.CurrentBasket = null;

            Comment = "";

        }
    }
}
