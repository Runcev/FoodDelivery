using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Models
{
    class DishAndOrder
    {
        public int Order_OrderID { get; set; }
        public int Dish_DishID { get; set; }
        public int OrderDish_Amount { get; set; }
        public string DishName { get; set; }
        public string CateringName { get; set; }
        public int DishAmount { get; set; }
        public int CateringID { get; set; }
        public DateTime OrderDate { get; set; }


        private string _date;
        public string OrderDateString
        {
            get
            {
                _date = OrderDate.Date.ToString("dd/MM/yyyy");
                return _date;
            }
            set => _date = value;
        }
        public double DishPrice { get; set; }

    }
}
