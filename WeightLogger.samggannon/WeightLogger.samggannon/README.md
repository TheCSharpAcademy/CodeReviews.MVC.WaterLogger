# WeightLogger

## Overview

WeightLogger is a web application that allows users to manage their weight logs. The application supports functionalities such as adding, updating, deleting, and viewing weight records. It uses SQLite for data storage and includes web pages for interacting with the weight logs.

## Project Structure

The project is divided into two main folders:

### Data Folder

This folder contains the core data access classes:

1. **`WeightRecordDto`**:
   - A Data Transfer Object (DTO) representing a weight log record.
   - Properties:
     - `Id`: Unique identifier for the record.
     - `weight`: The weight value recorded.
     - `loggedDate`: The date when the weight was recorded.

2. **`DatabaseAccess`**:
   - Manages direct interactions with the SQLite database.
   - Methods:
     - `Insert(string sql)`: Executes an SQL `INSERT` command.
     - `Update(string sql)`: Executes an SQL `UPDATE` command.
     - `Delete(string sql)`: Executes an SQL `DELETE` command.
     - `GetDataTable(string sql)`: Retrieves data as a `DataTable` from a given SQL query.
     - `GetWeightLogRecordById(string sql)`: Retrieves a single `WeightRecordDto` based on an SQL query.
   - Private Methods:
     - `GetDateTime(SQLiteDataReader reader, string columnName)`: Converts a string to a `DateTime?`.

3. **`DataAccess`**:
   - Provides higher-level methods for interacting with the weight log records.
   - Constructor:
     - `DataAccess(string connectionString)`: Initializes with a connection string.
   - Methods:
     - `TestConnection()`: Tests the database connection.
     - `DeleteWeightLog(int id)`: Deletes a weight log by its ID.
     - `EditWeightLog(int id, decimal weightValue, string loggedDate)`: Updates a weight log record.
     - `GetWeightHistory()`: Retrieves all weight log records.
     - `GetWeightLogById(int id)`: Retrieves a weight log record by its ID.
     - `LogThisWeight(decimal weight, string logDate)`: Inserts a new weight log record.

### Pages Folder

This folder contains Razor Pages that define the user interface:

1. **`Index.cshtml`**:
   - Displays the weight history and allows users to view, edit, or delete weight log records.
   - Features:
     - A chart displaying weight history.
     - A form for entering new weight logs.
     - A table listing weight logs with options to edit or delete each record.

2. **`WeightHistory.cshtml`**:
   - Displays the weight history and allows users to view, edit, or delete weight log records.
   - Features:
     - A chart displaying weight history.
     - A form for entering new weight logs.
     - A table listing weight logs with options to edit or delete each record.

3. **`Edit.cshtml`**:
   - Provides a form to edit a selected weight log record.
   - Features:
     - A modal dialog for editing record details.
     - Form fields for date and weight.

4. **`Delete.cshtml`**:
   - Provides a confirmation page for deleting a weight log record.
   - Features:
     - Displays record details.
     - A button to confirm deletion.

## Usage

1. **Setup**:
   - Ensure you have the necessary packages for SQLite and Razor Pages.
   - Configure the connection string for the SQLite database used in the `DataAccess` class in the appsettings.json file.

2. **Running the Application**:
   - Start the application using your preferred method (e.g., Visual Studio or CLI).
   - Access the web pages via a browser to interact with the weight logs.

3. **Managing Weight Logs**:
   - **Add**: Use the form on the WeightHistory page to log new weight records.
   - **Edit**: Click the "Edit" button next to a record to update its details.
   - **Delete**: Click the "Delete" button to remove a record.

## Dependencies

- SQLite for database management.
- ASP.NET Core for building and running the web application.
- Entity Framework Core (if using with EF for ORM).

## Contributing
