# SuggestionPanel

## What you need

- .NET 7.0
- MSSQL 2022 Express

## Installation

1. Open Project
 
1. Open Suggestion.Application
```
cd Suggestion.Application
```

3. Install EF Tools
```
dotnet tool install --global dotnet-ef
```

4. Update the Db

```
dotnet ef --startup-project ../SuggestionPanel.UI/ database update
```

5. Head to ``Application.UI``

6. Run the App

```
dotnet run
```
