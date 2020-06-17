using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows.Controls;

namespace FoodDelivery.Models
{
    class Dish
    {
        public int DishID { get; set; }
        public int CateringID { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public byte[] ImageInBytes { get; set; }
    }
}
