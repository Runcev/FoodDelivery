using System;

namespace FoodDelivery.Models
{
    public class User
    {
        private string _password;
        private string _date;

        public int UserID { get; set; }

        public string Role { get; set; }
        public string Login { get; set; }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                byte[] data = System.Text.Encoding.ASCII.GetBytes(_password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                string hash = System.Text.Encoding.ASCII.GetString(data);
                _password = hash;
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Flat { get; set; }
        public DateTime DateDB { get; set; }
        public string Created_at
        {
            get
            {
                _date = DateDB.Date.ToString("dd/MM/yyyy");
                return _date;
            }
            set => _date = value;
        }
    }
}
