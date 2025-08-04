AppointmentSystemAPI is a RESTful Web API built with ASP.NET Core that allows users to schedule appointments with experts, manage profiles, and handle role-based actions. The system supports user and expert roles, JWT authentication, SQLite database, and logging.

---

Technologies:

- ASP.NET Core 8
- Entity Framework Core (SQLite)
- JWT (JSON Web Tokens)
- AutoMapper
- Model Validation
- Swagger (Swashbuckle)
- Logging (ILogger)
- Unit Testing (MSTest / xUnit — planned)

---

User Roles:

| Role     | Permissions                                                         |
|----------|----------------------------------------------------------------------|
| **User**   | View experts, create/delete appointments, view own profile            |
| **Expert** | View own appointments, update profile, manage appointments            |

---

Project Structure:

AppointmentSystemAPI/
│
├── Controllers/
│ ├── AuthorizationController.cs
│ ├── UserController.cs
│ └── AppointmentController.cs
│
├── Models/
├── Services/
├── Migrations/
├── Controllers/
├── Data/
│ └── AppDbContext.cs
│
├── appsettings.json
├── Program.cs
└── README.md
---

MAIN FEATURES

Authentication & Authorization:

- Registration with password hashing
- Login with JWT token generation
- Role-based authorization using

User Management:

- Get own profile
- Update profile 
- View all experts
- View expert by ID

Appointments:

- Create appointment (User)
- View own appointments (User/Expert)
- Delete appointment (User/Expert)
- Update appointment (Expert)

---

SETUP

1. Clone the repository:
   
   git clone https://github.com/Dummkopf-hub/AppointmentApi.git
   cd AppointmentApi

2. Restore depemdencies:

   dotnet restore
   
3. Apply migrations and create the database:

   dotnet ef database update

4. Run the API:

   dotnet run

5. Open Swagger UI in your browser:

    https://localhost:<port>/swagger

---

Tests (Work in Progress)
Planned:

Unit tests for services

Integration tests for controllers
 
