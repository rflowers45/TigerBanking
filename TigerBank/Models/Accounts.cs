using System.ComponentModel.DataAnnotations;
namespace TigerBank.Models
{
    public class Accounts
    {
        [Key] 
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public int AccountTypeID { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public int Balance { get; set; }
    }
}