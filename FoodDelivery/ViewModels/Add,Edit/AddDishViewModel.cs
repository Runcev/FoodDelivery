using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FoodDelivery.Models;
using FoodDelivery.Tools;
using FoodDelivery.Tools.Managers;
using FoodDelivery.ViewModels.Add_Edit;
using Microsoft.Win32;

namespace FoodDelivery.ViewModels.Add_Edit
{
    class AddDishViewModel : PropertyChangedVM
    {
        public ObservableCollection<Catering> Caterings { get; set; }
        public Dish Dish { get; set; }

        private string _imageLocation;
        private byte[] _imageBytes;
        private Catering _catering;

      

        public Catering Catering
        {
            get
            {
                StationManager.CurrentCatering = _catering;
                return _catering;
            }
            set
            {
                _catering = value;
                OnPropertyChanged(nameof(Catering));
            }
        }

        private string _cuttentcatering;

        public string CurrentCatering {
            get => StationManager.DataStorage.GetCatering(StationManager.CurrentDish.CateringID).Name;
            set
            {
                _cuttentcatering = value;
                OnPropertyChanged(nameof(CurrentCatering));
            }
        }

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
        private bool AddDish { get; }

        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private ICommand _loadCommand;


        public AddDishViewModel()
        {
            Dish = StationManager.CurrentDish;
            AddDish = Dish.Name == null;
            Caterings = StationManager.DataStorage.SelectAllCaterings();
        }

        public ICommand SaveCommand => _saveCommand ??= new RelayCommand<Window>(SaveImplementation);
        public ICommand CancelCommand => _cancelCommand ??= new RelayCommand<Window>(w => w?.Close());
        public ICommand LoadImageCommand => _loadCommand ??= new RelayCommand<Window>(LoadImageImplementation);

        private void SaveImplementation(Window win)
        {
            if (ImageLocation == null)
            {
                ImageBytes = StationManager.CurrentDish.ImageInBytes;
            }
            else
            {
                FileStream fs = new FileStream(ImageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                ImageBytes = br.ReadBytes((int)fs.Length);
            }
            if (AddDish)
            {
                StationManager.DataStorage.InsertNewDish(Dish.Name, Dish.Weight, Dish.Price, Dish.IsActive, Dish.Description, ImageBytes, StationManager.CurrentCatering.CateringID);
            }
            else
            {
                StationManager.DataStorage.UpdateDish(Dish, ImageBytes, StationManager.CurrentCatering.CateringID);
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
