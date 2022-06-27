
﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TigerBank.Models
{
    public class Accounts
    {
        [Key] 
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public int AccountTypeID { get; set; }
        public string AccountTypeName { get; set; } = string.Empty;
        public int Balance { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public Users User { get; set; }

        [ForeignKey("AccountTypeId")]
        public AccountType AccountType { get; set; }

    }
}

