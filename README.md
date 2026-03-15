# Water Drinking Logger

A web-based CRUD application for tracking daily water intake, built with
ASP.NET Core Razor Pages and SQLite. Submitted as a solution to the
[C# Academy — Project #24: Water Drinking Logger](https://thecsharpacademy.com/project/24/water-drinking-logger).

---

## Features

- Log water intake entries with a date, quantity, and unit of measure
- View all records in a paginated table
- Edit and delete individual records
- Calculate the total quantity of water logged
- Export all records to an Excel spreadsheet (`.xlsx`)

---

## Project Requirements & How They Are Met

| Requirement | Implementation |
| --- | --- |
| This is a web application | ASP.NET Core Razor Pages web app |
| Users should be able to insert, delete, update and view their drinking water | Full CRUD via Create, Delete, Update, Index Razor Pages |
| You should use Entity Framework Core as your ORM | `WaterLoggerContext` extends `DbContext`; all DB operations use EF Core LINQ methods |
| Your database should be SQLite | `Microsoft.EntityFrameworkCore.Sqlite` provider; DB file stored at `DB/drinking_water` |
| You should have a model to represent the data | `DrinkingWaterModel` with `Id`, `Date`, `Quantity`, and `Measure` fields |

### Challenge — Export to Excel

The application includes an **Export to Spreadsheet** button on the Index page.
Clicking it downloads a `WaterLog.xlsx` file containing all logged records, with
a bold header row and auto-fitted column widths. This is implemented via the
`OnGetExport()` handler in `Index.cshtml.cs` using the **ClosedXML** library.

---

## Tech Stack

### Framework & Runtime

| Tool | Version | Purpose |
| --- | --- | --- |
| [.NET](https://dotnet.microsoft.com/) | 10.0 | Runtime and SDK |
| [ASP.NET Core Razor Pages](https://learn.microsoft.com/aspnet/core/razor-pages/) | 10.0 | Web framework and page routing |

### NuGet Packages

| Package | Version | Purpose |
| --- | --- | --- |
| [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite) | 9.0.3 | EF Core provider for SQLite |
| [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design) | 9.0.3 | Design-time EF Core tools (migrations) |
| [ClosedXML](https://www.nuget.org/packages/ClosedXML) | 0.105.0 | Generate `.xlsx` Excel files |

### Frontend Libraries (bundled via `wwwroot/lib/`)

| Library | Purpose |
| --- | --- |
| [Bootstrap 5](https://getbootstrap.com/) | Responsive layout and button styles |
| [jQuery](https://jquery.com/) | DOM manipulation |
| [jQuery Validation](https://jqueryvalidation.org/) | Client-side form validation |
| [jQuery Validation Unobtrusive](https://github.com/aspnet/jquery-validation-unobtrusive) | ASP.NET Core's unobtrusive validation adapter |

### Database

| Tool | Purpose |
| --- | --- |
| [SQLite](https://www.sqlite.org/) | Embedded, file-based relational database (no server required) |

### VS Code Extensions (recommended)

| Extension | Purpose |
| --- | --- |
| [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) | C# language support, IntelliSense, debugging |
| [SQLite Viewer](https://marketplace.visualstudio.com/items?itemName=qwtel.sqlite-viewer) | Browse the SQLite database file directly in VS Code |

---

## Prerequisites

### Both Platforms

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Git

### Windows

- Windows 10 / 11 (x64)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (with **ASP.NET and
  web development** workload) **or** [VS Code](https://code.visualstudio.com/)
  with the C# Dev Kit extension

### Linux

- Any modern distro (Ubuntu 20.04+, Fedora 36+, Arch, etc.)
- [VS Code](https://code.visualstudio.com/) with the C# Dev Kit extension
  (recommended)
- `libssl` and `libicu` — usually pre-installed; if missing, install via your
  package manager

---

## Installation & Running

### 1. Clone the repository

```bash
git clone https://github.com/RyanW84/WaterLoggerPractice.git
cd WaterLoggerPractice
```

### 2. Restore dependencies

```bash
dotnet restore WaterLogger.Ryanw84/WaterLogger.Ryanw84.csproj
```

### 3. Run the application

```bash
dotnet run --project WaterLogger.Ryanw84/WaterLogger.Ryanw84.csproj
```

The app will start and print the local URL (usually `http://localhost:5000`).
Open it in your browser.

> **Note:** The SQLite database file is created automatically at
> `WaterLogger.Ryanw84/DB/drinking_water` on first run — no manual setup
> required.

### Windows — running via Visual Studio

1. Open the solution or `WaterLogger.Ryanw84.csproj` in Visual Studio 2022.
2. Press **F5** to build and launch with the built-in browser.

### Linux — common issues

| Issue | Fix |
| --- | --- |
| `dotnet: command not found` | Install the .NET 10 SDK from [dotnet.microsoft.com](https://dotnet.microsoft.com/download) and ensure `~/.dotnet/tools` or `/usr/share/dotnet` is on your `PATH` |
| `libssl` errors on older distros | `sudo apt install libssl-dev libicu-dev` (Debian/Ubuntu) |
| Port already in use | Change the port: `dotnet run --urls "http://localhost:5001"` |

---

## Project Structure

```text
WaterLoggerPractice/
└── WaterLogger.Ryanw84/
    ├── Data/
    │   └── WaterLoggerContext.cs   # EF Core DbContext
    ├── DB/
    │   └── drinking_water          # SQLite database file (auto-created)
    ├── Models/
    │   └── DrinkingWaterModel.cs   # Entity model (maps to the DB table)
    ├── Pages/
    │   ├── Index.cshtml(.cs)       # List all records + export
    │   ├── Create.cshtml(.cs)      # Add a new record
    │   ├── Update.cshtml(.cs)      # Edit an existing record
    │   ├── Delete.cshtml(.cs)      # Delete a record
    │   └── Shared/
    │       └── _Layout.cshtml      # Shared Bootstrap layout
    ├── wwwroot/                    # Static assets (CSS, JS, lib)
    ├── appsettings.json            # App config (connection string)
    └── Program.cs                  # App bootstrap and DI setup
```

---

## Usage

| Action | How |
| --- | --- |
| Add a record | Click **Add Record**, fill in the date, quantity, and measure, then submit |
| Edit a record | Click the **pencil icon** on any row |
| Delete a record | Click the **trash icon** on any row and confirm |
| Calculate total | Click **Calculate Total** to sum all quantities |
| Export to Excel | Click **Export to Spreadsheet** to download `WaterLog.xlsx` |
