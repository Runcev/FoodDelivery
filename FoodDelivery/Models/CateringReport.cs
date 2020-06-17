using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Models
{
    class CateringReport
    {
        public string CateringName { get; set; }
        public DateTime DateDB { get; set; }

        private string _date;
        public string Created_at
        {
            get
            {
                _date = DateDB.Date.ToString("dd/MM/yyyy");
                return _date;
            }
            set => _date = value;
        }

        public double Total_Price { get; set; }

        public int Total_Orders { get; set; }
    }
}
