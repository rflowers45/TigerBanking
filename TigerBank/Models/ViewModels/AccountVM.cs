using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TigerBank.Models.ViewModels
{
    public class AccountVM : PageModel
    {
        public Accounts Account { get; set; } = default!;

        [ValidateNever]
        public IEnumerable<SelectListItem> UsersList { get; set; } = Enumerable.Empty<SelectListItem>();

        [ValidateNever]
        public IEnumerable<SelectListItem> AccountTypeList { get; set; } = Enumerable.Empty<SelectListItem>();
    }

}