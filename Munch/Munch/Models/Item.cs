namespace Munch.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using System.Threading;
    public class Item
    {
		public int Id { get; set; }

		[StringLength(30, MinimumLength = 3)]
		public string Name { get; set; }

		public int CategoryId { get; set; }

		[Required]
		public Category Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set;} 
		

	}
}