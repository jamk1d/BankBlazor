BankBlazor 

I built a bank application where you can manage customers and their bank accounts. The backend is an API built with ASP.NET Core and the frontend is Blazor WebAssembly.

What the app does

See all customers in a list
Click on a customer to see their profile
Deposit and withdraw money
Transfer money to another account
See transaction history for an account
The home page shows the next Scottish bank holiday

How to use it

Go to /customers to browse all customers. Click View Profile on any customer to see their details and accounts. From the profile page you can deposit, withdraw and transfer money. You can also search for a specific customer by typing their ID in the search field.
To see transactions for an account, click View Transactions on the account.

What I used to build it

ASP.NET Core Web API for the backend
Blazor WebAssembly for the frontend
Entity Framework Core with Database First
SQL Server for the database
Azure to host everything

I kept the backend and frontend completely separate. The API handles all the data and business logic, and Blazor just calls the API and shows the result to the user. I used services and interfaces to keep the code clean and separated the shared code into a Class Library that both projects can use.

Links
App: https://bankblazorclient-jamal-cvcyf8bwb5ahabbq.germanywestcentral-01.azurewebsites.net/

API: https://bankblazorapi-byh0frhqe7b2argt.germanywestcentral-01.azurewebsites.net/

