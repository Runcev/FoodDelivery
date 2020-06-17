using System;
using System.Collections.Generic;
using System.Text;
using FoodDelivery.Views;

namespace FoodDelivery.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner) { }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.SignIn:
                    ViewsDictionary.Add(viewType, new SignInView());
                    break;
                case ViewType.SignUp:
                    ViewsDictionary.Add(viewType, new SignUpView());
                    break;
                case ViewType.Profile:
                    ViewsDictionary.Add(viewType, new ProfileView());
                    break;
                case ViewType.Main:
                    ViewsDictionary.Add(viewType, new MainView());
                    break;
                case ViewType.Catering:
                    ViewsDictionary.Add(viewType, new CateringsView());
                    break;
                case ViewType.Dish:
                    ViewsDictionary.Add(viewType, new DishView());
                    break;
                case ViewType.Basket:
                    ViewsDictionary.Add(viewType, new BasketView());
                    break;
                case ViewType.OrderDetails:
                    ViewsDictionary.Add(viewType, new OrderDetails());
                    break;
                case ViewType.AdminPanel:
                    ViewsDictionary.Add(viewType, new AdminPanel());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }

        protected override void UpdateView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Profile:
                    ViewsDictionary[viewType].Update();
                    break;
                case ViewType.Basket:
                    ViewsDictionary[viewType].Update();
                    break;
                case ViewType.Main:
                    ViewsDictionary[viewType].Update();
                    break;
                case ViewType.Catering:
                    ViewsDictionary[viewType].Update();
                    break;
                case ViewType.Dish:
                    ViewsDictionary[viewType].Update();
                    break;
                case ViewType.OrderDetails:
                    ViewsDictionary[viewType].Update();
                    break;
            }
        }
    }
}
