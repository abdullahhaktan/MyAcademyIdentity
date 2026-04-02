# 🔐 MyAcademyIdentity

> ASP.NET Core Identity altyapısıyla geliştirilmiş, rol tabanlı yetkilendirme ve kullanıcı yönetimi sistemi.
> A role-based authorization and user management system built on ASP.NET Core Identity.

[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Framework](https://img.shields.io/badge/Framework-ASP.NET%20Core%20MVC-602C78.svg)]()
[![Database](https://img.shields.io/badge/Database-SQL%20Server-CC2927.svg)](https://learn.microsoft.com/en-us/sql/sql-server/)
[![Repo Size](https://img.shields.io/github/repo-size/abdullahhaktan/MyAcademyIdentity)](https://github.com/abdullahhaktan/MyAcademyIdentity)

---

## 🚀 Özellikler / Features

| 🇹🇷 Türkçe | 🇬🇧 English |
|------------|------------|
| Kullanıcı Kaydı ve Girişi | User Registration & Login |
| Parola Sıfırlama ve E-posta Doğrulama | Password Reset & Email Confirmation |
| Rol Tabanlı Yetkilendirme | Role-Based Authorization |
| Admin Paneli: Kullanıcı & Rol Yönetimi | Admin Panel: User & Role Management |
| Güvenli parola saklama (ASP.NET Core Identity) | Secure password storage (ASP.NET Core Identity) |
| Entity Framework Core ile veritabanı yönetimi | Database management via Entity Framework Core |

---

## 🏗️ Mimari / Architecture

```
MyAcademyIdentity/
├── Controllers/
│   ├── AccountController.cs
│   ├── AdminController.cs
│   └── RoleController.cs
├── Models/
│   ├── AppUser.cs
│   └── ViewModels/
├── Views/
│   ├── Account/
│   ├── Admin/
│   └── Role/
├── Data/
│   └── AppDbContext.cs
└── wwwroot/
```

---

## 🛠️ Kullanılan Teknolojiler / Tech Stack

| Katman / Layer | Teknoloji / Technology |
|----------------|------------------------|
| Framework | ASP.NET Core MVC |
| Kimlik / Identity | ASP.NET Core Identity |
| ORM | Entity Framework Core |
| Veritabanı / Database | SQL Server |
| Dil / Language | C# |

---

## ⚙️ Kurulum / Setup

### Gereksinimler / Requirements
- .NET 8 SDK
- SQL Server

### Adımlar / Steps

```bash
# Repoyu klonla / Clone the repo
git clone https://github.com/abdullahhaktan/MyAcademyIdentity.git
cd MyAcademyIdentity

# Bağımlılıkları yükle / Install dependencies
dotnet restore
```

**`appsettings.json` — Bağlantı dizesini güncelle / Update connection string:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MyAcademyIdentityDb;Trusted_Connection=True;"
  }
}
```

```bash
# Migration uygula / Apply migrations
dotnet ef database update

# Uygulamayı başlat / Run the application
dotnet run
```

---

## 📸 Ekran Görüntüleri / Screenshots

![1](https://github.com/user-attachments/assets/f44014d5-deee-46dd-94ce-fc61771936e1)
![2](https://github.com/user-attachments/assets/9e3adbda-bd34-4550-933d-84fde9d14e3e)
![3](https://github.com/user-attachments/assets/03300c49-2970-4652-b166-d020ed151e75)
![4](https://github.com/user-attachments/assets/524dc23e-7939-4515-ade7-219eb4233e99)
![5](https://github.com/user-attachments/assets/336da4e0-bfb4-43dc-a096-20ec3d7dadaa)

---

## 👨‍💻 Geliştirici / Developer

**Abdullah Haktan**
GitHub → [abdullahhaktan](https://github.com/abdullahhaktan)
