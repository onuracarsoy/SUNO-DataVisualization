# SUNO DataVisualizationUI 📊

Modern ve güçlü bir iş zekası dashboard uygulaması. .NET 10 ile geliştirilen bu proje, verilerinizi görselleştirmenin ötesine geçerek **yapay zeka (AI)** ve **makine öğrenimi (ML)** desteğiyle işletmenize değer katar.

## 🚀 Öne Çıkan Özellikler

- **🤖 AI Business Advice (SSE):** Semantic Kernel ve LLM entegrasyonu ile dashboard verilerinizden gerçek zamanlı iş tavsiyeleri üretir. Server-Sent Events (SSE) kullanılarak tavsiyeler anlık olarak akar.
- **📈 ML.NET Forecasting:** Satış ve sipariş verileriniz üzerinde ML.NET Time Series algoritmalarını kullanarak gelecek tahminleri yapar.
- **⚡ Modern Dashboard:** Siparişler, müşteriler, ürünler ve kategoriler için detaylı görselleştirmeler.
- **🏢 Minimal API & SSE:** Dashboard güncellemeleri ve AI akışları için optimize edilmiş Minimal API yapısı.
- **🎨 Tailwind CSS Stylings:** Şık ve dinamik kullanıcı arayüzü (bazı bileşenlerde Tailwind entegrasyonu).

## 🛠️ Teknoloji Yığını

*   **Runtime:** .NET 10
*   **Web Framework:** ASP.NET Core MVC & Minimal API
*   **Database:** Entity Framework Core (SQL Server)
*   **AI Engine:** Microsoft Semantic Kernel (OpenRouter / OpenAI LLM)
*   **Machine Learning:** ML.NET (TimeSeries Forecasting)
*   **Mapping:** AutoMapper
*   **Styling:** CSS & Tailwind CSS

## ⚙️ Kurulum ve Yapılandırma

### 1. Veritabanı Hazırlığı
`appsettings.json` dosyasındaki `DefaultConnection` bağlantı dizesini kendi SQL Server ayarlarınıza göre güncelleyin.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DataVisualizationDb;Trusted_Connection=True;"
}
```

Migrations paketlerini uygulayın:
```powershell
dotnet ef database update
```

### 2. AI Yapılandırması (Semantic Kernel)
Uygulamanın AI tavsiyeleri verebilmesi için `appsettings.json` dosyasında OpenRouter (veya OpenAI) ayarlarını yapın:

```json
"OpenRouterAI": {
  "Model": "your-model-id",
  "ApiKey": "your-api-key",
  "Endpoint": "https://openrouter.ai/api/v1"
}
```

### 3. Çalıştırma
Projeyi başlatmak için terminalden:
```powershell
dotnet run
```

## 📂 Proje Yapısı

- `Controllers`: MVC controller mantığı.
- `Endpoints`: Minimal API ve SSE uç noktaları.
- `ViewComponents`: Dashboard widget'ları ve bileşenleri.
- `Services`: İş mantığı, AI servisleri ve ML servisleri.
- `Context`: DBContext ve veri katmanı.
- `MLModels`: ML.NET tahmin modelleri ve eğitim mantığı.

<img width="1887" height="858" alt="Ekran görüntüsü 2026-01-19 231024" src="https://github.com/user-attachments/assets/857b6861-94e4-432d-bca4-22ed94f23475" />
<img width="1882" height="862" alt="Ekran görüntüsü 2026-01-19 230954" src="https://github.com/user-attachments/assets/78e07ca9-fcd0-4cfa-b428-7b94ea962450" />


