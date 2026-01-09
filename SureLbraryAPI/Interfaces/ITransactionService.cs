using SureLbraryAPI.DTOs;
using SureLbraryAPI.Enums;
using SureLbraryAPI.Utilities;


namespace SureLbraryAPI.Interfaces
{
    public interface ITransactionService
    {
        Task<ResponseDetails<List<GetTransactionDTO>>> GetAllTransactionsAsync();
        Task<ResponseDetails<GetTransactionDTO>> UpdateTransactionAsync (CreateTransactionDTO transactionDetails,int id);
        Task<ResponseDetails<bool>> DeleteTransactionAsync (int id);
        Task<ResponseDetails<IEnumerable<GetTransactionDTO>>>GetTransactionByUserIdAsync (int id);
        Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetTransactionByBookIdAsync(int  bookId);
        Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetOverdueTransactionAsync(int userId);
        Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetAllTransactionByStatusAsync(TransactionStatus status);
        Task<ResponseDetails<GetTransactionDTO?>> GetTransactionByIdAsync (int id);
        Task<ResponseDetails<GetTransactionDTO?>> CreateTransactionAsync (int bookId,int userId,CreateTransactionDTO transactionDetails);
    }
}
