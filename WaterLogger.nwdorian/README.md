# HabitGains

This application is designed to demonstrate a full stack .NET application using Razor Pages ASP.NET Framework.

It allows users to track habits and visualize entry data in a chart. They can create, update and delete habits and habit entries.

- [HabitGains](#habitgains)
  - [Technologies](#technologies)
  - [Features](#features)
    - [Habit management](#habit-management)
    - [Entries management](#entries-management)
    - [Favorite habit feature](#favorite-habit-feature)
    - [Input validation](#input-validation)
    - [Error display](#error-display)
    - [Chart data visualization](#chart-data-visualization)
    - [Database initialization](#database-initialization)
    - [Database seeding](#database-seeding)
    - [Dark and light mode toggle](#dark-and-light-mode-toggle)
  - [Installation](#installation)
    - [Prerequisites](#prerequisites)
    - [Running the application](#running-the-application)
  - [Contributing](#contributing)
  - [License](#license)
  - [Contact](#contact)

## Technologies

- [ASP.NET Core 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [SQlite](https://www.sqlite.org/index.html)
- [Bootstrap](https://getbootstrap.com/)
- [Javascript](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
- [ChartJS](https://www.chartjs.org/)
- [Bogus](https://github.com/bchavez/Bogus)
- [Fluent Validation](https://github.com/FluentValidation/FluentValidation)
- [Serilog](https://github.com/serilog/serilog)

## Features

### Habit management

- Create, update and delete operations with confirmation modal
- Paginated view
  - **Active page** indicator
  - **Page navigation** buttons
    - **First** page
    - **Previous** page
    - **Next** page
    - **Last** page
  - **Page size** dropdown options
  - **Sorting** in ascending or descending order
    - **Name** column
    - **Measurement** column
    - **Favorite** column
    - **Created** column
  - **Filtering**
    - **Measurement** dropdown options
    - **Favorite** dropdown options
    - **Search** by name or measurement

### Entries management

- Create update and delete operations
- Paginated view
  - **Active page** indicator
  - **Page navigation** buttons
    - **First** page
    - **Previous** page
    - **Next** page
    - **Last** page
  - **Page size** dropdown options
  - **Sorting** in ascending or descending order
    - **Date** column
    - **Quantity** column
  - **Filtering**
    - **Date** range
    - **Quantity** range

### Favorite habit feature

- Allows users to mark habits as favorite for fast and easy access to most used habits

### Input validation

- Client side validation
  - using built-in JQuery form validation with data annotation attributes on input models
- Server side validation
  - using `ModelState` checks with data annotation attributes on input models
  - using `FluentValidation` library to enforce invariants and avoid broken entity states
  - using `Result` pattern with strongly typed `Errors` for business rules

### Error display

- Form validation errors appear under input fields
- Server side validation errors are used for conditional rendering of pages

### Chart data visualization

- Grouping habit quantities by day and displaying the values in a chart
- Charts are generated using `ChartJS` library in a collocated javascript file

### Database initialization

- Connection string can be changed in `appsettings.json`
  - `ConnectionStrings` - `Default`
- SQlite database file is created on startup in `HabitGains.Web` project folder if it doesn't already exist
- Database is initialized with `habit` and `entry` tables

### Database seeding

- Data seeding on startup can be enabled or disabled in `appsettings.json`
  - `Seeding` database seeding options
    - `SeedOnStartup` enable or disable startup seeding (`true`/`false`)
    - `EntriesPerHabit` amount of entries to insert per habit (`10`-`100`)
- Seeding is only performed if no records are present in database tables
- Options are validated during startup - invalid options prevent the application from running
- Seeding inserts 15 habits into the database with a selected amount of entries per habit
- Entries per habit values range from 10 to 100
- Seeding is enabled in `Development` environment and disabled when running in `Production` environment

### Dark and light mode toggle

- using Bootstrap themes to switch between dark and light mode

## Installation

### Prerequisites

- .NET 10 SDK ([download link](https://dotnet.microsoft.com/en-us/download/dotnet/10.0))

### Running the application

1. Clone the repository
   - using **HTTPS**
     - `https://github.com/nwdorian/HabitGains.git`
   - using **SSH**
     - `git@github.com:nwdorian/HabitGains.git`
   - using **GitHub CLI**
     - `gh repo clone nwdorian/HabitGains`

2. Configure `appsettings.json` options if needed
   - replace the connection string
     - details in [Database Initialization](#database-initialization) section
   - edit data seeding settings
     - details in [Database Seeding](#database-seeding) section

3. Run the application from project root folder
    - `dotnet run --project ./src/HabitGains.Web`

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue.
