using System;
using System.Collections.Generic;
using System.Text;
using FoodDelivery.Tools;

namespace FoodDelivery.ViewModels
{
    class MainWindowViewModel : PropertyChangedVM
    {
        private bool _isControlEnabled = true;

        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        //internal MainViewModel() { }
    }
}
