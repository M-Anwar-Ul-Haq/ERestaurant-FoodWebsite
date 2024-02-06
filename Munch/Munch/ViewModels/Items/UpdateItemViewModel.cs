using Munch.Models;
using Munch.ViewModels.Categories;
using System.Collections.Generic;
using Munch.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Munch.ViewModels.Items
{
    public class UpdateItemViewModel
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        // Other properties as needed

        public List<CreateItemViewModel> Categories { get; set; }
    }

}
