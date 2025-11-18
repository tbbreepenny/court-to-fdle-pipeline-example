import json
import os
import requests
from datetime import datetime

COURT_EXPORT_DIR = os.path.join("..", "data", "court_exports")
FDLE_API_URL = "http://localhost:5109/fdle/cases/batch"

def transform_to_fdle(court_case: dict) -> dict:
    return {
        "externalCaseId": court_case["court_case_id"],
        "sourceSystem": "CountyCourt",
        "defendant": {
            "firstName": court_case["defendant_first_name"],
            "lastName": court_case["defendant_last_name"],
            "dob": court_case["defendant_dob"]
        },
        "charges": [
            {
                "code": c["charge_code"],
                "description": c["charge_description"],
                "severity": c["severity"]
            }
            for c in court_case.get("charges", [])
        ],
        "status": court_case.get("status", "OPEN"),
        "lastUpdated": court_case.get("last_updated", datetime.utcnow().isoformat() + "Z"),
        "county": court_case.get("county", "")
    }

def main():
    files = [f for f in os.listdir(COURT_EXPORT_DIR) if f.endswith(".json")]

    batch = []
    for filename in files:
        path = os.path.join(COURT_EXPORT_DIR, filename)
        with open(path) as f:
            court_case = json.load(f)
        fdle_case = transform_to_fdle(court_case)
        batch.append(fdle_case)

    if not batch:
        print("No files found to ingest.")
        return

    print(f"Sending {len(batch)} cases to FDLE API...")
    response = requests.post(FDLE_API_URL, json=batch)
    print("Status:", response.status_code)
    try:
        print("Response:", response.json())
    except Exception:
        print("Response text:", response.text)

if __name__ == "__main__":
    main()
