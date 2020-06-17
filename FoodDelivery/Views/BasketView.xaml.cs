using System.Windows.Controls;
using FoodDelivery.Tools.Navigation;
using FoodDelivery.ViewModels;

namespace FoodDelivery.Views
{
    /// <summary>
    /// Interaction logic for BasketView.xaml
    /// </summary>
    public partial class BasketView : UserControl, INavigatable
    {
        public BasketView()
        {
            InitializeComponent();
            DataContext = new BasketViewModel();
            Update();
        }
        public void Update()
        {
            DataContext = new BasketViewModel();
        }
    }
}
