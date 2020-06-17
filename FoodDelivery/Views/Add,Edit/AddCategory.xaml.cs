using System;
using System.Collections.Generic;
using System.Text;
using FoodDelivery.ViewModels.Add_Edit;

namespace FoodDelivery.Views.Add_Edit
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory
    {
        public AddCategory()
        {
            InitializeComponent();
            DataContext = new AddCategoryViewModel();
        }


    }
}
