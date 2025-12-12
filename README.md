LibraryManagementAPI 

A simple, maintainable library management API that manages books, users, and transactions (borrowing and returning books).built with N-Layered Architecture in ASP.NET Core — prioritizing separation of concerns, testability, and real-world workflows.
Features 

     User & Book Management (CRUD)  
     Transaction Lifecycle: Borrowing, returning, due date tracking  
     Validation: Ensures users/books exist before transactions  
     Secure Authentication: JWT-based login (with BCrypt hashing)  
     Resilient Design: Proper exception handling, logging, and async/await  
     

  Tech Stack 
Framework
	
ASP.NET Core 8 (Minimal APIs)
Architecture
	
N-Layered Architecture
Data
	
Entity Framework Core, SQL Server (SSMS)
Auth
	
JWT Bearer, BCrypt.Net for password hashing
API Docs
	
Swagger/OpenAPI
Testing
	
Tools
	
Git (project tracking)
 
 
 
 Getting Started 
Prerequisites 

    .NET 8 SDK 
    SQL Server (LocalDB, Express, or Docker)
>
Setup 

    Clone the repo: 
    bash
     

 
1
2
git clone https://github.com/your-username/LibraryManagementAPI.git
cd LibraryManagementAPI
 
 

Update the connection string in appsettings.json: 
json
 
 
1
2
3
⌄
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=true;"
}
 
 

Apply migrations & seed (optional): 
bash
 
 
1
dotnet ef database update
 
 

Run the API: 
bash
 

     
    1
    dotnet run
     
     

    Explore the API: 
        Swagger UI: https://localhost:5001/swagger
         
     

 
 Authentication Flow 

    Register a user (POST /api/auth/register)  
    Login to receive a JWT (POST /api/auth/login)  
    Include token in Authorization: Bearer <token> header for protected endpoints (e.g., /api/transactions)
     

    Passwords are hashed using BCrypt — never stored in plaintext. 
     

 
📦 Key Endpoints 
/api/books
	
GET
	
Get all books
/api/books/{id}
	
GET
	
Get book by ID
/api/users/{id}
	
GET
	
Get user by ID
/api/transactions
	
POST
	
Borrow a book
(requires
bookId
,
userId
,
expectedReturnDate
)
/api/transactions/{id}/return
	
PATCH
	
Return a book
(updates status & actual return date)
/api/transactions/user/{userId}
	
GET
	
Get all transactions for a user
 
 

Design Note:
Transaction creation validates existence of user & book before proceeding — avoiding invalid state. Future iterations can include: 

    Fine calculation on overdue returns
    Reservation queues
     

 
 Testing & Validation 

    ✅ Swagger auto-docs with example requests/responses  
    ✅ Proper HTTP status codes (400 for validation, 404 for not found, 401 for auth failures)
     

 
Contributing 

PRs welcome! For major changes, please open an issue first.
Let’s build a system that’s not just functional — but thoughtful. 
