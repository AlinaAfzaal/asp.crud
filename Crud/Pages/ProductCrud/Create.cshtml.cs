﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;
using Crud.Data.Model;
using Crud.Data;

namespace Crud.Pages.ProductCrud
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<SelectListItem> CategoryList =new List<SelectListItem>(); 

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            CategoryList = Functions.getCategories(_context);
            return Page();

        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            TempData["aletMsg"] = "Product has been added successfully"; 
            return RedirectToPage("./Index");
        }
    }
}
