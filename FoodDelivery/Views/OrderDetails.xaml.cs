using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;
using FoodDelivery.ViewModels;

namespace FoodDelivery.Views
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : UserControl, INavigatable
    {
        public OrderDetails()
        {
            InitializeComponent();
            DataContext = new OrderDetailsViewModel();
            Update();
        }

        public void Update()
        {
            DataContext = new OrderDetailsViewModel();
        }
    }
}
