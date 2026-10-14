# SMT Data Gatekeeper 🛡️🏭

## Overview
**SMT Data Gatekeeper** is a C# Windows Forms application designed for SMT (Surface-Mount Technology) Engineers. The core objective of this tool is to ensure data integrity during the NPI (New Product Introduction) phase by cross-validating production files before they are fed into SMT placement machines. 

By detecting discrepancies between the **Bill of Materials (BOM)** and **XY Coordinate Data**, this tool prevents misplacements, missing components, and unregistered parts, directly contributing to a target **First Pass Yield (FPY) of > 98%**.

## Features
- **CSV Data Parsing:** Easily import raw BOM and XY Data files.
- **Automated Cross-Validation:** Instantly compare datasets using relational logic.
- **Visual Error Detection:** Color-coded DataGridView to highlight errors:
  - 🟩 **Green (OK):** Component matches perfectly in both files.
  - 🟥 **Red (Error):** Highlights missing coordinates (in XY) or unregistered components (in BOM).

## Project Structure
- `Form1.cs`: The main application window containing the UI and core logic.
  - `btnLoadBOM_Click`: Handles `OpenFileDialog` to parse and load BOM.csv into a `DataTable`.
  - `btnLoadXY_Click`: Handles `OpenFileDialog` to parse and load XY_Data.csv into a `DataTable`.
  - `btnValidate_Click`: Executes the cross-validation algorithm and updates the UI with color formatting.
  - `ReadCSV()`: A helper method using `System.IO` and `System.Data` to convert CSV text lines into structured DataTables.
- `BOM.csv`: Mockup Bill of Materials data (Ref_Des, Part_Number, Qty).
- `XY_Data.csv`: Mockup placement machine data (Ref_Des, X_Loc, Y_Loc, Rotation).

## Core Logic (Validation Algorithm)
The validation process (`btnValidate_Click`) operates in two main phases:
1. **BOM to XY Verification:** It iterates through the BOM list and searches for the corresponding Reference Designator (`Ref_Des`) in the XY Data. If a component is in the BOM but missing in the XY Data, it flags it as `"Error: Missing in XY Data"`.
2. **XY to BOM Verification:** It then iterates through the XY Data and checks against the BOM. If a coordinate exists but the component is not listed in the BOM, it flags it as `"Error: Missing in BOM"`.

## How to Use
1. Launch the application.
2. Click **"1. Load BOM"** and select your `BOM.csv` file. The data will display in the grid.
3. Click **"2. Load XY Data"** and select your `XY_Data.csv` file. The grid will update to show this data.
4. Click **"3. Validate Data"**. The system will merge the insights and color-code the rows to highlight any production risks.

## Technologies Used
- **Language:** C#
- **Framework:** .NET 10.0 (LTS)
- **UI Framework:** Windows Forms (WinForms)
- **Environment:** Visual Studio 2026 Community

## Next Steps (Roadmap)
- [ ] **Web API Integration:** Transmit error logs to a centralized MES (Manufacturing Execution System) via RESTful POST requests.
- [ ] **API Testing:** Validate endpoints using Postman.
