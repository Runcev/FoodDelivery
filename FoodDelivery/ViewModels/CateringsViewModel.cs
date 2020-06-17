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
    class CateringsViewModel : PropertyChangedVM
    {
        private RelayCommand<object> _myprofileCommand;
        private RelayCommand<object> _categoriesCommand;
        private RelayCommand<object> _dishesCommand;

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
                _hideVisibility = value;
                _hideVisibility = _hideVisibility = StationManager.CurrentUser.Role == "Admin";
                OnPropertyChanged(nameof(HideVisibility));
            }
        }

        public ObservableCollection<Catering> Caterings { get; set; }

        public Catering SelectedCatering { get; set; }

        public CateringsViewModel()
        {
            if (StationManager.CurrentCategory == null)
            {
                Caterings = StationManager.DataStorage.SelectAllCaterings();
            }
            else
            {
                Caterings = StationManager.DataStorage.SelectCateringsByCategoryID(StationManager.CurrentCategory
                    .CategoryID);
            }
        }

        public RelayCommand<object> ProfileCommand =>
            _myprofileCommand ??= new RelayCommand<object>(o => ProfileImplementation());
        public RelayCommand<object> CategoriesCommand =>
            _categoriesCommand ??= new RelayCommand<object>(o => CategoriesImplementation());
        public RelayCommand<object> DishesCommand =>
            _dishesCommand ??= new RelayCommand<object>(o => DishesImplementation());

        public RelayCommand<object> AddCatering =>
            _addCommand ??= new RelayCommand<object>(o => AddCateringImplementation());

        public RelayCommand<object> UpdateCatering =>
            _updateCommand ??= new RelayCommand<object>(o => UpdateCateringImplementation(), o => CanExecuteCategory());

        public RelayCommand<object> DeleteCatering =>
            _deleteCommand ??= new RelayCommand<object>(o => DeleteCateringImplementation(), o => CanExecuteCategory());

        public RelayCommand<object> ToDishesCommand =>
            _doubleclickCommand ??= new RelayCommand<object>(o => ToDishesImplementation());


        private void ProfileImplementation()
        {
            StationManager.CurrentCategory = null;
            NavigationManager.Instance.Navigate(ViewType.Profile);

        }
        private void CategoriesImplementation()
        {
            StationManager.CurrentCategory = null;
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
        private void DishesImplementation()
        {
            StationManager.CurrentCategory = null;
            NavigationManager.Instance.Navigate(ViewType.Dish);
        }

        private void ToDishesImplementation()
        {
            StationManager.CurrentCatering = SelectedCatering;
            NavigationManager.Instance.Navigate(ViewType.Dish);
        }

        private void AddCateringImplementation()
        {
            StationManager.CurrentCatering = new Catering();
            OpenAddCateringList();
        }

        private void UpdateCateringImplementation()
        {
            StationManager.CurrentCatering = SelectedCatering;
            OpenAddCateringList();
        }

        private void DeleteCateringImplementation()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Catering", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                StationManager.DataStorage.DeleteCatering(SelectedCatering.CateringID);
            }
            UpdateCateringList();
        }

        private bool CanExecuteCategory() => SelectedCatering != null;

        private void OpenAddCateringList()
        {
            AddCatering win = new AddCatering();
            win.ShowDialog();
            UpdateCateringList();
        }


        private void UpdateCateringList()
        {
            Caterings = StationManager.DataStorage.SelectAllCaterings();
            OnPropertyChanged(nameof(Caterings));

        }
    }
}
