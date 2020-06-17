using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;
using FoodDelivery.Views.Add_Edit;

namespace FoodDelivery.ViewModels
{

    class MainViewModel : PropertyChangedVM
    {
        private RelayCommand<object> _myprofileCommand;
        private RelayCommand<object> _cateringsCommand;
        private RelayCommand<object> _dishesCommand;

        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _doubleclickCommand;


        public ObservableCollection<Category> Categories { get; set; }

        public Category SelectedCategory { get; set; }

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

        public MainViewModel()
        {
            Categories = StationManager.DataStorage.SelectAllCategories();
        }

        public RelayCommand<object> ProfileCommand =>
            _myprofileCommand ??= new RelayCommand<object>(o => ProfileImplementation());
        public RelayCommand<object> CateringsCommand =>
            _cateringsCommand ??= new RelayCommand<object>(o => CateringsImplementation());
        public RelayCommand<object> DishesCommand =>
            _dishesCommand ??= new RelayCommand<object>(o => DishesImplementation());


        public RelayCommand<object> AddCategory =>
            _addCommand ??= new RelayCommand<object>(o => AddCategoryImplementation());

        public RelayCommand<object> UpdateCategory =>
            _updateCommand ??= new RelayCommand<object>(o => UpdateCategoryImplementation(), o => CanExecuteCategory());

        public RelayCommand<object> DeleteCategory =>
            _deleteCommand ??= new RelayCommand<object>(o => DeleteCategoryImplementation(), o => CanExecuteCategory());

        public RelayCommand<object> ToCateringCommand =>
            _doubleclickCommand ??= new RelayCommand<object>(o => ToCateringImplementation());


        private void ProfileImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Profile);
        }
        private void CateringsImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Catering);
        }
        private void DishesImplementation()
        {
            StationManager.CurrentCategory = null;
            NavigationManager.Instance.Navigate(ViewType.Dish);
        }
        private void ToCateringImplementation()
        {
            StationManager.CurrentCategory = SelectedCategory;
            NavigationManager.Instance.Navigate(ViewType.Catering);
        }

        private void AddCategoryImplementation()
        {
            
            StationManager.CurrentCategory = new Category();
            OpenAddCategoryList();
        }

        private void UpdateCategoryImplementation()
        {
            StationManager.CurrentCategory = SelectedCategory;
            OpenAddCategoryList();
        }

        private void DeleteCategoryImplementation()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete Category", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                StationManager.DataStorage.DeleteCategory(SelectedCategory.CategoryID);
            }
            UpdateCategoryList();
        }


        private bool CanExecuteCategory() => SelectedCategory != null;


        private void OpenAddCategoryList()
        {
           AddCategory win = new AddCategory();
           win.ShowDialog();
           UpdateCategoryList();
        }

        private void UpdateCategoryList()
        {
            Categories = StationManager.DataStorage.SelectAllCategories();
            OnPropertyChanged(nameof(Categories));
        }
    }
}
