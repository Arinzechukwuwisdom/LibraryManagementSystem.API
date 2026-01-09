using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SureLbraryAPI.Context;
using SureLbraryAPI.DTOs;
using SureLbraryAPI.Enums;
using SureLbraryAPI.Interfaces;
using SureLbraryAPI.Migrations;
using SureLbraryAPI.Models;
using SureLbraryAPI.Utilities;
using System.Data;
using System.Linq;

namespace SureLbraryAPI.Repository
{
    public class TransactionRepository : ITransactionService
    {
        private readonly LibraryContext _libraryContext;
        public TransactionRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public async Task<ResponseDetails<GetTransactionDTO?>> CreateTransactionAsync(int bookId, int userId, CreateTransactionDTO transactionDetails)
        {
                var user = await _libraryContext.Users.FindAsync(userId);
                if (user==null)
                {
                    return ResponseDetails<GetTransactionDTO?>.Failed("Invalid User", "User Not Found", 400);
                }
                var book = await _libraryContext.Books.FindAsync(bookId);
                if (book is null)
                {
                    return ResponseDetails<GetTransactionDTO?>.Failed("Invalid Book", "Book Not Found", 400);
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
                    BorrowDate = transaction.BorrowDate,
                    BookTitle=book.Title,
                };
               return ResponseDetails<GetTransactionDTO?>.Success(transactionDt0);
        }

        public async Task<ResponseDetails<bool>> DeleteTransactionAsync(int id)
        {
            var transaction = await _libraryContext.Transactions.FindAsync(id);
            if (transaction is null)
            {
                return ResponseDetails<bool>.Failed("Not Found","Trnsaction does not Exist",404);
            }
            _libraryContext.Remove(id);
            await _libraryContext.SaveChangesAsync();
            return ResponseDetails<bool>.Success(true);
        }

        public async Task<ResponseDetails<List<GetTransactionDTO>>> GetAllTransactionsAsync()
        {
            try
            {
                var now = DateTime.UtcNow;
                var transactions = await _libraryContext.Transactions
                    .Select(u => new GetTransactionDTO
                    {
                        Id = u.Id,
                        UserName = u.User.Name,
                        BookTitle=u.Book.Title,
                        DaysOverdue = (int)(now - u.ExpectedReturnDate.Value).TotalDays,
                        Status = u.Status,
                        ExpectedReturnDate = u.ExpectedReturnDate,
                        BorrowDate = u.BorrowDate,
                    }).ToListAsync();
                if (transactions.Count==0)
                {
                    return ResponseDetails<List<GetTransactionDTO>>.Failed();
                    //This is supposed to be empty list not empty
                }
                return ResponseDetails<List<GetTransactionDTO>>.Success(tr);
                // return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetOverdueTransactionAsync(int userId)
        {
            try
            {
                var now = DateTime.UtcNow;

                var overdueTransactions = await _libraryContext.Transactions
                    .Where(t => t.UserId == userId
                             && t.ActualReturnDate == null
                             && t.ExpectedReturnDate < now
                             && t.Status == TransactionStatus.Borrowed)
                    .OrderBy(t => t.ExpectedReturnDate)
                    .Select(t => new GetTransactionDTO
                    {
                        Id = t.Id,
                        UserName = t.User.Name,
                        BookTitle = t.Book.Title,
                        BorrowDate = t.BorrowDate,
                        ExpectedReturnDate = t.ExpectedReturnDate.Value,
                        DaysOverdue = (int)(now - t.ExpectedReturnDate.Value).TotalDays,
                        Status = t.Status
                    })
                    .ToListAsync();

                return ResponseDetails<IEnumerable<GetTransactionDTO>>.Success(overdueTransactions,"Transaction was successful", 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetTransactionByBookIdAsync(int bookId)
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
                if (transactions.Count == 0)
                {
                    return ResponseDetails<IEnumerable<GetTransactionDTO>>.Failed();
                }

                    return ResponseDetails<IEnumerable<GetTransactionDTO>>.Success(transactions);
            }
            catch (Exception ex)
            {
                return ResponseDetails<IEnumerable<GetTransactionDTO>>.Failed("An Exception was Caught","Transaction Successfully Created",201);
            }
        }

        public async Task<ResponseDetails<GetTransactionDTO?>> GetTransactionByIdAsync(int id)
        {
            var transaction = await _libraryContext.Transactions
                .Include(t => t.User)
                .Include(t => t.Book)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return null;
            }

            var transactionDTO= new GetTransactionDTO
            {
                Id = transaction.Id,
                UserName = transaction.User.Name,
                BookTitle = transaction.Book.Title,
                Status = transaction.Status,
                BorrowDate = transaction.BorrowDate,
                ExpectedReturnDate = transaction.ExpectedReturnDate
            };
            return ResponseDetails<GetTransactionDTO?>.Success(transactionDTO);
        }

        public async Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetAllTransactionByStatusAsync(TransactionStatus status)
        {
            var transactions = await _libraryContext.Transactions
                            .Where(t => t.Status == status)
                            .Include(t => t.User)
                            .Include(t => t.Book)
                            .Select(t => new GetTransactionDTO
                            {
                                UserName = t.User.Name,
                                Status = t.Status,
                                BorrowDate = t.BorrowDate,
                                ExpectedReturnDate = t.ExpectedReturnDate

                            })
                            .ToListAsync();
            if(transactions.Count==0)
            {
                return ResponseDetails<IEnumerable<GetTransactionDTO>>.Failed();
            }
            return ResponseDetails<IEnumerable<GetTransactionDTO>>.Success(transactions);
        }

        public async Task<ResponseDetails<IEnumerable<GetTransactionDTO>>> GetTransactionByUserIdAsync(int id)
        {
            var transactions=await _libraryContext.Transactions
                .Where(u=>u.Id==id)
                .Include(t=>t.User)
                .Select(u=>new GetTransactionDTO
                {
                    UserName=u.User.Name,
                    Status=u.Status,
                    BorrowDate=u.BorrowDate,
                    ExpectedReturnDate=u.ExpectedReturnDate
                }).ToListAsync();
            if (transactions == null)
            {
                return ResponseDetails<IEnumerable<GetTransactionDTO>>.Failed();
                //return null;
            }
            return ResponseDetails<IEnumerable<GetTransactionDTO>>.Success(transactions,"Transaction successful",201);
        }

        public async Task<ResponseDetails<GetTransactionDTO>> UpdateTransactionAsync(CreateTransactionDTO transactionDetails, int id)
        {
            var transaction = await _libraryContext.Transactions
                .FindAsync(id);
            if (transaction == null) 
            {
                return ResponseDetails<GetTransactionDTO>.Failed();
            }
           
            var transactionDTO = new CreateTransactionDTO
            {
                Status=transactionDetails.Status,
                ReturnDate=transactionDetails.ReturnDate,
                ExpectedReturnDate = transactionDetails.ExpectedReturnDate,
                ActualReturnDate=transactionDetails.ActualReturnDate,
                BorrowDate = transactionDetails.BorrowDate,
            };
            transaction.Status = (transactionDetails.Status != null)
            ? transactionDetails.Status
            : transaction.Status;

            transaction.ReturnDate = (transactionDetails.ReturnDate != null)
            ? transactionDetails.ReturnDate
            : transaction.ReturnDate; 
            
            transaction.ExpectedReturnDate=(transactionDetails.ExpectedReturnDate != null)
                ?transactionDetails.ExpectedReturnDate
                : transaction.ExpectedReturnDate;
            
            transaction.ActualReturnDate=(transactionDetails != null)
                ? transactionDetails.ActualReturnDate
                : transaction.ActualReturnDate;

            transaction.BorrowDate=(transactionDetails!= null)
                ? transactionDetails.BorrowDate
                : transaction.BorrowDate;

            _libraryContext.Update<CreateTransactionDTO>(transactionDetails);
            await _libraryContext.SaveChangesAsync();
            var dto = new GetTransactionDTO
            {
                Id = transaction.Id,
                Status = transaction.Status,
                ExpectedReturnDate = transaction.ExpectedReturnDate,
                BookTitle = transaction.Book.Title,
                UserName = transaction.User.Name,
                BorrowDate = transaction.BorrowDate,
                //DaysOverdue = int(transaction.ExpectedReturnDate - transaction.ActualReturnDate)
            };
            return ResponseDetails<GetTransactionDTO>.Success(dto,"Update was Successful",200);
          
        }
    }
}
