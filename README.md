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
```

# **Court → FDLE Data Integration Pipeline**

A full end-to-end simulation of how criminal justice data flows from a county court system into a centralized state system (FDLE-style). 

* **.NET 8 Web API**
* **Entity Framework Core (In-Memory DB)**
* **Python ETL pipeline**
* **JSON batch ingestion**
* **Local API testing via Swagger or Postman**

---

##

This project simulates a realistic justice-system integration where:

1. **County courts export 50–100 criminal case files**
2. A **Python ETL job** transforms these files into an FDLE-compatible structure
3. The ETL sends the data in a **single batch** to a .NET API
4. The **FDLE API ingests and stores** the cases
5. Users can query or review ingested cases

This mirrors real processes used by state agencies, background check systems, and Appian-style automation workflows.

---

## **Structure**

```
court-to-fdle-pipeline-example/
│
├── api/
│   └── CourtToFdle.Api/
│       ├── Controllers/
│       │   └── FdleCasesController.cs
│       ├── Models/
│       │   ├── FdleCase.cs
│       │   └── FdleCaseDto.cs
│       ├── Data/
│       │   └── AppDbContext.cs
│       ├── Program.cs
│       └── CourtToFdle.Api.csproj
│
├── etl/
│   ├── generate_mock_court_data.py
│   └── ingest_court_files.py
│
├── data/
│   └── court_exports/
│
└── README.md
```

---

# **How to Run the Project**

## Start the FDLE API (C# / .NET)

```bash
cd api/CourtToFdle.Api
dotnet run
```

You should see:

```
Now listening on: http://localhost:5109
```

Swagger UI:

```
http://localhost:5109/swagger
```

---

## Generate Mock Court Case Files (Python)

```bash
cd etl
python3 generate_mock_court_data.py
```

This creates ~75 JSON case files in:

```
/data/court_exports/
```

---

## Run the ETL / Batch Ingestion

```bash
python3 ingest_court_files.py
```

Expected output:

```
Sending 75 cases to FDLE API...
Status: 201
```

---

## View All Ingested Cases

Browser:

```
http://localhost:5109/fdle/cases
```

---

# **Key Components**

## **.NET 8 Web API**

* REST endpoints:

  * `POST /fdle/cases/batch`
  * `GET /fdle/cases`
  * `GET /fdle/cases/{id}`
* EF Core InMemory database
* Swagger for API documentation
* Clean DTO → entity mapping workflow

---

## **Python ETL Pipeline**

### `generate_mock_court_data.py`

Creates realistic criminal case files with:

* Defendant name + DOB
* County
* Charges (code, description, severity)
* Case status
* Timestamp

### `ingest_court_files.py`

* Reads JSON case files
* Transforms them to FDLE DTO structure
* Sends them as a **single batch** to the API
* Handles HTTP communication + response handling

---

# **Real-World Context (Justice System)**

This project mimics real automation workflows used by:

* State agencies (like FDLE)
* Courts transmitting data to centralized systems
* CJIS-related data flows
* Appian / low-code automation platforms integrating with legacy systems
* Background check pipelines
* Criminal case reporting systems

It demonstrates technical ability to move sensitive system data between agencies using modern API-driven approaches.

---

# **Tech Stack**

### Backend

* C#
* .NET 8
* REST API
* Entity Framework Core (InMemory)
* Swagger / OpenAPI

### ETL

* Python 3.x
* JSON processing
* Requests library

### Data

* JSON case files
* In-memory storage

---

# **Skills Demonstrated**

* API development using .NET
* Multi-language project design (C# + Python)
* ETL engineering
* JSON transformation
* Batch data ingestion
* Modeling real justice-system data
* Integration pipeline architecture
* Experience with APIs similar to Appian automations
* Handling 50–100+ files in a single workflow

---

# **Optional Enhancements (Future)**

* Containerize with Docker
* Add a SQL database
* Add authentication (API keys, JWT)
* Build an Appian-style UI layer
* Add unit tests
* Automate ETL run via cron or GitHub Actions
