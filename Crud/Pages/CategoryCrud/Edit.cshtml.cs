using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud.Data.Model;
using Crud.Data;

namespace Crud.Pages.CategoryCrud
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<SelectListItem> GroupList = new List<SelectListItem>();


        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            GroupList = Functions.getGroups(_context);
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category =  await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Category).State = EntityState.Modified; //only change values are update ; context.Entry(entity).State = EntityState.Modified; all values will be update
            await _context.SaveChangesAsync();
            TempData["aletMsg"] = "Category has been updated successfully";
            return RedirectToPage("./Index");
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
