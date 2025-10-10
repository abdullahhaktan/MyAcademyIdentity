# MyAcademyIdentity

[TR]

**Kimlik Yönetim Sistemi (ASP.NET Core Identity)**

---

## 💻 Proje Hakkında

Bu proje, **ASP.NET Core Identity** altyapısını kullanarak geliştirilmiş, merkezi bir kullanıcı ve yetkilendirme yönetim sistemidir. Güvenli kullanıcı kimlik doğrulama, rol yönetimi ve yetkilendirme işlevlerini sağlam, genişletilebilir bir yapıda sunar. Eğitim veya küçük/orta ölçekli uygulamaların güvenlik temelini oluşturmak için tasarlanmıştır.

---

## ✨ Temel Özellikler

### Teknik Özellikler

* **ASP.NET Core Identity**: Kullanıcı, rol ve talep (claim) yönetimi.
* **Katmanlı mimari** (Tercihe bağlı olarak: DataAccess / Business / WebUI)
* **Entity Framework Core (EF Core)**: Veritabanı işlemleri ve Migrations yönetimi.
* **Güvenli Parola Saklama**: Identity'nin yerleşik hash algoritmaları kullanılır.
* **Veritabanı Esnekliği**: EF Core ile MSSQL, SQLite veya diğer veritabanlarına kolayca adapte edilebilir.

### Kullanıcı / Panel Özellikleri

* **Kullanıcı Kaydı ve Girişi** (Register / Login)
* **Parola Sıfırlama** ve **E-posta Doğrulama** mekanizmaları
* **Rol Tabanlı Yetkilendirme** (Role-Based Authorization)
* Yönetici (Admin) Paneli: **Kullanıcı ve rol yönetimi** (Eğer arayüz entegre edilmişse)

---

### 🚀 Nasıl Çalıştırılır?

Bu projeyi yerel ortamınızda ayağa kaldırmak için aşağıdaki adımları izleyin:

1.  **Gereksinimler:**
    * [.NET SDK 6.0 veya üzeri](https://dotnet.microsoft.com/download)
    * [SQL Server](https://www.microsoft.com/en-us/sql-server) (veya tercih edilen veritabanı)
    * [Visual Studio 2022](https://visualstudio.microsoft.com/) (Önerilen)

2.  **Projeyi Klonlama:**
    ```bash
    git clone [https://github.com/abdullahhaktan/MyAcademyIdentity.git](https://github.com/abdullahhaktan/MyAcademyIdentity.git)
    cd MyAcademyIdentity
    ```

3.  **Bağımlılıkları Yükleme:**
    ```bash
    dotnet restore
    ```

4.  **Veritabanı Ayarları:**
    * `appsettings.json` dosyasını açın ve `ConnectionStrings` bölümündeki veritabanı bağlantı dizesini (`DefaultConnection`) kendi yerel veritabanı ayarlarınıza göre güncelleyin.

5.  **Veritabanını Oluşturma (Migrations):**
    * Projenin kök dizininde komut satırını kullanın:
        ```bash
        dotnet ef database update
        ```

6.  **Projeyi Çalıştırma:**
    * Projeyi Visual Studio'da açın veya komut satırında çalıştırın:
        ```bash
        dotnet run
        ```
    * Uygulama genellikle `https://localhost:[PORT]` (veya benzeri bir adreste) çalışmaya başlayacaktır.

---
---

[EN]

# MyAcademyIdentity

## 💻 About the Project

This project is a centralized user and authorization management system developed using the **ASP.NET Core Identity** framework. It provides secure user authentication, role management, and authorization functionalities within a robust and extensible structure, designed to form the security foundation for educational or small/medium-scale applications.

---

## ✨ Core Features

### Technical Features

* **ASP.NET Core Identity**: Core management of users, roles, and claims.
* **Layered architecture** (Optionally: DataAccess / Business / WebUI)
* **Entity Framework Core (EF Core)**: Database operations and Migrations management.
* **Secure Password Storage**: Utilizes Identity's built-in hashing algorithms.
* **Database Flexibility**: Easily adaptable to MSSQL, SQLite, or other databases via EF Core.

### User / UI Features

* **User Registration and Login**
* **Password Reset** and **Email Confirmation** mechanisms
* **Role-Based Authorization**
* Admin Panel: **User and role management** (If UI integrated)

---

### 🚀 How to Run

Follow these steps to set up and run the project locally:

1.  **Prerequisites:**
    * [.NET SDK 6.0 or higher](https://dotnet.microsoft.com/download)
    * [SQL Server](https://www.microsoft.com/en-us/sql-server) (or preferred database)
    * [Visual Studio 2022](https://visualstudio.microsoft.com/) (Recommended)

2.  **Cloning the Project:**
    ```bash
    git clone [https://github.com/abdullahhaktan/MyAcademyIdentity.git](https://github.com/abdullahhaktan/MyAcademyIdentity.git)
    cd MyAcademyIdentity
    ```
    
3.  **Installing Dependencies:**
    ```bash
    dotnet restore
    ```

4.  **Database Configuration:**
    * Open the `appsettings.json` file and update the database connection string (`DefaultConnection`) under the `ConnectionStrings` section to match your local database settings.

5.  **Creating the Database (Migrations):**
    * Use the command line in the project's root directory:
        ```bash
        dotnet ef database update
        ```

6.  **Running the Project:**
    * Open the project in Visual Studio or run it via the command line:
        ```bash
        dotnet run
        ```
    * The application will typically start running at an address like `https://localhost:[PORT]`.

---
---
![1757943473621](https://github.com/user-attachments/assets/f44014d5-deee-46dd-94ce-fc61771936e1)

![1757943475164](https://github.com/user-attachments/assets/9e3adbda-bd34-4550-933d-84fde9d14e3e)

![1757943478196](https://github.com/user-attachments/assets/03300c49-2970-4652-b166-d020ed151e75)

![1757943476971](https://github.com/user-attachments/assets/524dc23e-7939-4515-ade7-219eb4233e99)

![1757943478044](https://github.com/user-attachments/assets/336da4e0-bfb4-43dc-a096-20ec3d7dadaa)
