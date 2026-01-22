using SureLbraryAPI.DTOs;
using SureLbraryAPI.Models;
using SureLbraryAPI.Utilities;

namespace SureLbraryAPI.Interfaces
{
    public interface IBookRepository
    {
        Task<ResponseDetails<GetBookDTO>> AddBookAsync(CreateBookDTO bookDetail);
        Task<ResponseDetails<GetBookDTO>> GetBookByAuthorAsync(string author);
        Task<ResponseDetails<List<GetBookDTO>>> GetAllBooksAsync();
        Task<ResponseDetails<GetBookDTO>> GetBookByIdAsync(int id);
        Task<ResponseDetails<GetBookDTO>> UpdateBookAsync (int id,CreateBookDTO bookDetails);
        Task<ResponseDetails<GetBookDTO>> DeleteBookAsync(int id);
        Task<ResponseDetails<IEnumerable<GetBookDTO>>> SearchBooksAsync(string searchTerm);
        Task<ResponseDetails<IEnumerable<GetBookDTO>>> GetAvailableBooksAsync();
        Task<ResponseDetails<IEnumerable<GetBookDTO>>> GetBookBySpecificFilterAsync(string filter);
    }
}
