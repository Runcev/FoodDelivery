
using System.Windows.Controls;

using FoodDelivery.Tools.Managers;
using FoodDelivery.Tools.Navigation;
using FoodDelivery.ViewModels;

namespace FoodDelivery.Views
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl, INavigatable
    {
        public SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
            StationManager.Password = new PasswordBox();
        }
    }
}
