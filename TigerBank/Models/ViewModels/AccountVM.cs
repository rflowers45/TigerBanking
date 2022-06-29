using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TigerBank.Models.ViewModels
{
    public class AccountVM
    {
        public Accounts Account { get; set; } 

        public Users User { get; set; }
        public AccountType AccountType { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> UsersList { get; set; } 

        [ValidateNever]
        public IEnumerable<SelectListItem> AccountTypeList { get; set; } 
    }

}