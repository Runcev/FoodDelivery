using System.Windows;
using FoodDelivery.ViewModels;
using FoodDelivery.ViewModels.Add_Edit;

namespace FoodDelivery.Views.Add_Edit
{
    /// <summary>
    /// Interaction logic for AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        public AddDish()
        {
            InitializeComponent();
            DataContext = new AddDishViewModel();
        }
    }
}
