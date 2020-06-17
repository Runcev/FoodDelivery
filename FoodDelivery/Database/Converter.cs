using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Documents;
using System.Windows.Navigation;
using FoodDelivery.Models;

namespace FoodDelivery.Database
{
    class Converter
    {

        public static ObservableCollection<User> GetUsersCollection(SqlCommand query)
        {
            SqlDataReader reader = query.ExecuteReader();

            ObservableCollection<User> users = new ObservableCollection<User>();

            var flatindex = reader.GetOrdinal("Flat");
            var roleindex = reader.GetOrdinal("Role");
            var patronymicIndex = reader.GetOrdinal("Patronymic");

            while (reader.Read())
            {
                User user = new User
                {
                    UserID = reader.GetInt32(0),
                    Role = reader.IsDBNull(roleindex) ? "" : reader.GetString(1),
                    Login = reader.GetString(2),
                    Password = reader.GetString(3),
                    Name = reader.GetString(4),
                    Surname = reader.GetString(5),
                    Patronymic = reader.IsDBNull(patronymicIndex) ? "" : reader.GetString(6),
                    Email = reader.GetString(7),
                    Phone = reader.GetString(8),
                    City = reader.GetString(9),
                    Street = reader.GetString(10),
                    Building = reader.GetString(11),
                    Flat = reader.IsDBNull(flatindex) ? "" : reader.GetString(12),
                    DateDB = reader.GetDateTime(13)
                };

                users.Add(user);
            }

            reader.Close();

            return users;
        }

        public static User GetUser(SqlCommand query)
        {
            return GetUsersCollection(query)[0];
        }

        public static ObservableCollection<Category> GetCategoryCollection(SqlCommand query)
        {
            SqlDataReader reader = query.ExecuteReader();

            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            while (reader.Read())
            {
                Category category = new Category
                {
                    CategoryID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ImageInBytes = (byte[])(reader[2])

                };

                categories.Add(category);
            }

            reader.Close();

            return categories;
        }

        public static Category GetCategory(SqlCommand query)
        {
            return GetCategoryCollection(query)[0];
        }

        public static ObservableCollection<Catering> GetCateringCollection(SqlCommand query)
        {
            SqlDataReader reader = query.ExecuteReader();

            ObservableCollection<Catering> caterings = new ObservableCollection<Catering>();

            var imageIndex = reader.GetOrdinal("Image");


            while (reader.Read())
            {
                Catering catering = new Catering
                {
                    CateringID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    Building = reader.GetString(4),
                    Phone = reader.GetString(5),
                    Work_From = reader.GetString(6),
                    Work_To = reader.GetString(7),
                    ImageInBytes = reader.IsDBNull(imageIndex) ? null : (byte[])(reader[8]),
                    CategoryID = reader.GetInt32(9)
                };
                caterings.Add(catering);
            }

            reader.Close();

            return caterings;
        }

        public static Catering GetCatering(SqlCommand query)
        {
            return GetCateringCollection(query)[0];
        }

        public static ObservableCollection<Dish> GetDishesCollection(SqlCommand query)
        {
            SqlDataReader reader = query.ExecuteReader();

            ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();

            var descriptionIndex = reader.GetOrdinal("Description");
            var imageIndex = reader.GetOrdinal("Image");

            while (reader.Read())
            {
                Dish dish = new Dish
                {
                    DishID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Weight = reader.GetFloat(2),
                    Price = reader.GetDouble(3),
                    IsActive = reader.GetBoolean(4),
                    Description = reader.IsDBNull(descriptionIndex) ? "" : reader.GetString(5),
                    ImageInBytes = reader.IsDBNull(imageIndex) ? null : (byte[])(reader[6]),
                    CateringID = reader.GetInt32(7)
                };
                dishes.Add(dish);
            }

            reader.Close();

            return dishes;
        }
        public static Dish GetDish(SqlCommand query)
        {
            return GetDishesCollection(query)[0];
        }

        public static ObservableCollection<Order> GetOrdersCollection(SqlCommand query)
        {
            SqlDataReader reader = query.ExecuteReader();

            ObservableCollection<Order> orders = new ObservableCollection<Order>();

            var commentIndex = reader.GetOrdinal("Comment");

            while (reader.Read())
            {
                Order order = new Order
                {
                    OrderID = reader.GetInt32(0),
                    Price = reader.GetDouble(1),
                    DateDB = reader.GetDateTime(2),
                    Comment = reader.IsDBNull(commentIndex) ? "" : reader.GetString(3),
                    ID = reader.GetInt32(4),
                };
                orders.Add(order);
            }
            
            reader.Close();

            return orders;
        }
        public static Order GetOrder(SqlCommand query)
        {
            return GetOrdersCollection(query)[0];
        }

    }
}
