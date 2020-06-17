using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Models
{
    class Catering
    {
        public int CateringID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Phone { get; set; }
        public string Work_From { get; set; }
        public string Work_To { get; set; }
        public byte[] ImageInBytes { get; set; }
    }
}
