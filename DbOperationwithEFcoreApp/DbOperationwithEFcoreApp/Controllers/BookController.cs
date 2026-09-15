using DbOperationwithEFcoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace DbOperationwithEFcoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(AppDbContext _appDbcontext) : ControllerBase
    {
        [HttpGet("{Bookid}")]
        public async Task<IActionResult> GetCurrencies([FromRoute] int Bookid)
        {

            var books = await _appDbcontext.Books
           //.Include(x => x.Auther)
           .Include(b => b.Language)
          .FirstOrDefaultAsync(x => x.Id == Bookid);
            return Ok(books);
        }
        //Eagere Loding
        //[HttpGet("Authers")]
        //public async Task<IActionResult> GetCurrenciesWithRout()
        //{

        //    var Authers = await _appDbcontext.Authers
        //   .Include(x => x.Books)
        //   .ToListAsync();
        //    return Ok(Authers);
        //}

        //Explicit Loading
        [HttpGet("Explicit_Loading")]
        public async Task<IActionResult> GetCurrenciesWithRout()
        {

            var book = await _appDbcontext.Books.FirstOrDefaultAsync(x => x.Id == 1);

            // await _appDbcontext.Entry(book).Reference(x => x.Auther).LoadAsync();
            await _appDbcontext.Entry(book).Reference(x => x.Language).LoadAsync();

            return Ok(book);
        }

        [HttpGet("Explicit_Loading_with_many")]
        public async Task<IActionResult> GetBooksWithRout()
        {

            var book = await _appDbcontext.Authers.ToListAsync();
            //await _appDbcontext.Entry(book).Reference(x => x.Books).LoadAsync();
            foreach (var Authers in book)
            {
                await _appDbcontext.Entry(Authers).Collection(x => x.Books)
                    .Query()
                    .Where(x => x.AutherId == 4)
                    .LoadAsync();

            }


            return Ok(book);
        }

        //Navigation property

        [HttpGet("")]
        public async Task<IActionResult> GetBooksAsync()
        {


            var books = await _appDbcontext.Books.Select(x => new
            { Id = x.Id,
                Title = x.Title,
                Description = x.Description,

                AutherId = x.AutherId,
                //  AutherName = x.Auther.AutherName,
                LanguageTitle = x.Language.Title,
            }).ToListAsync();

            return Ok(books);
        }

        [HttpPost("")]
        public async Task<IActionResult> GetBooks([FromBody] Book model)
        {


            _appDbcontext.Books.Add(model);
            await _appDbcontext.SaveChangesAsync();

            return Ok(model);


        }

        [HttpPost("bulk")]
        public async Task<IActionResult> GetbulkBooks([FromBody] List<Book> model)
        {
            _appDbcontext.Books.AddRange(model);
            await _appDbcontext.SaveChangesAsync();

            return Ok(model);


        }

        [HttpPut("{bookid}")]
        public async Task<IActionResult> UpdateDateOfTable([FromRoute] int bookid, [FromBody] Book model)
        {
            var book = await _appDbcontext.Books.FirstOrDefaultAsync(x => x.Id == bookid);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Description = model.Description;

            await _appDbcontext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDate([FromBody] Book model)
        {
            _appDbcontext.Update(model);

            await _appDbcontext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("")]
        //This code createtd Just one hit to update database
        public async Task<IActionResult> UpdateDatewithSingalQuery([FromBody] Book model)
        {
            _appDbcontext.Entry(model).State = EntityState.Modified;


            await _appDbcontext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("Bulk")]
        public async Task<IActionResult> UpdateDateinBulk()
        {
            await _appDbcontext.Books.Where(m => m.Id == 6).ExecuteUpdateAsync(x => x
            .SetProperty(p => p.Description, "this iss updated data ")
            .SetProperty(p => p.Title, "Tushar pakhidde"));
            return Ok();
        }

        [HttpDelete("{bookid}")]
        public async Task<IActionResult> Deletedata([FromRoute] int bookid)
        {
            //This code detete Row of data.

            var book = await _appDbcontext.Books.FindAsync(bookid);
            // This code is used to delete bulk recode in database
            var booook = new Book { Id = bookid };
            var boook = await _appDbcontext.Books.Where(x => x.Id == 11).ToListAsync();
            if (boook == null)
            {
                return NotFound();
            }
            // <- changed
            //_appDbcontext.Entry(book).State = EntityState.Deleted;



            _appDbcontext.Books.Remove(book);
            _appDbcontext.Books.RemoveRange(boook);
            _appDbcontext.Books.RemoveRange(booook);

            await _appDbcontext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("BulkDelete")]
        public async Task<IActionResult> BulkDelete()
        {

            var book = await _appDbcontext.Books.Where(x => x.Id == 7 && x.Id == 8).ExecuteDeleteAsync();

            await _appDbcontext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpGet("sql")]
        public async Task<IActionResult> SQLQuery()
          
        {
            var Book = _appDbcontext.Books.FromSql($"select * from Books" );
            return Ok(Book);

        }


    }
}
