using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Tools.Navigation
{
    internal enum ViewType
    {
        SignIn,
        SignUp,
        Profile,
        Main,
        Catering,
        Dish,
        Basket,
        OrderDetails,
        AdminPanel
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
