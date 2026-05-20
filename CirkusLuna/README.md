# Cirkus Luna - Reservation System

**Project Type:** 1st Semester Computer Science Assignment  
**Institution:** UCL Erhvervsakademi og Professionshøjskole  
**Technology:** C# / ASP.NET Core Razor Pages / Visual Studio

---

## Project Structure

```
CirkusLuna/
├── CirkusLuna.Core/              (Class Library - EXAM FOCUS)
│   └── Models/                   (Domain models)
├── CirkusLuna.ConsoleApp/        (Console App - Demonstration)
└── CirkusLuna.Web/               (ASP.NET Core Razor Pages)
```

---

## How to Run

### Prerequisites
- Visual Studio 2022
- .NET 8.0 SDK

### Console App (Primary for Exam)
1. Open `CirkusLuna.sln` in Visual Studio
2. Set `CirkusLuna.ConsoleApp` as startup project
3. Press F5 to run

### Web App (Secondary)
1. Open `CirkusLuna.sln` in Visual Studio
2. Set `CirkusLuna.Web` as startup project
3. Press F5 to run

---

## Implementation Status

### ✅ Phase 1: Foundation (Complete)
- [x] Solution structure created
- [x] Three projects created (Core, ConsoleApp, Web)
- [x] Project references configured
- [x] Core folder structure created

### ✅ Phase 2: Core Models (Complete)
- [x] By (City)
- [x] Artist (Performer)
- [x] Kunde (Customer)
- [x] Billettype (Ticket Type enum)
- [x] BillettypePris (Price helper)
- [x] Forestilling (Performance with capacity constraints)
- [x] Reservation (Booking with price calculation)

### 🔄 Next Phase: Repository Layer
- [ ] Repository interfaces
- [ ] In-memory repository implementations
- [ ] Self-written search algorithm (manual loop)
- [ ] Self-written sorting algorithm (bubble sort)

---

## Key Features

- **Self-written algorithms** for search and sorting (exam requirement)
- **Capacity management** (150 total seats, 10 VIP seats per performance)
- **Price calculation** (Normal=120 DKK, Barn=80 DKK, VIP=250 DKK)
- **Business rule validation** (future performances, capacity limits)

---

## Technology Stack

✅ **Allowed:**
- C# / .NET 8.0
- ASP.NET Core Razor Pages
- Visual Studio 2022
- In-memory data storage
- Bootstrap 5 + custom CSS

❌ **Not Allowed:**
- Next.js, React, Vue, Angular
- TypeScript
- Tailwind CSS
- Supabase, Vercel
- Docker
- Entity Framework (not needed for in-memory)

---

## Project Goals

This is a **1st semester exam foundation project** focusing on:

1. Class Library architecture (exam focus)
2. Self-written algorithms (search and sorting)
3. Console App demonstration (primary exam tool)
4. Repository and Service layer patterns
5. Business rule validation

**Remember:** "En simpel implementering kan virke som et lige så godt grundlag for eksamen, som en meget avanceret løsning."

---

## Documentation

See the `docs/` folder for:
- Full specification
- Implementation plan
- Design guidelines
- Task list

---

## Contributors

[To be filled in by group members]

---

## GitHub Repository

[Repository will be made public before submission]
