using Microsoft.EntityFrameworkCore;
using SureLbraryAPI.Context;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Interfaces;
using SureLbraryAPI.Models;
using SureLbraryAPI.Utilities;
using System.Reflection;

namespace SureLbraryAPI.Repository
{
    public class BookRepository : IBookService
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context) 
        {
            _context = context;
        }
        public async Task<ResponseDetails<GetBookDTO>> AddBookAsync(CreateBookDTO bookDetail)
        {
            try
            {
                var bookExists = await _context.Books.AnyAsync(u => u.Title == bookDetail.Title && u.Author == bookDetail.Author);
                if (bookExists)
                {
                    return ResponseDetails<GetBookDTO>.Failed("Exception was caught", "$Book with the same {Title} and {Author} already Exist", 400);
                }
                
                var book = new Book
                {
                    Title = bookDetail.Title,
                    Author = bookDetail.Author,
                    CopiesAvailable = bookDetail.CopiesAvailable,
                    Genre = bookDetail.Genre,
                    PublishedDate = bookDetail.PublishedDate,
                   //Quantity = bookDetail.Quantity,
                    Price =bookDetail.Price
                };
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                var dto = new GetBookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,
                    Genre = book.Genre,
                    Price = book.Price,
                    Quantity = book.Quantity,
                    IsAvailable = true,
                    CopiesAvailable = book.CopiesAvailable,
                };
                return ResponseDetails<GetBookDTO>.Success(dto,"Book Added Successfully", 200);
                

            }
            catch (Exception ex) 
            {
                return ResponseDetails<GetBookDTO>.Failed("Caught Exception", ex.Message,ex.HResult);
            }
        }

        public async Task<ResponseDetails<GetBookDTO>> DeleteBookAsync(int id)
        {
            try
            {
                var book =await _context.Books.FindAsync(id);
                if(book is null)
                {
                    return ResponseDetails<GetBookDTO>.Failed();
                }
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                var bookDTO = new GetBookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,


                };
                return ResponseDetails<GetBookDTO>.Success(bookDTO,"Book Deleted Successfully",200);
            }
            catch (Exception ex) 
            {
                return ResponseDetails<GetBookDTO>.Failed("Exception was caught",ex.Message,ex.HResult);
            }
        }

        public async Task<ResponseDetails<List<GetBookDTO>>> GetAllBooksAsync()
        {
            try
            {
                var books = await _context.Books.Select(u => new GetBookDTO
                {
                    Id=u.Id,
                    Title = u.Title,
                    Author=u.Author,
                    PublishedDate = u.PublishedDate,
                    Price = u.Price,
                    Genre = u.Genre,
                    Quantity = u.Quantity,
                    IsAvailable = u.IsAvailable,
                    CopiesAvailable = u.CopiesAvailable,
                }).ToListAsync();
                if(books.Count==0)
                {
                    return ResponseDetails<List<GetBookDTO>>.Success([],"No Book Found",200);
                }
                return ResponseDetails<List<GetBookDTO>>.Success(books,"Users retrieved Successfully",200);
            }
            catch (Exception ex) 
            {
                return ResponseDetails<List<GetBookDTO>>.Failed("Exception was caught",ex.Message,ex.HResult);
            }
        }

        public async Task<ResponseDetails<IEnumerable<GetBookDTO>>> GetAvailableBooksAsync()
        {
            try
            {
                var books=await  _context.Books
                    .Where(b => b.CopiesAvailable > 0)
                    .Select(b=> new GetBookDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        PublishedDate = b.PublishedDate,
                        Price = b.Price,
                        Genre = b.Genre,
                        Quantity = b.Quantity,
                        IsAvailable = b.IsAvailable,
                        CopiesAvailable = b.CopiesAvailable,
                    })
                    .ToListAsync();
                if(books!=null)
                {
                    return ResponseDetails <IEnumerable<GetBookDTO>>.Success(Enumerable.Empty<GetBookDTO>(),"No Book Found,204");
                }
                return ResponseDetails<IEnumerable<GetBookDTO>>.Success(books, "Books Retrieved Succesfully", 200);
                }
            catch (Exception ex) 
            {
                return ResponseDetails<IEnumerable<GetBookDTO>>.Failed("Exception was caught", ex.Message,ex.HResult);       
            }
        }

        public async Task<ResponseDetails<GetBookDTO>> GetBookByIdAsync(int id)
        {
            try
            {
              var book =await _context.Books.FindAsync(id);
                if (book == null)
                {
                    ResponseDetails<GetBookDTO>.Failed("Exception was caught", $"Book with ID{id} not found", 400);
                }
                var bookDTO = new GetBookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author=book.Author,
                    Price=book.Price,
                    PublishedDate = book.PublishedDate,
                    Genre=book.Genre,
                    Quantity=book.Quantity,
                    CopiesAvailable=book.CopiesAvailable,
                    IsAvailable=book.IsAvailable,
                };
                return ResponseDetails<GetBookDTO>.Success(bookDTO);
            }
            catch (Exception ex) 
            {
                return ResponseDetails<GetBookDTO>.Failed("Exception was Caught", ex.Message, ex.HResult);
            }
        }

        public async Task<ResponseDetails<IEnumerable<GetBookDTO>>> GetBookBySpecificFilterAsync(string filter)
        {
            try
            {
                var books = await _context.Books
                     .Where(b => b.Title.Contains(filter) || b.Author.Contains(filter) || b.Genre.Contains(filter))
                     .Select(b => new GetBookDTO
                     {
                         Id=b.Id,
                         Title=b.Title,
                         Author=b.Author,
                         Price=b.Price,
                         PublishedDate=b.PublishedDate,
                         Genre=b.Genre,
                         Quantity=b.Quantity,
                         CopiesAvailable=b.CopiesAvailable,
                         IsAvailable=b.IsAvailable,
                     }).ToListAsync();
                if (!books.Any())
                {
                    return ResponseDetails<IEnumerable<GetBookDTO>>.Success(Enumerable.Empty<GetBookDTO>(),"No Book Found",204);
                }
                {
                    return ResponseDetails<IEnumerable<GetBookDTO>>.Success(books,"Books Retrieved Successfully",200);
                }

            }
            catch (Exception ex)
            {
                return ResponseDetails<IEnumerable<GetBookDTO>>.Failed("Exception was Caught", ex.Message,ex.HResult);
            }
        }

        public async Task<ResponseDetails<IEnumerable<GetBookDTO>>> SearchBooksAsync(string searchTerm)
        {
            try
            {
                var books= await _context.Books 
                    .Where(b=>b.Title.ToLower().Contains(searchTerm)||b.Author.ToLower().Contains(searchTerm))
                    .Select(b=>new GetBookDTO
                    {
                        Title=b.Title,
                        Author=b.Author,
                        Price=b.Price,
                        PublishedDate=b.PublishedDate,
                        Genre=b.Genre,
                        Quantity=b.Quantity,
                        CopiesAvailable=b.CopiesAvailable,
                        IsAvailable=b.IsAvailable,
                    })
                    .ToListAsync();
                if (!books.Any())
                {
                    return ResponseDetails<IEnumerable<GetBookDTO>>.Success(Enumerable.Empty<GetBookDTO>(),"No Book Found",204);
                }
                 return ResponseDetails<IEnumerable<GetBookDTO>>.Success(books, "Book Retrieved Successfully", 200);

            }
            catch (Exception ex)
            {
                return ResponseDetails<IEnumerable<GetBookDTO>>.Failed("Exception was Caught", ex.Message,ex.HResult);
            }
        }
    }
}
