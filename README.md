# 🛒 Talabat - Full Stack E-Commerce Application  

Full-stack e-commerce web app built with **ASP.NET Core Web API** (backend) and **Angular** (frontend), modeling a Talabat-like
online food ordering platform. It includes user authentication, product browsing, shopping cart, checkout, orders, and Stripe (test) payment integration.  


## 🔑 Executive Summary
Talabat is a learning project demonstrating real-world full-stack web development.
It allows users to register/login, browse and filter products, manage a shopping basket,
checkout with delivery options, and view order history. Stripe in Test Mode is used for payment simulation. The backend follows
clean architecture (controllers, services, repository, EF Core), and the frontend is a modular Angular app with responsive design.  

### 🛠️ Required Software

| Software          |Version          |
|-------------------|-----------------|
| .NET SDK          | 6.0             |  
| Node.js           | 16.16.0         |  
| Angular CLI       | 11.2.19         |  
| SQL Server        | 2019+           |  


## 📋 Table of Contents
- Overview
- Features
- Technologies
- Architecture
- Project Structure 
- Quick Start 
- Configuration
- Screenshots
- API Documentation
- Security


## 📖 Overview
Talabat is a **full-stack** web application simulating an e-commerce platform. It supports:
- **User Accounts:** Registration, login, profile management (ASP.NET Core Identity & JWT)  
- **Product Catalog:** Browse, search, filter by category/brand, pagination, product details  
- **Shopping Cart:** Add/remove items, adjust quantities, persistent cart for users  
- **Checkout Flow:** Enter delivery address, choose delivery method, review order  
- **Orders:** Create orders, view current and past orders, order status tracking  
- **Payments:** Stripe payment integration (Test Mode) to simulate payment processing  
- **Error Handling:** Global error middleware with consistent API responses  
  
All RESTful APIs are documented with Swagger/OpenAPI. The frontend uses Angular services, 
routing, and state management for seamless user experience.  
  

## ✨ Features
- **User Registration & Login:** Secure user accounts (ASP.NET Identity, JWT)  
- **Product Browsing:** View product list, details, images, and filters (brand/category)  
- **Shopping Basket:** Add to cart, update quantities, calculate totals  
- **Checkout Process:** Multi-step checkout (address, delivery method, review, payment)  
- **Order Management:** Create orders, view order history and details  
- **Stripe Integration:** Test payments with Stripe (no real charges)  
- **Responsive UI:** Works on desktop and mobile (Bootstrap/SCSS)  
- **Error Handling:** Consistent error format and client-side notifications  
- **API Documentation:** Swagger UI for easy testing of endpoints  
  

## 🛠️ Technologies
- **Backend:** C#, ASP.NET Core Web API, Entity Framework Core, SQL Server, ASP.NET Core Identity, JWT, AutoMapper, LINQ, Swagger/OpenAPI, Stripe  
- **Frontend:** Angular, TypeScript, HTML5, SCSS, Bootstrap, RxJS, Angular Router, HttpClient  
- **Tools:** Git, Visual Studio/VS Code, Node.js, npm, Angular CLI, SQL Server Management Studio  


## 🏗️ Architecture
The app uses a layered architecture. Typical data flow:  
```
Angular Frontend (Client)
       ↓ HTTP Request
Controllers (Talabat.APIs project)
       ↓
Business Services (Talabat.Service)
       ↓
Data Repositories (Talabat.Repository, using GenericRepository & UnitOfWork)
       ↓
Entity Framework Core
       ↓
SQL Server Database
```  

## 📂 Project Structure 
```
Talabat/
├── Backend/
│   ├── Talabat.APIs/        # ASP.NET Core Web API project (controllers, appsettings.json, Swagger)
│   ├── Talabat.Core/        # Domain entities, interfaces, DTOs, specifications
│   ├── Talabat.Repository/  # EF Core DbContext, Migrations, Repositories, Data seeding
│   ├── Talabat.Service/     # Application services (AuthService, ProductService, etc.)
│   └── Talabat.G01.Solution.sln  # Visual Studio solution file
│
└── Frontend/
    ├── src/                # Angular application source code (app modules, components, services)
    ├── angular.json        # Angular CLI configuration
    ├── package.json        # Node dependencies and scripts
    └── ...                 
```

## ✅ Quick Start
**Prerequisites:** Ensure .NET SDK, Node.js, and Angular CLI are installed.

1. **Clone the repository:**  
   ```bash
   git clone https://github.com/7AbdoFouad/Talabat.git
   cd Talabat
   ```
   
2. **Backend setup:**  
   ```bash
   cd Backend/Talabat.APIs      # Windows or macOS/Linux
   dotnet restore              # Restore NuGet packages
   dotnet tool restore         # (If any .NET tools are required)
   dotnet ef database update   # Apply EF Core migrations (ensure DB configured)
   dotnet run                  # Launch the API (e.g. https://localhost:5001)
   ```
   
3. **Frontend setup:**  
   ```bash
   cd ../../Frontend
   npm install                 # Install dependencies
   ng serve --open             # Run Angular (opens http://localhost:4200)
   ```
   
4. **Access the app:**  
   Open your browser and go to `http://localhost:4200`. The frontend should communicate with the running backend API.  


## ⚙️ Configuration / الإعدادات
- **Connection Strings:** Update the SQL Server connection string in `Backend/Talabat.APIs/appsettings.json` or via environment/user secrets. Example:  
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=<YOUR_DB_NAME>;Trusted_Connection=True;"
    },
    "Jwt": {
      "Key": "<YOUR_JWT_SECRET>"
    },
    "Stripe": {
      "SecretKey": "<YOUR_STRIPE_SECRET>"
    }
  }
  ```
  Replace `<YOUR_DB_NAME>`, `<YOUR_JWT_SECRET>`, `<YOUR_STRIPE_SECRET>` with actual values.

- **Environment Variables / User Secrets:** For local development,
   consider storing sensitive values (JWT key, Stripe keys, connection strings)
   using [dotnet user-secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) or OS environment variables.  
  ```bash
  # Example .env file (not committed to source control)
  API_URL=http://localhost:5001/api/
  DB_CONNECTION=Server=(localdb)\\mssqllocaldb;Database=<YOUR_DB>;Trusted_Connection=True;
  JWT_SECRET=some_long_secret_key
  STRIPE_SECRET_KEY=sk_test_...
  ```
  
- **Angular environment:** In `Frontend/src/environments/environment.ts`, set the `apiUrl` to your backend API (e.g. `"https://localhost:5001/api/"`).  

## 📸 Screenshots / لقطات شاشة
_Add relevant screenshots to illustrate the app’s features._ For example:  
-[Home Page]<img width="1900" height="862" alt="1" src="https://github.com/user-attachments/assets/7c180b31-1399-4f9d-9c40-b74907ab0394" />
-[Products] <img width="1917" height="867" alt="2" src="https://github.com/user-attachments/assets/55f6b868-518f-4881-88aa-5e26a87df46e" />
-[Basket]<img width="1918" height="868" alt="3" src="https://github.com/user-attachments/assets/25787dc6-74ea-4de2-86b3-e57810bfff3a" />
-[Checkout-Address]<img width="1918" height="870" alt="4" src="https://github.com/user-attachments/assets/f7fce33d-f26e-4bf7-b418-dcb79c47447a" />
-[Checkout-Delivery Method]<img width="1917" height="866" alt="5" src="https://github.com/user-attachments/assets/2beb142d-8555-47b8-9d07-64651fa488ef" />
-[Checkout-Review]<img width="1917" height="865" alt="6" src="https://github.com/user-attachments/assets/4b4f71e9-6ab9-400d-a26b-1087b815baa9" />
-[Checkout-Payment]<img width="1918" height="868" alt="7" src="https://github.com/user-attachments/assets/4354d162-f15d-4c71-b2e3-cc95dade1d5a" />
-[Order Confirmation]<img width="1918" height="870" alt="9" src="https://github.com/user-attachments/assets/9da7f44e-94f3-42e4-b117-9a4b96091eac" />
-[Payment Transaction Succeeded On Stripe]<img width="1918" height="876" alt="10" src="https://github.com/user-attachments/assets/fda6783d-e367-42f1-9f06-e16e369302ee" />
-[Payment Received in "Current Order View"]<img width="1917" height="858" alt="11" src="https://github.com/user-attachments/assets/b8a85ac6-5f79-4495-aeb2-39446050bd22" />
-[view past order history]<img width="1915" height="871" alt="14" src="https://github.com/user-attachments/assets/25f4d589-0d54-44fb-83e5-db722bb29069" />
-[Login]<img width="1916" height="866" alt="12" src="https://github.com/user-attachments/assets/4d46fecc-7c9a-41c6-bee8-eb403d36aad1" />
-[Register]<img width="1915" height="865" alt="13" src="https://github.com/user-attachments/assets/480c562e-0f99-4974-be61-1db6c1f68266" />





## 📑 API Documentation / توثيق API
The backend uses **Swagger** for API documentation. After running the API, navigate to `https://localhost:<API_PORT>/swagger` 
(e.g. `https://localhost:5001/swagger`) to view and test all endpoints interactively.  


## 🔒 Security / الأمن
- **Do Not Commit Secrets:** Never commit sensitive keys (JWT secrets, Stripe keys, passwords) to version control.
   Use `.gitignore` to exclude files like `.env`.  
- **Rotate Keys:** If a secret is exposed, generate and replace it immediately (especially for Stripe/JWT).  
- **HTTPS:** Always use HTTPS in production to encrypt data in transit.  
- **Input Validation:** The API validates input and returns appropriate error responses to prevent invalid data.  
