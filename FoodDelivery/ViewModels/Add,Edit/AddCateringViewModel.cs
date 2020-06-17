using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.ViewModels.Add_Edit;
using Microsoft.Win32;


namespace FoodDelivery.ViewModels.Add_Edit
{
    class AddCateringViewModel : PropertyChangedVM
    {
        private Category _category;
        private string _imageLocation;
        private byte[] _imageBytes;


        public ObservableCollection<Category> Categories { get; set; }
        public Catering Catering { get; set; }
        private bool AddCatering { get; }


        public byte[] ImageBytes
        {
            get => _imageBytes;
            set
            {
                _imageBytes = value;
                OnPropertyChanged(nameof(ImageBytes));
            }
        }

        public string ImageLocation
        {
            get => _imageLocation;
            set
            {
                _imageLocation = value;
                OnPropertyChanged();
            }
        }

        public Category Category
        {
            get
            {
                StationManager.CurrentCategory = _category;
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }


        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private ICommand _loadCommand;


        public AddCateringViewModel()
        {
            Catering = StationManager.CurrentCatering;
            AddCatering = Catering.Name == null;
            Categories = StationManager.DataStorage.SelectAllCategories();

            foreach (var category in Categories)
            {
                if (Catering.CategoryID == category.CategoryID)
                {
                    Category = category;
                    break;
                }
            }
        }

        public ICommand SaveCommand => _saveCommand ??= new RelayCommand<Window>(SaveImplementation);
        public ICommand CancelCommand => _cancelCommand ??= new RelayCommand<Window>(w => w?.Close());
        public ICommand LoadImageCommand => _loadCommand ??= new RelayCommand<Window>(LoadImageImplementation);


        private void SaveImplementation(Window win)
        {
            if (ImageLocation == null)
            {
                ImageBytes = StationManager.CurrentCatering.ImageInBytes;
            }
            else
            {
                FileStream fs = new FileStream(ImageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                ImageBytes = br.ReadBytes((int)fs.Length);
            }

            string patern = @"[0-9]{9}";
            if (!Regex.IsMatch(Catering.Phone, patern))
            {
                MessageBox.Show($"Adding failed for catering {Catering.Name}. Reason:{Environment.NewLine} Phone {Catering.Phone} is not valid. Format: 10 numbers");
            }

            if (AddCatering)
            {
                StationManager.DataStorage.InsertNewCatering(Catering.Name, Catering.City, Catering.Street, Catering.Building, Catering.Phone,Catering.Work_From, Catering.Work_To, ImageBytes, StationManager.CurrentCategory.CategoryID);
            }
            else
            {
                StationManager.DataStorage.UpdateCatering(Catering, ImageBytes, StationManager.CurrentCategory.CategoryID);
            }

            win?.Close();
        }
        private void LoadImageImplementation(Window win)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "JPG Files (*.jpg|*.jpg|All files (*.*)|*.*)";
            openFileDialog.Title = "Select Category ImageLocation";

            if (openFileDialog.ShowDialog() == true)
            {
                ImageLocation = (openFileDialog.FileName);
            }
        }
    }
}

