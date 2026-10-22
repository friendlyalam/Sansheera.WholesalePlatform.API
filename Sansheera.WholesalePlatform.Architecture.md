# Sansheera Wholesale Platform — Architecture & Project Structure

**Parent Brand:** SANSHEERA  
**Solution:** `Sansheera.WholesalePlatform`  
**Architecture:** Modular Monolith + Feature-Based / Vertical Slice + Clean Architecture principles  
**Backend:** ASP.NET Core Web API, C# / .NET  
**Database:** PostgreSQL  
**ORM:** Entity Framework Core  
**Read Queries:** Dapper where useful  
**Application Patterns:** CQRS + MediatR where appropriate  
**Database Management:** DBA-managed SQL scripts; no EF Core migrations

---

# 1. Project Vision

Sansheera Wholesale Platform ek shared backend platform hoga jise multiple applications use karengi.

Initial aur future applications:

1. Wholesale Ordering App — retailers ke liye.
2. Delivery App — delivery employees ke liye.
3. Admin & Operations Dashboard — internal staff aur administrators ke liye.
4. Future Warehouse Management — stock aur warehouse operations ke liye.
5. Future Reporting & Business Intelligence — business reports aur analytics ke liye.

Har application ka UI alag ho sakta hai, lekin business rules aur backend capabilities shared rahengi.

**Important:** Multiple applications hone ka matlab multiple backend projects ya microservices banana nahi hai. Shuruaat mein ek modular backend aur ek PostgreSQL database sufficient hai.

---

# 2. Complete Solution Structure

```text
Sansheera.WholesalePlatform/
│
├── Sansheera.WholesalePlatform.sln
├── Sansheera.WholesalePlatform.Architecture.md
├── README.md
├── .gitignore
│
├── src/
│   │
│   ├── Sansheera.WholesalePlatform.Api/
│   │   ├── Controllers/
│   │   │   ├── IdentityController.cs
│   │   │   ├── CustomersController.cs
│   │   │   ├── ProductsController.cs
│   │   │   └── OrdersController.cs
│   │   │
│   │   ├── Contracts/
│   │   │   ├── Identity/
│   │   │   ├── Customers/
│   │   │   ├── Products/
│   │   │   └── Orders/
│   │   │
│   │   ├── Middleware/
│   │   ├── Filters/
│   │   ├── Configuration/
│   │   ├── Extensions/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── Sansheera.WholesalePlatform.Application/
│   │   ├── Features/
│   │   │   ├── Identity/
│   │   │   ├── Customers/
│   │   │   ├── Employees/
│   │   │   ├── Catalog/
│   │   │   ├── Inventory/
│   │   │   ├── Cart/
│   │   │   ├── Orders/
│   │   │   ├── Payments/
│   │   │   ├── Delivery/
│   │   │   └── Admin/
│   │   │
│   │   ├── Abstractions/
│   │   │   ├── Persistence/
│   │   │   ├── Messaging/
│   │   │   ├── Notifications/
│   │   │   └── Time/
│   │   │
│   │   ├── Behaviors/
│   │   ├── Exceptions/
│   │   └── DependencyInjection.cs
│   │
│   ├── Sansheera.WholesalePlatform.Domain/
│   │   ├── Customers/
│   │   ├── Employees/
│   │   ├── Products/
│   │   ├── Inventory/
│   │   ├── Orders/
│   │   ├── Payments/
│   │   ├── Delivery/
│   │   ├── Common/
│   │   └── Exceptions/
│   │
│   └── Sansheera.WholesalePlatform.Infrastructure/
│       ├── Persistence/
│       │   ├── AppDbContext.cs
│       │   ├── Configurations/
│       │   └── Interceptors/
│       │
│       ├── Features/
│       │   ├── Identity/
│       │   ├── Customers/
│       │   ├── Catalog/
│       │   ├── Orders/
│       │   ├── Payments/
│       │   └── Delivery/
│       │
│       ├── Database/
│       │   └── Scripts/
│       │       ├── 01_Schema/
│       │       ├── 02_MasterData/
│       │       ├── 03_Programmability/
│       │       └── 04_DataCorrections/
│       │
│       ├── Identity/
│       ├── Messaging/
│       ├── Notifications/
│       ├── ExternalServices/
│       └── DependencyInjection.cs
│
├── tests/
│   ├── Sansheera.WholesalePlatform.UnitTests/
│   ├── Sansheera.WholesalePlatform.IntegrationTests/
│   └── Sansheera.WholesalePlatform.ArchitectureTests/
│
└── docs/
    ├── Architecture/
    ├── Database/
    ├── Api/
    └── Decisions/
```

**Note:** Yeh target structure hai, mandatory requirement nahi ki saare folders abhi create kar do. Folder tab banao jab usmein actual code rakhna ho. Agar existing project structure alag hai, toh bina zarurat project recreate ya rename mat karna.

---

# 3. Each Project ki Responsibility

## 3.1 `Sansheera.WholesalePlatform.Api`

**Purpose:** HTTP/API entry point.

Is project ka kaam client applications se requests receive karna aur application use cases invoke karna hai.

### Yahan kya rahega?

- Controllers
- API request/response contracts
- HTTP status codes
- Request binding
- API-level validation/configuration
- Authentication and authorization middleware setup
- Exception handling middleware
- Swagger/OpenAPI configuration
- API versioning, agar required ho

### Yahan kya nahi rahega?

- Database queries
- `DbContext` se direct business operations
- Customer/order business rules
- Repository implementations
- Core use-case logic

### Controller ki responsibility

Controller:

1. HTTP request receive karega.
2. Request ko bind/validate karega.
3. Request DTO ko Application command/query mein map karega.
4. MediatR ke through use case invoke karega.
5. Application result ko HTTP response mein convert karega.

Controller ko business logic ka owner nahi banana.

---

## 3.2 `Sansheera.WholesalePlatform.Application`

**Purpose:** Application use cases aur orchestration.

Application layer batati hai ki system ka koi kaam kaise execute hoga, jaise:

- Create Customer
- Update Customer
- Create Order
- Cancel Order
- Confirm Payment
- Assign Delivery

### Yahan kya rahega?

- Commands
- Queries
- Command/query handlers
- FluentValidation validators
- Application results
- Application-specific interfaces
- Pipeline behaviors
- Use-case orchestration
- Authorization/use-case policies, where appropriate

### Yahan kya nahi rahega?

- ASP.NET Core controllers
- PostgreSQL-specific SQL implementation
- EF Core repository implementations
- HTTP request/response handling
- External provider ki concrete implementation

### Feature folders

Har feature ke andar uske use cases organize honge.

Example:

```text
Application/
└── Features/
    └── Customers/
        ├── Abstractions/
        │   └── ICustomerRepository.cs
        │
        ├── CreateCustomer/
        │   ├── CreateCustomerCommand.cs
        │   ├── CreateCustomerCommandHandler.cs
        │   └── CreateCustomerCommandValidator.cs
        │
        ├── UpdateCustomer/
        ├── DeactivateCustomer/
        └── GetCustomerById/
```

Har folder mein sirf wahi files rakho jo actual use case ke liye required hain.

---

## 3.3 `Sansheera.WholesalePlatform.Domain`

**Purpose:** Core business model aur domain rules.

Domain layer business concepts ko represent karegi, infrastructure technologies ko nahi.

### Yahan kya rahega?

- Entities
- Value objects, when useful
- Domain enums
- Domain-specific exceptions
- Business invariants
- Domain events, when justified
- Domain services, when a rule cannot naturally belong to one entity

Examples:

```text
Domain/
├── Customers/
│   └── Customer.cs
├── Products/
│   └── Product.cs
├── Orders/
│   ├── Order.cs
│   └── OrderItem.cs
└── Payments/
    └── Payment.cs
```

### Entity vs DTO

**Entity:** Business/domain object, jaise `Customer`.

**DTO:** Data transfer contract, jaise `CreateCustomerRequest`.

Entity ko directly API response ke taur par expose mat karo.

### Validation rule

FluentValidation incoming command/request ko validate karega.

Domain entities mein sirf meaningful business invariants rakho jo har entry point par enforce hone chahiye. Har DTO validation ko entity mein duplicate mat karo.

### Constants aur enums

- Domain-specific status enums, jaise `OrderStatus`, Domain mein.
- Application-only policy constants, agar genuinely needed hon, Application mein.
- HTTP status codes API layer mein.
- SQL object names Infrastructure mein.
- Har string ke liye global constants class mat banao.

---

## 3.4 `Sansheera.WholesalePlatform.Infrastructure`

**Purpose:** Technical implementations aur external systems se integration.

Yeh layer Application ke interfaces implement karti hai aur concrete technologies use karti hai.

### Yahan kya rahega?

- EF Core `DbContext`
- Entity configurations
- Repository implementations
- Dapper query implementations
- PostgreSQL connection configuration
- External payment gateway clients
- Email/SMS/notification providers
- Message broker integration
- Database SQL scripts
- Technical persistence implementations

Example:

```text
Infrastructure/
├── Persistence/
│   ├── AppDbContext.cs
│   └── Configurations/
│       └── CustomerConfiguration.cs
│
├── Features/
│   └── Customers/
│       └── CustomerRepository.cs
│
└── Database/
    └── Scripts/
```

Application batayegi ki use case ko repository ya service chahiye. Infrastructure batayegi ki woh PostgreSQL, EF Core ya kisi external provider ke through kaise implement hoga.

---

# 4. Dependency Rules — Sabse Important

Allowed dependency direction:

```text
API ───────────────► Application
 │                       ▲
 │                       │
 └────► Infrastructure ─┘
               │
               ▼
             Domain

Application ────────────► Domain
Infrastructure ─────────► Domain
```

Is diagram ko simplified dependency rule ke taur par samjho:

- Domain kisi doosre project par depend nahi karega.
- Application, Domain ko reference kar sakta hai.
- Infrastructure, Application ke abstractions aur Domain ko reference kar sakta hai.
- API, Application aur Infrastructure ko reference kar sakta hai.
- Domain mein `DbContext`, controller ya MediatR handler mat rakho.
- Application mein PostgreSQL implementation mat rakho.
- Circular references avoid karo.

Infrastructure ko API startup par register karna theek hai. Isse Domain ya Application ko Infrastructure ka concrete code import karne ki zarurat nahi hoti.

---

# 5. CQRS + MediatR — Kab aur Kaise?

CQRS ka matlab read aur write use cases ko alag represent karna hai.

## Command

Data create/update/delete karne wala intent.

Examples:

- `CreateCustomerCommand`
- `UpdateCustomerCommand`
- `CancelOrderCommand`

## Query

Data retrieve karne wala intent.

Examples:

- `GetCustomerByIdQuery`
- `SearchCustomersQuery`
- `GetOrderDetailsQuery`

Typical flow:

```text
HTTP Request
    ↓
Controller
    ↓
Request DTO → Command / Query
    ↓
MediatR
    ↓
Validation Pipeline
    ↓
Handler
    ↓
Application Abstractions
    ↓
Infrastructure Implementation
    ↓
PostgreSQL
```

MediatR request dispatch karta hai. Business logic handler/use case mein hoti hai.

CQRS ka matlab automatically separate databases ya microservices nahi hai. Hum initially ek application aur ek database use karenge.

Har chhote method ko command/query banana compulsory nahi. CQRS ko use cases ke liye use karo jahan separation meaningful ho.

---

# 6. API Contracts vs Application Contracts

Multiple clients hone ke karan API contract ko Application command se separate rakhna preferred hai.

Example:

```text
Api/
└── Contracts/
    └── Customers/
        ├── CreateCustomerRequest.cs
        └── CustomerResponse.cs

Application/
└── Features/
    └── Customers/
        └── CreateCustomer/
            ├── CreateCustomerCommand.cs
            ├── CreateCustomerCommandHandler.cs
            ├── CreateCustomerCommandValidator.cs
            └── CreateCustomerResult.cs
```

Responsibilities:

- `CreateCustomerRequest`: client se aane wale HTTP fields.
- `CreateCustomerCommand`: application use case ko required data.
- `CreateCustomerResult`: use case ka result.
- `CustomerResponse`: public API response.

Controller request ko command mein map karega aur result ko API response mein convert karega.

Isse future mein API version ya client contract change hone par Application ko unnecessary changes nahi karne padenge.

Small projects mein DTOs ko Application mein rakhna bhi acceptable hai; lekin humare multi-client platform ke liye API contracts ko API project mein rakhna clearer boundary hai.

---

# 7. 50+ Interfaces Ko Kaise Manage Karna Hai?

Interfaces ko sirf ek global folder mein jama mat karo.

## Feature-specific interfaces

```text
Application/
└── Features/
    ├── Customers/
    │   └── Abstractions/
    │       └── ICustomerRepository.cs
    ├── Orders/
    │   └── Abstractions/
    │       └── IOrderRepository.cs
    └── Catalog/
        └── Abstractions/
            └── IProductRepository.cs
```

## Cross-feature abstractions

```text
Application/
└── Abstractions/
    ├── Persistence/
    ├── Messaging/
    ├── Notifications/
    └── Time/
```

Examples:

- `ICustomerRepository` — customer feature.
- `IOrderRepository` — order feature.
- `IUnitOfWork` — shared persistence contract, only if useful.
- `INotificationSender` — shared notification capability, if multiple features need it.

**Rule:** Interface tab banao jab abstraction ka meaningful purpose ho, na ki har class ke liye mechanically.

---

# 8. Repository, EF Core, Dapper aur Unit of Work

## EF Core

Use when:

- Entity create/update karni ho.
- Domain entities ke saath kaam karna ho.
- Change tracking useful ho.
- Straightforward transactional writes karne hon.

## Dapper

Use when:

- Complex read queries hon.
- Reports/aggregations hon.
- SQL control aur measured performance benefit important ho.
- Query ko explicit SQL mein maintain karna better ho.

Dapper aur EF Core ek hi PostgreSQL database use kar sakte hain.

## Repository

Repository ka interface Application mein aur implementation Infrastructure mein hogi.

Repository ko sirf `DbContext` ka duplicate wrapper mat banao. Use meaningful persistence boundary aur domain/application needs ke according design karo.

## Unit of Work

EF Core `DbContext` already Unit of Work jaisa behavior provide karta hai.

Agar project mein `IUnitOfWork` abstraction use karte hain, toh `AppDbContext` usse implement kar sakta hai. Same request mein repository aur Unit of Work ko same scoped context milna chahiye.

Har project mein separate custom transaction framework banana zaroori nahi.

---

# 9. Database and DBA Scripts

Location:

```text
Infrastructure/
└── Database/
    └── Scripts/
        ├── 01_Schema/
        ├── 02_MasterData/
        ├── 03_Programmability/
        └── 04_DataCorrections/
```

## 01_Schema

- Tables
- Columns
- Primary/foreign keys
- Indexes
- Constraints

## 02_MasterData

- Order statuses
- Units of measurement
- Payment methods
- Roles and permissions, according to the identity design

## 03_Programmability

- Stored procedures
- Database functions
- Triggers, only when justified

## 04_DataCorrections

- Controlled data correction scripts
- Approved data repair operations

Database scripts Git mein version-control hongi aur DBA/deployment process se execute hongi.

Application startup par automatically DBA scripts execute nahi karni.

EF Core entity configurations ko existing database schema ke saath match rakhna hai. Migrations mandatory nahi hain.

---

# 10. Identity, Customers and Employees

In teen concepts ko separate samjho.

## Identity

- Login
- Password management
- Token/session management
- Authentication
- Roles and permissions

## Customers

- Retailer business profile
- Business contact information
- Addresses
- Customer-specific business settings
- Customer status

## Employees

- Internal employee profile
- Employee-specific assignments
- Delivery/warehouse/operations responsibilities

Authentication account aur customer business profile alag concepts hain. Unka relationship future identity design ke according establish hoga.

Ek shared authentication system multiple user types ko support kar sakta hai. Har application ke liye separate login system banana mandatory nahi.

---

# 11. Authentication and Authorization

Authentication configuration:

- API middleware/pipeline mein.
- JWT bearer/cookie setup: API configuration.
- Account and token operations: Identity use cases.
- Token/provider-specific implementation: Infrastructure, where applicable.
- Roles/permissions ka business model: chosen identity/domain design ke according.

Authorization ko sirf UI buttons hide karne tak limit mat karna. Har protected backend endpoint par appropriate authorization enforce honi chahiye.

Examples:

- Retailer sirf apne permitted customer data ko access kare.
- Delivery employee sirf assigned delivery operations perform kare.
- Admin ko sirf assigned permissions ke according access mile.

---

# 12. Validation and Error Handling

## Validation

- API model binding/HTTP validation: API.
- Command/query validation: FluentValidation.
- Business invariants: Domain, where required.
- Database uniqueness/integrity: PostgreSQL constraints.

## Error handling

- Global exception handling: API middleware.
- Application-specific exceptions/results: Application.
- Database-specific exceptions ka translation: Infrastructure/Application boundary, as appropriate.
- Unexpected error details client ko expose nahi karne.
- Logs mein secrets, passwords aur sensitive tokens nahi likhne.

Validation errors generally HTTP 400 ho sakte hain. Successful resource creation ke liye HTTP 201 common choice hai. Final status codes API contract aur team conventions ke according define karne hain.

---

# 13. Dependency Injection

## Application DI

Responsibilities:

- MediatR handlers register karna.
- FluentValidation validators register karna.
- Pipeline behaviors register karna.

## Infrastructure DI

Responsibilities:

- PostgreSQL connection configuration.
- `AppDbContext` registration.
- Repository interface-to-implementation mappings.
- Dapper/data source registration, if needed.
- External service implementations.

## API startup

`Program.cs` sab modules ko compose karega:

```csharp
builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(
    builder.Configuration);
```

Connection string configuration se aayegi. Production secrets source control mein commit nahi karne.

---

# 14. Testing Strategy

## Unit Tests

Location:

`tests/Sansheera.WholesalePlatform.UnitTests/`

Test:

- Domain invariants.
- Validators.
- Handler behavior.
- Business rules.
- Failure scenarios.

## Integration Tests

Location:

`tests/Sansheera.WholesalePlatform.IntegrationTests/`

Test:

- API endpoints.
- PostgreSQL persistence.
- Database constraints.
- Repository behavior.
- End-to-end request/response flow.

PostgreSQL-specific behavior ke liye actual PostgreSQL test environment prefer karo; in-memory provider real database ka complete substitute nahi hai.

## Architecture Tests

Location:

`tests/Sansheera.WholesalePlatform.ArchitectureTests/`

Test:

- Domain layer ki dependency restrictions.
- Circular dependencies.
- Architecture conventions.
- Project boundaries.

---

# 15. Future Applications Kaise Add Hongi?

Initially shared backend:

```text
Sansheera.WholesalePlatform.Api
```

Future clients:

```text
Clients/
├── WholesaleOrderingApp/
├── DeliveryApp/
└── AdminDashboard/
```

Ye clients backend solution ke andar ya separate repositories mein ho sakte hain. Unka UI backend Application aur Domain projects mein duplicate nahi karna.

Sab clients authorized API endpoints use karenge.

Future mein delivery ya warehouse ko independently scale/deploy karne ki actual need ho, tab module boundaries evaluate karke service extraction consider kar sakte hain. Abhi microservices create nahi karni.

---

# 16. Naming Conventions

- Projects: `Sansheera.WholesalePlatform.*`
- Classes: PascalCase
- Interfaces: `I` prefix, e.g. `ICustomerRepository`
- Commands: `CreateCustomerCommand`
- Queries: `GetCustomerByIdQuery`
- Handlers: `CreateCustomerCommandHandler`
- Validators: `CreateCustomerCommandValidator`
- Entities: `Customer`
- API request DTOs: `CreateCustomerRequest`
- API response DTOs: `CustomerResponse`
- Repository implementations: `CustomerRepository`
- Entity configuration: `CustomerConfiguration`
- SQL scripts: ordered, descriptive, version-controlled filenames

Naming consistent rakho; har folder mein extra `Models`, `Services`, `Helpers` folders sirf convention ke liye create mat karo.

---

# 17. Architecture Rules — Final Checklist

1. Modular Monolith se start karna.
2. Feature-based organization use karna.
3. Domain ko independent rakhna.
4. Application mein use-case logic rakhna.
5. API ko thin rakhna.
6. Infrastructure mein concrete database/external implementations rakhna.
7. CQRS + MediatR ko meaningful use cases par apply karna.
8. FluentValidation pipeline consistently register karna.
9. API DTOs aur Application commands ko separate rakhna where useful.
10. Har class ke liye interface mat banana.
11. EF Core aur Dapper ko use-case ke according select karna.
12. DBA-managed SQL scripts ko application startup par execute mat karna.
13. Database constraints aur transaction boundaries design karna.
14. Authentication aur authorization backend par enforce karna.
15. Unit, integration aur architecture tests maintain karna.
16. Production secrets source control mein store mat karna.
17. Future requirements aane par architecture evolve karna; abhi unnecessary abstractions mat banana.

---

# 18. Feature Implementation Checklist

Har new feature ke liye:

1. Requirement aur business rules define karo.
2. Decide karo ki kaunse Domain objects change honge.
3. Application command/query define karo.
4. Request validation add karo.
5. Handler implement karo.
6. Required interfaces define karo.
7. Infrastructure implementation add karo.
8. Database script/configuration update karo, if required.
9. API contract aur controller endpoint add karo.
10. DI registrations verify karo.
11. Unit/integration tests add karo.
12. Error handling, authorization aur logging verify karo.
13. API response/status codes verify karo.
14. DBA/deployment process ke through database changes release karo.

**Final principle:** Architecture ka goal folders badhana nahi hai. Goal hai clear ownership, testable business behavior, controlled dependencies aur future changes ko safely accommodate karna.

Is document ko project ka master architecture reference rakho. Agar future mein architecture decision change hota hai, toh document aur code ko saath update karna.
