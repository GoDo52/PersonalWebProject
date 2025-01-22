# Personal Finance Management App

## Overview

This project is my personal pet project — a simple CRUD application designed to help manage personal finances (or finances of a family/group of people). It allows users to track their spending habits by logging expenses, categorizing them, and viewing (so far) simple analytics to understand where their money goes. I wanted to build something practical, secure, and user-friendly while improving my skills in C#/ASP.NET Core development.

The app uses an N-tier architecture and it is built with ASP.NET Core MVC. It integrates well with Azure SQL Database for data storage and has role-based authentication and authorization. The project has essential features of a modern web app: simple and clean design, secure authentication, and effective data handling.

---

## Features

- **User Authentication:**
  - Secure login system using hashed passwords.
  - Role-based access control (Admin, Manager, User).

- **Spending Management:**
  - Add, edit, and delete spending records.
  - Categorize spendings for better organization.
  
- **Analytics: (so far)**
  - View total spending for the current and previous months.
  - Identify top spenders if multiple users use an app.

- **Category Management:**
  - Admins can create, update, and delete spending categories.

- **Secure Database Integration:**
  - Supports Azure SQL Database with environment-based configuration for connection strings.

---

## Technologies Used

- **Frameworks:** ASP.NET Core MVC, Entity Framework Core
- **Frontend:** Bootstrap
- **Database:** Azure SQL Database
- **Authentication:** Custom authentication with password hashing and role-based authorization
- **Deployment:** Hosted on Azure App Services

---

## Prerequisites

1. **.NET SDK:** Ensure .NET SDK 6.0 or later is installed.
2. **SQL Server:** Azure SQL Database or a local SQL Server instance.

---

## Setup Instructions

### Clone the Repository
```bash
git clone https://github.com/your-username/your-repository.git
cd your-repository
```

### Configure the Database

1. Set up your database connection string.
  - Add your DefaultConnection string in appsettings.Development.json for local development.

```cs
"ConnectionStrings": {
  "DefaultConnection": "YourConnectionStringHere"
}
```
2. For deployment Use Azure App Service environment variables for production.

---

## Deployment

### Deploy to Azure

1. Configure your Azure App Service.

2. Set environment variables (e.g., connection strings) in the Azure Portal.

3. Use the GitHub Actions workflow provided in the repository for CI/CD:

4. Push to the master branch to trigger deployment.
