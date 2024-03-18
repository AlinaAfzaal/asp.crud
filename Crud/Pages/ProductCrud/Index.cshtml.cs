using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Crud.Data.Model;
using Crud.Data;
using System.Drawing;

namespace Crud.Pages.ProductCrud
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;
        public async Task OnGetAsync()
        {

            if (_context.Products != null)
            {
                Product = await _context.Products.ToListAsync();
            }

            //var ProJoinCat = from p in _context.Products
            //                 from c in _context.Categories
            //                 where p.Category == c.Id
            //                 select new
            //                 {
            //                     ProductId = p.Id,
            //                     ProdutName = p.Name,
            //                     Category = c.Name
            //                 };
        }

        //public async Task<String> CategoryName(int id)
        //{
        //    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        //    if (category != null)
        //    {
        //        return category.Name; /*Task.FromResult(category.Name);*/
        //    }
        //    return null;
        //}
    }
}
