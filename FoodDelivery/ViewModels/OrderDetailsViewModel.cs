using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;

namespace FoodDelivery.ViewModels
{
    class OrderDetailsViewModel : PropertyChangedVM
    {
        private RelayCommand<object> _toProfileCommand;

        private ObservableCollection<DishAndOrder> _dishAndOrders;

        public ObservableCollection<DishAndOrder> DishAndOrders
        {
            get => _dishAndOrders;
            set
            {
                _dishAndOrders = value;
                OnPropertyChanged(nameof(DishAndOrders));
            }
        }

        public OrderDetailsViewModel()
        {
            DishAndOrders = StationManager.DataStorage.GetAllDishAndOrders(StationManager.CurrentOrder.OrderID);
        }

        public RelayCommand<object> ToProfileCommand =>
            _toProfileCommand ??= new RelayCommand<object>(o => ToProfileImplementation());


        private void ToProfileImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Profile);
        }
    }
}
