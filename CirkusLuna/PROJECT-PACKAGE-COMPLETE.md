# ✅ Cirkus Luna - Complete Project Package

**Status:** Ready for Implementation  
**Date:** May 20, 2026  
**Project:** 1st Semester Danish Computer Science Assignment

---

## 📦 What's Been Created

### 1. Project Constitution
**File:** `.speckit.constitution`  
**Purpose:** Protects project from scope creep and inappropriate technology choices  
**Contains:** 14 core principles, forbidden technologies, architecture requirements

### 2. Full System Specification
**File:** `docs/SPECIFICATION.md` (67 pages)  
**Purpose:** Complete requirements extracted from assignment PDFs  
**Contains:** All functional requirements, domain model, UML examples, exam preparation

### 3. MVP Implementation Plan ⭐
**File:** `IMPLEMENTATION-PLAN.md`  
**Purpose:** Realistic scope for 4-week 1st semester project  
**Contains:**
- 6 core models (not 11)
- 2 services (not 5+)
- Complete C# code for self-written algorithms
- 4-week timeline
- "Features Not Implemented in MVP" section with rationale
- Simple price calculation (Normal=120, Barn=80, VIP=250)

### 4. Implementation Tasks ⭐
**File:** `IMPLEMENTATION-TASKS.md`  
**Purpose:** ~80 concrete, ordered tasks  
**Contains:**
- 11 task groups
- Exact file locations
- Code snippets for critical algorithms
- Testing checklist
- Marked with ⭐ for critical tasks

### 5. Design Guidelines
**File:** `docs/DESIGN-GUIDELINES.md`  
**Purpose:** UI implementation guide (visual inspiration only)  
**Contains:** Color palette, Bootstrap patterns, Danish labels, what to avoid

### 6. Source Materials
**Location:** `docs/input/`  
**Files:**
- `cirkus-luna-case.pdf` ✅
- `cirkus-luna-opgavebeskrivelse.pdf` ✅
- `design-reference.png` ✅

---

## 🎯 Key Points

### Assignment Requirements Captured

✅ **Self-Written Algorithms (EXAM CRITICAL):**
- Manual search by city using loops (not just LINQ)
- Bubble sort for alphabetical city sorting (not .OrderBy())
- Full code provided in implementation plan

✅ **Three-Project Structure:**
- CirkusLuna.Core (Class Library - EXAM FOCUS)
- CirkusLuna.ConsoleApp (Console App - EXAM DEMONSTRATION)
- CirkusLuna.Web (Razor Pages - SECONDARY)

✅ **Business Rules:**
- Future performances only
- Max 150 normal seats per performance
- Max 10 VIP seats per performance
- No overbooking

✅ **Console App (CRITICAL FOR EXAM):**
- 10 menu options demonstrating all features
- Tests search algorithm
- Tests sorting algorithm
- Tests capacity validation
- Tests exception handling

### Scope Management

**Simplified from Full Spec:**
- ❌ No Person inheritance hierarchy (Medarbejder removed from MVP)
- ❌ No Lokation entity (City is sufficient)
- ❌ No Nyhed/Blog system (lower priority)
- ❌ No detailed seating chart (capacity tracking only)

**Rationale Documented:**
- "Features Not Implemented in MVP" section in implementation plan
- Clear explanation: Focus on core programming requirements
- Shows censor/lærer that decisions were deliberate, not oversights

**Price Calculation Added:**
- Simple enum-based pricing
- BillettypePris helper class
- Normal = 120 DKK
- Barn = 80 DKK
- VIP = 250 DKK
- TotalPris calculated property on Reservation

### Technology Stack

✅ **Allowed:**
- C#, .NET 8.0
- Visual Studio 2022
- ASP.NET Core Razor Pages
- In-memory repositories
- Bootstrap 5
- Simple custom CSS
- GitHub

❌ **Forbidden:**
- Next.js, React, Vue, Angular
- TypeScript
- Tailwind CSS
- Supabase, Vercel
- Docker, Kubernetes
- Entity Framework (not needed)
- API-first architecture

---

## 📋 Implementation Roadmap

### Week 1: Foundation (Days 1-3)
**Tasks 1.1 - 5.6** (~30 tasks)
- Solution setup (3 projects)
- 6 core models + price helper
- 6 repository interfaces
- 6 repository implementations
- Self-written search algorithm ⭐
- Self-written bubble sort algorithm ⭐
- Seed data (8 cities, 5 artists, 10-12 performances)

**Key Deliverable:** Core library compiles, algorithms work

### Week 2: Business Logic (Days 4-5)
**Tasks 6.1 - 8.12** (~20 tasks)
- 3 custom exceptions
- 2 service interfaces
- 2 service implementations
- All validation rules
- Complete Console App with 10 menu options ⭐

**Key Deliverable:** Console App demonstrates all features

### Week 3: Web UI (Days 6-8)
**Tasks 9.1 - 10.6** (~15 tasks)
- Service registration in Program.cs
- 6 Razor Pages (Index, Program, Detaljer, Reserver, Bekraeftelse, Artister)
- Bootstrap layout with Danish navigation
- Custom CSS (burgundy, gold, cream)
- Responsive design

**Key Deliverable:** Web app functional with clean UI

### Week 4: Documentation (Days 9-10)
**Tasks 11.1 - 11.10** (~10 tasks)
- Product Backlog
- User Stories with Acceptance Criteria
- UML Domain Model
- UML Class Diagram (focus on Core)
- UML Sequence Diagram (reservation flow)
- Test plan
- Complete README
- Report (max 10 pages)
- GitHub repository (public)

**Key Deliverable:** Complete documentation, exam-ready

---

## ✅ Critical Success Factors

### For Exam Preparation

**Must Demonstrate:**
1. ⭐ Self-written search algorithm (manual loop, not just LINQ)
2. ⭐ Self-written sorting algorithm (bubble sort, not .OrderBy())
3. ⭐ Console App can test all features without web UI
4. ⭐ All business logic in Class Library (not Razor Pages)
5. ⭐ Capacity validation works (150 total, 10 VIP)
6. ⭐ Custom exceptions for business rules
7. ⭐ System runs locally in Visual Studio

**Must Explain:**
- Why algorithms are self-written (exam requirement)
- Three-layer architecture (UI → Service → Repository)
- How to extend the system (add new model, repository, service)
- Business rules and validation
- UML diagrams and their relationships

**Must Document:**
- What IS implemented (focus here)
- What is NOT implemented with rationale (brief section)
- Individual contributions (who did what)
- GitHub repository link

---

## 🎓 Assignment Compliance

### From `cirkus-luna-opgavebeskrivelse.pdf`:

✅ **Three projects required** (Core, ConsoleApp, Web)  
✅ **Class Library focus** (exam evaluates this)  
✅ **Console App must test system** (exam does NOT focus on Razor Pages)  
✅ **Self-written algorithms** (search and sorting)  
✅ **SCRUM methodology** (Product Backlog, User Stories)  
✅ **UML diagrams** (Domain Model, Class, Sequence)  
✅ **Code comments** (explain business rules)  
✅ **GitHub repository** (public)  
✅ **Simple is acceptable** ("En simpel implementering kan virke som et lige så godt grundlag for eksamen")

### From `cirkus-luna-case.pdf`:

✅ **Capacity management** (150 total, 10 VIP)  
✅ **Reservation validation** (future only, capacity checks)  
✅ **Search by city** (self-written algorithm)  
✅ **Alphabetical city sorting** (self-written algorithm)  
✅ **Many-to-many relationship** (Performance ↔ Artist)  
✅ **Ticket types** (Normal, Barn, VIP) with pricing

---

## 📊 Scope Summary

### In MVP (Will Implement):

**Models (6):**
1. By (City)
2. Artist
3. Kunde (Customer)
4. Billettype (enum)
5. Forestilling (Performance)
6. Reservation

**Repositories (6):**
- InMemoryByRepository (with bubble sort)
- InMemoryForestillingRepository (with manual search)
- InMemoryArtistRepository
- InMemoryKundeRepository
- InMemoryReservationRepository

**Services (2):**
- ForestillingService
- ReservationService (with price calculation)

**Console App:**
- 10 menu options
- Demonstrates search algorithm
- Demonstrates sorting algorithm
- Tests all validation rules

**Razor Pages (6):**
- Index (homepage)
- Program (list performances)
- Detaljer (performance details)
- Reserver (reservation form)
- Bekraeftelse (confirmation)
- Artister (list artists)

### Not in MVP (Documented as Future Work):

**From Assignment:**
- ❌ Medarbejder (Employee) - Same pattern as Kunde, deferred
- ❌ Nyhed/Blog - Lower priority, deferred
- ❌ Detailed seating chart - Capacity tracking implemented instead
- ❌ Separate Lokation entity - City is sufficient for MVP

**Rationale:** Focus on core programming requirements (algorithms, repositories, services, validation)

**Not in Assignment Scope:**
- ❌ Payment processing
- ❌ Email notifications
- ❌ User authentication
- ❌ Real database
- ❌ API endpoints
- ❌ Mobile app

---

## 🚀 Next Steps

### To Start Implementation:

1. **Read these documents in order:**
   - `IMPLEMENTATION-PLAN.md` (understand MVP scope)
   - `IMPLEMENTATION-TASKS.md` (follow task list)
   - `docs/SPECIFICATION.md` (reference for details)

2. **Open Visual Studio:**
   - Start with Task 1.1 (Create solution)
   - Follow tasks sequentially through Groups 1-4 (Foundation)

3. **Week 1 Goal:**
   - Complete all models
   - Complete all repositories with self-written algorithms
   - Seed data working
   - Console App can display data

4. **Critical Early Tasks:**
   - Task 4.1: Bubble sort algorithm ⭐
   - Task 4.2: Manual search algorithm ⭐
   - Task 5.5: Seed performances with variety ⭐

### Before Demo Day (May 28, 2026):

- [ ] Console App works (all 10 options)
- [ ] Web App works (all 6 pages)
- [ ] Self-written algorithms demonstrated
- [ ] Documentation complete
- [ ] GitHub repository public
- [ ] Report submitted to Wiseflow

---

## 📖 Document Reference Guide

**For Implementation Team:**
1. Start: `IMPLEMENTATION-TASKS.md` (task-by-task guide)
2. Questions: `IMPLEMENTATION-PLAN.md` (detailed explanations)
3. Full requirements: `docs/SPECIFICATION.md` (reference)

**For Project Management:**
- Timeline: `IMPLEMENTATION-PLAN.md` Section 9 (4-week phases)
- User Stories: To be created in `docs/scrum/user-stories.md`
- Testing: `IMPLEMENTATION-TASKS.md` (testing checklist at end)

**For Design/UI:**
- Guidelines: `docs/DESIGN-GUIDELINES.md`
- Colors: Burgundy (#8B1C1C), Gold (#F4C542), Cream (#FAF8F3)
- Danish labels reference included

**For Exam Preparation:**
- Constitution: `.speckit.constitution` (constraints and principles)
- Algorithms: `IMPLEMENTATION-PLAN.md` Sections 3.2 and 4.2
- Architecture: `IMPLEMENTATION-PLAN.md` Section 4.1

---

## ⚠️ Important Reminders

### Do NOT:
- ❌ Expand beyond 6 models (scope creep)
- ❌ Add database (in-memory is sufficient)
- ❌ Use Next.js, React, TypeScript, Tailwind
- ❌ Use .OrderBy() or .Sort() as the ONLY city sorting implementation
- ❌ Use only LINQ for the search algorithm
- ❌ Put business logic in Razor Pages

### DO:
- ✅ Follow the task list strictly
- ✅ Comment business rules in code
- ✅ Test Console App thoroughly (exam focus)
- ✅ Document what's NOT implemented with rationale
- ✅ Keep it simple and explainable
- ✅ Make GitHub repository public
- ✅ Include repository link in documentation

---

## 🎯 Success Metrics

**Project is exam-ready when:**

1. ✅ Console App demonstrates search algorithm (manual loop)
2. ✅ Console App demonstrates sorting algorithm (bubble sort)
3. ✅ Console App tests all validation rules
4. ✅ Web App creates reservations successfully
5. ✅ All business logic is in CirkusLuna.Core
6. ✅ UML diagrams focus on Class Library
7. ✅ Documentation explains what's implemented and why
8. ✅ Every group member can explain their parts
9. ✅ System runs locally in Visual Studio
10. ✅ GitHub repository is public with all code

**Quote from Assignment:**
> "En simpel implementering kan virke som et lige så godt grundlag for eksamen, som en meget avanceret løsning. Det er vigtigt, at alle har indblik og forståelse for det udviklede system."

**Translation:** A simple implementation can work just as well as an advanced solution for the exam. It's important that everyone has insight and understanding of the developed system.

---

## 📁 Project Structure (Final)

```
CirkusLuna/
├── .speckit.constitution
├── README.md
├── IMPLEMENTATION-PLAN.md
├── IMPLEMENTATION-TASKS.md
├── SPECIFICATION-COMPLETE.md
├── CirkusLuna.sln
│
├── CirkusLuna.Core/
│   ├── Models/ (6 models + price helper)
│   ├── Interfaces/ (8 interfaces)
│   ├── Repositories/ (6 in-memory repositories)
│   ├── Services/ (2 services)
│   ├── Exceptions/ (3 custom exceptions)
│   └── SeedData/ (DataSeeder)
│
├── CirkusLuna.ConsoleApp/
│   ├── Program.cs
│   └── Menus/ (MainMenu with 10 options)
│
├── CirkusLuna.Web/
│   ├── Pages/ (6 Razor Pages)
│   ├── wwwroot/css/ (site.css with circus styling)
│   └── Program.cs
│
└── docs/
    ├── SPECIFICATION.md (full requirements)
    ├── DESIGN-GUIDELINES.md (UI guide)
    ├── input/ (PDFs and design reference)
    ├── uml/ (Domain Model, Class, Sequence diagrams)
    └── scrum/ (Product Backlog, User Stories)
```

---

## ✅ Package Complete

**The Cirkus Luna project now has everything needed for successful implementation:**

1. ✅ Complete specification from assignment PDFs
2. ✅ Realistic MVP scope (6 models, 2 services, 4 weeks)
3. ✅ ~80 concrete implementation tasks
4. ✅ Constitution protecting from scope creep
5. ✅ Design guidelines (visual inspiration only)
6. ✅ "Future Work" section explaining omissions
7. ✅ Price calculation system
8. ✅ Self-written algorithms with full code
9. ✅ Console App as exam focus
10. ✅ Clear documentation requirements

**Ready to implement in Visual Studio with C#, ASP.NET Core Razor Pages, and simple architecture appropriate for 1st semester exam.**

**Good luck! 🎪**
