using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using FoodDelivery.ViewModels.Add_Edit;

namespace FoodDelivery.Views.Add_Edit
{
    /// <summary>
    /// Interaction logic for AddCatering.xaml
    /// </summary>
    public partial class AddCatering : Window
    {
        public AddCatering()
        {
            InitializeComponent();
            DataContext = new AddCateringViewModel();
        }
    }
}
