using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crud.Data.Model;
using Crud.Data;

namespace Crud.Pages.CategoryCrud
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public bool deleteCheck { get; set; }

        public List<Product> listProducts = new List<Product>(); 


        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            setlists((int)id); 

            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            setlists((int)id);
            if (category != null && deleteCheck==true)
            {
                Category = category;
                _context.Products.RemoveRange(listProducts);
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }
            else if(category!=null && listProducts.Count==0)
            {
                Category = category;
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }
            else if(category!=null && deleteCheck == false)
            {
                Category = category;
                TempData["FocusCheckbox"] = "true";
                return Page();
            }
            TempData["aletMsg"] = "Category has been deleted successfully";
            return RedirectToPage("./Index");
        }


        public void setlists(int id)
        {
           var products = _context.Products.Where(p => p.Category == id).ToList();
           listProducts.AddRange(products);

        }
    }
}
