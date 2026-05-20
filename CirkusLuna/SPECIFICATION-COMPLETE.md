# ✅ Cirkus Luna - Specification Complete

**Status:** Ready for Implementation  
**Date:** May 20, 2026  
**Project Type:** 1st Semester C# Computer Science Assignment

---

## 📄 Documents Created

### 1. **`.speckit.constitution`** (Project Constitution)
- 14 core principles protecting the project
- Technology constraints (C# required, Next.js forbidden)
- Architecture requirements (3 projects, 3 layers)
- Self-written algorithm requirements
- Exam readiness guidelines
- Updated with design realism constraints

### 2. **`docs/SPECIFICATION.md`** (Full System Specification) ⭐ NEW
**67 pages of detailed requirements extracted from the assignment PDFs:**

- Assignment context and deadlines
- Case description (Cirkus Luna traveling circus)
- Functional requirements (11 major feature areas)
- Non-functional requirements
- Domain model with C# class definitions
- Technology stack (allowed and forbidden)
- Implementation priorities (4-week roadmap)
- UI guidelines with Danish labels
- Deliverables checklist
- Exam preparation guide
- Example User Stories
- Seed data examples
- Glossary (Danish to English)

### 3. **`docs/DESIGN-GUIDELINES.md`** (UI Implementation Guide)
- Color palette from design reference
- Bootstrap + custom CSS approach
- Danish labels reference
- Component patterns (cards, forms, tables)
- What to avoid (marketing fluff, fake stats)

### 4. **`docs/input/`** (Source Materials)
- `cirkus-luna-case.pdf` ✅
- `cirkus-luna-opgavebeskrivelse.pdf` ✅
- `design-reference.png` ✅

---

## 🎯 Key Requirements from Assignment PDFs

### Critical Assignment Requirements

**From `cirkus-luna-opgavebeskrivelse.pdf`:**

1. **Three-Project Structure (MANDATORY):**
   - `CirkusLuna.Core` (Class Library) - EXAM FOCUS
   - `CirkusLuna.ConsoleApp` (Console App) - EXAM DEMONSTRATION
   - `CirkusLuna.Web` (Razor Pages) - SECONDARY

2. **Self-Written Algorithms (MANDATORY):**
   - ⭐ Search/filtering algorithm (manual loop, not just LINQ)
   - ⭐ Alphabetical city sorting algorithm (bubble/selection/insertion sort)

3. **Architecture (MANDATORY):**
   - Three layers: UI → Service → Repository
   - Business logic in Service layer (NOT in Razor Pages)
   - UML diagrams focus on Class Library only

4. **Console App (CRITICAL FOR EXAM):**
   - "Da eksamen ikke omhandler implementeringen i Razor Page app er det vigtigt, at man kan afprøve (teste) applikation fra en Console app"
   - Translation: Because the exam does NOT focus on Razor Pages implementation, it's important that you can test the application from a Console App

5. **Documentation (MANDATORY):**
   - SCRUM: Product Backlog, User Stories with Acceptance Criteria
   - UML: Domain Model, Class Diagram, Sequence Diagram
   - Report: Max 10 pages + appendices
   - GitHub: Public repository with link in documentation

6. **Code Quality:**
   - Code must be commented
   - Every group member must understand their parts
   - "En simpel implementering kan virke som et lige så godt grundlag for eksamen, som en meget avanceret løsning"
   - Translation: A simple implementation can work just as well as an advanced solution for the exam

### Critical Case Requirements

**From `cirkus-luna-case.pdf`:**

1. **Capacity Management (MANDATORY):**
   - Maximum 150 total seats per performance
   - Maximum 10 VIP seats per performance
   - System must enforce these limits

2. **Reservation Validation (MANDATORY):**
   - Only future performances (no past bookings)
   - Check available capacity
   - Check VIP capacity separately

3. **Search and Filter (MANDATORY):**
   - Search by city
   - Search by date
   - Filter by ticket type
   - Filter by availability

4. **Alphabetical City Sorting (EXPLICITLY REQUIRED):**
   - "Sortere listen over byer alfabetisk ved brug af en selvimplementeret sorteringsalgoritme"
   - Translation: Sort the list of cities alphabetically using a self-implemented sorting algorithm

5. **Many-to-Many Relationship:**
   - Performance ↔ Artists
   - One performance has many artists
   - One artist performs in many performances

6. **Ticket Types:**
   - Almindelig (Regular)
   - Børn (Children)
   - VIP (Premium)

---

## 📊 Project Structure Specified

```
CirkusLuna.sln
│
├── CirkusLuna.Core/                    ← EXAM FOCUS (Class Library)
│   ├── Models/                         ← Domain entities
│   │   ├── Person.cs, Kunde.cs, Medarbejder.cs, Artist.cs
│   │   ├── Forestilling.cs, Lokation.cs, Reservation.cs
│   │   └── Billettype.cs (enum)
│   ├── Interfaces/                     ← Repository and service contracts
│   ├── Repositories/                   ← Data access + self-written algorithms
│   │   └── InMemoryForestillingRepository.cs (with search algorithm)
│   ├── Services/                       ← Business logic
│   │   ├── ForestillingService.cs
│   │   ├── ReservationService.cs
│   │   ├── CapacityService.cs
│   │   └── SorteringService.cs (self-written sorting)
│   ├── Exceptions/                     ← Custom exceptions
│   │   ├── ReservationFullException.cs
│   │   ├── VIPCapacityExceededException.cs
│   │   └── PastPerformanceException.cs
│   └── Validation/
│
├── CirkusLuna.ConsoleApp/              ← EXAM DEMONSTRATION (Console App)
│   └── Program.cs                      ← Menu-driven interface
│       ├── Search performances by city (show self-written search)
│       ├── Display cities alphabetically (show self-written sort)
│       ├── Create reservations (show validation)
│       ├── Check capacity (show business rules)
│       └── Handle exceptions (show error handling)
│
└── CirkusLuna.Web/                     ← SECONDARY (Razor Pages)
    ├── Pages/
    │   ├── Index.cshtml                ← Homepage
    │   ├── Forestillinger/             ← List and details
    │   ├── Turneplan/                  ← Tour schedule (sorted cities)
    │   └── Reservationer/              ← Create reservation
    └── wwwroot/
        └── css/site.css                ← Custom CSS (burgundy, gold, cream)
```

---

## 🚀 Implementation Roadmap (from Specification)

### Week 1-2: Core Foundation (CRITICAL)
- ✅ Setup three-project solution
- ✅ Create domain models (Person, Forestilling, Reservation, etc.)
- ✅ Create repository interfaces
- ✅ Implement in-memory repositories with seed data
- ✅ Seed 10-15 performances across 8-10 Danish cities

### Week 2: Required Algorithms (CRITICAL - EXAM REQUIREMENT)
- ✅ Self-written search algorithm (manual loop in repository)
- ✅ Self-written alphabetical city sorting (bubble/selection/insertion sort)
- ✅ Place in `SorteringService` or repository

### Week 2-3: Service Layer (HIGH PRIORITY)
- ✅ ForestillingService (search, filter, get performances)
- ✅ ReservationService (create with validation)
- ✅ CapacityService (check availability, enforce limits)
- ✅ Custom exceptions (ReservationFullException, etc.)

### Week 3: Console App (CRITICAL - EXAM FOCUS)
- ✅ Menu-driven interface
- ✅ Demonstrate self-written search algorithm
- ✅ Demonstrate self-written sorting algorithm
- ✅ Create reservations with validation
- ✅ Show exception handling
- ✅ Test all core features without web UI

### Week 3-4: Razor Pages (MEDIUM PRIORITY)
- ✅ Homepage with hero and featured performances
- ✅ Forestillinger list and details
- ✅ Turnéplan (tour schedule sorted by city)
- ✅ Reservation form with validation
- ✅ Bootstrap layout with Danish labels

### Week 4: UI Design (LOW PRIORITY)
- ✅ Custom CSS (burgundy, gold, cream colors)
- ✅ Performance cards with date badges
- ✅ Responsive Bootstrap grid
- ✅ Statistics calculated from real seed data

### Week 5: Documentation (CRITICAL)
- ✅ UML Domain Model
- ✅ UML Class Diagram (focus on Core library)
- ✅ UML Sequence Diagram (reservation flow)
- ✅ SCRUM Product Backlog with User Stories
- ✅ Report (max 10 pages)
- ✅ GitHub repository README

---

## ✅ Constitution Compliance

The specification follows ALL constitution principles:

1. ✅ **Assignment requirements first** - All features from assignment PDFs
2. ✅ **Required tech stack** - C#, Visual Studio, Razor Pages only
3. ✅ **Forbidden technologies** - No Next.js, React, TypeScript, Tailwind, etc.
4. ✅ **Three-project structure** - Core, ConsoleApp, Web
5. ✅ **Clear layer separation** - UI → Service → Repository
6. ✅ **Core library focus** - All exam code in Class Library
7. ✅ **Useful Console App** - Can demonstrate all features
8. ✅ **Simple architecture** - Appropriate for 1st semester
9. ✅ **In-memory data** - No database required
10. ✅ **Self-written algorithms** - Search and sorting explicitly specified
11. ✅ **SCRUM and UML** - All documentation requirements covered
12. ✅ **Design without overengineering** - Visual inspiration only, no complexity creep
13. ✅ **Local-first** - Runs in Visual Studio on student's PC
14. ✅ **Understandable** - Simple, explainable code throughout

---

## 🎯 Priority Matrix

### CRITICAL (Must-Have for Exam)
1. ⭐ Self-written search algorithm (manual loop)
2. ⭐ Self-written alphabetical city sorting algorithm
3. ⭐ Console App demonstrating all features
4. ⭐ Class Library with Models, Repositories, Services
5. ⭐ Business logic in Service layer (not Razor Pages)
6. ⭐ Custom exceptions for business rules
7. ⭐ UML diagrams (Domain Model, Class, Sequence)
8. ⭐ SCRUM documentation (Backlog, User Stories)
9. ⭐ Code comments explaining business rules
10. ⭐ GitHub public repository

### HIGH (Important)
- Capacity validation (150 total, 10 VIP)
- Reservation validation (future only, capacity check)
- In-memory repositories with seed data
- Clear layer separation
- Razor Pages basic functionality

### MEDIUM (Nice-to-Have)
- Full Razor Pages UI with all pages
- Danish labels throughout
- Bootstrap responsive design
- Custom CSS (burgundy, gold, cream)
- Statistics calculated from seed data

### LOW (Optional)
- Seating chart visualization
- Admin pages for managing data
- News/blog functionality
- Advanced filtering
- Simply.com deployment

---

## 📚 Key Documents Reference

### For Implementation Team:
1. **Read first:** `docs/SPECIFICATION.md` (67 pages, complete requirements)
2. **Architecture guide:** `.speckit.constitution` (principles and constraints)
3. **UI design:** `docs/DESIGN-GUIDELINES.md` (visual implementation)

### For Project Management:
- Section 5: Functional Requirements (all features)
- Section 9: Implementation Priorities (4-week roadmap)
- Section 15.1: Example User Stories (acceptance criteria)

### For Development:
- Section 4: System Architecture (project structure)
- Section 7: Domain Model (C# class definitions)
- Section 8: Technology Stack (allowed/forbidden)

### For Exam Preparation:
- Section 13: Exam Preparation (what to expect, practice scenarios)
- Section 12: Success Criteria (must-have checklist)
- Section 2.4: Required Algorithms (search and sorting)

---

## 🔑 Critical Success Factors

### What Makes This Exam-Ready:

1. **Focus on Class Library:**
   - All exam-relevant code in `CirkusLuna.Core`
   - Models, Repositories, Services, Exceptions
   - UML diagrams cover this only

2. **Console App as Proof:**
   - Exam doesn't focus on Razor Pages
   - Console App must demonstrate everything
   - Menu-driven, tests all features

3. **Self-Written Algorithms:**
   - Explicitly required by assignment
   - Must be able to explain them
   - Not just LINQ/built-in methods

4. **Understandable Code:**
   - "Simple is better than complex" for this assignment
   - Every group member must understand their parts
   - Well-commented business rules

5. **Local Execution:**
   - Must run on student's PC
   - No cloud dependencies
   - In-memory data is sufficient

---

## 🚦 Next Steps

### Ready to Implement:

1. **Setup Visual Studio solution** with three projects
2. **Create domain models** from Section 7 (Domain Model)
3. **Implement self-written algorithms** as specified
4. **Build Console App** following the menu structure
5. **Add Razor Pages** following UI guidelines
6. **Create UML diagrams** from completed code
7. **Write documentation** using specification as source

### Reference Documents:

- **Requirements:** `docs/SPECIFICATION.md`
- **Constraints:** `.speckit.constitution`
- **UI Design:** `docs/DESIGN-GUIDELINES.md`
- **Source PDFs:** `docs/input/`

---

## ✅ Specification Quality Checklist

- ✅ Based on actual assignment PDFs (not assumptions)
- ✅ All functional requirements from case document
- ✅ All technical requirements from opgavebeskrivelse
- ✅ Self-written algorithms explicitly specified
- ✅ Three-project structure detailed
- ✅ Console App requirements clear (exam focus)
- ✅ Layer separation enforced
- ✅ Danish terminology throughout
- ✅ Capacity limits specified (150 total, 10 VIP)
- ✅ Validation rules documented
- ✅ Technology stack constraints clear
- ✅ Implementation roadmap provided
- ✅ Exam preparation guidance included
- ✅ Success criteria defined
- ✅ Example User Stories with Acceptance Criteria
- ✅ Seed data examples for Danish cities
- ✅ Design guidelines (visual only, not complexity)
- ✅ No forbidden technologies introduced
- ✅ Appropriate for 1st semester skill level

---

## 🎓 Assignment Context Summary

**Due Date:** May 28, 2026, 10:00 (Wiseflow)  
**Demo Day:** May 28, 2026, 10:15+  
**Institution:** UCL Erhvervsakademi og Professionshøjskole  
**Class:** 1.B  
**Instructors:** Camilla Mai Ryskjær (CARY) og Jakob Nørager Christensen (JNCH)

**Exam Information:**
- System is foundation for programming exam after 1st semester
- At exam: Students will be asked to extend the system
- System must run on student's own PC
- Focus on Class Library and Console App (not Razor Pages)

**Key Quote from Assignment:**
> "En simpel implementering kan virke som et lige så godt grundlag for eksamen, som en meget avanceret løsning. Det er vigtigt, at alle har indblik og forståelse for det udviklede system."

Translation: "A simple implementation can work just as well as a very advanced solution for the exam. It's important that everyone has insight and understanding of the developed system."

---

## 🎉 Specification Complete!

The Cirkus Luna project now has:

1. ✅ Complete system specification (67 pages)
2. ✅ Project constitution (protection from scope creep)
3. ✅ Design guidelines (visual direction)
4. ✅ Implementation roadmap (4-week plan)
5. ✅ All source documents (PDFs and design reference)

**The specification prioritizes:**
- Assignment requirements over design ambitions
- Class Library and Console App over Razor Pages
- Self-written algorithms over library methods
- Understandability over complexity
- Exam readiness over production polish

**Ready for implementation in Visual Studio with C#, ASP.NET Core Razor Pages, and simple architecture appropriate for 1st semester students.**
