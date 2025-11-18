import json
import os
import random
from datetime import datetime, timedelta

OUTPUT_DIR = os.path.join("..", "data", "court_exports")
os.makedirs(OUTPUT_DIR, exist_ok=True)

FIRST_NAMES = ["Jane", "John", "Alex", "Maria", "Chris", "Taylor"]
LAST_NAMES = ["Doe", "Smith", "Johnson", "Brown", "Garcia", "Lee"]
COUNTIES = ["Orange", "Miami-Dade", "Hillsborough", "Duval"]

def random_date(start_year=1970, end_year=2005):
    start = datetime(start_year, 1, 1)
    end = datetime(end_year, 12, 31)
    delta = end - start
    return (start + timedelta(days=random.randint(0, delta.days))).date()

def create_mock_case(case_id: int) -> dict:
    first = random.choice(FIRST_NAMES)
    last = random.choice(LAST_NAMES)

    return {
        "court_case_id": f"COURT-{case_id:04d}",
        "defendant_first_name": first,
        "defendant_last_name": last,
        "defendant_dob": random_date().isoformat(),
        "charges": [
            {
                "charge_code": "123.45",
                "charge_description": "Sample Felony Charge",
                "severity": random.choice(["FELONY", "MISDEMEANOR"])
            }
        ],
        "status": random.choice(["OPEN", "CLOSED"]),
        "last_updated": datetime.utcnow().isoformat() + "Z",
        "county": random.choice(COUNTIES)
    }

def main():
    num_files = 75  # adjust to mimic 50–100 files
    for i in range(1, num_files + 1):
        case = create_mock_case(i)
        filename = os.path.join(OUTPUT_DIR, f"court_case_{i:04d}.json")
        with open(filename, "w") as f:
            json.dump(case, f, indent=2)
    print(f"Generated {num_files} mock court files in {OUTPUT_DIR}")

if __name__ == "__main__":
    main()
