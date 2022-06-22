using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigerBank.Models
{
    public class Accounts
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public string AccountType { get; set; } = string.Empty;

        [Required]
        public int Balance { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public Users User { get; set; }

    }
}
