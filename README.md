# 🛒 Talabat - Full Stack E-Commerce Application  

Full-stack e-commerce web app built with **ASP.NET Core Web API** (backend) and **Angular** (frontend), modeling a Talabat-like
online food ordering platform. It includes user authentication, product browsing, shopping cart, checkout, orders, and Stripe (test) payment integration.  

## 🚀 Badges / شارات
[![.NET 7.0](https://img.shields.io/badge/.NET-7.0-blue)]() 
[![Angular 14](https://img.shields.io/badge/Angular-14-red)]() 
[![License: MIT](https://img.shields.io/badge/License-MIT-green)]()  

## 🔑 Executive Summary / ملخص المشروع
Talabat is a learning project demonstrating real-world full-stack web development.
It allows users to register/login, browse and filter products, manage a shopping basket,
checkout with delivery options, and view order history. Stripe in Test Mode is used for payment simulation. The backend follows
clean architecture (controllers, services, repository, EF Core), and the frontend is a modular Angular app with responsive design.  

### 🛠️ Required Software / البرامج المطلوبة

| Software          | Minimum Version | ملاحظات |
|-------------------|-----------------|---------|
| .NET SDK          | 6.0+            |  
| Node.js           | 14.x+           |  
| Angular CLI       | 12+             |  
| SQL Server (or localdb) | 2019+     |  
| Git               | -               |  

## 📋 Table of Contents / جدول المحتويات
- [Overview / لمحة عامة](#overview-لمحة-عامة)  
- [Features / الميزات](#features-الميزات)  
- [Technologies / التقنيات](#technologies-التقنيات)  
- [Architecture / البنية المعمارية](#architecture-البنية-المعمارية)  
- [Project Structure / هيكل المشروع](#project-structure-هيكل-المشروع)  
- [Quick Start / التشغيل السريع](#quick-start-التشغيل-السريع)  
- [Configuration / الإعدادات](#configuration-الإعدادات)  
- [Screenshots / لقطات شاشة](#screenshots-لقطات-شاشة)  
- [API Documentation / توثيق API](#api-documentation-توثيق-api)  
- [Testing / الاختبار](#testing-الاختبار)  
- [Build & Deployment / البناء والنشر](#build--deployment-البناء-والنشر)  
- [Security / الأمن](#security-الأمن)  
- [Contribution / المساهمة](#contribution-المساهمة)  
- [License / الرخصة](#license-الرخصة)  
- [Author / المؤلف](#author-المؤلف)  
- [Add README to Repository / إضافة README للمستودع](#add-readmemd-to-repository-إضافة-readmemd-للمستودع)

## 📖 Overview / لمحة عامة
Talabat is a **full-stack** web application simulating an e-commerce platform. It supports:
- **User Accounts:** Registration, login, profile management (ASP.NET Core Identity & JWT)  
- **Product Catalog:** Browse, search, filter by category/brand, pagination, product details  
- **Shopping Cart:** Add/remove items, adjust quantities, persistent cart for users  
- **Checkout Flow:** Enter delivery address, choose delivery method, review order  
- **Orders:** Create orders, view current and past orders, order status tracking  
- **Payments:** Stripe payment integration (Test Mode) to simulate payment processing  
- **Error Handling:** Global error middleware with consistent API responses  
  
All RESTful APIs are documented with Swagger/OpenAPI. The frontend uses Angular services, routing, and state management for seamless user experience.  
  

## ✨ Features / الميزات
- **User Registration & Login:** Secure user accounts (ASP.NET Identity, JWT)  
- **Product Browsing:** View product list, details, images, and filters (brand/category)  
- **Shopping Basket:** Add to cart, update quantities, calculate totals  
- **Checkout Process:** Multi-step checkout (address, delivery method, review, payment)  
- **Order Management:** Create orders, view order history and details  
- **Stripe Integration:** Test payments with Stripe (no real charges)  
- **Responsive UI:** Works on desktop and mobile (Bootstrap/SCSS)  
- **Error Handling:** Consistent error format and client-side notifications  
- **API Documentation:** Swagger UI for easy testing of endpoints  
  

## 🛠️ Technologies / التقنيات
- **Backend:** C#, ASP.NET Core Web API, Entity Framework Core, SQL Server, ASP.NET Core Identity, JWT, AutoMapper, LINQ, Swagger/OpenAPI, Stripe  
- **Frontend:** Angular, TypeScript, HTML5, SCSS, Bootstrap, RxJS, Angular Router, HttpClient  
- **Tools:** Git, Visual Studio/VS Code, Node.js, npm, Angular CLI, SQL Server Management Studio  

*(Backend: C# Web API, بيانات بـ Entity Framework Core، مصادقة JWT. Frontend: Angular وتصفح تفاعلي. الأدوات: Git, Node, Angular CLI)*

## 🏗️ Architecture / البنية المعمارية
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

## 📂 Project Structure / هيكل المشروع
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

## ✅ Quick Start / التشغيل السريع
**Prerequisites:** Ensure .NET SDK, Node.js, and Angular CLI are installed.

use:
-Angular CLI: 11.2.19
-Node: 16.16.0

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
-[Home Page](https://github.com/user-attachments/assets/bfeeb312-f1c8-4155-ae78-98742d0a1626)
-[Products](https://github.com/user-attachments/assets/ba1b60a6-dc2c-421f-8cca-9f7d3d199ac2) 
-[Basket](https://github.com/user-attachments/assets/e64995e9-f526-4fe6-95b4-d53931b8830b)
-[Checkout-Address](https://github.com/user-attachments/assets/685d0a69-4c1e-43cb-886b-b811894ccea9)
-[Checkout-Delivery Method](https://github.com/user-attachments/assets/2684f500-e6fd-4f6a-b69f-947b2ff05ac3)
-[Checkout-Review](https://github.com/user-attachments/assets/fb0654fd-be22-4323-93e2-d1dc1b696794)
-[Checkout-Payment](https://github.com/user-attachments/assets/d9ce2b6e-dc3d-4c98-9f17-9116152f160b)
-[Order Confirmation](https://github.com/user-attachments/assets/998b46de-e2e9-4cb1-b705-83c4aaf4f76e)
-[Payment Transaction Succeeded On Stripe](https://github.com/user-attachments/assets/c2e7596d-0e14-43f2-9f50-637b62de772c)
-[ Payment Received in "Order View"](https://github.com/user-attachments/assets/84b968d9-c3dc-4bf1-86cd-ef13d1fced13)



## 📑 API Documentation / توثيق API
The backend uses **Swagger** for API documentation. After running the API, navigate to `https://localhost:<API_PORT>/swagger` 
(e.g. `https://localhost:5001/swagger`) to view and test all endpoints interactively.  


## 🔒 Security / الأمن
- **Do Not Commit Secrets:** Never commit sensitive keys (JWT secrets, Stripe keys, passwords) to version control.
   Use `.gitignore` to exclude files like `.env`.  
- **Rotate Keys:** If a secret is exposed, generate and replace it immediately (especially for Stripe/JWT).  
- **HTTPS:** Always use HTTPS in production to encrypt data in transit.  
- **Input Validation:** The API validates input and returns appropriate error responses to prevent invalid data.  


