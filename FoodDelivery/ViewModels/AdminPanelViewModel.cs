using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;

namespace FoodDelivery.ViewModels
{
    class AdminPanelViewModel : PropertyChangedVM
    {
        private RelayCommand<object> _checkCommand;

        private RelayCommand<object> _toProfileCommand;

        public ObservableCollection<User> Users { get; set; }

        public ObservableCollection<Catering> Caterings { get; set; }

        private ObservableCollection<CateringReport> _cateringReports;

        public ObservableCollection<CateringReport> CateringsToReport
        {
            get => _cateringReports;
            set
            {
                _cateringReports = value;
                OnPropertyChanged(nameof(CateringsToReport));
            }
        }


        private Catering _catering;

        public Catering SelectedCatering
        {
            get
            {
                StationManager.CurrentCatering = _catering;
                return _catering;
            }
            set => _catering = value;
        }

        public AdminPanelViewModel()
        {
            Users = StationManager.DataStorage.GetNonActiveUsers();
            Caterings = StationManager.DataStorage.SelectAllCaterings();
        }

        public RelayCommand<object> ToProfileCommand =>
            _toProfileCommand ??= new RelayCommand<object>(o => ToProfileImplementation());

        public RelayCommand<object> CheckCommand =>
            _checkCommand ??= new RelayCommand<object>(o => CheckImplementation());


        private void CheckImplementation()
        {
            CateringsToReport = StationManager.DataStorage.GetCateringReport(StationManager.CurrentCatering.CateringID);
        }

        private void ToProfileImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.Profile);
        }
    }
}
