# SUNO DataVisualizationUI 📊

A modern and extensible business intelligence dashboard built with .NET 10. This project goes beyond basic visualization by integrating AI-driven advice (Semantic Kernel / LLM) and ML.NET time-series forecasting to provide actionable business insights in real time.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![AI](https://img.shields.io/badge/AI-SemanticKernel-yellowgreen?style=flat)](https://github.com/microsoft/semantic-kernel)
[![ML.NET](https://img.shields.io/badge/ML.NET-TimeSeries-blue?style=flat)](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet)

---

## Table of Contents

- [Key Features](#key-features)
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
  - [Database](#database)
  - [AI / Semantic Kernel](#ai--semantic-kernel)
- [Run the App](#run-the-app)
- [Project Structure](#project-structure)
- [API & Endpoints (summary)](#api--endpoints-summary)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)
- [Contact](#contact)

---

## Key Features

- Real-time dashboard with financial and operational metrics
- AI-driven business advice using Semantic Kernel + LLM (Server-Sent Events)
- ML.NET time-series forecasting for sales/orders
- Modern UI with Tailwind CSS and responsive widgets
- Map visualizations (Leaflet) and interactive charts
- Minimal API endpoints optimized for SSE and dashboard updates
- Entity Framework Core (SQL Server) for persistence

---

## Technologies

| Technology | Purpose |
|---|---|
| .NET 10 | Runtime / Web framework |
| ASP.NET Core MVC + Minimal API | Web UI + API endpoints |
| Entity Framework Core | Data access / Migrations |
| Semantic Kernel (OpenRouter / OpenAI) | AI business advice |
| ML.NET | Time-series forecasting |
| Tailwind CSS | UI styling |
| Leaflet.js | Map visualizations |
| AutoMapper | DTO mapping |

---

## Prerequisites

Make sure you have the following installed locally:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server or SQL Server LocalDB
- [Node.js & npm] (optional, if building client assets)
- Docker (optional, for running AI-related services or DB containers)

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/onuracarsoy/SUNO-DataVisualization.git
   cd SUNO-DataVisualization
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Apply EF Core migrations (if any) to create/update the database:
   ```bash
   dotnet ef database update --project DataVisualizationUI
   ```

---

## Configuration

Configuration values live in `appsettings.json` (and can be overridden via environment variables or secrets). Never commit production secrets.

### Database

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DataVisualizationDb;Trusted_Connection=True;"
}
```

If you use LocalDB, an example:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(LocalDB)\\\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\\\DataVisualizationDb.mdf;Integrated Security=True"
}
```

Then run EF migrations:
```bash
dotnet ef database update --project DataVisualizationUI
```

### AI / Semantic Kernel

To enable AI-driven advice, configure the Semantic Kernel / LLM provider settings in `appsettings.json`. Example using OpenRouter:

```json
"OpenRouterAI": {
  "Model": "gpt-4o-mini",
  "ApiKey": "YOUR_OPENROUTER_API_KEY",
  "Endpoint": "https://openrouter.ai/api/v1"
}
```

Security note: store API keys in environment variables or a secret manager for production.

---

## Run the App

From the project folder (DataVisualizationUI or solution root):

```bash
dotnet run --project DataVisualizationUI
```

By default, the web UI will be available at:
- https://localhost:7000 (example)
- http://localhost:5000

Open the dashboard, explore widgets, ask for AI advice (if configured), and try forecasting features.

---

## Project Structure

```
SUNO-DataVisualization/
├── DataVisualizationUI/                # ASP.NET Core MVC + Minimal API project (UI & APIs)
│   ├── Controllers/
│   ├── Endpoints/                      # Minimal API endpoints (SSE, etc.)
│   ├── ViewComponents/                 # Dashboard widgets
│   ├── Services/                       # Business logic, AI & ML services
│   ├── Context/                        # EF DbContext and entities
│   ├── Views/
│   └── wwwroot/                        # Static assets (MainUI, JS, CSS)
├── README.md
└── ...
```

---

## API & Endpoints (summary)

The solution exposes both MVC controllers for the UI and several Minimal API endpoints for dashboard updates, SSE streams, and AI advice. Key areas:

- Authentication / User context (if implemented via middleware)
- Dashboard endpoints: metrics, charts, map data
- AI advice endpoint: SSE stream that emits LLM-generated advice
- Forecasting endpoints: training/prediction (ML.NET)

Refer to the project `Controllers` and `Endpoints` folders for a complete, up-to-date list of routes and payloads.

---

## Screenshots

(Images are preserved from the repository.)

Dashboard screenshot 1  
<img width="1887" height="858" alt="Dashboard screenshot" src="https://github.com/user-attachments/assets/857b6861-94e4-432d-bca4-22ed94f23475" />

Dashboard screenshot 2  
<img width="1882" height="862" alt="Dashboard screenshot 2" src="https://github.com/user-attachments/assets/78e07ca9-fcd0-4cfa-b428-7b94ea962450" />

---

## Contributing

Contributions are welcome! Suggested workflow:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/my-feature`
3. Commit your changes and push
4. Open a Pull Request describing your changes

Please open issues for bugs or feature requests.

---

## License

This project is open source. Add a LICENSE file to specify the exact license (e.g., MIT) if desired.

---

## Acknowledgments

- [Semantic Kernel (Microsoft)](https://github.com/microsoft/semantic-kernel) — AI orchestration
- [ML.NET](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet) — Machine learning
- [Tailwind CSS](https://tailwindcss.com/) — Styling
- [Leaflet.js](https://leafletjs.com/) — Map visualizations

---

## Contact

Maintainer: onuracarsoy  
For questions or feedback, please open an issue or reach out via GitHub.

---

Made with ❤️ using .NET 10 and modern AI/ML tools.


<img wi


