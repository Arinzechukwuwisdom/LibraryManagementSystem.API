using Microsoft.EntityFrameworkCore;
using SureLbraryAPI.Context;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Enums;
using SureLbraryAPI.Interfaces;
using SureLbraryAPI.Migrations;
using SureLbraryAPI.Models;

namespace SureLbraryAPI.Repository
{
    public class TransactionRepository : ITransactionService
    {
        private readonly LibraryContext _libraryContext;
        public TransactionRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task<GetTransactionDTO> CreateTransactionAsync(int bookId, int userId, CreateTransactionDTO transactionDetails)
        {
            try
            {
                var user = await _libraryContext.Users.FindAsync(userId);
                if (user is null)
                {
                    throw new ArgumentException("User not found");
                }
                var book=await _libraryContext.Books.FindAsync(bookId);
                if (book is null) 
                {
                    throw new ArgumentException("Book Not Found");
                }
                
                var transaction = new Transaction
                {
                    BorrowDate= transactionDetails.BorrowDate,
                    UserId=userId,
                    BookId=bookId,
                    ReturnDate=transactionDetails.ReturnDate,
                    ExpectedReturnDate=transactionDetails.ExpectedReturnDate,
                    ActualReturnDate=transactionDetails.ActualReturnDate
                };
                
                await _libraryContext.Transactions.AddAsync(transaction);
                await _libraryContext.SaveChangesAsync();

                var transactionDt0 = new GetTransactionDTO
                {
                    Id=transaction.Id,
                    UserName = user.Name,
                    Status = transaction.Status,
                    ExpectedReturnDate = transaction.ExpectedReturnDate,
                };
                return transactionDt0 ;
            }
            catch (Exception ex) 
            {
                throw new Exception("Transaction Failed");
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _libraryContext.Transactions.FindAsync(id);
            if (transaction is null)
            {
                return false;
            }
            _libraryContext.Remove(id);
            await _libraryContext.SaveChangesAsync();
            return true;
        }


        public async Task< List<GetTransactionDTO>> GetAllTransactionsAsync()
        {
            try
            {
                var transactions = await _libraryContext.Transactions
                    .Select(u=>new GetTransactionDTO
                    {
                        Id=u.Id,
                        UserName=u.User.Name,
                        Status=u.Status,
                        ExpectedReturnDate=u.ExpectedReturnDate,
                        BorrowDate=u.BorrowDate,
                    }).ToListAsync();
                    if(!transactions.Any())
                    {
                        Console.WriteLine("No transaction Found");
                    }
                        return transactions; 
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<IEnumerable<GetTransactionDTO>> GetOverdueTransactionAsync(int userId)
        {
            try
            {
                var now = DateTime.UtcNow;

                var overdueTransactions = await _libraryContext.Transactions
                    .Where(t => t.UserId == userId
                             && t.ActualReturnDate == null
                             && t.ExpectedReturnDate < now
                             && t.Status == TransactionStatus.Borrowed) // or t.Status != "Returned"
                    .OrderBy(t => t.ExpectedReturnDate) // earliest overdue first
                    .Select(t => new GetTransactionDTO
                    {
                        Id = t.Id,
                        UserName = t.User.Name,          // ← navigation property
                        BookTitle = t.Book.Title,        // ← navigation property
                        BorrowDate = t.BorrowDate,
                        ExpectedReturnDate = t.ExpectedReturnDate.Value,
                        DaysOverdue = (int)(now - t.ExpectedReturnDate.Value).TotalDays,
                        Status = t.Status
                    })
                    .ToListAsync();

                return overdueTransactions;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
           
        }
                
      
        public async Task<IEnumerable<GetTransactionDTO>> GetTransactionByBookIdAsync(int bookId)
        {
            try
            {
                var transactions = await _libraryContext.Transactions
                .Where(t => t.BookId == bookId)
                .Select(t => new GetTransactionDTO
                {
                    Id = t.Id,
                    UserName = t.User.Name,
                    Status = t.Status,
                    BorrowDate = t.BorrowDate,
                    ExpectedReturnDate = t.ExpectedReturnDate,
                    BookTitle = t.Book.Title,
                    DaysOverdue = (t.BorrowDate - t.ReturnDate).Days,
                })
                .ToListAsync();

                return transactions;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<GetTransactionDTO>();
            }
        }

        public Task<GetTransactionDTO> GetTransactionByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetTransactionDTO>> GetTransactionByStatusAsync(int status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetTransactionDTO>> GetTransactionByUserIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetTransactionDTO> UpdateTransactionAsync(CreateTransactionDTO transaction, int id)
        {
            throw new NotImplementedException();
        }
    }
}
