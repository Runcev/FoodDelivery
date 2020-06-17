using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Models
{
    class Order
    {
        private string _date;
        private double _price;
        public int OrderID { get; set; }

        // User ID
        public int ID{ get; set; }

        public int CateringID { get; set; }
        public DateTime DateDB { get; set; }
        public string Created_at
        {
            get
            {
                _date = DateDB.Date.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                return _date;
            }
            set => _date = value;
        }

        public string Comment { get; set; }

        public double Price { get; set; }
    }
}
