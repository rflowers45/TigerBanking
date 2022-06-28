using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TigerBank.Models;

namespace TigerBank.Views.Home
{
    public class TransactionsModel : PageModel
    {
        private readonly TigerBank.Models.ApplicationDbContext _context;

        public TransactionsModel(TigerBank.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Transactions Transactions { get; set; } = default!;
        public Users Users { get; set; } = default!;

        public Accounts Accounts { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.userId == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                Users = user;
            }
            return Page();
        }
    }
}
