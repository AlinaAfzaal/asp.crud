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
        }

        public async Task<String> CategoryName(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                return category.Name; /*Task.FromResult(category.Name);*/
            }
            return null;
        }
    }
}


//public class Author
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    // other properties
//    public ICollection<Book> Books { get; set; }
//}

//public class Book
//{
//    public int Id { get; set; }
//    public string Title { get; set; }
//    // other properties
//    public int AuthorId { get; set; }
//    public Author Author { get; set; }
//}


//@page
//@model YourPageModel
//@using YourProjectNamespace.Models // Assuming your models are in a specific namespace

//@{
//    var dbContext = Model.Context; // Assuming you have DbContext injected into your page model
//var booksWithAuthors = dbContext.Books
//    .Join(
//        dbContext.Authors,
//        book => book.AuthorId,
//        author => author.Id,
//        (book, author) => new { Book = book, Author = author }
//    )
//    .Select(result => new { result.Book.Title, result.Author.Name })
//    .ToList();
//}

//< ul >
//    @foreach(var item in booksWithAuthors)
//    {
//        < li > @item.Title by @item.Name </ li >
//    }
//</ ul >
