**Local setup instructions**

1. This project expects a SQL database - replace the tech test connection string in appsettings.development.json with your own local sql connection string. Database name I used is TechTest but you can rename.
2. You will need to have a working installation of EF core Tools and call update-database to set up your local database. Set the default project as Persistence in package manager console (or equivalent) when doing this.

That should be all that is needed to run locally, please let me know if you have any issues with this.
