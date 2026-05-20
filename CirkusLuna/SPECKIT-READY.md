# ✅ Cirkus Luna - Pre-SpecKit Checklist

**Status:** Ready for SpecKit generation  
**Date:** May 20, 2026  
**Project Type:** 1st Semester C# Assignment

---

## ✅ Setup Complete

### 1. Directory Structure Created

```
CirkusLuna/
├── .speckit.constitution                       ✅ Created & validated
├── README.md                                   ✅ Created
└── docs/
    ├── DESIGN-GUIDELINES.md                    ✅ Created
    └── input/
        ├── cirkus-luna-case.pdf                ✅ Copied
        ├── cirkus-luna-opgavebeskrivelse.pdf   ✅ Copied
        ├── design-reference.png                ✅ Copied
        └── README.md                           ✅ Created
```

### 2. Next.js Cleanup

✅ No Next.js-related files found - workspace is clean

### 3. Constitution Created

✅ `.speckit.constitution` with 14 core principles:

1. Assignment requirements first
2. Required technology stack (C#, Razor Pages, Visual Studio)
3. Forbidden technologies (Next.js, React, TypeScript, etc.)
4. Three-project solution structure
5. Clear layer separation
6. Core library focus
7. Console App must be useful
8. Simple and explainable architecture
9. Data strategy (in-memory)
10. Required self-written algorithms
11. Documentation and Scrum
12. Design quality without overengineering ⭐ (updated with Danish labels and data constraints)
13. Local-first exam readiness
14. Understandability

### 4. Design Guidelines Created

✅ `docs/DESIGN-GUIDELINES.md` includes:

- Color palette (burgundy, purple, gold, cream)
- Typography guidelines
- Component patterns (nav, hero, cards, forms, tables)
- Danish labels reference
- Custom CSS examples
- Responsive design guidelines
- What NOT to do list

### 5. Important Constraints Documented

✅ **Design Reference Usage:**
- Visual inspiration only (colors, layout style)
- NOT a spec for complexity
- Statistics must be calculated from real seed data
- Danish labels throughout
- No marketing fluff or fake content
- Realistic for 1st semester project

---

## 🎯 SpecKit Generation Objectives

When SpecKit runs, it should generate:

### **1. Visual Studio Solution Structure**

```
CirkusLuna.sln
├── CirkusLuna.Core/                    (Class Library)
│   ├── Models/
│   │   ├── Forestilling.cs
│   │   ├── Lokation.cs
│   │   ├── Reservation.cs
│   │   └── (other domain models)
│   ├── Interfaces/
│   │   ├── IForestillingRepository.cs
│   │   ├── ILokationRepository.cs
│   │   └── IReservationRepository.cs
│   ├── Repositories/
│   │   ├── InMemoryForestillingRepository.cs
│   │   ├── InMemoryLokationRepository.cs
│   │   └── InMemoryReservationRepository.cs
│   ├── Services/
│   │   ├── ForestillingService.cs
│   │   ├── ReservationService.cs
│   │   └── (business logic services)
│   ├── Exceptions/
│   │   ├── ReservationFullException.cs
│   │   ├── InvalidVIPReservationException.cs
│   │   └── (custom exceptions)
│   └── Validation/
│       └── (validation logic if needed)
│
├── CirkusLuna.ConsoleApp/              (Console Application)
│   └── Program.cs
│       ├── Menu-driven interface
│       ├── Search demonstrations
│       ├── Sorting demonstrations
│       ├── Reservation creation
│       ├── Exception handling examples
│       └── All core feature tests
│
└── CirkusLuna.Web/                     (ASP.NET Core Web App)
    ├── Pages/
    │   ├── Index.cshtml + .cs          (Homepage)
    │   ├── Forestillinger/
    │   │   ├── Index.cshtml            (List performances)
    │   │   └── Details.cshtml          (Performance details)
    │   ├── Turnéplan/
    │   │   └── Index.cshtml            (Tour schedule sorted by city)
    │   └── Reservationer/
    │       ├── Create.cshtml           (New reservation)
    │       └── Confirmation.cshtml     (Reservation confirmed)
    ├── wwwroot/
    │   ├── css/
    │   │   └── site.css                (Custom CSS following design guidelines)
    │   ├── js/
    │   │   └── site.js                 (Minimal JS, Bootstrap only)
    │   └── lib/                        (Bootstrap 5)
    └── Program.cs                      (Startup configuration)
```

### **2. Core Features to Implement**

#### **A. Models (CirkusLuna.Core/Models/)**

- `Forestilling` (Performance)
  - Properties: Id, Titel, Dato, Tidspunkt, Lokation, Kapacitet, ReserveredeAntal, etc.
- `Lokation` (Venue)
  - Properties: Id, Navn, By, Adresse, etc.
- `Reservation` (Booking)
  - Properties: Id, ForestillingId, Navn, Email, AntalBilletter, ErVIP, etc.

#### **B. Repositories (CirkusLuna.Core/Repositories/)**

Must include:
- ✅ In-memory data storage with seed data
- ✅ Self-written search algorithm (manual loop, not just LINQ)
- ✅ Self-written alphabetical city sorting (bubble/selection/insertion sort)
- ✅ CRUD operations
- ✅ Capacity checks
- ✅ VIP validation

#### **C. Services (CirkusLuna.Core/Services/)**

Business logic:
- ✅ Reservation validation
- ✅ Capacity management
- ✅ VIP eligibility rules
- ✅ Search orchestration
- ✅ Sorting orchestration

#### **D. Console App (CirkusLuna.ConsoleApp/)**

Menu-driven interface with:
1. Search performances by city
2. Display all cities alphabetically sorted
3. View performance details
4. Create reservation (regular)
5. Create VIP reservation
6. Check available seats
7. Exit

Must demonstrate ALL core features without web UI.

#### **E. Razor Pages Web App (CirkusLuna.Web/)**

Pages needed:
- **Homepage** (Index.cshtml)
  - Hero with circus branding
  - Optional stats (calculated from seed data)
  - Featured performances
  - CTA buttons

- **Forestillinger** (Performances listing)
  - List all performances
  - Search/filter by city
  - Show available seats
  - Links to details and reservation

- **Turnéplan** (Tour schedule)
  - Performances sorted alphabetically by city
  - Date display
  - Venue information

- **Reservation Create**
  - Form with name, email, antal billetter
  - VIP checkbox
  - Validation
  - Capacity check
  - Success/error feedback

### **3. Seed Data Requirements**

The `InMemoryForestillingRepository` must seed realistic data:

**Minimum 8-13 performances across multiple Danish cities:**
- København
- Aarhus
- Odense
- Aalborg
- Esbjerg
- Roskilde
- Kolding
- Horsens

**Each performance needs:**
- Date (spread across 2026)
- Time
- Venue/location
- Capacity (e.g., 150-300 seats)
- Some with partial reservations (to show available seats)

### **4. Self-Written Algorithms Required**

#### **Search Algorithm Example:**

```csharp
public List<Forestilling> SearchByCity(string city)
{
    List<Forestilling> results = new List<Forestilling>();
    
    foreach (var forestilling in _forestillinger)
    {
        if (forestilling.Lokation.By.Equals(city, StringComparison.OrdinalIgnoreCase))
        {
            results.Add(forestilling);
        }
    }
    
    return results;
}
```

#### **Sorting Algorithm Example:**

```csharp
public List<string> GetCitiesSortedAlphabetically()
{
    List<string> cities = GetAllCities();
    
    // Bubble sort
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

### **5. Business Rules to Implement**

1. **Capacity Validation**
   - Cannot reserve more seats than available
   - Throw `ReservationFullException` if capacity exceeded

2. **VIP Rules**
   - VIP guests might have special requirements (define based on assignment PDFs)
   - Validate VIP eligibility
   - Throw `InvalidVIPReservationException` if rules violated

3. **Reservation Rules**
   - Require name and email
   - Minimum 1 ticket per reservation
   - Maximum tickets per reservation (e.g., 10)

### **6. UI Design Specifications**

Follow `docs/DESIGN-GUIDELINES.md`:

**Colors:**
- Burgundy `#8B1C1C` (backgrounds)
- Gold `#F4C542` (primary buttons)
- Cream `#FAF8F3` (content surfaces)

**Danish Labels Throughout:**
- "Forestillinger", "Turnéplan", "Reserver Billet", "Ledige Pladser", etc.

**Bootstrap 5 + Custom CSS:**
- Use Bootstrap grid, cards, forms, navbar
- Custom CSS in `wwwroot/css/site.css`

**Statistics Must Be Real:**
```csharp
@{
    var totalCities = ForestillingService.GetUniqueCities().Count;
    var totalShows = ForestillingService.GetAllForestillinger().Count;
}
```

---

## 🚫 What SpecKit Must NOT Do

### **Forbidden Technologies:**
- ❌ Next.js, React, Vue, Angular
- ❌ TypeScript
- ❌ Tailwind CSS
- ❌ Supabase, Vercel
- ❌ Entity Framework (unless explicitly needed)
- ❌ Docker, microservices

### **Forbidden UI Elements:**
- ❌ Fake statistics or hardcoded marketing numbers
- ❌ Generic AI marketing copy
- ❌ Complex animations or JavaScript frameworks
- ❌ SaaS/startup aesthetics
- ❌ Dashboard with charts (unless required)
- ❌ "Sign up for newsletter" sections
- ❌ Loading spinners (in-memory data is instant)

### **Forbidden Complexity:**
- ❌ Dependency injection containers (manual constructor injection only)
- ❌ Advanced design patterns (CQRS, Mediator, etc.)
- ❌ Reflection or metaprogramming
- ❌ Complex LINQ that's hard to explain

---

## 📋 Documentation Requirements

After code generation, SpecKit should scaffold:

### **UML Diagrams** (in `docs/uml/`)
1. **Domain Model** - Conceptual relationships
2. **Class Diagram** - Focus on CirkusLuna.Core
3. **Sequence Diagram** - E.g., reservation flow

### **Scrum Documentation** (in `docs/scrum/`)
1. **Product Backlog** - Prioritized features
2. **User Stories** - With Acceptance Criteria
3. **Sprint Plan** (template)

---

## ✅ Ready to Run SpecKit

All prerequisites are in place:

- ✅ Constitution defines constraints
- ✅ Design guidelines provide visual direction
- ✅ Assignment PDFs are in `docs/input/`
- ✅ Design reference is in `docs/input/`
- ✅ Workspace is clean (no Next.js files)

### **Next Command:**

```bash
# From workspace root
speckit generate --constitution CirkusLuna/.speckit.constitution --input CirkusLuna/docs/input/
```

---

## 🎯 Success Criteria

The generated project is successful if:

1. ✅ Three Visual Studio projects (Core, ConsoleApp, Web)
2. ✅ All code is C# / ASP.NET Core Razor Pages
3. ✅ Console App can demonstrate all features
4. ✅ Self-written search and sorting algorithms exist
5. ✅ Business logic is in Core library, not in Razor Pages
6. ✅ UI uses Danish labels
7. ✅ Statistics are calculated from seed data
8. ✅ No forbidden technologies
9. ✅ Code is simple and explainable
10. ✅ Runs locally in Visual Studio

---

## 🔑 Key Reminders

**Priority Order:**
1. Core library (Models, Repositories, Services)
2. Console App demonstration
3. Self-written algorithms
4. Razor Pages functionality
5. UI design (last priority)

**Philosophy:**
- Simple > Complex
- Explainable > Clever
- Assignment requirements > Modern trends
- Educational value > Production readiness

**Goal:**
Demonstrate 1st semester C# programming skills in a clear, maintainable way.

---

**Ready for SpecKit generation! 🚀**
