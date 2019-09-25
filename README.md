# EF Core Query Bug Project

This is a simple test project to demonstrate the EF Core bug described here:

https://github.com/aspnet/EntityFrameworkCore/issues/18007

This is a test project that contains the same data set that is used in the bug report.

To set this up:

* Clone the repo
* Open the Test file `EfQueryBug.cs`
* Find the `ImportAlbumData` Test
* For SQLite set `useSqLite=true` (will create/update the table)
* For SQL Server change connection string to an existing DB


To demonstrate the failure run the first test.

If you need to reset the data set delete the SqLite data base or drop the tables in the SQL database. 

I recommend you use a real SQL Db instead of the in memory or the local sql store, so you can browse the original data to ensure the data set is valid (I think it is but maybe somebody should verify).

## What you should see
If you run the first once the database has been created you should see the test fail. The full album list should be 91 records which is correct. However, you will end up with 38 records of uninitialized Artist records which is a fail. 

## Run the same thing in EF Core 2.2.6
Now run the same thing with EF Core 2.2.6. Go into the project file and uncomment the 2.2.6 EF Core libraries. Clean and rebuild the project.

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="2.2.6" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="2.2.6" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="2.2.6" />
```

Now when you run the test passes.
