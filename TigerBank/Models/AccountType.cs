using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TigerBank.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }

        [Required]
        [DisplayName("Account Type Name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
