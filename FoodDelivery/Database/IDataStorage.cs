using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FoodDelivery.Models;
using Microsoft.VisualBasic;

namespace FoodDelivery.Database
{
    interface IDataStorage
    {
        // user
        bool UserExists(String login);
        String GetPassword(string login);
        User GetUser(string login);
        void InsertNewUser(string login, string password, string name, string surname, string patronymic,
                            string email, string phone, string city, string street, string building, string flat);
        void UpdateUser(User user);
        ObservableCollection<User> SelectAllUsers();

        ObservableCollection<User> GetNonActiveUsers();


        //category
        Category GetCategory(int id);
        bool CategoryExists(string name);
        void InsertNewCategory(string name, byte[] image);
        void UpdateCategory(Category category, byte[] image);
        void DeleteCategory(int id);
        ObservableCollection<Category> SelectAllCategories();


        // Catering
        Catering GetCatering(int id);
        bool CateringExists(string name);
        void InsertNewCatering(string name, string city, string street, string building, string phone,
            string workfrom, string workto, byte[] image, int categoryid);

        void UpdateCatering(Catering catering, byte[] image, int categoryId);
        void DeleteCatering(int id);
        ObservableCollection<Catering> SelectAllCaterings();
        ObservableCollection<Catering> SelectCateringsByCategoryID(int categoryID);

        ObservableCollection<CateringReport> GetCateringReport(int cateringID);



        // Dish
        Dish GetDish(int id);
        bool DishExists(string dishName);
        void InsertNewDish(string name, double weight, double price, bool isActive, string description, byte[] image, int cateringId);
        void UpdateDish(Dish dish, byte[] image, int cateringId);
        void DeleteDish(int dishId);
        ObservableCollection<Dish> SelectAllDishes();
        ObservableCollection<Dish> SelectDishesByCateringID(int cateringID);



        // Order
        Order GetOrderById(int orderId);
        Order InsertNewOrder(double price, string comment, int userId);

        ObservableCollection<Order> GetUserOrders(int userID);



        // pivot DishAndOrder
        DishAndOrder GetDishAndOrder(int orderId);
        void InsertNewDishAndOrder(int orderId, Dictionary<Dish, int> dishAmount);

        ObservableCollection<DishAndOrder> GetAllDishAndOrders(int orderID);
    }
}
