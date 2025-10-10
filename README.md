# MyAcademyIdentity

#[TR]
# ASP.NET Core Identity ile Güvenli Kimlik Yönetimi

# Bu proje, ASP.NET Core Identity çatısını kullanarak gelişmiş bir kullanıcı kimlik doğrulama ve yetkilendirme mekanizmasını uygulamaktadır. Akademi (MyAcademy) eğitimleri kapsamında, bir web uygulamasında güvenli kullanıcı yönetiminin A'dan Z'ye nasıl kurulacağını göstermektedir.

## ✨ Temel Özellikler

# * **Gelişmiş Kimlik Doğrulama:** ASP.NET Core Identity'nin standartlarını (parola karma, güvenlik damgası vb.) kullanır.
# * **Rol Yönetimi:** Kullanıcılar için farklı yetki seviyeleri (Örn: Admin, Member) tanımlama ve atama.
# * **Kullanıcı Kaydı ve Girişi:** Güvenli kayıt ve oturum açma işlemleri.
# * **Şifre Sıfırlama:** E-posta yoluyla şifre sıfırlama işlevi.
# * **E-posta Onayı:** Yeni kullanıcılar için hesap doğrulama zorunluluğu.
# * **Özelleştirilmiş Kullanıcı Modeli:** Ek profil bilgileri tutmak için IdentityUser sınıfının özelleştirilmesi.

## 🚀 Nasıl Kurulur ve Çalıştırılır?

# Bu projenin çalıştırılması için **.NET SDK** ve bir **SQL Server** veritabanı gereklidir.

# 1. Projeyi Klonlama:
git clone https://github.com/abdullahhaktan/MyAcademyIdentity
cd MyAcademyIdentity

# 2. Veritabanını Hazırlama:
# * Bağlantı Dizesini (appsettings.json dosyasında) kendi yerel SQL Sunucusu ayarlarınıza göre güncelleyin.
# * Veritabanı şemasını oluşturmak ve veritabanını otomatik olarak oluşturmak için Entity Framework Core migrasyonlarını uygulayın:
dotnet ef database update

# * Not: Bu komut, belirtilen sunucuda veritabanı yoksa otomatik olarak oluşturacaktır. El ile veritabanı oluşturmanıza gerek yoktur.

# 3. Çözümü Başlatma:
# * Visual Studio veya VS Code ile .sln (Solution) dosyasını açın.
# * Projeyi derleyin ve F5 tuşu (Visual Studio) veya dotnet run komutu ile uygulamayı başlatın.

#[EN]
# Secure Identity Management with ASP.NET Core Identity

# This project implements an advanced user authentication and authorization mechanism using the ASP.NET Core Identity framework. Developed within the scope of MyAcademy training, it demonstrates how to establish secure user management from start to finish in a web application.

## ✨ Core Features

# * **Advanced Authentication:** Utilizes ASP.NET Core Identity standards (password hashing, security stamp, etc.).
# * **Role Management:** Defining and assigning different authorization levels for users (E.g.: Admin, Member).
# * **User Registration and Login:** Secure registration and sign-in processes.
# * **Password Reset:** Password reset functionality via email.
# * **Email Confirmation:** Enforcing account verification for new users.
# * **Custom User Model:** Customizing the IdentityUser class to store additional profile information.

## 🚀 How to Set Up and Run?

# This project requires the **.NET SDK** and an **SQL Server** database to run.

# 1. Clone the Repository:
git clone https://github.com/abdullahhaktan/MyAcademyIdentity
cd MyAcademyIdentity

# 2. Prepare the Database:
# * Update the Connection String (in the appsettings.json file) to match your local SQL Server settings.
# * Apply Entity Framework Core migrations to create the database schema and automatically create the database:
dotnet ef database update

# * Note: This command will automatically create the database on the specified server if it doesn't already exist. You do not need to manually create the database beforehand.

# 3. Start the Solution:
# * Open the .sln (Solution) file with Visual Studio or VS Code.
# * Build the project and start the application using the F5 key (Visual Studio) or the dotnet run command.

---
---
![1757943473621](https://github.com/user-attachments/assets/f44014d5-deee-46dd-94ce-fc61771936e1)

![1757943475164](https://github.com/user-attachments/assets/9e3adbda-bd34-4550-933d-84fde9d14e3e)

![1757943478196](https://github.com/user-attachments/assets/03300c49-2970-4652-b166-d020ed151e75)

![1757943476971](https://github.com/user-attachments/assets/524dc23e-7939-4515-ade7-219eb4233e99)

![1757943478044](https://github.com/user-attachments/assets/336da4e0-bfb4-43dc-a096-20ec3d7dadaa)
