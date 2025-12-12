using SureLbraryAPI.DTOs;
using System.Transactions;

namespace SureLbraryAPI.Interfaces
{
    public interface ITransactionService
    {
        Task<List<GetTransactionDTO>> GetAllTransactionsAsync();
        Task<GetTransactionDTO> GetTransactionByIdAsync (int Id);
        Task<GetTransactionDTO> CreateTransactionAsync (int bookId,int userId,CreateTransactionDTO transactionDetails);
        Task<GetTransactionDTO> UpdateTransactionAsync (CreateTransactionDTO transaction,int id);
        Task<bool> DeleteTransactionAsync (int id);
        Task<IEnumerable<GetTransactionDTO>> GetTransactionByUserIdAsync (int id);
        Task<IEnumerable<GetTransactionDTO>> GetTransactionByBookIdAsync(int  bookId);
        Task<IEnumerable<GetTransactionDTO>> GetOverdueTransactionAsync(int id);
        Task<IEnumerable<GetTransactionDTO>> GetTransactionByStatusAsync (int status);
    }
}
