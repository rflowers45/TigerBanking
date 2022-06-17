using System.ComponentModel.DataAnnotations;

namespace TigerBank.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeID { get; set; }
        public int AccountTypeNum { get; set; }
        public string AccountTypeName { get; set; } = string.Empty;
    }
}

