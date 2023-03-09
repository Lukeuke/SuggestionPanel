# SuggestionPanel

## What you need

- .NET 7.0
- MSSQL 2022 Express

## Installation

1. Open Project
 
1. Change the connetion string to the Database

```
cd SuggestionPanel.UI/
```

2.1 Open the ``appsettings.json`` and the ``appsettings.Development.json``

2.2
Change the connection string
```json
"ConnectionStrings": {
  "DB": "<your_connection_string>"
}
```

3. Install EF Tools
```
dotnet tool install --global dotnet-ef
```

4. Update the Db

```
dotnet ef --startup-project ../SuggestionPanel.UI/ database update
```

5. Head to ``SuggestionPanel.UI``

6. Run the App

```
dotnet run
```

## Changing Admin and Committee Password

1. Go to ``SuggestionPanel.UI``

```
cd SuggestionPanel.UI/
```

2. Open the ``appsettings.json`` and the ``appsettings.Development.json``

3. Change the variables
```json
"AdminPass": "<your_strong_pass>",
"CommitteePass": "<your_strong_pass>"
```
