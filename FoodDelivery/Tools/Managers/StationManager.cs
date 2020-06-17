using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using FoodDelivery.Database;
using FoodDelivery.Models;

namespace FoodDelivery.Tools.Managers
{
    class StationManager
    {
        public static IDataStorage DataStorage { get; } = new Database.Database();
        public static PasswordBox Password { get; set; }
        public static String CurrentLogin { get; set; }
        public static User CurrentUser { get; set; }
        public static Order CurrentOrder { get; set; }
        public static Dish CurrentDish { get; set; }
        public static Catering CurrentCatering { get; set; }
        public static Category CurrentCategory { get; set; }

        public static Basket CurrentBasket = new Basket();

        public static bool DoubleClick { get; set; }
    }


}
