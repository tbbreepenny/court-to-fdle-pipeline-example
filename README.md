# court-to-fdle-pipeline-example
```text
court-to-fdle-pipeline/
├─ README.md
├─ api/
│  ├─ CourtToFdle.Api.csproj
│  ├─ Program.cs
│  ├─ appsettings.json
│  ├─ Models/
│  │  ├─ FdleCaseDto.cs
│  │  └─ FdleCase.cs
│  ├─ Data/
│  │  └─ AppDbContext.cs
│  └─ Controllers/
│     └─ FdleCasesController.cs
├─ etl/
│  ├─ generate_mock_court_data.py
│  └─ ingest_court_files.py
├─ data/
│  └─ court_exports/   # 50–100 generated files
└─ postman/
   └─ CourtToFDLE.postman_collection.json

   # Court to FDLE Pipeline

This project simulates a real-world integration between **county court systems** and an **FDLE-style justice system**.  

It uses a **Python ETL script** to read 50–100 mock “court export” files, transform them into a standard schema, and send them into a **.NET Web API** that acts as the FDLE ingestion endpoint. A **Postman collection** is included for exploring and testing the API.

---

## ⚙️ Tech Stack

- **.NET** 8 Web API (C#)
- **Python** 3 (ETL and data generation)
- **Entity Framework Core** with an in-memory / SQLite database
- **RESTful JSON APIs**
- **Postman** for API exploration and tests


## 🧩 Architecture Overview

**Flow:**

1. Mock court data files are generated into `data/court_exports/`.
2. A Python ETL script reads each file, validates and transforms the data into a standard **FDLE case** format.
3. The ETL script sends batched cases to the **FDLE API** (`/fdle/cases/batch`) hosted by the .NET project.
4. The .NET API persists the cases and exposes query endpoints for reporting and debugging.

```text
Court Files (JSON/CSV) 
       ↓
Python ETL (validate + transform + batch)
       ↓
.NET FDLE API (/fdle/cases/batch)
       ↓
Database (cases stored for querying)

