using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;
using FoodDelivery.Views.Add_Edit;

namespace FoodDelivery.ViewModels
{
    class DishViewModel : PropertyChangedVM
    {
        private RelayCommand<object> _myprofileCommand;
        private RelayCommand<object> _basketCommand;
        private RelayCommand<object> _cateringCommand;

        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;

        private RelayCommand<object> _doubleclickCommand;

        private bool _hideVisibility;

        public bool HideVisibility
        {
            get => _hideVisibility = StationManager.CurrentUser.Role == "Admin";
            set
            {
                _hideVisibility = _hideVisibility = StationManager.CurrentUser.Role == "Admin";
                OnPropertyChanged(nameof(HideVisibility));
                OnPropertyChanged(StationManager.CurrentUser.Role);
            }
        }

        public ObservableCollection<Dish> Dishes { get; set; }

        public Dish SelectedDish { get; set; }

        private Basket _basket;
        public Basket Basket
        {
            get => _basket;
            set
            {
                _basket = value;
                OnPropertyChanged(nameof(Basket));
            }
        }

        public DishViewModel()
        {
            if (StationManager.CurrentCatering == null)
            {
                Dishes = StationManager.DataStorage.SelectAllDishes();
            }
            else
            {
                Dishes = StationManager.DataStorage.SelectDishesByCateringID(StationManager.CurrentCatering.CateringID);
            }
        }

        public RelayCommand<object> MyProfileCommand =>
            _myprofileCommand ??= new RelayCommand<object>(o => MyProfileImplementation());

        public RelayCommand<object> BasketCommand =>
            _basketCommand ??= new RelayCommand<object>(o => BasketImplementation());

        public RelayCommand<object> CateringCommand =>
            _cateringCommand ??= new RelayCommand<object>(o => CateringImplementation());

        public RelayCommand<object> AddCatering =>
            _addCommand ??= new RelayCommand<object>(o => AddDishImplementation());

        public RelayCommand<object> UpdateCatering =>
            _updateCommand ??= new RelayCommand<object>(o => UpdateDishImplementation(), o => CanExecuteCategory());

        public RelayCommand<object> DeleteCatering =>
            _deleteCommand ??= new RelayCommand<object>(o => DeleteDishImplementation(), o => CanExecuteCategory());
        

        public RelayCommand<object> DoubleClickCommand =>
            _doubleclickCommand ??= new RelayCommand<object>(o => DoubleClickImplementation());


        private void MyProfileImplementation()
        {
            StationManager.CurrentCategory = null;
            StationManager.CurrentCatering = null;
            NavigationManager.Instance.Navigate(ViewType.Profile);
        }
        private void BasketImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Basket);

        }
        private void CateringImplementation()
        {
            StationManager.CurrentCategory = null;
            StationManager.CurrentCatering = null;
            NavigationManager.Instance.Navigate(ViewType.Catering);
        }

        private void AddDishImplementation()
        {
            StationManager.CurrentDish = new Dish();
            OpenAddDishList();
        }

        private void UpdateDishImplementation()
        {
            StationManager.CurrentDish = SelectedDish;
            OpenAddDishList();
        }

        private void DeleteDishImplementation()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Dish", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                StationManager.DataStorage.DeleteDish(SelectedDish.DishID);
            }
            UpdateDishesList();
        }

        private void DoubleClickImplementation()
        {
            StationManager.CurrentBasket.Add(SelectedDish);
            Basket = StationManager.CurrentBasket;
            StationManager.CurrentBasket.GetPrice();
        }

        private bool CanExecuteCategory() => SelectedDish != null;

        private void OpenAddDishList()
        {
            AddDish win = new AddDish();
            win.ShowDialog();
            UpdateDishesList();
        }

        private void UpdateDishesList()
        {
            Dishes = StationManager.DataStorage.SelectAllDishes();
            OnPropertyChanged(nameof(Dishes));
        }
    }
}

