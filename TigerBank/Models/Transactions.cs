using System.ComponentModel.DataAnnotations;
namespace TigerBank.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Amount { get; set; }
        public int Date { get; set; }
    }
}