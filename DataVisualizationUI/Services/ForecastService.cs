using DataVisualizationUI.Context;
using DataVisualizationUI.Models.ForecastViewModels.OrderForecastViewModels;
using DataVisualizationUI.Models.ForecastViewModels.ReviewForecastViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System.Globalization;

namespace DataVisualizationUI.Services
{
    public class ForecastService
    {
        private readonly DataVisualizationDbContext _context;
        private readonly MLContext _mlContext;

        public ForecastService(DataVisualizationDbContext context)
        {
            _context = context;
            _mlContext = new MLContext(seed: 0);
        }

       

        //Order Country Forecast (Sipariş Ülke Tahmini)
        public async Task<List<CountryForecastViewModel>> GetOrderCountryForecastAsync()
        {
            // 1️⃣ Fetch data for the last 12 months from the database via Customer.CustomerCountry (Veritabanından son 12 ayın verilerini çek (Customer.CustomerCountry üzerinden))
            var endDate = DateTime.Now;
            var startDate = new DateTime(endDate.Year - 1, endDate.Month, 1);

            var monthlyData = _context.Orders
                .Include(o => o.Customers) // For country information (Country bilgisi için)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .AsEnumerable()
                .Where(o => !string.IsNullOrEmpty(o.Customers?.CustomerCountry)) // Ensure no empty countries (Boş ülke olmasın)
                .GroupBy(o => new
                {
                    Country = o.Customers.CustomerCountry,
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month
                })
                .Select(g => new
                {
                    Country = g.Key.Country,
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Count = (float)g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            var viewModels = new List<CountryForecastViewModel>();
            var countries = monthlyData.Select(x => x.Country).Distinct();

            // 2️⃣ Make forecasts for each country (Her ülke için tahmin yap)
            foreach (var country in countries)
            {
                var countryData = monthlyData
                    .Where(x => x.Country == country)
                    .OrderBy(x => x.Date)
                    .ToList();

                // ML.NET SSA requires at least 6 months of data (trainSize > 2 * windowSize rule) (ML.NET SSA için minimum 6 ay veri olmalı (trainSize > 2 * windowSize kuralı))
                if (countryData.Count < 6) continue;

                // 3️⃣ Prepare data for ML.NET (ML.NET için veri hazırla)
                var mlInputData = countryData.Select((x, idx) => new CountryForecastData
                {
                    MonthIndex = idx + 1,
                    OrderCount = x.Count
                }).ToList();

                var dataView = _mlContext.Data.LoadFromEnumerable(mlInputData);

                // 4️⃣ SSA Forecasting Pipeline (SSA Tahmin Boru Hattı)
                int windowSize = Math.Max(2, countryData.Count / 3);
                int horizon = Math.Min(12, countryData.Count);

                var pipeline = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: nameof(CountryForecastData.OrderCount),
                    windowSize: windowSize,
                    seriesLength: countryData.Count,
                    trainSize: countryData.Count,
                    horizon: horizon
                );

                // 5️⃣ Train the model and make forecasts (Model eğit ve tahmin yap)
                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<CountryForecastData, CountryForecastPrediction>(_mlContext);
                var prediction = engine.Predict();

                // 6️⃣ Convert results to UI format (Sonuçları UI formatına dönüştür)
                var turCulture = CultureInfo.GetCultureInfo("tr-TR");

                var historicalPoints = countryData.Select(x => new MonthlyData
                {
                    Period = x.Date.ToString("MMM yy", turCulture),
                    Value = x.Count
                }).ToList();

                var forecastPoints = new List<MonthlyData>();
                var lastDate = countryData.Last().Date;
                float totalForecast = 0;

                for (int i = 0; i < prediction.ForecastedValues.Length; i++)
                {
                    lastDate = lastDate.AddMonths(1);
                    float value = Math.Max(0, prediction.ForecastedValues[i]);

                    forecastPoints.Add(new MonthlyData
                    {
                        Period = lastDate.ToString("MMM yy", turCulture),
                        Value = value
                    });
                    totalForecast += value;
                }

                // 7️⃣ Calculate growth (Büyüme hesapla)
                float totalHistorical = countryData.Sum(x => x.Count);
                double growth = totalHistorical > 0
                    ? ((totalForecast - totalHistorical) / totalHistorical) * 100
                    : 0;

                viewModels.Add(new CountryForecastViewModel
                {
                    CountryName = country,
                    HistoricalData = historicalPoints,
                    ForecastData = forecastPoints,
                    GrowthPercentage = growth,
                    TotalHistorical = (int)totalHistorical,
                    TotalForecast = (int)totalForecast
                });
            }

            // Sort by countries with most orders first (En çok sipariş alan ülkeleri başa al)
            viewModels = viewModels.OrderByDescending(x => x.TotalHistorical).ToList();

            return viewModels;
        }


        //Order Payment Method Forecast (Sipariş Ödeme Yöntemi Tahmini)
        public async Task<List<PaymentMethodForecastViewModel>> GetOrderPaymentMethodForecastAsync()
        {
            // 1️⃣ Fetch data for the last 12 months from the database (Veritabanından son 12 ayın verilerini çek)
            var endDate = DateTime.Now;
            var startDate = new DateTime(endDate.Year - 1, endDate.Month, 1);

            var monthlyData = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .AsEnumerable()
                .GroupBy(o => new
                {
                    o.PaymentMethod,
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month
                })
                .Select(g => new
                {
                    PaymentMethod = g.Key.PaymentMethod,
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Count = (float)g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            var viewModels = new List<PaymentMethodForecastViewModel>();
            var paymentMethods = monthlyData.Select(x => x.PaymentMethod).Distinct();

            // 2️⃣ Make forecasts for each payment method (Her ödeme yöntemi için tahmin yap)
            foreach (var method in paymentMethods)
            {
                var methodData = monthlyData
                    .Where(x => x.PaymentMethod == method)
                    .OrderBy(x => x.Date)
                    .ToList();

                // ML.NET SSA requires at least 6 months of data (trainSize > 2 * windowSize rule) (ML.NET SSA için minimum 6 ay veri olmalı (trainSize > 2 * windowSize kuralı))
                if (methodData.Count < 6) continue;

                // 3️⃣ Prepare data for ML.NET (ML.NET için veri hazırla)
                var mlInputData = methodData.Select((x, idx) => new PaymentForecastData
                {
                    MonthIndex = idx + 1,
                    OrderCount = x.Count
                }).ToList();

                var dataView = _mlContext.Data.LoadFromEnumerable(mlInputData);

                // 4️⃣ SSA Forecasting Pipeline (SSA Tahmin Boru Hattı)

                int windowSize = Math.Max(2, methodData.Count / 3);
                int horizon = Math.Min(12, methodData.Count);

                var pipeline = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: nameof(PaymentForecastData.OrderCount),
                    windowSize: windowSize,
                    seriesLength: methodData.Count,
                    trainSize: methodData.Count,
                    horizon: horizon
                );

                // 5️⃣ Train the model and make forecasts (Model eğit ve tahmin yap)
                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<PaymentForecastData, PaymentForecastPrediction>(_mlContext);
                var prediction = engine.Predict();

                // 6️⃣ Convert results to UI format (Sonuçları UI formatına dönüştür)
                var turCulture = CultureInfo.GetCultureInfo("tr-TR");

                var historicalPoints = methodData.Select(x => new MonthlyData
                {
                    Period = x.Date.ToString("MMM yy", turCulture),
                    Value = x.Count
                }).ToList();

                var forecastPoints = new List<MonthlyData>();
                var lastDate = methodData.Last().Date;
                float totalForecast = 0;

                for (int i = 0; i < prediction.ForecastedValues.Length; i++)
                {
                    lastDate = lastDate.AddMonths(1);
                    float value = Math.Max(0, prediction.ForecastedValues[i]); // Ensure no negative values (Negatif değer olmasın)

                    forecastPoints.Add(new MonthlyData
                    {
                        Period = lastDate.ToString("MMM yy", turCulture),
                        Value = value
                    });
                    totalForecast += value;
                }

                // 7️⃣ Calculate growth (Büyüme hesapla)
                float totalHistorical = methodData.Sum(x => x.Count);
                double growth = totalHistorical > 0
                    ? ((totalForecast - totalHistorical) / totalHistorical) * 100
                    : 0;

                viewModels.Add(new PaymentMethodForecastViewModel
                {
                    PaymentMethod = method,
                    HistoricalData = historicalPoints,
                    ForecastData = forecastPoints,
                    GrowthPercentage = growth
                });
            }

            return viewModels;
        }



        // Review Sentiment Forecast (Yorum Duygu Analizi Tahmini)
        public async Task<ReviewSentimentViewModel> GetReviewSentimentForecastAsync()
        {
            // 1️⃣ Prepare Data (Label according to Rating) (Veriyi Hazırla (Rating'e göre Etiketle))
            // 4-5 Stars = Positive (true), 1-2-3 Stars = Negative (false) (4-5 Yıldız = Pozitif (true), 1-2-3 Yıldız = Negatif (false))
            // In reality, 3 could be Neutral, but for Binary Classification we'll use Negative or a separate logic for now (Gerçek dünyada 3 Nötr olabilir ama Binary Classification için şimdilik Negatif veya ayrı bir mantık gerekebilir.)
            // To simplify: >= 4 Positive, < 4 Negative (Basitleştirmek için: >= 4 Pozitif, < 4 Negatif diyelim.)

            var reviews = await _context.Reviews
                .Include(r => r.Customer)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();


            if (!reviews.Any())
            {
                return new ReviewSentimentViewModel
                {
                    RecentReviews = new List<ReviewSentimentItem>(),
                    TotalReviews = 0
                };
            }

            var trainingData = reviews.Select(r => new SentimentData
            {
                SentimentText = r.ReviewText,
                Sentiment = r.Rating >= 4 // Labeling logic (Etiketleme mantığı)
            }).ToList();


            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);


            var pipeline = _mlContext.Transforms.Text.FeaturizeText(
                outputColumnName: "Features",
                inputColumnName: nameof(SentimentData.SentimentText))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                labelColumnName: "Label",
                featureColumnName: "Features"));

            var model = pipeline.Fit(dataView);


            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            int positiveCount = 0;
            int negativeCount = 0;
            var sentimentItems = new List<ReviewSentimentItem>();

            foreach (var review in reviews)
            {
                var input = new SentimentData { SentimentText = review.ReviewText };
                var prediction = predictionEngine.Predict(input);

                bool isPositive = prediction.Prediction;
                string sentimentLabel = isPositive ? "Positive" : "Negative";

                if (isPositive) positiveCount++;
                else negativeCount++;


                if (sentimentItems.Count < 10)
                {
                    sentimentItems.Add(new ReviewSentimentItem
                    {
                        CustomerName = review.Customer != null ? $"{review.Customer.CustomerName} {review.Customer.CustomerSurname}" : "Anonim",
                        ReviewText = review.ReviewText,
                        Rating = review.Rating,
                        Date = review.ReviewDate,
                        Sentiment = sentimentLabel
                    });
                }
            }


            int total = reviews.Count;
            double posPct = total > 0 ? (double)positiveCount / total * 100 : 0;
            double negPct = total > 0 ? (double)negativeCount / total * 100 : 0;

            return new ReviewSentimentViewModel
            {
                TotalReviews = total,
                PositivePercentage = Math.Round(posPct, 1),
                NegativePercentage = Math.Round(negPct, 1),
                NeutralPercentage = 0, // Binary model used (İkili model kullanıldı)
                RecentReviews = sentimentItems
            };
        }
    }
}
