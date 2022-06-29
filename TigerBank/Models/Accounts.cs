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
        public int Balance { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AccountTypeId { get; set; }

        [ForeignKey("userId")]
        [ValidateNever]
        public Users User { get; set; }

        [ForeignKey("AccountTypeId")]
        [ValidateNever]
        public AccountType AccountType { get; set; }

    }
}
