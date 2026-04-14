# Frontend Design Document
## University & Professor Rating Platform

**Capstone II Project — Fall 2024**

**Prepared By:** Osama Jankoot • Anas Ibraiwish • Saif Hamadeh

**Supervised By:** Eng. Fadia Alaeddin

---

## Table of Contents

1. Design Philosophy & Visual Identity
   - 1.1 Color Palette
   - 1.2 Typography System
   - 1.3 Spacing & Grid System
   - 1.4 Iconography & Visual Elements
2. Page Designs & Wireframes
   - 2.1 Authentication Pages (Login / Sign Up)
   - 2.2 Home Page
   - 2.3 University Module
   - 2.4 Professor Module
   - 2.5 Comparison Pages
   - 2.6 FAQ Page
3. Component Library
   - 3.1 Navigation Bar
   - 3.2 Search Bar Component
   - 3.3 Rating Cards
   - 3.4 Review Cards
   - 3.5 Comparison Table
   - 3.6 Charts & Visualizations
   - 3.7 Buttons & Form Elements
4. Responsive Design Strategy
5. User Flow Diagrams
6. Interaction & Animation Design
7. Accessibility Standards

---

## 1. Design Philosophy & Visual Identity

The platform's design is built around the core principle of **clarity-driven trust**. Students are making life-impacting decisions, so every visual element must instill confidence, reduce cognitive load, and surface the right information at the right moment. The aesthetic direction is **clean, modern, and data-forward** — combining sharp card-based layouts with generous whitespace, a strong typographic hierarchy, and subtle data visualizations that make ratings and comparisons instantly understandable.

| Principle | Description |
|---|---|
| Clarity First | Information hierarchy guides the eye — ratings, scores, and key metrics are immediately visible without scrolling. |
| Trust Through Transparency | Verified badges, sentiment indicators, and consistent rating categories build user confidence. |
| Effortless Comparison | Side-by-side layouts, consistent data presentation, and interactive charts make comparing options intuitive. |
| Mobile-Native Thinking | Every component is designed mobile-first, then scaled up — not the reverse. |
| Accessible by Default | WCAG 2.1 AA compliant contrast ratios, keyboard navigation, and screen-reader support are built in. |

---

### 1.1 Color Palette

The color system uses a professional navy-blue primary with high-energy accent colors for interactive elements. Semantic colors (green, amber, red) are reserved for ratings, sentiment indicators, and system feedback states.

| Role | Hex Code | Usage |
|---|---|---|
| Primary | `#1B2A4A` | Headers, nav, primary text |
| Accent Blue | `#3B82F6` | Buttons, links, active states |
| Accent Light | `#DBEAFE` | Card highlights, hover bg |
| Success / Positive | `#10B981` | Positive sentiment, verified |
| Warning / Neutral | `#F59E0B` | Neutral sentiment, caution |
| Danger / Negative | `#EF4444` | Negative sentiment, errors |
| Gray 700 | `#374151` | Body text |
| Gray 100 | `#F3F4F6` | Page backgrounds, cards |
| White | `#FFFFFF` | Card surfaces, inputs |

---

### 1.2 Typography System

The type system pairs **Poppins** (headings) with **Inter** (body text) for a clean, approachable yet professional feel. The scale follows a modular ratio of 1.25 to create clear visual hierarchy across the application.

| Element | Font / Weight | Size | Line Height | Use Case |
|---|---|---|---|---|
| Display | Poppins Bold | 32px | 1.2 | Page titles, hero text |
| H1 | Poppins SemiBold | 24px | 1.3 | Section headings |
| H2 | Poppins Medium | 20px | 1.3 | Card titles, sub-sections |
| H3 | Poppins Medium | 16px | 1.4 | Component headers |
| Body | Inter Regular | 14px | 1.6 | Paragraphs, descriptions |
| Body Small | Inter Regular | 12px | 1.5 | Labels, captions |
| Button | Inter SemiBold | 14px | 1.0 | Button labels, links |
| Caption | Inter Regular | 11px | 1.4 | Helper text, timestamps |

---

### 1.3 Spacing & Grid System

The layout uses a **12-column grid** with responsive breakpoints. The base spacing unit is **4px**, with all margins and padding using multiples of this unit for visual consistency. Maximum content width is **1280px** for desktop, centered with auto margins.

| Breakpoint | Width | Columns | Gutter | Margin |
|---|---|---|---|---|
| Mobile | < 640px | 4 | 16px | 16px |
| Tablet | 640–1024px | 8 | 20px | 24px |
| Desktop | 1024–1280px | 12 | 24px | 32px |
| Wide | > 1280px | 12 | 24px | auto (centered) |

---

### 1.4 Iconography & Visual Elements

Icons use the **Lucide** icon set (open-source, consistent 24px grid, 1.5px stroke). All icons are rendered as SVG for crisp scaling. Rating stars use a custom 5-star component with half-star precision. Sentiment badges use color-coded pill shapes with micro-icons.

| Category | Icons Used | Style |
|---|---|---|
| Navigation | Home, Search, User, Menu, ChevronDown | Outline, 24px |
| Ratings | Star, StarHalf, ThumbsUp, TrendingUp | Filled for active, outline for inactive |
| University | Building, MapPin, Shield, Coffee, Smile | Outline, 20px in cards |
| Professor | GraduationCap, BookOpen, MessageCircle, Heart | Outline, 20px in cards |
| Sentiment | SmilePlus, Meh, Frown | Filled, color-coded (green/amber/red) |
| Actions | Plus, Filter, ArrowRight, ExternalLink, Copy | Outline, 16–20px |

---

## 2. Page Designs & Wireframes

This section presents the layout structure and key design decisions for each page in the platform. Wireframes illustrate spatial composition, component placement, and information hierarchy.

---

### 2.1 Authentication Pages (Login / Sign Up)

The authentication flow uses a **split-screen layout** on desktop: the left panel shows a branded illustration with a motivational tagline, while the right panel contains the form. On mobile, the illustration collapses into a compact header above the form.

**Login Page — Desktop Layout:**

```
┌─────────────────────────────────────────────────────────────────┐
│                                                                 │
│  ┌────────────────────────┐  ┌────────────────────────────────┐ │
│  │                        │  │                                │ │
│  │  Branded Illustration  │  │        Login Form              │ │
│  │  Panel                 │  │                                │ │
│  │  (Gradient bg +        │  │  Email: [________________]     │ │
│  │   tagline)             │  │  Password: [______________]    │ │
│  │                        │  │                                │ │
│  │  "Make Informed        │  │  [       Log In        ]       │ │
│  │   Choices for Your     │  │                                │ │
│  │   Future"              │  │  ── or continue with ──        │ │
│  │                        │  │  [     Google OAuth    ]       │ │
│  │                        │  │                                │ │
│  └────────────────────────┘  └────────────────────────────────┘ │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

| Element | Design Detail |
|---|---|
| Email Input | University email validation (.edu / institution domain). Placeholder: `your.name@university.edu` |
| Password Input | Show/hide toggle, strength indicator on sign-up. Minimum 8 characters. |
| Submit Button | Full-width, Primary blue (`#3B82F6`), 48px height, rounded-lg, bold white text. |
| Social Login | Optional: Google OAuth button below the divider line ("or continue with"). |
| Role Toggle | Sign-up page includes Student / Professor toggle selector at the top of the form. |
| Error States | Inline red text below fields. Toast notification for server errors. |

---

### 2.2 Home Page

The home page acts as the **gateway** — designed for immediate action. A prominent hero section contains the central search bar. Below it, featured universities and top-rated professors are displayed in scrollable card rows. A stats counter section provides social proof.

**Home Page — Desktop Layout:**

```
┌─────────────────────────────────────────────────────────────────┐
│  Logo          Search Bar              Login / Sign Up          │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│              Hero Section — Gradient Background                 │
│                                                                 │
│          "Find Your Perfect University & Professor"             │
│                                                                 │
│              ┌────────────────────────────────┐                 │
│              │  🔍 Search universities...     │                 │
│              └────────────────────────────────┘                 │
│                                                                 │
│         [Universities]  [Professors]  [Programmes]              │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│   ┌──────────────┐  ┌──────────────┐  ┌──────────────┐         │
│   │  150+         │  │  500+         │  │  10,000+      │       │
│   │  Universities │  │  Professors   │  │  Reviews       │      │
│   └──────────────┘  └──────────────┘  └──────────────┘         │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│  Featured Universities — Horizontal Card Scroll ──►            │
│  ┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐             │
│  │  Uni 1  │ │  Uni 2  │ │  Uni 3  │ │  Uni 4  │             │
│  │  ★★★★☆  │ │  ★★★★★  │ │  ★★★☆☆  │ │  ★★★★☆  │             │
│  └─────────┘ └─────────┘ └─────────┘ └─────────┘             │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│  Top Rated Professors                                          │
│  ┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐             │
│  │  Prof 1 │ │  Prof 2 │ │  Prof 3 │ │  Prof 4 │             │
│  │  4.8/5  │ │  4.7/5  │ │  4.6/5  │ │  4.5/5  │             │
│  └─────────┘ └─────────┘ └─────────┘ └─────────┘             │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│  Footer: About | Contact | FAQ | Privacy          © 2024       │
└─────────────────────────────────────────────────────────────────┘
```

| Section | Content & Behavior |
|---|---|
| Navigation | Fixed top bar. Logo left, search center (expandable), auth/profile right. Becomes hamburger on mobile. |
| Hero Section | Full-width gradient bg (navy to dark blue). Tagline in Poppins Bold 32px. Search bar: 600px max width, rounded-full, shadow-lg. |
| Quick Filters | Pill-shaped filter buttons below search: "Universities", "Professors", "Programmes". Toggle active state. |
| Stats Counter | Three-column stat cards with animated count-up: total universities, professors, reviews. Icon + number + label. |
| Featured Cards | Horizontal scroll of university cards. Each card: thumbnail, name, location, overall rating stars, review count. |
| Top Professors | Grid of professor cards: avatar placeholder, name, department, rating, "Would Take Again" percentage. |
| Footer | Links: About, Contact, FAQ, Privacy. Social icons. Copyright notice. |

---

### 2.3 University Module

The university module consists of two key views: the **university search/listing page** and the **university profile page**. The profile page is the richest view in the platform, combining category ratings, sentiment summary, charts, and student reviews.

#### University Search Page

```
┌─────────────────────────────────────────────────────────────────┐
│  Nav Bar                                                        │
├─────────────────────────────────────────────────────────────────┤
│  🔍 Search Bar    │  City Filter ▼  │  Sort By ▼               │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐          │
│  │  University   │  │  University   │  │  University   │        │
│  │  Card 1       │  │  Card 2       │  │  Card 3       │       │
│  │  ★★★★☆ 4.2   │  │  ★★★★★ 4.8   │  │  ★★★☆☆ 3.5   │       │
│  │  📍 Amman     │  │  📍 Irbid     │  │  📍 Zarqa     │       │
│  │  124 reviews  │  │  89 reviews   │  │  56 reviews   │       │
│  └──────────────┘  └──────────────┘  └──────────────┘          │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

#### University Profile Page

The profile page uses a **two-column layout** on desktop: the left column (65%) contains detailed category ratings and student reviews, while the right column (35%) shows the overall score card, sentiment summary, and a quick comparison CTA.

```
┌─────────────────────────────────────────────────────────────────┐
│  Nav Bar                                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  University Name          📍 Location    ★★★★☆ 4.2   156 reviews│
│                                          [ Write a Review ]     │
│                                                                 │
├──────────────────────────────────┬──────────────────────────────┤
│                                  │                              │
│  Category Ratings Grid           │  Overall Score Card          │
│                                  │  ┌────────────────────┐     │
│  Location      ████████░░  4.1   │  │     4.2 / 5.0      │     │
│  Reputation    █████████░  4.5   │  │   ★★★★☆            │     │
│  Safety        ███████░░░  3.8   │  │   156 reviews       │     │
│  Food          ██████░░░░  3.2   │  └────────────────────┘     │
│  Facilities    ████████░░  4.0   │                              │
│  Clubs         ███████░░░  3.6   │  Sentiment Summary           │
│  Happiness     █████████░  4.4   │  🟢 Positive: 68%           │
│  Internet      ██████░░░░  3.3   │  🟡 Neutral:  22%           │
│  Opportunities ████████░░  4.2   │  🔴 Negative: 10%           │
│  Social Life   ███████░░░  3.7   │                              │
│                                  │  [ Compare University ]      │
├──────────────────────────────────┴──────────────────────────────┤
│                                                                 │
│  Student Reviews                Sort: Most Recent ▼             │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │  ★★★★★  🟢 Positive                     2 weeks ago    │   │
│  │  "Great campus and very supportive professors..."       │   │
│  │  ✓ Verified Student              👍 12                  │   │
│  └─────────────────────────────────────────────────────────┘   │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │  ★★★☆☆  🟡 Neutral                      1 month ago    │   │
│  │  "Decent facilities but WiFi needs improvement..."      │   │
│  │  ✓ Verified Student              👍 5                   │   │
│  └─────────────────────────────────────────────────────────┘   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

#### University Rating Categories — Design Spec

| Category | Icon | Display Style | Description |
|---|---|---|---|
| Location | MapPin | 1-5 stars + bar | Accessibility, surroundings, transport |
| Reputation | Award | 1-5 stars + bar | Academic prestige, rankings |
| Opportunities | Briefcase | 1-5 stars + bar | Career services, internships |
| Happiness | Smile | 1-5 stars + bar | Student satisfaction, well-being |
| Internet | Wifi | 1-5 stars + bar | Campus WiFi quality and coverage |
| Facilities | Building | 1-5 stars + bar | Labs, libraries, classrooms |
| Clubs | Users | 1-5 stars + bar | Student organizations, activities |
| Social Life | Heart | 1-5 stars + bar | Events, community, campus vibe |
| Food | Coffee | 1-5 stars + bar | Cafeteria quality, variety |
| Safety | Shield | 1-5 stars + bar | Campus security, student safety |

---

### 2.4 Professor Module

The professor module is accessed from the university page or via direct search. It features a **department filter sidebar**, professor listing cards, and detailed profile pages with course-filterable reviews.

**Professor Profile — Desktop Layout:**

```
┌─────────────────────────────────────────────────────────────────┐
│  Nav Bar                                                        │
├──────────────────┬──────────────────────────────────────────────┤
│                  │                                              │
│  Professor Info  │  Rating Breakdown                            │
│  ┌────────────┐  │  Difficulty     ████████░░  4.0              │
│  │            │  │  Workload       ██████░░░░  3.2              │
│  │   Avatar   │  │  Communication  █████████░  4.6              │
│  │            │  │  Friendliness   █████████░  4.5              │
│  └────────────┘  │  Clarity        ████████░░  4.1              │
│                  │                                              │
│  Dr. Ahmad Ali   ├──────────────────────────────────────────────┤
│  Dept: CS        │                                              │
│  Courses: 4      │  Sentiment Summary                           │
│                  │  🟢█████████████ 🟡████ 🔴██                │
│  Overall: 4.3/5  │  72% Positive  |  18% Neutral  |  10% Neg   │
│                  │                                              │
│  Would Take      ├──────────────────────────────────────────────┤
│  Again: 85%      │                                              │
│                  │  Course Filter: [All] [CS101] [CS201] [AI301]│
│  [ Compare ]     │                                              │
│                  │  Reviews                                     │
│                  │  ┌──────────────────────────────────────┐    │
│                  │  │ ★★★★★  🟢 Positive   CS101           │    │
│                  │  │ "Excellent professor, very clear..."  │    │
│                  │  │ ✓ Verified            👍 8            │    │
│                  │  └──────────────────────────────────────┘    │
│                  │                                              │
└──────────────────┴──────────────────────────────────────────────┘
```

#### Professor Rating Categories — Design Spec

| Category | Input Type | Display |
|---|---|---|
| Course Selection | Dropdown (required) | Tag shown on review card |
| Would Take Again | Yes / No toggle | Percentage shown on profile |
| Difficulty | 1–5 slider | Horizontal bar chart |
| Workload | 1–5 slider | Horizontal bar chart |
| Friendliness | 1–5 slider | Horizontal bar chart |
| Communication | 1–5 slider | Horizontal bar chart |
| Grade Received | Dropdown (A–F) | Grade distribution chart |
| Comment | Textarea (required) | Review card with sentiment badge |

---

### 2.5 Comparison Pages

Comparison is a core differentiator. Both university and professor comparison pages use a **vertical split layout** showing two entities side-by-side with synchronized category rows. Differences are highlighted with color-coded indicators (green = higher, red = lower).

**University Comparison — Desktop Layout:**

```
┌─────────────────────────────────────────────────────────────────┐
│  Nav Bar                                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│   Select University A ▼        VS       Select University B ▼   │
│                                                                 │
├───────────────────────────────┬─────────────────────────────────┤
│                               │                                 │
│   University A                │   University B                  │
│   ★★★★☆ 4.2                  │   ★★★★★ 4.6                    │
│                               │                                 │
│   Location     ████████░░ 4.1 │   Location     █████████░ 4.5  │
│   Reputation   █████████░ 4.5 │   Reputation   ████████░░ 4.3  │
│   Safety       ███████░░░ 3.8 │   Safety       █████████░ 4.4  │
│   Food         ██████░░░░ 3.2 │   Food         ███████░░░ 3.8  │
│   Facilities   ████████░░ 4.0 │   Facilities   █████████░ 4.7  │
│   Clubs        ███████░░░ 3.6 │   Clubs        ████████░░ 4.2  │
│                               │                                 │
│  🏆 Higher in 2 categories    │  🏆 Higher in 4 categories     │
│                               │                                 │
├───────────────────────────────┴─────────────────────────────────┤
│                                                                 │
│           Radar Chart Overlay — Both Universities               │
│           Superimposed (Using Chart.js or Recharts)             │
│                                                                 │
│                         ╱╲                                      │
│                        ╱  ╲                                     │
│                   ────╱    ╲────                                │
│                  ╱              ╲                               │
│                  ╲              ╱                               │
│                   ────╲    ╱────                                │
│                        ╲  ╱                                     │
│                         ╲╱                                      │
│                                                                 │
│            ── University A    ── University B                   │
│                                                                 │
│                    [ Share Comparison ]                          │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

| Feature | Implementation |
|---|---|
| Selection | Two search-enabled dropdowns. Pre-populated if navigating from a profile page. |
| Category Rows | Each row shows both ratings with colored progress bars. Higher score highlighted green. |
| Radar Chart | Overlay chart comparing all 10 categories. Transparent fill, distinct line colors. |
| Winner Badge | Overall "Recommended" badge appears on the entity with the higher aggregate score. |
| Share/Export | Button to copy comparison link or export as image for sharing. |

---

### 2.6 FAQ Page

An accordion-style FAQ page with categorized sections. Each question expands on click to reveal the answer. A search bar at the top filters questions in real-time. Categories include: General, Reviews, Comparing, Account, and Privacy.

```
┌─────────────────────────────────────────────────────────────────┐
│  Nav Bar                                                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Frequently Asked Questions                                     │
│  🔍 Search questions...                                        │
│                                                                 │
│  [General] [Reviews] [Comparing] [Account] [Privacy]           │
│                                                                 │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │  ▸ How do I create an account?                          │   │
│  ├─────────────────────────────────────────────────────────┤   │
│  │  ▾ How are ratings calculated?                          │   │
│  │    Ratings are calculated as the average of all          │   │
│  │    verified student submissions in each category...      │   │
│  ├─────────────────────────────────────────────────────────┤   │
│  │  ▸ Are reviews really anonymous?                        │   │
│  ├─────────────────────────────────────────────────────────┤   │
│  │  ▸ How do I compare two universities?                   │   │
│  └─────────────────────────────────────────────────────────┘   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

---

## 3. Component Library

All UI components are built as reusable, self-contained modules following the **Atomic Design** methodology. Components are grouped into atoms (buttons, inputs), molecules (search bars, rating displays), and organisms (cards, comparison tables).

---

### 3.1 Navigation Bar

| Property | Desktop | Mobile |
|---|---|---|
| Height | 64px | 56px |
| Background | `#1B2A4A` (Primary) | `#1B2A4A` (Primary) |
| Position | Fixed top, z-index: 50 | Fixed top, z-index: 50 |
| Logo | Left-aligned, 36px height | Left-aligned, 28px height |
| Search | Center, 400px max width, expandable | Hidden; icon toggles overlay |
| Auth/Profile | Right: Login/Sign Up buttons OR avatar dropdown | Hamburger menu |
| Shadow | `0 2px 8px rgba(0,0,0,0.1)` | `0 2px 8px rgba(0,0,0,0.1)` |

---

### 3.2 Search Bar Component

| Property | Specification |
|---|---|
| Width | 100% of container, max-width: 640px |
| Height | 48px (home hero), 40px (nav, pages) |
| Border | `1px solid #D1D5DB`, `2px solid #3B82F6` on focus |
| Border Radius | rounded-full (24px) |
| Placeholder | "Search universities, professors..." in Gray 400 |
| Icon | Search icon (Lucide) left-aligned, 20px, Gray 400 |
| Autocomplete | Dropdown with top 5 matches, icons for type (university/professor) |
| Keyboard | Enter to search, Arrow keys to navigate suggestions, Esc to close |

---

### 3.3 Rating Cards

| Property | University Card | Professor Card |
|---|---|---|
| Dimensions | 320px width, auto height | 280px width, auto height |
| Border | `1px solid #E5E7EB` | `1px solid #E5E7EB` |
| Border Radius | 12px | 12px |
| Shadow | `0 1px 3px rgba(0,0,0,0.1)` | `0 1px 3px rgba(0,0,0,0.1)` |
| Hover | Shadow elevation + 2px Y translate | Shadow elevation + 2px Y translate |
| Header | University name + city tag | Professor name + dept tag |
| Body | Overall stars + top 3 categories | Rating + Would Take Again % |
| Footer | Review count + "View Profile" link | Course count + "View Profile" link |

---

### 3.4 Review Cards

| Element | Design |
|---|---|
| Container | Full-width card, 16px padding, 1px border, 8px radius |
| Sentiment Badge | Pill shape top-right: green (Positive), amber (Neutral), red (Negative) |
| Rating Stars | 5-star display, filled in accent blue |
| Comment Text | Inter Regular 14px, Gray 700, max 3 lines with "Read more" expansion |
| Metadata | Date posted (relative: "2 weeks ago"), verified student badge icon |
| Course Tag | Pill tag showing course name (professor reviews only) |
| Helpful Button | ThumbsUp icon + count, toggleable, subtle hover state |

---

### 3.5 Comparison Table Component

A two-column table where each row represents a rating category. Progress bars visually show the score difference. The winning score in each row is highlighted with a green background tint. The header row shows the entity names with their overall scores.

---

### 3.6 Charts & Visualizations

| Chart Type | Library | Usage |
|---|---|---|
| Radar Chart | Recharts / Chart.js | University comparison overlay (all categories) |
| Bar Chart | Recharts | Category rating breakdowns on profile pages |
| Pie / Donut Chart | Recharts | Sentiment distribution (positive/neutral/negative) |
| Horizontal Bar | CSS + JS | Individual category ratings (inline progress bar style) |
| Line Chart | Recharts | Rating trend over time (semester-by-semester) |
| Grade Distribution | Recharts | Professor grade histogram (A through F) |

---

### 3.7 Buttons & Form Elements

| Element | Variant | Specs |
|---|---|---|
| Primary Button | Filled | bg: `#3B82F6`, text: white, h: 44px, rounded-lg, font: Inter SemiBold 14px |
| Secondary Button | Outlined | border: `1px #3B82F6`, text: `#3B82F6`, h: 44px, rounded-lg |
| Ghost Button | Text only | text: `#3B82F6`, no border, h: 36px, hover: bg `#DBEAFE` |
| Danger Button | Filled | bg: `#EF4444`, text: white, h: 44px, rounded-lg (for destructive actions) |
| Text Input | Standard | h: 44px, border: `1px #D1D5DB`, rounded-md, focus: `2px #3B82F6` |
| Textarea | Standard | min-h: 120px, same border style, resize: vertical |
| Select/Dropdown | Standard | h: 44px, ChevronDown icon right, same border styling |
| Toggle/Switch | Binary | w: 44px, h: 24px, bg: `#D1D5DB` off / `#3B82F6` on, transition 200ms |
| Star Rating Input | Interactive | 5 clickable stars, 28px each, hover: preview state, amber filled |
| Slider (1-5) | Range | Track: `#E5E7EB`, thumb: `#3B82F6`, labels at 1 and 5, step markers |

---

## 4. Responsive Design Strategy

The platform is designed **mobile-first** using Bootstrap's responsive grid system. All layouts adapt fluidly across four breakpoints. Key adaptation patterns are documented below.

| Component | Mobile (< 640px) | Tablet (640–1024px) | Desktop (> 1024px) |
|---|---|---|---|
| Navigation | Hamburger menu + logo | Logo + condensed links | Full nav with search |
| Hero Search | Full-width, 40px height | Centered, 500px width | Centered, 640px width |
| Card Grid | 1 column, full-width | 2 columns | 3 columns |
| University Profile | Single column, stacked | Single column, stacked | 2-column (65/35 split) |
| Comparison | Tabbed (swipe A / B) | Side-by-side (compact) | Side-by-side (full) |
| Reviews | Full-width cards | Full-width cards | Max-width 800px centered |
| Charts | Simplified / smaller | Standard size | Full interactive charts |
| Footer | Stacked sections | 2-column grid | 4-column grid |

---

## 5. User Flow Diagrams

### 5.1 Student Review Submission Flow

| Step | Screen | Action | Next |
|---|---|---|---|
| 1 | Home / Search | User searches for university or professor | Results page |
| 2 | Results Page | User selects a university or professor | Profile page |
| 3 | Profile Page | User clicks "Write a Review" button | Auth check |
| 4 | Auth Gate | If not logged in: redirect to login. If logged in: continue | Review form |
| 5 | Review Form | User fills in category ratings + writes comment | Verification |
| 6 | Verification | System verifies student is enrolled (email domain check) | Confirmation |
| 7 | Confirmation | Success toast: "Review submitted anonymously!" | Profile (updated) |

```
┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐
│  Home /  │───►│ Results  │───►│ Profile  │───►│  Auth    │
│  Search  │    │  Page    │    │  Page    │    │  Check   │
└──────────┘    └──────────┘    └──────────┘    └────┬─────┘
                                                     │
                                          ┌──────────┴──────────┐
                                          ▼                     ▼
                                    ┌──────────┐          ┌──────────┐
                                    │  Login   │          │  Review  │
                                    │  Page    │─────────►│  Form    │
                                    └──────────┘          └────┬─────┘
                                                               │
                                                          ┌────▼─────┐
                                                          │ Verify & │
                                                          │ Submit   │
                                                          └────┬─────┘
                                                               │
                                                          ┌────▼─────┐
                                                          │ Success  │
                                                          │ Toast    │
                                                          └──────────┘
```

### 5.2 Comparison Flow

| Step | Screen | Action | Next |
|---|---|---|---|
| 1 | Profile Page | User clicks "Compare" button on university/professor | Comparison page |
| 2 | Comparison Page | Entity A is pre-filled. User selects Entity B from dropdown | Results render |
| 3 | Comparison View | Category-by-category comparison + radar chart loads | User decision |
| 4 | Action | User can share link, go to profile, or write a review | Respective page |

---

## 6. Interaction & Animation Design

Micro-interactions are used sparingly but intentionally to guide user attention and provide feedback. All animations use CSS transitions and respect **prefers-reduced-motion** settings.

| Interaction | Animation | Duration | Easing |
|---|---|---|---|
| Card Hover | Scale(1.02) + shadow elevation | 200ms | ease-out |
| Button Press | Scale(0.97) + bg darken 10% | 100ms | ease-in |
| Page Load | Staggered fade-in-up for cards | 300ms + 50ms delay per item | ease-out |
| Search Focus | Border color transition + width expand | 200ms | ease-in-out |
| Star Rating Hover | Stars fill sequentially with scale bounce | 150ms per star | spring |
| Toast Notification | Slide in from top-right + fade out after 4s | 300ms in / 200ms out | ease-out |
| Accordion (FAQ) | Height auto transition + chevron rotation | 250ms | ease-in-out |
| Comparison Load | Progress bars grow from 0 to value | 600ms + 100ms stagger | ease-out |
| Sentiment Badge | Subtle pulse on first load | 1s, once | ease-in-out |
| Modal Overlay | Backdrop fade + modal scale from 0.95 | 200ms | ease-out |

---

## 7. Accessibility Standards

The platform targets **WCAG 2.1 Level AA** compliance. All interactive elements are keyboard-accessible, screen-reader-friendly, and designed with sufficient color contrast.

| Requirement | Implementation |
|---|---|
| Color Contrast | All text meets 4.5:1 contrast ratio (AA). Large text: 3:1 minimum. Tested with WebAIM Contrast Checker. |
| Keyboard Navigation | Full tab order across all interactive elements. Focus ring: `2px solid #3B82F6`, 2px offset. Skip-to-content link. |
| Screen Readers | Semantic HTML5 elements (`nav`, `main`, `section`, `article`). ARIA labels on icons and interactive widgets. |
| Alt Text | All meaningful images have descriptive alt text. Decorative images use `alt=""`. |
| Form Labels | Every input has an associated `<label>`. Error messages linked via `aria-describedby`. |
| Reduced Motion | `@media (prefers-reduced-motion: reduce)` disables all non-essential animations. |
| Touch Targets | Minimum 44x44px touch targets on mobile. Adequate spacing between tappable elements. |
| Language | `html lang` attribute set. Future: RTL support for Arabic interface (`lang="ar"`, `dir="rtl"`). |

---

*— End of Frontend Design Document —*
