using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigerBank.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public int AccountTypeId { get; set; }
        public int UserID { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Amount { get; set; }
        public int Date { get; set; }
       
        [ForeignKey("AccountID")]
        [ValidateNever]
        public Accounts Account { get; set; }
    }
}