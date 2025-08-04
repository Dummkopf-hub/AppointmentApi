AppointmentSystemAPI is a RESTful Web API built with ASP.NET Core that allows users to schedule appointments with experts, manage profiles, and handle role-based actions. The system supports user and expert roles, JWT authentication, SQLite database, and logging.

---

<h2>Technologies:</h2>

- ASP.NET Core 8
- Entity Framework Core (SQLite)
- JWT (JSON Web Tokens)
- AutoMapper
- Model Validation
- Swagger (Swashbuckle)
- Logging (ILogger)
- Unit Testing (MSTest / xUnit — planned)

---

<h2>User Roles:</h2>

| Role     | Permissions                                                         |
|----------|----------------------------------------------------------------------|
| **User**   | View experts, create/delete appointments, view own profile            |
| **Expert** | View own appointments, update profile, manage appointments            |

---

<h2>Project Structure:</h2>

<p>AppointmentSystemAPI/</p>
<p>│</p>
<p>├── Controllers/</p>
<p>│ ├── AuthorizationController.cs</p>
<p>│ ├── UserController.cs</p>
<p>│ └── AppointmentController.cs</p>
<p>│</p>
<p>├── Controllers/</p>
<p>├── Migrations/</p>
<p>├── Models/</p>
<p>├── Services/</p>
<p>├── Data/</p>
<p>│</p>
<p>├── appsettings.json</p>
<p>├── Program.cs</p>
<p>└── README.md</p>
---

<h1>MAIN FEATURES

<h1>Authentication & Authorization:

- Registration with password hashing
- Login with JWT token generation
- Role-based authorization using

<h1>User Management:

- Get own profile
- Update profile 
- View all experts
- View expert by ID

<h1>Appointments:

- Create appointment (User)
- View own appointments (User/Expert)
- Delete appointment (User/Expert)
- Update appointment (Expert)

---

<h1>SETUP</h1>

<h2>1. Clone the repository:</h2>
   
   git clone https://github.com/Dummkopf-hub/AppointmentApi.git
   cd AppointmentApi

<h2>2. Restore depemdencies:</h2>

   dotnet restore
   
<h2>3. Apply migrations and create the database:</h2>

   dotnet ef database update

<h2>4. Run the API:</h2>

   dotnet run

<h2>5. Open Swagger UI in your browser:</h2>

    https://localhost:<port>/swagger

---

Tests (Work in Progress)
Planned:

Unit tests for services

Integration tests for controllers
 
