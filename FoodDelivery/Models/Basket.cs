using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace FoodDelivery.Models
{
    class Basket
    {
        protected ObservableCollection<Dish> Dishes { get; set; } = new ObservableCollection<Dish>();
        public int Count { get; set; }


        public void Add(Dish dish)
        {
            Dishes.Add(dish);
            Count++;
        }

        public ObservableCollection<Dish> GetDishes()
        {
            return Dishes;
        }

        public void Delete(Dish SelectedDish)
        {
            foreach (Dish dish in Dishes)
            {
                if (dish.DishID == SelectedDish.DishID)
                {
                    Dishes.Remove(dish);
                    return;
                }
            }
        }

        public void DeleteAll()
        {
            foreach (Dish dish in Dishes)
            {
                Dishes.Clear();
            }
        }

        public double GetPrice()
        {
            double price = 0;

            foreach (Dish dish in Dishes)
            {
                price += dish.Price;
            }

            return price;
        }



        public Dictionary<Dish, int> Dictionary = new Dictionary<Dish, int>();
        

        public Dictionary<Dish, int> DishAmount()
        {
            int i = 1;
            foreach (Dish dish in Dishes)
            {
                if (!Dictionary.ContainsKey(dish))
                {
                    Dictionary.Add(dish,i);
                }
                else
                {
                    Dictionary[dish]++;
                }
            }

            return Dictionary;
        }
    }
}
