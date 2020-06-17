using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using FoodDelivery.Models;
using Microsoft.VisualBasic;

namespace FoodDelivery.Database
{
    class Database : IDataStorage
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-57BMGOR;Initial Catalog=FoodDelivery;Integrated Security=True");

        private SqlCommand PrepareCommand(string query)
        {
            if (connection == null) { throw new Exception("Connection String is Null"); }
            connection.Open();
            return new SqlCommand(query, connection);
        }

        #region User

        public bool UserExists(String login)
        {
            try
            {
                

                SqlCommand query = PrepareCommand("SELECT COUNT(*) " +
                                                  "FROM Users " +
                                                  $"WHERE Login = '{login}'");

                int count = (int)query.ExecuteScalar();
                return count == 1;
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { connection?.Close(); }
            return false;
        }
        public User GetUser(string login)
        {
            User user = new User();
            try
            {
                SqlCommand query = PrepareCommand("SELECT * " +
                                                  "FROM Users " +
                                                  "WHERE Login = '" + login + "'");

                user = Converter.GetUser(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return user;
        }
        public String GetPassword(string login)
        {
            string password = "";
            try
            {
                SqlCommand query = PrepareCommand("SELECT Password " +
                                                  "FROM Users " +
                                                  $"WHERE Login = '{login}'");

                SqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    password = reader.GetString(0);
                }

                reader.Close();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { connection?.Close(); }

            return password;
        }

        public void InsertNewUser(string login, string password, string username, string surname, string patronymic,
            string email, string phone, string city, string street, string building, string flat)
        {
            try
            {
                string quer = @"INSERT INTO Users (Login, Password, UserName, Surname, Patronymic, Email, Phone, City, Street, Building, Flat) VALUES ( @login, @password, @username, @surname, @patronymic, @email, @phone, @city, @street, @building, @flat) ";
                
                SqlCommand query = PrepareCommand(quer);



                query.Parameters.AddWithValue("Login", login);
                query.Parameters.AddWithValue("Password", password);
                query.Parameters.AddWithValue("UserName", username);
                query.Parameters.AddWithValue("Surname", surname);

                if (string.IsNullOrWhiteSpace(patronymic))
                {
                    query.Parameters.AddWithValue("Patronymic", DBNull.Value);

                }
                else
                {
                    query.Parameters.AddWithValue("Patronymic", patronymic);
                }

                query.Parameters.AddWithValue("Email", email);
                query.Parameters.AddWithValue("Phone", phone);
                query.Parameters.AddWithValue("City", city);
                query.Parameters.AddWithValue("Street", street);
                query.Parameters.AddWithValue("Building", building);

                if (string.IsNullOrWhiteSpace(flat))
                {
                    query.Parameters.AddWithValue("Flat", DBNull.Value);

                }
                else
                {
                    query.Parameters.AddWithValue("Flat", flat);
                }

                query.ExecuteNonQuery();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { connection?.Close(); }
        }

        public void UpdateUser(User user)
        {
            try
            {
                SqlCommand query = PrepareCommand("UPDATE Users " +
                                                  $"SET Login = N'{user.Login}', UserName = N'{user.Name}', Surname = N'{user.Surname}', " +
                                                  $"Patronymic = N'{user.Patronymic}', Email = N'{user.Email}', Phone = N'{user.Phone}', " +
                                                  $"City = N'{user.City}', Street = N'{user.Street}', Building = N'{user.Building}', Flat = N'{user.Flat}'" +
                                                  $"WHERE ID = '{user.UserID}'");

                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public ObservableCollection<User> GetNonActiveUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Users WHERE NOT EXISTS(SELECT * FROM Orders WHERE ID = Users.ID)");

                users = Converter.GetUsersCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return users;
        }
        #endregion

        #region Category
        public Category GetCategory(int id)
        {
            Category category = new Category();
            try
            {
                SqlCommand query = PrepareCommand("SELECT * " +
                                                  "FROM Category " +
                                                  "WHERE CategoryID = '" + id + "'");

                category = Converter.GetCategory(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return category;
        }

        public bool CategoryExists(string name)
        {
            try
            {
                SqlCommand query = PrepareCommand("SELECT COUNT(*) " +
                                                  "FROM Category " +
                                                  $"WHERE CategoryName = '{name}'");

                int count = (int) query.ExecuteScalar();
                return count == 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return false;
        }

        public void InsertNewCategory(string name, byte[] image)
        {
            try
            {
                string quer = @"INSERT INTO Category (CategoryName, Image) VALUES (@name, @image)";

                SqlCommand query = PrepareCommand(quer);


                query.Parameters.AddWithValue("@name", name);
                query.Parameters.AddWithValue("@image", image);


                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public void UpdateCategory(Category category, byte[] image)
        {
            try
            {
                SqlCommand query = PrepareCommand("UPDATE Category " +
                                                  $"SET CategoryName = @name, Image = @image WHERE CategoryID = '{category.CategoryID}'");

                query.Parameters.AddWithValue("@name", category.Name);
                query.Parameters.AddWithValue("@image", image);


                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {

                SqlCommand query = PrepareCommand("DELETE FROM Category " +
                                                  $"WHERE CategoryID = '{id}'");

                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }


        #endregion

        #region Catering

        public Catering GetCatering(int id)
        {
            Catering catering = new Catering();
            try
            {
                SqlCommand query = PrepareCommand("SELECT * " +
                                                  "FROM Catering " +
                                                  "WHERE CateringID = '" + id + "'");

                catering = Converter.GetCatering(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return catering;
        }

        public bool CateringExists(string name)
        {
            try
            {
                SqlCommand query = PrepareCommand("SELECT COUNT(*) " +
                                                  "FROM Catering " +
                                                  $"WHERE CateringName = '{name}'");

                int count = (int)query.ExecuteScalar();
                return count == 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return false;
        }

        public void InsertNewCatering(string name, string city, string street, string building, string phone, string workFrom, string workTo, byte[] image, int categoryId)
        {
            try
            {
                string quer = @"INSERT INTO Catering (CateringName, City, Street, Building, Phone, WorkFrom, WorkTo, Image, CategoryID) VALUES (@name, @city, @street, @building, @phone, @workFrom , @workTo, @image,  @categoryId)";

                SqlCommand query = PrepareCommand(quer);


                query.Parameters.AddWithValue("@name", name);
                query.Parameters.AddWithValue("@city", city);
                query.Parameters.AddWithValue("@street", street);
                query.Parameters.AddWithValue("@building", building);
                query.Parameters.AddWithValue("@phone", phone);
                query.Parameters.AddWithValue("@workFrom", workFrom);
                query.Parameters.AddWithValue("@workTo", workTo);

                if (image == null)
                {
                    SqlParameter img = new SqlParameter("@image", SqlDbType.Image);
                    img.Value = DBNull.Value;
                    query.Parameters.Add(img);
                }
                else
                {
                    query.Parameters.AddWithValue("@image", image);
                }

                query.Parameters.AddWithValue("@categoryId", categoryId);

                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public void UpdateCatering(Catering catering, byte[] image, int categoryId)
        {
            try
            {
                SqlCommand query = PrepareCommand("UPDATE Catering " +
                                                  $"SET CateringName = @name , City = @city, Street = @street, Building = @building , Phone = @phone , WorkFrom = @workFrom , WorkTo = @workTo, Image = @image, CategoryID = @categoryId WHERE CateringID = '{catering.CateringID}'");
                query.Parameters.AddWithValue("@name", catering.Name);
                query.Parameters.AddWithValue("@city", catering.City);
                query.Parameters.AddWithValue("@street", catering.Street);
                query.Parameters.AddWithValue("@building", catering.Building);
                query.Parameters.AddWithValue("@phone", catering.Phone);
                query.Parameters.AddWithValue("@workFrom", catering.Work_From);
                query.Parameters.AddWithValue("@workTo", catering.Work_To);
                query.Parameters.AddWithValue("@image", image);
                query.Parameters.AddWithValue("@categoryId", categoryId);

                query.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public ObservableCollection<Catering> SelectCateringsByCategoryID(int categoryID)
        {
            ObservableCollection<Catering> caterings = new ObservableCollection<Catering>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Catering WHERE CategoryID = '" + categoryID + "'");

                caterings = Converter.GetCateringCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return caterings;
        }
        public  ObservableCollection<CateringReport> GetCateringReport(int cateringID)
        {
            ObservableCollection<CateringReport> cateringsReports = new ObservableCollection<CateringReport>();

            try
            {
                SqlCommand query = PrepareCommand(
                    "SELECT C.CateringName, CAST(O.DateDB AS DATE), COUNT(*) as total_count, SUM(O.Price) AS TotalPRice FROM DishOrders AS DO " +
                    "join ORDERS AS O ON O.OrderID = DO.OrderID " +
                    "INNER JOIN Dish AS D ON D.DishID = DO.DishID " +
                    "INNER JOIN Catering AS C ON C.CateringID = D.CateringID " +
                    $"WHERE C.CateringID = {cateringID} GROUP BY C.CateringName, CAST(O.DateDB AS DATE)");

                SqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    CateringReport cateringReport = new CateringReport
                    {
                        CateringName = reader.GetString(0),
                        DateDB = reader.GetDateTime(1),
                        Total_Orders = reader.GetInt32(2),
                        Total_Price = reader.GetDouble(3)
                    };
                    cateringsReports.Add(cateringReport);
                }

                reader.Close();

                return cateringsReports;


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return cateringsReports;
        }


        public void DeleteCatering(int id)
        {
            try
            {
                SqlCommand query = PrepareCommand("DELETE FROM Catering " +
                                                  $"WHERE CateringID = '{id}'");

                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        #endregion

        #region Dishes

        public Dish GetDish(int id)
        {
            Dish dish = new Dish();

            try
            {
                SqlCommand query = PrepareCommand("SELECT * " +
                                                  "FROM Dishes " +
                                                  "WHERE ID = '" + id + "'");

                dish = Converter.GetDish(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return dish;
        }

        public bool DishExists(string name)
        {
            try
            {
                SqlCommand query = PrepareCommand("SELECT COUNT(*) " +
                                                  "FROM Dish " +
                                                  $"WHERE DishName = '{name}'");

                int count = (int)query.ExecuteScalar();

                return count == 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return false;
        }

        public void InsertNewDish(string name, double weight, double price, bool isActive, string description, byte[] image, int cateringId)
        {
            try
            {
                string quer = @"INSERT INTO Dish (DishName, Weight, Price, IsActive, Description, Image, CateringID) VALUES (@name, @weight, @price, @isActive, @description, @image, @cateringId)";

                SqlCommand query = PrepareCommand(quer);

                query.Parameters.AddWithValue("@name", name);
                query.Parameters.AddWithValue("@weight", weight);
                query.Parameters.AddWithValue("@price", price);
                query.Parameters.AddWithValue("@isActive", isActive);
                if (string.IsNullOrWhiteSpace(description))
                {
                    query.Parameters.AddWithValue("Description", DBNull.Value);

                }
                else
                {
                    query.Parameters.AddWithValue("Description", description);
                }

                

                if (image == null)
                {
                    SqlParameter img = new SqlParameter("@image", SqlDbType.Image);
                    img.Value = DBNull.Value;
                    query.Parameters.Add(img);
                }
                else
                {
                    query.Parameters.AddWithValue("@image", image);
                }

                query.Parameters.AddWithValue("@cateringId", cateringId);

                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public void UpdateDish(Dish dish, byte[] image, int cateringId)
        {

            try
            {
                SqlCommand query = PrepareCommand("UPDATE Dish " +
                                                  $"SET DishName = @name, Weight = @weight, Price = @price, IsActive = @isActive, Description = @description, Image = @image, CateringID = @cateringId WHERE DishID = '{dish.DishID}'");

                query.Parameters.AddWithValue("@name", dish.Name);
                query.Parameters.AddWithValue("@weight", dish.Weight);
                query.Parameters.AddWithValue("@price", dish.Price);
                query.Parameters.AddWithValue("@isActive", dish.IsActive);
                query.Parameters.AddWithValue("@description", dish.Description);
                query.Parameters.AddWithValue("@image", image);
                query.Parameters.AddWithValue("@cateringId", cateringId);



                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }
        public ObservableCollection<Dish> SelectDishesByCateringID(int cateringID)
        {

            ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Dish WHERE CateringID = '" + cateringID + "' ");

                dishes = Converter.GetDishesCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return dishes;
        }
        public void DeleteDish(int dishId)
        {
            try
            {
                SqlCommand query = PrepareCommand("DELETE FROM Dish " +
                                                  $"WHERE DishID = '{dishId}'");

                query.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }
        }


        #endregion

        #region Orders

        public Order GetOrderById(int orderId)
        {
            Order order = new Order();
            try
            {
                SqlCommand query = PrepareCommand("SELECT * " +
                                                  "FROM Orders " +
                                                  "WHERE OrderID = '" + orderId + "'");

                order = Converter.GetOrder(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return order;
        }

        public Order InsertNewOrder(double price, string comment, int userId)
        {
            Int32 orderID = 0;
            Order order = new Order();
            try
            {
                string quer = @"INSERT INTO Orders (Price, Comment, ID) OUTPUT INSERTED.OrderID VALUES (@price, @comment, @userId)";

                SqlCommand query = PrepareCommand(quer);

                query.Parameters.AddWithValue("@price", price);

                if (string.IsNullOrWhiteSpace(comment))
                {
                    query.Parameters.AddWithValue("Comment", DBNull.Value);

                }
                else
                {
                    query.Parameters.AddWithValue("Comment", comment);
                }

                query.Parameters.AddWithValue("@userId", userId);

                //query.ExecuteNonQuery();

                orderID = (Int32)query.ExecuteScalar();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            order = GetOrderById(orderID);

            return order;
        }

        public ObservableCollection<Order> GetUserOrders(int userID)
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            try
            {

                SqlCommand query = PrepareCommand(
                    $"SELECT * FROM Orders WHERE ID = '{userID}'");

                orders = Converter.GetOrdersCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return orders;
        }


        #endregion

            #region DishAndOrder

            public DishAndOrder GetDishAndOrder(int orderId)
        {
            DishAndOrder dishAndOrder = new DishAndOrder();
            try
            {
                SqlCommand query = PrepareCommand("SELECT * " +
                                                  "FROM DishOrders " +
                                                  "WHERE OrderID = '" + orderId + "'");

                //dishAndOrder = Converter.ConvertTo(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return dishAndOrder;
        }

        public void InsertNewDishAndOrder(int orderId, Dictionary<Dish, int> dishAmount)
        {
            foreach (var key in dishAmount.Keys)
            {
                try
                {
                    int amount = 0;
                    string quer = @"INSERT INTO DishOrders (OrderID, DishID, Amount) VALUES (@orderId, @dishId, @amount)";

                    SqlCommand query = PrepareCommand(quer);

                    query.Parameters.AddWithValue("@orderId", orderId);
                    query.Parameters.AddWithValue("@dishId", key.DishID);
                    dishAmount.TryGetValue(key, out amount);
                    query.Parameters.AddWithValue("@amount", amount);

                    query.ExecuteNonQuery();

                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    connection?.Close();
                }
            }
        }

        #endregion

        #region SelectAll

        public ObservableCollection<User> SelectAllUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Users");

                users = Converter.GetUsersCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return users;
        }

        public ObservableCollection<Category> SelectAllCategories()
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Category");

                categories = Converter.GetCategoryCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return categories;
        }
        public ObservableCollection<Catering> SelectAllCaterings()
        {
            ObservableCollection<Catering> caterings = new ObservableCollection<Catering>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Catering");

                caterings = Converter.GetCateringCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return caterings;
        }
        public ObservableCollection<Dish> SelectAllDishes()
        {
            ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();

            try
            {

                SqlCommand query = PrepareCommand(
                    "SELECT * FROM Dish");

                dishes = Converter.GetDishesCollection(query);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return dishes;
        }

        public ObservableCollection<DishAndOrder> GetAllDishAndOrders(int orderID)
        {
            
            ObservableCollection<DishAndOrder> dishAndOrders = new ObservableCollection<DishAndOrder>();

            try
            {

                SqlCommand query = PrepareCommand( 
                    "SELECT Catering.CateringName, Dish.DishName, Dish.Price, DishOrders.Amount, Dish.CateringID FROM DishOrders " +
                    $"INNER JOIN Dish ON DishOrders.DishID = Dish.DishID INNER JOIN Catering ON Catering.CateringID = Dish.CateringID WHERE DishOrders.OrderID = '{orderID}'");
                
                var reader = query.ExecuteReader();
                while (reader.Read())
                {
                    DishAndOrder dishAndOrder = new DishAndOrder()
                    {
                        CateringName = reader.GetString(0),
                        DishName = reader.GetString(1),
                        DishPrice = reader.GetDouble(2),
                        DishAmount = reader.GetInt32(3),
                        CateringID = reader.GetInt32(4)
                    };
                    dishAndOrders.Add(dishAndOrder);
                }
                reader.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection?.Close();
            }

            return dishAndOrders;
        }


        #endregion
    }
}
