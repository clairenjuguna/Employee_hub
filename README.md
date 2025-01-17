# Employee Hub

Employee Hub is a web-based employee management system built with ASP.NET Core 6.0. It provides functionality for managing employees, including user authentication, role-based access control, and communication features.

## Features

- **User Authentication & Authorization**
  - User registration with email verification
  - Role-based access control (Admin, Manager, Employee)
  - Secure login system
  - Password reset functionality

- **Employee Management**
  - Employee profiles with personal and professional information
  - Department and position management
  - Salary tracking
  - Profile picture support

## Technologies Used

- ASP.NET Core 6.0
- Entity Framework Core
- Microsoft SQL Server
- ASP.NET Core Identity
- Bootstrap 5
- C# 10

## Prerequisites

- .NET 6.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

## Getting Started

1. Clone the repository:
```bash
git clone https://github.com/clairenjuguna/Employee_hub.git
```

2. Navigate to the project directory:
```bash
cd Employee_hub_new
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Update the database:
```bash
dotnet ef database update
```

5. Run the application:
```bash
dotnet run
```

## Configuration

1. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeHub;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

2. Configure email settings in `appsettings.json`:
```json
{
  "MailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Email": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

## Project Structure

```
Employee_hub_new/
├── Controllers/          # MVC Controllers
├── Models/              # Domain models and ViewModels
├── Views/               # Razor views
├── Services/            # Business logic and services
├── Data/               # Database context and migrations
└── wwwroot/            # Static files (CSS, JS, images)
```

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

Claire Njuguna - [@clairenjuguna](https://github.com/clairenjuguna)

Project Link: [https://github.com/clairenjuguna/Employee_hub](https://github.com/clairenjuguna/Employee_hub) 