Assumptions
1. The right file will be uploaded and well-formed.
2. All the account ids are present and of the integer data type. If this were a risk, one would write another set of validation tests and add it before the current AccountHandler in the validation chain of responsibility.
3. All dates are present and well formed. Again, if this were not the case it would require more validation.
4. All readings are greater than zero.
5. If there are duplicates within a set of valid meter readings, then the latest meter is selected for upload.

If a meter reading fails more than one validation, then only the first validation failure is shown. If the requirement was to show all validation errors, then the error would be changed to a list from a string.

Interfaces are only used where required. In this case for external datasources and where required in a design pattern.

I have not included any unit testing for AccountHandler as the external data source just returns a list of integers. This would just be testing my ability to write a list of View Models that intersect with another list. This would be a better candidate for integration testing.

The application requires a local SQL Server database with integrated security. If this is not the case then amend appsettings.json as required.

The database is reset on each run of the application.