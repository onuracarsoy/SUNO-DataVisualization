namespace DataVisualizationUI.Models.ForecastViewModels.ReviewForecastViewModels
{
    public record ReviewSentimentViewModel
    {
        public double PositivePercentage { get; init; }
        public double NegativePercentage { get; init; }
        public double NeutralPercentage { get; init; }
        public int TotalReviews { get; init; }
        public required List<ReviewSentimentItem> RecentReviews { get; init; }
    }

    public record ReviewSentimentItem
    {
        public required string CustomerName { get; init; }
        public required string ReviewText { get; init; }
        public required string Sentiment { get; init; } // "Positive", "Negative", "Neutral"
        public int Rating { get; init; }
        public DateTime Date { get; init; }
    }
}
