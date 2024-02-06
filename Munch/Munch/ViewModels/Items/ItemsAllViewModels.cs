namespace Munch.ViewModels.Items
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class ItemsAllViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; } 

        public string Category { get; set; }
    }
}
