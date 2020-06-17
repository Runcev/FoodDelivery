using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using Microsoft.Win32;
using FoodDelivery.ViewModels.Add_Edit;

namespace FoodDelivery.ViewModels.Add_Edit
{
    class AddCategoryViewModel : PropertyChangedVM
    {
        public Category Category { get; set; }

        private string _imageLocation;
        private byte[] _imageBytes;

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

        private bool AddCategory { get; }

        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private ICommand _loadCommand;


        public AddCategoryViewModel()
        {
            Category =  StationManager.CurrentCategory;
            AddCategory = Category.Name == null;
        }

        public ICommand SaveCommand => _saveCommand ??= new RelayCommand<Window>(SaveImplementation);
        public ICommand CancelCommand => _cancelCommand ??= new RelayCommand<Window>(w => w?.Close());
        public ICommand LoadImageCommand => _loadCommand ??= new RelayCommand<Window>(LoadImageImplementation);

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(Category.Name) &&
                   !string.IsNullOrWhiteSpace(ImageLocation);
        }
        private void SaveImplementation(Window win)
        {
            if (ImageLocation == null)
            {
                ImageBytes = StationManager.CurrentCategory.ImageInBytes;
            }
            else
            {
                FileStream fs = new FileStream(ImageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                ImageBytes = br.ReadBytes((int)fs.Length);
            }

            if (AddCategory)
            {
                StationManager.DataStorage.InsertNewCategory(Category.Name, ImageBytes);
            }
            else
            {
                StationManager.DataStorage.UpdateCategory(Category, ImageBytes);
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
