using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FoodDelivery.Tools
{
    class PropertyChangedVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
