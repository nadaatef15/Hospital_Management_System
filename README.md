 # 🏥 Hospital Management System (HMS)

Welcome to the Hospital Management System (HMS)!  
This repository offers a full-featured, modular, and scalable web application tailored for hospitals and clinics. It follows Clean Architecture principles and is designed to streamline operations and enhance patient care.

---

## 📌 Overview

This system provides:

- 🧑‍⚕️ **User Registration & Permission-Based Access**
- 🩺 **Doctor Management** (specialties, schedules)
- 📅 **Appointment Scheduling & Tracking**
- 🧾 **Prescriptions, Diagnoses, and Medicines**
- 🗂️ **Patient Medical Records Management**
- 📦 **Inventory Tracking**
- 🌐 **Multi-Language Support (Localization)**
- 🗑️ **Soft Delete for Data Integrity**
- 🕵️ **Audit Logging for Created/Updated Records**
- 🔐 **JWT Authentication & ASP.NET Identity**
- ✅ **Validation with FluentValidation**
- 🧰 **Global Exception Handling Middleware**
- 🧪 **xUnit for Unit Testing**
- ☁️ **Cloudinary Integration for Image Uploads**

---


🏗️ Built With

| Technology            | Description                       |
| --------------------- | --------------------------------- |
| .NET Core             | Backend framework                 |
| Entity Framework Core | ORM for database management       |
| ASP.NET Identity      | Authentication & Authorization    |
| Angular               | Frontend SPA framework            |
| FluentValidation      | Input validation                  |
| xUnit                 | Unit testing framework            |
| Cloudinary            | Image storage and transformation  |
| JWT                   | Secure token-based authentication |


---




## 🗂️ Entity Relationship Diagram (ERD)

Here’s a visual overview of the data relationships in the system:

ERD Diagram ![a Hospital Management System (HMS) drawio (1)](https://github.com/user-attachments/assets/2afe5792-27ed-4892-8a7a-467ffc082eaa)

---
## 📂 Project Structure

```text
HMS/

├── HMS/                        
│   ├── Controllers/
│   ├── Middlewares/
│   ├── Program.cs
│   └── appsettings.json
│
├── HMS.BLL/                     
│   ├── Managers/
│   ├── Interfaces/
│   ├── Validators/
│   ├── Filter/
│   ├── Mappers/
│   ├── ManagePermissions/
│   ├── Seeds/
│   └── Resources/
│
├── HMS.DAL/                   
│   ├── Repositories/
│   ├── Configuration/
│   ├── Migrations/
│   └── ApplicationDbContext.cs
│
├── HMS.Contracts/               
│   ├── Constants/
│   ├── CustomValidation/
│   ├── Language/
│   ├── Model/
│   └── Infrastructure/
│       └── Exceptions/
│
├── HMS.Tests/                  
│
└── README.md

```

## 🚀 Getting Started

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/your-username/hms.git
cd hms
