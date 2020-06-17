using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FoodDelivery.Models
{
    class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public byte[] ImageInBytes { get; set; }
    }
}
