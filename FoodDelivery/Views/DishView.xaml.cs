using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using FoodDelivery.Tools.Navigation;
using FoodDelivery.ViewModels;

namespace FoodDelivery.Views
{
    /// <summary>
    /// Interaction logic for DishView.xaml
    /// </summary>
    public partial class DishView : UserControl, INavigatable
    {
        public DishView()
        {
            InitializeComponent();
            DataContext = new DishViewModel();
            Update();
        }
        public void Update()
        {
            DataContext = new DishViewModel();
        }
    }
}
