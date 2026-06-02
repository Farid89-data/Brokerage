# 🏦 Brokerage Data Ecosystem

> A comprehensive data platform for a brokerage firm, covering data ingestion, transformation, analytical modeling, reporting, and system integrations ensuring accurate and timely access to business and trading information.

---

## 📁 Repository Structure

```
Brokerage/
├── ETL/                        # Extract, Transform, Load pipelines
│   ├── RawDB.V2/               # Raw data ingestion layer
│   ├── ETL_DimensionTable/     # Dimension table pipelines
│   ├── DW_AutomationDW_Fact/   # Fact table pipelines
│   ├── DW_AttachmentDW/        # Attachment data warehouse
│   ├── ETL_TSETMC_Stage/       # Stock Exchange data staging
│   ├── ETL_TSETMC_StageSmall/  # Lightweight TSETMC staging
│   ├── MBS_UpdateTables/       # Master data update pipelines
│   ├── PaymentPowerbi/         # Payment data for Power BI
│   ├── WebApi/                 # Web API data collectors (.NET 6)
│   └── ...
├── SSAS/                       # SQL Server Analysis Services models
│   ├── Balance/
│   ├── CustomerGroupMonitoring/
│   ├── Customer_EfficiencyV2/
│   ├── MarketerRate_SSAS/
│   └── SSAS_IMEMonitoring/
├── PowerBI/                    # Power BI reports & dashboards (coming soon)
└── WebApiRequests/             # Deployed Web API binaries & runtime logs
```

---

## 🔄 ETL Layer

The ETL layer is built entirely with **SQL Server Integration Services (SSIS)** and handles all data movement across the ecosystem.

### Data Flow Architecture

```
Source Systems
     │
     ▼
 RawDB (Raw Layer)
     │
     ▼
 Stage DB (Staging Layer)
     │
     ▼
 AutomationDW / Data Warehouse (DW Layer)
     │
     ▼
 SSAS / Power BI (Analytical Layer)
```

### ETL Projects

| Project | Description |
|---|---|
| `RawDB.V2` | Ingests raw data from source systems into the Raw database |
| `ETL_DimensionTable` | Builds and maintains all dimension tables (Customer, Marketer, Instrument, Branch, etc.) |
| `DW_AutomationDW_Fact` | Populates fact tables: Trade, ChangeBroker, ClubScore, EnergyTrade, FutureTrade, etc. |
| `DW_AttachmentDW` | Manages customer attachment facts |
| `ETL_AttachmentStage_UpdateTable` | Updates the attachment staging tables |
| `ETL_AutomationStage_UpdateStageTable` | Updates all automation staging tables |
| `ETL_PaymentStage_UpdateTable` | Manages payment-related staging data |
| `ETL_TSETMC_Stage` | Full ingestion pipeline from TSETMC (Securities Exchange) API |
| `ETL_TSETMC_StageSmall` | Lightweight version for incremental TSETMC updates |
| `MBS_UpdateTables` | Updates master data: Branch, Marketer, Customer, Instrument, Sector, etc. |
| `PaymentPowerbi` | Prepares and aggregates payment data for Power BI consumption |
| `DW_TSETMC` | Loads stock market dimension data into the Data Warehouse |

### WebApi Data Collectors (.NET 6)

These are lightweight C# console applications that call internal REST APIs and feed data into the pipeline:

| Service | Purpose |
|---|---|
| `GetCustomerV2` | Fetches full customer data |
| `GetCustomerGroups` | Retrieves customer group assignments |
| `GetGroups` | Fetches group definitions |
| `GetSettlement` | Collects settlement records |
| `GetRequestMasterStockMarket` | Fetches stock market request master data |
| `GetCompanyBranch` | Retrieves company branch information |
| `ChangeBrokerAttachment` | Collects broker transfer attachment data |
| `LeftCustomerByChangeBroker` | Tracks customers who transferred out via broker change |
| `LeftCustomerByChangeBrokerDetail` | Detailed records of broker-change transfers |
| `CalculateCompanyBranchRank` | Triggers branch ranking calculations |
| `CalculateRankMarketer` | Triggers marketer ranking calculations |
| `CallCenter_Calls` | Ingests call center interaction data |
| `CustomerElectronicRequestAccountV2` | Collects electronic account request data |
| `ReportCallInfo` | Aggregates call center reporting data |

---

## 📊 SSAS Layer

The **SQL Server Analysis Services (SSAS)** layer provides multidimensional and tabular models for high-performance analytical queries, powering dashboards and reports.

### SSAS Models

| Model | Description |
|---|---|
| `Balance` | Customer portfolio balance analysis |
| `CustomerGroupMonitoring` | Monitors customer group behavior and composition |
| `Customer_EfficiencyV2` | Measures customer trading efficiency and activity |
| `MarketerRate_SSAS` | Marketer performance rating model |
| `SSAS_IMEMonitoring` | Mercantile Exchange (IME) monitoring model |

---

## 📈 Power BI Layer *(Coming Soon)*

The **Power BI** layer is the business-facing reporting layer that connects directly to SSAS models and prepared data sources to deliver interactive dashboards and scheduled reports.

### Planned Reports & Dashboards

| Report | Description |
|---|---|
| **Customer Efficiency Dashboard** | Visualizes trading activity, portfolio value, and efficiency scores per customer |
| **Marketer Performance Report** | Tracks marketer KPIs, rankings, and commission metrics |
| **Branch Ranking Dashboard** | Compares branch performance across regions and periods |
| **Settlement & Payment Analysis** | Monitors payment flows, settlement status, and outstanding balances |
| **Broker Transfer (ChangeBroker) Report** | Tracks inbound and outbound customer transfers between brokers |
| **IME Trading Monitor** | Real-time and historical view of Mercantile Exchange trades |
| **TSETMC Market Data Dashboard** | Stock market data: price movements, trading volume, index trends |
| **Call Center Analytics** | Call volume, resolution rates, and customer interaction trends |
| **Customer Group Monitoring** | Group composition changes, new joiners, and churned customers |
| **Club Member Score Report** | Loyalty program scores, tier distribution, and trend analysis |

### Power BI Architecture

```
SSAS Tabular / Direct Query
        │
        ▼
  Power BI Dataset
        │
        ▼
  Power BI Reports
        │
        ▼
  Power BI Dashboards  ──►  End Users / Management
```

> 📌 Power BI `.pbix` files and deployment scripts will be added to the `/PowerBI` folder in the next update.

---

## 🛠️ Technology Stack

| Layer | Technology |
|---|---|
| Data Ingestion | SSIS (SQL Server Integration Services) |
| API Collectors | C# / .NET 6 Console Applications |
| Staging & DW | SQL Server |
| Analytical Models | SSAS (Tabular & Multidimensional) |
| Reporting | Power BI |
| Source Control | Git / GitHub |

---

## 🚀 Getting Started

### Prerequisites

- SQL Server 2019 or later
- Visual Studio 2019/2022 with SSIS extension
- SQL Server Data Tools (SSDT)
- .NET 6 SDK
- Power BI Desktop *(for Power BI layer)*

### Running an ETL Package

1. Open the desired `.sln` file in Visual Studio
2. Configure connection managers to point to your environment
3. Build the project in `Development` configuration
4. Deploy the `.ispac` file to your SSIS catalog
5. Execute the package from SQL Server Agent or SSISDB

### Running a WebApi Collector

```bash
cd ETL/WebApi/<ProjectName>/<ProjectName>
dotnet run
```

---

## 📌 Notes

- The `WebApiRequests/` folder contains the **deployed runtime copies** of each Web API collector along with their execution logs.
- Log files (`.txt`) and build artifacts (`bin/`, `obj/`) are excluded from version control via `.gitignore`.
- The `old/` folders inside some projects contain archived previous versions kept for reference.

---

## 👤 Author

**Farid Negahbani**
Data Engineer | Business Intelligence Developer
[GitHub Profile](https://github.com/Farid89-data)
[Portfolio](https://www.datascienceportfol.io/frdngbsn)
