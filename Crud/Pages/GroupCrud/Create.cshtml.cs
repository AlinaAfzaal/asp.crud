using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Crud.Data.Model;
using Crud.Data;

namespace Crud.Pages.GroupCrud
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Groups.Add(Group);
            await _context.SaveChangesAsync();
            TempData["aletMsg"] = "Group has been added successfully";
            return RedirectToPage("./Index");
        }
    }
}
