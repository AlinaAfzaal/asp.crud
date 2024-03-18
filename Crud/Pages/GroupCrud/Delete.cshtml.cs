using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crud.Data.Model;
using Crud.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Crud.Pages.GroupCrud
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public bool deleteCheck { get; set; }
        public List<Category> listCategories = new List<Category>();
        List<Product> listProducts = new List<Product>();
        [BindProperty]
        public Group Group { get; set; } = default!;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            setlists((int)id); 
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FirstOrDefaultAsync(m => m.Id == id);

            if (group == null)
            {
                return NotFound();
            }
            else 
            {
                Group = group;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            setlists((int)id);
            if (id == null || _context.Groups == null)
            {
                return NotFound();
            }
            var group = await _context.Groups.FindAsync(id);

            if (group != null && deleteCheck==true)
            {
                Group = group;
                _context.Products.RemoveRange(listProducts) ;
                _context.Categories.RemoveRange(listCategories);
                _context.Groups.Remove(Group);
                await _context.SaveChangesAsync();
            }
            else
            {
                Group = group;
                TempData["FocusCheckbox"] = "true";
                return Page();
            }
            return RedirectToPage("./Index");
        }

        public void setlists(int id)
        {
            var categories = _context.Categories.Where(c => c.Group == id).ToList();
            foreach (var category in categories)
            {
                listCategories.Add(category);
                var products = _context.Products.Where(p => p.Category == category.Id).ToList();
                listProducts.AddRange(products);
            }
        }
    }
}
