RunTrack
========

-  A web application that helps users track their runs by date and
distance.
-  The application allows users to create and edit running logs.
-  Logs are displayed as a list and also visualized as a chart to help user
track their growth over time.
-  Users also have the option to select from multiple unit options (km, m,
miles etc.).

Run Locally (Development)
-------------------------

To run this locally via command line:

-  Clone this repository
-  ``cd RunningLogger``
-  ``dotnet run``

Tech Stack
----------
-  C# razor pages web application
-  ADO.NET + SQLite

Notes
------
- When the application starts, the database & schema should be
auto-created if it doesn't exist.
- Configuration for the database connection modifiable in `appsettings.json`
(Default should work)