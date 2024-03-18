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
        public String message = "";
        List<Product> listProducts = new List<Product>();


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

            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }



            if (listProducts != null)
            {
                message = "This Category contains Some Products! Do you want to Delete All Related Records in this Category";
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

            if (category != null)
            {
                Category = category;
                setlists((int)id); 
                _context.Products.RemoveRange(listProducts);
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }


        public void setlists(int id)
        {

            var products = _context.Products;
            foreach (var product in products)
            {
                if (product.Category == id)
                {
                    listProducts.Add(product);                
                }
            }
        }
    }
}
