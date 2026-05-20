# Cirkus Luna - System Specification

**Project Type:** 1st Semester Computer Science Assignment  
**Institution:** UCL Erhvervsakademi og Professionshøjskole  
**Due Date:** May 28, 2026  
**Group Size:** 3-4 students  
**Version:** 1.0

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Assignment Context](#assignment-context)
3. [Case Description](#case-description)
4. [System Architecture](#system-architecture)
5. [Functional Requirements](#functional-requirements)
6. [Non-Functional Requirements](#non-functional-requirements)
7. [Domain Model](#domain-model)
8. [Technology Stack](#technology-stack)
9. [Implementation Priorities](#implementation-priorities)
10. [User Interface Guidelines](#user-interface-guidelines)
11. [Deliverables](#deliverables)

---

## 1. Project Overview

### Purpose

Develop a web-based reservation and management system for Cirkus Luna, a traveling circus that tours Danish cities during the spring and summer season (May 1 - August 31).

The system enables:
- **Customers:** View tour schedule, search performances, reserve tickets
- **Employees:** Manage tour plan, performances, artists, customers, and reservations

### Critical Constraint

This is a **1st semester exam foundation project**. The focus is on demonstrating fundamental C# programming skills, object-oriented design, and clean architecture - NOT on building a production-ready system.

**Exam Focus:** The Class Library and Console App are the primary deliverables. The Razor Pages web app is secondary.

---

## 2. Assignment Context

### Assignment Requirements (from opgavebeskrivelse.pdf)

#### 2.1 Technology Requirements

- **Language:** C#
- **IDE:** Visual Studio Enterprise
- **Framework:** ASP.NET Core Razor Pages
- **Version Control:** GitHub (public repository)

#### 2.2 Project Structure

**Three projects required:**

1. **CirkusLuna.Core** (Class Library)
   - Contains: Models, Repositories, Interfaces, Exceptions, Services
   - **This is the exam focus** - all UML diagrams should cover this

2. **CirkusLuna.ConsoleApp** (Console Application)
   - Must be able to test/demonstrate all core functionality
   - **Critical for exam:** Exam does NOT focus on Razor Pages, so Console App must prove the system works

3. **CirkusLuna.Web** (ASP.NET Core Razor Pages)
   - Final deliverable for customers/employees
   - Secondary importance for exam

#### 2.3 Architecture Requirements

**Three-layer architecture:**

1. **UI Layer:** Razor Pages + Console App
2. **Service Layer:** Business logic, validation, rules
3. **Repository Layer:** Data access, CRUD operations, search/filtering

**Critical Rule:** Business logic must NOT be in Razor Pages. Razor Pages should only call services and display results.

#### 2.4 Required Algorithms

**Self-written algorithms required:**

1. **Search/Filtering Algorithm** (in Repository layer)
   - Must implement manual search/filtering (not just LINQ)
   - Example: Search performances by city using a loop

2. **Alphabetical City Sorting Algorithm**
   - Must implement a self-written sorting algorithm (bubble sort, selection sort, or insertion sort)
   - **Explicitly required:** "Sortere listen over byer alfabetisk ved brug af en selvimplementeret sorteringsalgoritme"

#### 2.5 Documentation Requirements

**SCRUM Methodology:**
- Product Backlog with User Stories
- User Stories must include Acceptance Criteria
- Sprint Planning documents

**UML Diagrams (focus on Class Library only):**
- Domain Model
- Class Diagram (detailed design)
- Sequence Diagrams (for complex User Stories)

**Report:**
- Maximum 10 normal pages + front page, table of contents, and appendices
- Each group member must be responsible for specific parts
- Must clearly indicate who implemented which parts of the system

#### 2.6 Code Quality

- Code must be commented
- Every group member must understand all parts they implemented
- **Important:** "En simpel implementering kan virke som et lige så godt grundlag for eksamen, som en meget avanceret løsning"
  - Translation: A simple implementation can work just as well as an advanced solution for the exam

#### 2.7 Exam Context

- **At the exam:** Students will be asked to extend the developed system
- **Critical:** The system must run on the student's own PC (local development environment)
- **Not all features need to be fully implemented** - focus on structure, design, and correct use of techniques

---

## 3. Case Description

### 3.1 Business Context

**Cirkus Luna** is a traveling circus that tours Denmark annually during the spring and summer season.

**Tour Season:** May 1 - August 31

**Tour Model:**
- Visits multiple Danish cities
- Sets up a tent in each city
- Holds performances on specific dates
- Some cities have multiple performances over several consecutive days

**Challenge:**
- Tour plan is adjusted continuously during the season
- Need easy updates to cities, dates, and performances
- Public must always have access to the latest information

### 3.2 Artists and Performances

**Artist Types:**
1. **Permanent Artists:** Part of the circus for the entire season
2. **Guest Artists:** Only participate in specific performances in certain cities

**Performance Variability:**
- Performances vary from city to city
- Depends on which artists participate
- Depends on which acts are performed

### 3.3 Ticket Types

1. **Regular Tickets:** Standard admission
2. **Children's Tickets:** Reduced price for children
3. **VIP Tickets:**
   - Better seats
   - Access to an extra experience before the performance
   - Limited availability

### 3.4 Capacity Constraints

**Per Performance:**
- **Maximum 150 total seats**
- **Maximum 10 VIP seats**

These limits must be enforced by the system.

### 3.5 Seating Chart

The circus wants to display a seating chart showing:
- Which seats are VIP
- Which seats are available
- Which seats are reserved

This helps customers during the reservation process.

### 3.6 Problem Statement

**Current Situation:**
- Outdated and difficult-to-maintain website

**Desired Solution:**
- New web application that handles:
  - Tour planning
  - Performance presentation
  - Customer and reservation administration

---

## 4. System Architecture

### 4.1 Visual Studio Solution Structure

```
CirkusLuna.sln
│
├── CirkusLuna.Core/                    (Class Library - .NET 8.0)
│   │
│   ├── Models/
│   │   ├── Person.cs                  (Base class)
│   │   ├── Kunde.cs                   (Customer)
│   │   ├── Medarbejder.cs             (Employee)
│   │   ├── Artist.cs                  (Performer)
│   │   ├── Forestilling.cs            (Performance)
│   │   ├── Lokation.cs                (Venue/Location)
│   │   ├── Reservation.cs             (Booking)
│   │   ├── Plads.cs                   (Seat)
│   │   ├── Billettype.cs              (Ticket Type - enum)
│   │   └── Nyhed.cs                   (News/Blog Post)
│   │
│   ├── Interfaces/
│   │   ├── IRepository.cs             (Generic repository interface)
│   │   ├── IForestillingRepository.cs
│   │   ├── ILokationRepository.cs
│   │   ├── IReservationRepository.cs
│   │   ├── IPersonRepository.cs
│   │   ├── INyhedRepository.cs
│   │   ├── IForestillingService.cs
│   │   └── IReservationService.cs
│   │
│   ├── Repositories/
│   │   ├── InMemoryForestillingRepository.cs
│   │   ├── InMemoryLokationRepository.cs
│   │   ├── InMemoryReservationRepository.cs
│   │   ├── InMemoryPersonRepository.cs
│   │   └── InMemoryNyhedRepository.cs
│   │
│   ├── Services/
│   │   ├── ForestillingService.cs     (Performance business logic)
│   │   ├── ReservationService.cs      (Reservation business logic)
│   │   ├── CapacityService.cs         (Capacity validation)
│   │   └── SorteringService.cs        (Self-written sorting algorithms)
│   │
│   ├── Exceptions/
│   │   ├── ReservationFullException.cs
│   │   ├── VIPCapacityExceededException.cs
│   │   ├── PastPerformanceException.cs
│   │   └── InvalidReservationException.cs
│   │
│   └── Validation/
│       └── ReservationValidator.cs
│
├── CirkusLuna.ConsoleApp/              (Console Application - .NET 8.0)
│   ├── Program.cs
│   └── MenuSystem.cs                   (Helper for menu-driven interface)
│
└── CirkusLuna.Web/                     (ASP.NET Core Web App - .NET 8.0)
    ├── Pages/
    │   ├── Index.cshtml + .cs          (Homepage)
    │   ├── Shared/
    │   │   ├── _Layout.cshtml          (Master layout)
    │   │   └── _ValidationScriptsPartial.cshtml
    │   ├── Forestillinger/
    │   │   ├── Index.cshtml            (List performances)
    │   │   └── Details.cshtml          (Performance details)
    │   ├── Turneplan/
    │   │   └── Index.cshtml            (Tour schedule sorted by city)
    │   ├── Reservationer/
    │   │   ├── Create.cshtml           (New reservation)
    │   │   └── Bekraeftelse.cshtml     (Confirmation)
    │   ├── Pladsoversigt/
    │   │   └── Index.cshtml            (Seating chart)
    │   └── Admin/
    │       ├── Forestillinger/         (Admin: Manage performances)
    │       ├── Artister/               (Admin: Manage artists)
    │       └── Kunder/                 (Admin: Manage customers)
    ├── wwwroot/
    │   ├── css/
    │   │   ├── site.css                (Custom CSS)
    │   │   └── bootstrap.min.css       (Bootstrap 5)
    │   ├── js/
    │   │   ├── site.js                 (Minimal custom JS)
    │   │   └── bootstrap.bundle.min.js
    │   └── images/
    │       └── logo.png
    ├── Program.cs                      (Startup configuration)
    └── appsettings.json                (Configuration)
```

### 4.2 Layer Responsibilities

#### UI Layer (Razor Pages + Console App)
**Responsibility:** User interaction, input/output, display logic only

**Razor Pages:**
- Display data
- Capture user input
- Call service layer methods
- Display results or errors

**Console App:**
- Menu-driven interface
- Demonstrate all core features
- Test repository algorithms
- Validate business rules

#### Service Layer
**Responsibility:** Business logic, orchestration, validation

**Services:**
- `ForestillingService`: Search, filter, get performances
- `ReservationService`: Create reservations, validate rules
- `CapacityService`: Check available seats, enforce limits
- `SorteringService`: Self-written sorting algorithms

**Business Rules:**
- Reservations only for future performances
- Capacity limits (150 total, 10 VIP)
- VIP seat availability
- Customer validation

#### Repository Layer
**Responsibility:** Data access, CRUD operations, self-written algorithms

**Repositories:**
- In-memory data storage (List<T>)
- Seed data for demonstration
- CRUD operations (Create, Read, Update, Delete)
- **Self-written search algorithm** (manual loop-based search)
- Data retrieval for services

**Data Strategy:** In-memory repositories with seed data (no database required for this assignment)

---

## 5. Functional Requirements

### 5.1 Person Management

**FR-1.1: Oprette og administrere kunder**
- Create new customers with: Name, Email, Phone, Address
- View list of all customers
- Edit customer information
- Delete customers (if no active reservations)

**FR-1.2: Administrere medarbejdere**
- Create employees with: Name, Email, Phone, Position
- View list of employees
- Edit employee information

**FR-1.3: Administrere artister**
- Create artists with: Name, Specialty, Bio
- View list of artists
- Edit artist information
- Link artists to performances (many-to-many relationship)

### 5.2 Performance Management

**FR-2.1: Oprette forestillinger**
- Create performance with: Title, Date, Time, Location, Capacity
- Assign multiple artists to a performance
- Set total capacity (max 150 seats)
- Set VIP capacity (max 10 VIP seats)

**FR-2.2: Administrere forestillinger**
- Edit performance details
- Update artist assignments
- View performance details with assigned artists

**FR-2.3: Tilknytte artister til forestillinger**
- Many-to-many relationship
- One performance can have multiple artists
- One artist can perform in multiple performances
- Some artists only participate in selected performances

### 5.3 Tour Schedule and Search

**FR-3.1: Se turnéplan**
- View all performances organized by city and date
- Display: City, Date, Time, Venue, Available seats

**FR-3.2: Søge efter forestillinger**
- **Search by city:** Find all performances in a specific city
- **Search by date:** Find all performances on or after a specific date
- **Combined search:** City AND date

**Implementation Note:** Must use self-written search algorithm (manual loop) in repository layer.

**FR-3.3: Filtrere forestillinger**
- Filter by city
- Filter by date range
- Filter by ticket type availability (Regular, VIP)
- Filter by availability (only performances with available seats)

**FR-3.4: Sortere byer alfabetisk** ⭐ **CRITICAL REQUIREMENT**
- Display list of cities sorted alphabetically
- **Must use self-implemented sorting algorithm** (bubble sort, selection sort, or insertion sort)
- **Do NOT use `.Sort()` or `.OrderBy()` as the only implementation**

Example:
```csharp
// Self-written bubble sort for cities
public List<string> GetCitiesSortedAlphabetically()
{
    List<string> cities = GetAllCities();
    
    // Bubble sort implementation
    for (int i = 0; i < cities.Count - 1; i++)
    {
        for (int j = 0; j < cities.Count - i - 1; j++)
        {
            if (string.Compare(cities[j], cities[j + 1], StringComparison.Ordinal) > 0)
            {
                string temp = cities[j];
                cities[j] = cities[j + 1];
                cities[j + 1] = temp;
            }
        }
    }
    
    return cities;
}
```

### 5.4 Reservation System

**FR-4.1: Reservere billetter**
- Customer selects a performance
- Specifies number of tickets
- Selects ticket type: Regular, Children, or VIP
- Provides customer information (or selects existing customer)
- System creates reservation if valid

**FR-4.2: Validering af reservationer**
- **Future performances only:** Cannot reserve tickets for past performances
- **Capacity check:** Ensure sufficient available seats
- **VIP capacity check:** Ensure sufficient VIP seats if VIP tickets requested
- **Minimum 1 ticket:** Reservation must have at least 1 ticket

**FR-4.3: Reservation model**
A reservation must include:
- Customer (Kunde)
- Performance (Forestilling)
- Number of tickets (AntalBilletter)
- Ticket type (Billettype)
- Reservation date (ReservationsDato)

### 5.5 Capacity Management

**FR-5.1: Håndtere kapacitet**
- **Maximum 150 seats per performance** (total capacity)
- **Maximum 10 VIP seats per performance**
- System must enforce these limits

**FR-5.2: Kontrollere ledige pladser**
- Calculate available seats: Total capacity - Reserved seats
- Calculate available VIP seats: VIP capacity - Reserved VIP seats
- Display available seats on performance list and details

**FR-5.3: Validering ved reservation**
- Reject reservation if insufficient total seats
- Reject VIP reservation if insufficient VIP seats
- Throw appropriate exceptions:
  - `ReservationFullException`: No seats available
  - `VIPCapacityExceededException`: No VIP seats available

### 5.6 Seating Chart Overview

**FR-6.1: Vise pladsoversigt**
- Display seating chart for a specific performance
- Show seat numbers (1-150)
- Indicate VIP seats (seats 1-10)
- Indicate seat status: Available or Reserved
- **Optional for MVP:** Color coding (green = available, red = reserved, gold = VIP)

**FR-6.2: Anvendelse ved reservation**
- Display seating chart when customer reserves tickets
- Help customer understand seat availability
- **Note:** Full seat selection (picking specific seat numbers) is optional

### 5.7 Ticket Type Management

**FR-7.1: Billettyper**

Three ticket types:
1. **Almindelig billet** (Regular ticket) - Standard admission
2. **Børnebillet** (Children's ticket) - For children
3. **VIP-billet** (VIP ticket) - Premium seats and experience

**Implementation:** Use an enum:
```csharp
public enum Billettype
{
    Almindelig,
    Boern,
    VIP
}
```

**Optional:** Add price information to ticket types (not required for MVP)

### 5.8 News/Blog Management

**FR-8.1: Oprette nyheder**
- Create news/blog posts about the circus
- Include: Title, Content, Publish Date, Author

**FR-8.2: Administrere nyheder**
- Edit existing news posts
- Delete news posts
- Display news on homepage or news page

**Priority:** This is a lower-priority feature. Focus on core reservation functionality first.

---

## 6. Non-Functional Requirements

### 6.1 Usability

**NFR-1: User-friendly and clear interface**
- Simple navigation
- Clear labels in Danish
- Intuitive forms with validation feedback
- Responsive design (Bootstrap)

### 6.2 Technology Constraints

**NFR-2: ASP.NET Core Razor Pages**
- Web application must use Razor Pages (NOT MVC, NOT API-first)
- .NET 8.0 (or latest LTS version in Visual Studio)

**NFR-3: Layered architecture**
- Clear separation: UI → Service → Repository
- Business logic in Service layer, NOT in Razor Pages
- Data access in Repository layer only

### 6.3 Code Quality

**NFR-4: Code comments**
- Comment business rules and validation logic
- Explain "why", not "what"
- Use XML documentation comments for public classes and methods

**NFR-5: Understandability**
- Every group member must understand their implemented parts
- Prefer simple, explainable code over clever optimizations
- Method length: < 30 lines where possible

### 6.4 Performance (Not Critical for Assignment)

**NFR-6: Acceptable performance**
- In-memory data storage is instant
- No loading spinners needed
- Focus on correctness, not optimization

### 6.5 Local Deployment

**NFR-7: Must run on student's PC**
- No cloud dependencies
- No database required (in-memory is sufficient)
- Easy to run in Visual Studio (F5)
- **Critical for exam:** System must be runnable locally

### 6.6 Version Control

**NFR-8: GitHub repository**
- All code must be in a public GitHub repository
- Include link in documentation
- Commit regularly with meaningful messages

---

## 7. Domain Model

### 7.1 Core Domain Entities

#### Person (Base Class)
```csharp
public abstract class Person
{
    public int Id { get; set; }
    public string Navn { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
}
```

#### Kunde (Customer) : Person
```csharp
public class Kunde : Person
{
    public string Adresse { get; set; }
    public List<Reservation> Reservationer { get; set; }
}
```

#### Medarbejder (Employee) : Person
```csharp
public class Medarbejder : Person
{
    public string Stilling { get; set; }
}
```

#### Artist (Performer) : Person
```csharp
public class Artist : Person
{
    public string Specialitet { get; set; }
    public string Bio { get; set; }
    public List<Forestilling> Forestillinger { get; set; } // Many-to-many
}
```

#### Lokation (Venue/Location)
```csharp
public class Lokation
{
    public int Id { get; set; }
    public string Navn { get; set; }
    public string By { get; set; }
    public string Adresse { get; set; }
}
```

#### Forestilling (Performance)
```csharp
public class Forestilling
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public DateTime Dato { get; set; }
    public TimeSpan Tidspunkt { get; set; }
    public int LokationId { get; set; }
    public Lokation Lokation { get; set; }
    public int TotalKapacitet { get; set; } = 150;
    public int VIPKapacitet { get; set; } = 10;
    public List<Artist> Artister { get; set; } // Many-to-many
    public List<Reservation> Reservationer { get; set; }
    
    // Calculated properties
    public int LedigePladser => TotalKapacitet - Reservationer.Sum(r => r.AntalBilletter);
    public int LedigeVIPPladser => VIPKapacitet - Reservationer
        .Where(r => r.Billettype == Billettype.VIP)
        .Sum(r => r.AntalBilletter);
}
```

#### Reservation (Booking)
```csharp
public class Reservation
{
    public int Id { get; set; }
    public int KundeId { get; set; }
    public Kunde Kunde { get; set; }
    public int ForestillingId { get; set; }
    public Forestilling Forestilling { get; set; }
    public int AntalBilletter { get; set; }
    public Billettype Billettype { get; set; }
    public DateTime ReservationsDato { get; set; }
}
```

#### Billettype (Ticket Type - Enum)
```csharp
public enum Billettype
{
    Almindelig,
    Boern,
    VIP
}
```

#### Plads (Seat) - Optional for MVP
```csharp
public class Plads
{
    public int Id { get; set; }
    public int PladsNummer { get; set; } // 1-150
    public bool ErVIP { get; set; } // Seats 1-10 are VIP
    public int? ReservationId { get; set; } // Null if available
    public Reservation Reservation { get; set; }
}
```

#### Nyhed (News/Blog Post) - Lower Priority
```csharp
public class Nyhed
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Indhold { get; set; }
    public DateTime PubliceringsDato { get; set; }
    public string Forfatter { get; set; }
}
```

### 7.2 Relationships

**Many-to-Many:**
- `Forestilling` ↔ `Artist` (A performance has many artists; an artist performs in many performances)

**One-to-Many:**
- `Kunde` → `Reservation` (A customer has many reservations)
- `Forestilling` → `Reservation` (A performance has many reservations)
- `Lokation` → `Forestilling` (A location hosts many performances)

---

## 8. Technology Stack

### 8.1 Required Technologies

✅ **Allowed:**
- **Language:** C#
- **Framework:** .NET 8.0 (or latest LTS)
- **Web Framework:** ASP.NET Core Razor Pages
- **IDE:** Visual Studio 2022 Enterprise
- **Version Control:** Git + GitHub
- **UI Framework:** Bootstrap 5
- **CSS:** Custom CSS in `wwwroot/css/site.css`
- **Data Storage:** In-memory repositories (List<T>) with seed data

### 8.2 Forbidden Technologies

❌ **NOT Allowed for this Assignment:**
- Next.js, React, Vue, Angular, Svelte
- TypeScript
- Tailwind CSS
- Supabase, Vercel
- Node.js backend
- Docker, Kubernetes
- Microservices architecture
- REST API-first architecture
- Entity Framework (not needed for in-memory data)
- SQL Server, PostgreSQL, MySQL (no database required)

**Why:** These technologies are outside the scope of a 1st semester C# assignment and would obscure the fundamental programming skills being evaluated.

### 8.3 Development Environment

**Required Software:**
- Visual Studio 2022 (Enterprise or Community Edition)
- .NET 8.0 SDK
- Git for Windows
- Modern web browser (Chrome, Edge, Firefox)

**Recommended Extensions:**
- GitHub integration in Visual Studio
- Web Essentials (for CSS/HTML editing)

---

## 9. Implementation Priorities

### Phase 1: Core Foundation (Week 1-2)

**Priority: CRITICAL**

1. **Setup Solution Structure**
   - Create three projects: Core, ConsoleApp, Web
   - Add project references (Web → Core, ConsoleApp → Core)

2. **Domain Models** (CirkusLuna.Core/Models)
   - Person, Kunde, Medarbejder, Artist
   - Lokation
   - Forestilling
   - Reservation
   - Billettype enum

3. **Repository Interfaces** (CirkusLuna.Core/Interfaces)
   - `IRepository<T>` (generic)
   - `IForestillingRepository`
   - `IReservationRepository`
   - `IPersonRepository`

4. **In-Memory Repositories** (CirkusLuna.Core/Repositories)
   - `InMemoryForestillingRepository` with seed data
   - `InMemoryReservationRepository`
   - `InMemoryPersonRepository`

5. **Seed Data**
   - At least 10-15 performances across 8-10 Danish cities
   - 5-8 artists (some permanent, some guest)
   - 3-5 customers for testing
   - Variety of dates (some past, most future)

### Phase 2: Required Algorithms (Week 2)

**Priority: CRITICAL (Exam Requirement)**

6. **Self-Written Search Algorithm** (in Repository)
   - Implement manual loop-based search in `InMemoryForestillingRepository`
   - Method: `SearchByCity(string city)`
   - Must use a loop, not just LINQ `.Where()`

7. **Self-Written Sorting Algorithm** ⭐
   - Implement bubble sort, selection sort, or insertion sort
   - Method: `GetCitiesSortedAlphabetically()`
   - Sort list of city names alphabetically
   - Place in `SorteringService` or repository

### Phase 3: Service Layer (Week 2-3)

**Priority: HIGH**

8. **Service Classes** (CirkusLuna.Core/Services)
   - `ForestillingService`: Search, filter, get performances
   - `ReservationService`: Create reservations with validation
   - `CapacityService`: Check availability, enforce limits

9. **Business Rules**
   - Validate future performances only
   - Check total capacity (max 150)
   - Check VIP capacity (max 10)
   - Prevent overbooking

10. **Custom Exceptions** (CirkusLuna.Core/Exceptions)
    - `ReservationFullException`
    - `VIPCapacityExceededException`
    - `PastPerformanceException`

### Phase 4: Console App (Week 3)

**Priority: CRITICAL (Exam Focus)**

11. **Console App Menu** (CirkusLuna.ConsoleApp)
    - Menu-driven interface
    - Options:
      1. View all performances
      2. Search performances by city (demonstrate self-written search)
      3. View cities alphabetically sorted (demonstrate self-written sort)
      4. View performance details
      5. Create reservation (regular)
      6. Create VIP reservation
      7. View available seats for a performance
      8. Exit
    - Exception handling with user-friendly messages
    - Test all core features

### Phase 5: Razor Pages Web App (Week 3-4)

**Priority: MEDIUM (Secondary for Exam)**

12. **Basic Pages** (CirkusLuna.Web/Pages)
    - Index.cshtml (Homepage)
    - Forestillinger/Index.cshtml (List performances)
    - Forestillinger/Details.cshtml (Performance details)
    - Reservationer/Create.cshtml (Create reservation)
    - Reservationer/Bekraeftelse.cshtml (Confirmation)

13. **Layout and Navigation**
    - _Layout.cshtml with Bootstrap navbar
    - Danish labels: "Forside", "Forestillinger", "Turnéplan", "Reserver"
    - Footer with contact information

14. **Turnéplan (Tour Schedule)**
    - Turneplan/Index.cshtml
    - Display performances sorted by city (alphabetically)
    - Use the self-written sorting algorithm

15. **Forms and Validation**
    - Reservation form with model validation
    - Display validation errors
    - Success/error messages with TempData

### Phase 6: UI Design (Week 4)

**Priority: LOW (Visual Polish)**

16. **Custom CSS** (wwwroot/css/site.css)
    - Implement color scheme from design reference:
      - Burgundy `#8B1C1C`
      - Gold `#F4C542`
      - Cream `#FAF8F3`
    - Performance cards with date badges
    - Button styling

17. **Bootstrap Integration**
    - Responsive grid layout
    - Bootstrap cards for performances
    - Bootstrap forms for reservations
    - Bootstrap tables for lists

### Phase 7: Optional Features (Week 4-5)

**Priority: OPTIONAL**

18. **Seating Chart** (Pladsoversigt)
    - Display seat grid (1-150)
    - Indicate VIP seats (1-10)
    - Show available vs reserved

19. **Admin Pages**
    - Admin/Forestillinger (Manage performances)
    - Admin/Artister (Manage artists)
    - Admin/Kunder (Manage customers)

20. **News/Blog** (Lower priority)
    - Create and edit news posts
    - Display on homepage

### Phase 8: Documentation (Week 5)

**Priority: CRITICAL**

21. **UML Diagrams**
    - Domain Model
    - Class Diagram (focus on CirkusLuna.Core)
    - Sequence Diagram (e.g., create reservation flow)

22. **SCRUM Documentation**
    - Product Backlog with User Stories
    - User Stories with Acceptance Criteria
    - Sprint planning documents

23. **Report**
    - Maximum 10 pages + appendices
    - Indicate who implemented what
    - Include GitHub repository link

---

## 10. User Interface Guidelines

### 10.1 Design Philosophy

**Primary Goal:** Clean, functional UI that demonstrates good Razor Pages development.

**NOT the Goal:** Production-ready startup website or marketing showcase.

**Inspiration Source:** `docs/input/design-reference.png` provides visual direction (colors, layout style) but NOT a complexity specification.

### 10.2 Color Palette

**Primary Colors:**
- **Burgundy:** `#8B1C1C` (backgrounds, header)
- **Gold/Yellow:** `#F4C542` (primary buttons, highlights)
- **Cream:** `#FAF8F3` (content surfaces, cards)

**Neutral Colors:**
- **White:** `#FFFFFF` (text on dark backgrounds)
- **Dark Gray:** `#2B2B2B` (text on light backgrounds)

**Accent Colors:**
- **Red:** `#D32F2F` (date badges, alerts)
- **Green:** `#4CAF50` (available seats, success messages)

### 10.3 Danish Language

**All labels, buttons, and text must be in Danish:**

**Navigation:**
- Forside (Home)
- Forestillinger (Performances)
- Turnéplan (Tour Schedule)
- Reserver Billet (Reserve Ticket)

**Common Labels:**
- Dato (Date)
- Tidspunkt (Time)
- By (City)
- Lokation (Venue)
- Ledige Pladser (Available Seats)
- VIP-pladser (VIP Seats)
- Antal Billetter (Number of Tickets)
- Bekræft (Confirm)

### 10.4 Layout Components

**Homepage:**
- Simple hero section with "Cirkus Luna" branding
- Optional statistics (calculated from real seed data)
- Featured performances (next 3-4 upcoming)
- CTA buttons: "Se Turnéplan", "Reserver Billet"

**Performance List:**
- Bootstrap cards in grid layout (3 columns on desktop)
- Each card shows: Date badge, City, Venue, Available seats
- Two buttons: "Se mere" (Details) and "Reserver" (Reserve)

**Tour Schedule:**
- Performances grouped by city
- Cities sorted alphabetically (using self-written algorithm)
- Display: City heading → List of performances with dates

**Reservation Form:**
- Simple Bootstrap form
- Fields: Customer info, Number of tickets, Ticket type
- Validation feedback
- Submit button: "Bekræft Reservation"

### 10.5 Statistics Must Be Real

**Critical Rule:** If the UI displays statistics, they MUST be calculated from seed data.

**Example:**
```csharp
@inject IForestillingService ForestillingService

@{
    var totalCities = ForestillingService.GetUniqueCities().Count;
    var totalShows = ForestillingService.GetAllForestillinger().Count;
    var totalSeats = totalShows * 150;
}

<div class="stats">
    <div class="stat">
        <h3>@totalCities</h3>
        <p>Byer på Turnéen</p>
    </div>
    <div class="stat">
        <h3>@totalShows</h3>
        <p>Forestillinger</p>
    </div>
    <div class="stat">
        <h3>@totalSeats</h3>
        <p>Pladser i Alt</p>
    </div>
</div>
```

**Forbidden:** Hardcoded marketing numbers like "20 år af underholdning" or "10,000+ glade kunder" if not based on actual data.

### 10.6 What to Avoid

❌ **Forbidden UI Elements:**
- Complex JavaScript frameworks beyond Bootstrap
- Advanced animations or parallax effects
- Generic AI marketing copy ("Experience the magic...")
- Fake testimonials or social proof
- Loading spinners (in-memory data is instant)
- Complex dashboard charts (unless explicitly required)

### 10.7 Responsive Design

- Use Bootstrap grid: `.container`, `.row`, `.col-*`
- Test on mobile, tablet, and desktop
- Simple, clean layout on all screen sizes

---

## 11. Deliverables

### 11.1 Code Deliverables

**GitHub Repository (Public):**
1. ✅ CirkusLuna.sln (Visual Studio solution)
2. ✅ CirkusLuna.Core (Class Library project)
3. ✅ CirkusLuna.ConsoleApp (Console App project)
4. ✅ CirkusLuna.Web (Razor Pages project)
5. ✅ README.md (How to run the project)
6. ✅ .gitignore (Exclude bin/, obj/, .vs/)

### 11.2 Documentation Deliverables

**Via Wiseflow (Due: May 28, 2026, 10:00):**

1. **Report (Max 10 pages + front page + appendices)**
   - Introduction (case description, purpose)
   - SCRUM methodology (Product Backlog, User Stories)
   - UML Diagrams (Domain Model, Class Diagram, Sequence Diagram)
   - Implementation description
   - Individual contributions (who did what)
   - Conclusion
   - GitHub repository link

2. **UML Diagrams**
   - Domain Model (conceptual)
   - Class Diagram (detailed, focus on CirkusLuna.Core)
   - Sequence Diagram(s) (complex flows, e.g., reservation)

3. **SCRUM Documentation**
   - Product Backlog with User Stories
   - User Stories with Acceptance Criteria
   - Sprint planning artifacts

### 11.3 Presentation (Demo Day: May 28, 2026, 10:15+)

**Each group presents:**
1. System overview (2 minutes)
2. Demo of Console App (show core features, algorithms)
3. Demo of Web App (show key pages)
4. Architecture explanation (layers, Class Library)
5. Q&A

**Focus:** Show that you understand what you built and can explain it.

---

## 12. Success Criteria

### 12.1 Must-Have (Critical for Exam)

- ✅ Three-project structure (Core, ConsoleApp, Web)
- ✅ All core business logic in CirkusLuna.Core
- ✅ Console App can demonstrate all features
- ✅ Self-written search algorithm (manual loop)
- ✅ Self-written alphabetical city sorting algorithm
- ✅ Business rules enforced (capacity, future performances, VIP limits)
- ✅ Custom exceptions for business rule violations
- ✅ Clear layer separation (UI → Service → Repository)
- ✅ UML diagrams (Domain Model, Class Diagram, Sequence Diagram)
- ✅ SCRUM documentation (Product Backlog, User Stories)
- ✅ GitHub repository (public, with README)
- ✅ Code comments explaining business rules
- ✅ Every group member understands their parts

### 12.2 Should-Have (Important)

- ✅ Razor Pages Web App with key features working
- ✅ Danish labels throughout the UI
- ✅ Bootstrap-based responsive layout
- ✅ Custom CSS for visual polish
- ✅ In-memory repositories with seed data
- ✅ Reservation form with validation
- ✅ Tour schedule sorted by city

### 12.3 Nice-to-Have (Optional)

- ✅ Seating chart overview
- ✅ Admin pages for managing data
- ✅ News/blog functionality
- ✅ Advanced filtering options
- ✅ Deployment to Simply.com (optional)

---

## 13. Exam Preparation

### 13.1 What to Expect at Exam

**From assignment description:**
- "Ved eksamen vil man blive bedt om at udvide det udviklede system"
- Translation: At the exam, you will be asked to extend the developed system

**This means:**
- Be prepared to add new features
- Understand your architecture well enough to extend it
- Have the system running on your PC
- Know where to add new models, services, repositories

### 13.2 Key Areas to Master

1. **Class Library (CirkusLuna.Core)**
   - Explain all models and their relationships
   - Explain repository pattern
   - Explain service layer and business rules
   - Walk through self-written algorithms

2. **Console App**
   - Demonstrate all core features
   - Show how it uses the Class Library
   - Explain exception handling

3. **Architecture**
   - Explain three-layer architecture
   - Show how layers communicate
   - Explain why business logic is NOT in Razor Pages

4. **Algorithms**
   - Explain self-written search algorithm
   - Explain self-written sorting algorithm
   - Be ready to modify or extend them

### 13.3 Practice Scenarios

**Before the exam, practice:**
1. Running the system from scratch in Visual Studio
2. Adding a new model (e.g., Anmeldelse - Review)
3. Adding a new repository method (e.g., SearchByDate)
4. Adding a new service method (e.g., CancelReservation)
5. Explaining UML diagrams
6. Walking through a sequence diagram (e.g., reservation flow)

---

## 14. Constraints and Limitations

### 14.1 Assignment Constraints

**Explicit Constraints:**
- Not all features need to be fully implemented
- Focus on structure, design, and correct use of techniques
- Simple implementation is acceptable if well-structured
- Maximum 10 pages for report (excluding front matter and appendices)

**Implicit Constraints:**
- 1st semester skill level (don't over-engineer)
- Limited time (approximately 4-5 weeks)
- Group project (3-4 people)

### 14.2 Scope Limitations

**In Scope:**
- Core reservation system
- Search and filter performances
- Self-written algorithms
- Basic Razor Pages UI

**Out of Scope (Unless Time Permits):**
- Payment processing
- Email notifications
- User authentication/authorization
- Complex admin dashboard
- Mobile app
- Real-time seat selection
- PDF ticket generation
- Integration with third-party services

### 14.3 Data Limitations

**For this assignment:**
- In-memory data storage is sufficient
- Data resets when application restarts (acceptable)
- No database migration or persistence required
- Seed data provides demonstration scenarios

---

## 15. Appendix

### 15.1 Example User Stories

**US-1: Søge forestillinger efter by**
- **As a** customer
- **I want to** search for performances in a specific city
- **So that** I can find shows near me

**Acceptance Criteria:**
- Given I am on the performance list page
- When I enter a city name and click "Søg"
- Then I see only performances in that city
- And the search uses the self-written search algorithm

---

**US-2: Reservere almindelige billetter**
- **As a** customer
- **I want to** reserve regular tickets for a performance
- **So that** I can attend the circus show

**Acceptance Criteria:**
- Given a performance has available seats
- When I fill out the reservation form with valid information
- And I select "Almindelig" as ticket type
- And I submit the form
- Then a reservation is created
- And the available seats decrease by the number of tickets reserved
- And I see a confirmation message

---

**US-3: Validere VIP kapacitet**
- **As a** system
- **I want to** enforce the VIP capacity limit
- **So that** we don't overbook VIP seats

**Acceptance Criteria:**
- Given a performance has only 2 VIP seats available
- When a customer tries to reserve 3 VIP tickets
- Then the system throws a `VIPCapacityExceededException`
- And the reservation is not created
- And the customer sees an error message

---

**US-4: Sortere byer alfabetisk**
- **As a** customer
- **I want to** see the tour schedule with cities sorted alphabetically
- **So that** I can easily find my city

**Acceptance Criteria:**
- Given the system has performances in: Odense, København, Aalborg, Aarhus
- When I view the tour schedule
- Then cities are displayed in order: Aalborg, Aarhus, København, Odense
- And the sorting uses a self-written sorting algorithm (bubble, selection, or insertion sort)

---

### 15.2 Seed Data Examples

**Danish Cities for Tour:**
1. København
2. Aarhus
3. Odense
4. Aalborg
5. Esbjerg
6. Roskilde
7. Kolding
8. Horsens
9. Vejle
10. Randers

**Sample Performances:**
- København - Cirkusbygningen - June 1, 2026, 19:00
- Aarhus - Kongressal - June 5, 2026, 18:00
- Odense - Eventhal - June 10, 2026, 19:30
- Aalborg - Arena Nord - June 15, 2026, 20:00
- Esbjerg - Musikhuset - June 20, 2026, 18:30

**Sample Artists:**
- Lars Henriksen - Trapez-artist
- Maria Sørensen - Jonglør
- Peter Nielsen - Klovn
- Anna Andersen - Akrobat
- Thomas Jensen - Tryllekunstner

### 15.3 Glossary (Danish to English)

| Danish | English |
|--------|---------|
| Forestilling | Performance |
| Turnéplan | Tour Schedule |
| By | City |
| Lokation | Venue/Location |
| Kunde | Customer |
| Medarbejder | Employee |
| Artist | Artist/Performer |
| Reservation | Reservation/Booking |
| Billet | Ticket |
| Billettype | Ticket Type |
| Plads | Seat |
| Ledige pladser | Available seats |
| Kapacitet | Capacity |
| VIP-pladser | VIP seats |
| Almindelig | Regular/Standard |
| Børn | Children |
| Dato | Date |
| Tidspunkt | Time |
| Nyhed | News |

---

## Document Control

**Version:** 1.0  
**Created:** May 20, 2026  
**Last Updated:** May 20, 2026  
**Status:** Final  
**Approved by:** Constitution Compliance  

---

**End of Specification**
