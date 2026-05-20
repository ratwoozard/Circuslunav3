# Cirkus Luna - Design Guidelines

**Important:** These guidelines support the visual implementation of the Razor Pages UI. They are subordinate to the `.speckit.constitution` and must not compromise the educational objectives of the project.

---

## Design Philosophy

**Primary Goal:** Create a clean, functional, and visually appealing UI that demonstrates good Razor Pages development while remaining explainable and realistic for a 1st semester project.

**NOT the Goal:** Build a production-ready startup website, marketing showcase, or enterprise SaaS application.

---

## Visual Direction

### Color Palette

Inspired by `docs/input/design-reference.png`:

**Primary Colors:**
- **Dark Red / Burgundy:** `#8B1C1C` (backgrounds, accents)
- **Deep Purple:** `#4A1F4A` (gradient backgrounds, optional)
- **Warm Yellow/Gold:** `#F4C542` (primary CTA buttons, highlights)

**Neutral Colors:**
- **Cream/Ivory:** `#FAF8F3` (content surfaces, cards)
- **White:** `#FFFFFF` (text on dark backgrounds)
- **Dark Gray:** `#2B2B2B` (text on light backgrounds)
- **Light Gray:** `#E5E5E5` (borders, dividers)

**Accent Colors:**
- **Red (badges):** `#D32F2F` (for date badges, alerts)
- **Green (success):** `#4CAF50` (available seats, confirmations)

### Typography

Use system fonts or Bootstrap defaults:

```css
font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
```

**Hierarchy:**
- **H1 (Hero):** 2.5rem, bold, white on dark background
- **H2 (Section Headings):** 2rem, semi-bold
- **H3 (Card Titles):** 1.5rem, semi-bold
- **Body Text:** 1rem, normal weight
- **Small Text:** 0.875rem (labels, metadata)

### Layout

Use **Bootstrap 5 grid system** for all layouts:

- **Container:** `.container` or `.container-fluid`
- **Grid:** `.row` and `.col-*` classes
- **Spacing:** Bootstrap spacing utilities (`mt-3`, `mb-4`, `py-5`, etc.)

**Do not introduce custom grid systems or complex CSS frameworks.**

---

## Component Patterns

### 1. Navigation

Simple Bootstrap navbar with Danish labels:

```
Cirkus Luna | Forside | Forestillinger | Turnéplan | Reservationer | [Køb Billet Button]
```

**Implementation:**
- Use Bootstrap `.navbar` with `.navbar-expand-lg`
- Dark background (`bg-dark` or custom `bg-burgundy`)
- Yellow/gold button for primary CTA

### 2. Hero Section

**Simple hero with:**
- Dark red to purple gradient background (optional, CSS only)
- Centered heading: "Cirkus Luna"
- Subtitle with tour dates and tagline
- Optional star rating (if relevant to assignment)
- Two CTA buttons: "Se Turnéplan" (primary yellow) and "Bestil Billetter" (secondary outlined)

**Keep it simple:** No parallax, no video backgrounds, no complex animations.

### 3. Statistics Cards (Optional)

If statistics are shown, they **must** be real:

```csharp
@inject IForestillingService ForestillingService

@{
    var totalCities = ForestillingService.GetUniqueCities().Count;
    var totalShows = ForestillingService.GetAllForestillinger().Count;
    var totalArtists = 8; // If you track artists in your model
}

<div class="row text-center my-5">
    <div class="col-md-3">
        <h3>@totalCities</h3>
        <p>Byer på Turnéen</p>
    </div>
    <div class="col-md-3">
        <h3>@totalShows</h3>
        <p>Forestillinger</p>
    </div>
    <!-- etc -->
</div>
```

**Rule:** No fake numbers. If you don't have the data, don't show the statistic.

### 4. Performance Cards

Bootstrap card layout for each performance:

**Card structure:**
- Date badge (top-left corner, red background)
- Performance title
- City and venue
- Available seats (calculated, not hardcoded)
- VIP seats available (if applicable)
- Two action buttons: "Se mere" (info) and "Reserver" (primary yellow)

**Example:**

```html
<div class="card mb-4">
    <div class="card-body position-relative">
        <span class="badge bg-danger position-absolute top-0 start-0 m-2">3 MAR</span>
        <h5 class="card-title mt-3">København</h5>
        <p class="card-text">
            <i class="bi bi-geo-alt"></i> Cirkus Bygningen<br>
            <i class="bi bi-people"></i> @Model.LedigePladser / @Model.Kapacitet ledige pladser
        </p>
        <a href="/Forestillinger/Details?id=@Model.Id" class="btn btn-outline-secondary">Se mere</a>
        <a href="/Reservationer/Create?forestillingId=@Model.Id" class="btn btn-warning">Reserver</a>
    </div>
</div>
```

### 5. Forms

Use Bootstrap form controls:

```html
<form method="post">
    <div class="mb-3">
        <label asp-for="Navn" class="form-label">Navn</label>
        <input asp-for="Navn" class="form-control" />
        <span asp-validation-for="Navn" class="text-danger"></span>
    </div>
    
    <div class="mb-3">
        <label asp-for="Email" class="form-label">Email</label>
        <input asp-for="Email" type="email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>
    
    <div class="form-check mb-3">
        <input asp-for="ErVIP" class="form-check-input" type="checkbox" />
        <label asp-for="ErVIP" class="form-check-label">VIP Reservation</label>
    </div>
    
    <button type="submit" class="btn btn-warning">Reserver Billet</button>
</form>
```

### 6. Tables (for lists and admin views)

Use Bootstrap table classes:

```html
<table class="table table-striped table-hover">
    <thead class="table-dark">
        <tr>
            <th>Dato</th>
            <th>By</th>
            <th>Lokation</th>
            <th>Ledige Pladser</th>
            <th>Handlinger</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var forestilling in Model.Forestillinger)
        {
            <tr>
                <td>@forestilling.Dato.ToString("dd/MM/yyyy")</td>
                <td>@forestilling.Lokation.By</td>
                <td>@forestilling.Lokation.Navn</td>
                <td>@forestilling.LedigePladser</td>
                <td>
                    <a href="/Forestillinger/Details?id=@forestilling.Id" class="btn btn-sm btn-outline-primary">Detaljer</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```

### 7. Alerts and Messages

Bootstrap alerts for feedback:

```html
@if (TempData["Success"] != null)
{
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        @TempData["Success"]
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
}

@if (TempData["Error"] != null)
{
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        @TempData["Error"]
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
}
```

---

## Custom CSS Guidelines

Keep custom CSS in `wwwroot/css/site.css`:

### Example Custom Styles

```css
/* Color scheme variables */
:root {
    --burgundy: #8B1C1C;
    --purple: #4A1F4A;
    --gold: #F4C542;
    --cream: #FAF8F3;
    --dark-gray: #2B2B2B;
}

/* Custom button styles */
.btn-warning {
    background-color: var(--gold);
    border-color: var(--gold);
    color: var(--dark-gray);
    font-weight: 600;
}

.btn-warning:hover {
    background-color: #E0B035;
    border-color: #E0B035;
}

/* Burgundy background helper */
.bg-burgundy {
    background-color: var(--burgundy) !important;
}

/* Card styling */
.card {
    border: none;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    background-color: var(--cream);
}

/* Hero section */
.hero {
    background: linear-gradient(135deg, var(--burgundy) 0%, var(--purple) 100%);
    color: white;
    padding: 4rem 2rem;
    text-align: center;
}

.hero h1 {
    font-size: 3rem;
    font-weight: bold;
    margin-bottom: 1rem;
}

.hero p {
    font-size: 1.2rem;
    margin-bottom: 2rem;
}

/* Optional: Festive striped border */
.circus-border {
    height: 20px;
    background: repeating-linear-gradient(
        90deg,
        #D32F2F 0px,
        #D32F2F 30px,
        #FFFFFF 30px,
        #FFFFFF 60px,
        #F4C542 60px,
        #F4C542 90px
    );
}
```

**Rule:** Keep CSS simple and understandable. Avoid complex animations, transforms, or vendor-specific prefixes unless necessary.

---

## Danish Labels Reference

Use these throughout the UI:

### Navigation & General
- Forside = Home
- Forestillinger = Performances
- Turnéplan = Tour Schedule
- Reservationer = Reservations
- Søg = Search
- Køb Billet = Buy Ticket
- Se Turnéplan = View Tour Schedule
- Reserver Billet = Reserve Ticket

### Performance Details
- Dato = Date
- Tidspunkt = Time
- By = City
- Lokation = Location/Venue
- Kapacitet = Capacity
- Ledige Pladser = Available Seats
- VIP-pladser = VIP Seats
- Pris = Price

### Reservation Form
- Navn = Name
- Email = Email
- Telefon = Phone
- Antal Billetter = Number of Tickets
- VIP Reservation = VIP Reservation
- Bekræft Reservation = Confirm Reservation

### Status & Feedback
- Succes = Success
- Fejl = Error
- Ikke Tilgængelig = Not Available
- Udsolgt = Sold Out
- Bekræftet = Confirmed
- Annulleret = Cancelled

---

## Responsive Design

Use Bootstrap responsive utilities:

```html
<!-- Stack on mobile, 2 columns on tablet, 3 on desktop -->
<div class="row">
    <div class="col-12 col-md-6 col-lg-4">
        <!-- Card -->
    </div>
</div>

<!-- Hide on small screens -->
<div class="d-none d-md-block">
    <!-- Advanced features -->
</div>

<!-- Different layout for mobile -->
<div class="d-block d-md-none">
    <!-- Simplified mobile view -->
</div>
```

**Rule:** Ensure the site is usable on mobile, tablet, and desktop. Use Bootstrap breakpoints, not custom media queries.

---

## What NOT to Do

### ❌ Forbidden UI Elements:

1. **No complex JavaScript frameworks** (React, Vue, Angular)
2. **No advanced animations** (parallax, scroll-triggered animations, GSAP)
3. **No video backgrounds** or auto-playing media
4. **No third-party component libraries** (beyond Bootstrap)
5. **No fake testimonials**, "trusted by X companies", or marketing fluff
6. **No complex dashboards** with charts/graphs (unless explicitly required)
7. **No "Sign up for our newsletter"** or growth-hacking elements
8. **No cookie banners, GDPR notices** (out of scope for assignment)
9. **No loading spinners** unless truly necessary (in-memory data is instant)
10. **No "coming soon"** or placeholder sections

### ❌ Avoid Generic AI Copy:

**Bad (generic AI):**
> "Experience the magic of Cirkus Luna, where dreams come alive and memories are made. Join us for an unforgettable journey through the world of wonder and amazement!"

**Good (simple and factual):**
> "Cirkus Luna kommer til hele Danmark i sæson 2026. Se vores turnéplan og reserver dine billetter."

---

## Implementation Checklist

Before considering the UI complete:

- [ ] All labels are in Danish
- [ ] Statistics (if shown) are calculated from real seed data
- [ ] Forms use Bootstrap form controls with validation
- [ ] Cards use Bootstrap card components
- [ ] Navigation uses Bootstrap navbar
- [ ] Colors match the defined palette
- [ ] Layout uses Bootstrap grid system
- [ ] Custom CSS is in `wwwroot/css/site.css`
- [ ] No JavaScript frameworks beyond Bootstrap's JS
- [ ] Responsive on mobile, tablet, and desktop
- [ ] No marketing fluff or fake content
- [ ] UI looks professional but realistic for a school project

---

## Priority Reminder

**The UI is not the primary deliverable.** The focus should be on:

1. **CirkusLuna.Core** - Class Library with business logic
2. **Console App** - Demonstration of core features
3. **Self-written algorithms** - Search and sorting
4. **Repository and Service layers** - Clean architecture
5. **Exception handling** - Business rule validation
6. **Documentation** - UML and Scrum artifacts

The UI should be clean and functional, but **not at the expense of the core programming work**.

---

## Summary

These design guidelines ensure the UI is:
- ✅ Visually appealing and professional
- ✅ Realistic for a 1st semester project
- ✅ Easy to implement with Bootstrap + custom CSS
- ✅ Danish throughout
- ✅ Based on real data, not fake marketing content
- ✅ Subordinate to the core programming objectives

When in doubt, **keep it simple**.
