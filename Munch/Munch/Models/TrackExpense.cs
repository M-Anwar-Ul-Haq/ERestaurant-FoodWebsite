namespace Munch.Models
{
    public class TrackExpense
    {
        public int TrackExpenseId { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string? Category { get; set; } // Food, Drinks, Supplies, etc.

        public string? Type { get; set; } // Expense or Earning
        
        public decimal NetAmount { get; set; } // Net amount after considering earnings and expenses

    }
}
