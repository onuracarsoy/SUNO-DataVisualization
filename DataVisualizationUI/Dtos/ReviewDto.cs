using System.ComponentModel.DataAnnotations;

namespace DataVisualizationUI.Dtos
{
    public record ReviewDto
    {
        required public int ReviewId { get; init; }
        
        [Display(Name = "Ürün")]
        required public int ProductId { get; init; }
        
        required public string ProductName { get; init; }
        
        [Display(Name = "Müşteri")]
        required public int CustomerId { get; init; }
        
        required public string CustomerName { get; init; }
        
        [Display(Name = "Satın Alma Türü")]
        required public string PurchaseType { get; init; }
        
        [Display(Name = "Puan")]
        required public byte Rating { get; init; }
        
        [Display(Name = "Duygu Durumu")]
        public string? Sentiment { get; init; }
        
        [Display(Name = "Yorum")]
        required public string ReviewText { get; init; }
        
        [Display(Name = "Yorum Tarihi")]
        required public DateTime ReviewDate { get; init; }
    }

    public record CreateReviewDto
    {
        [Required(ErrorMessage = "Ürün seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ürün seçiniz")]
        [Display(Name = "Ürün")]
        required public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Müşteri seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir müşteri seçiniz")]
        [Display(Name = "Müşteri")]
        required public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Satın alma türü zorunludur")]
        [StringLength(50, ErrorMessage = "Satın alma türü en fazla 50 karakter olabilir")]
        [Display(Name = "Satın Alma Türü")]
        required public string PurchaseType { get; set; }
        
        [Required(ErrorMessage = "Puan zorunludur")]
        [Range(1, 5, ErrorMessage = "Puan 1-5 arasında olmalıdır")]
        [Display(Name = "Puan (1-5)")]
        required public byte Rating { get; set; }

        [StringLength(50, ErrorMessage = "Duygu durumu en fazla 50 karakter olabilir")]
        [Display(Name = "Duygu Durumu")]
        public string? Sentiment { get; set; }
        
        [Required(ErrorMessage = "Yorum metni zorunludur")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Yorum 10-250 karakter arasında olmalıdır")]
        [Display(Name = "Yorum")]
        required public string ReviewText { get; set; }
        
        [Required(ErrorMessage = "Yorum tarihi zorunludur")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Yorum Tarihi")]
        required public DateTime ReviewDate { get; set; }
    }

    public record UpdateReviewDto
    {
        [Required]
        required public int ReviewId { get; init; }
        
        [Required(ErrorMessage = "Ürün seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ürün seçiniz")]
        [Display(Name = "Ürün")]
        required public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Müşteri seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir müşteri seçiniz")]
        [Display(Name = "Müşteri")]
        required public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Satın alma türü zorunludur")]
        [StringLength(50, ErrorMessage = "Satın alma türü en fazla 50 karakter olabilir")]
        [Display(Name = "Satın Alma Türü")]
        required public string PurchaseType { get; set; }
        
        [Required(ErrorMessage = "Puan zorunludur")]
        [Range(1, 5, ErrorMessage = "Puan 1-5 arasında olmalıdır")]
        [Display(Name = "Puan (1-5)")]
        required public byte Rating { get; set; }
     
        [StringLength(50, ErrorMessage = "Duygu durumu en fazla 50 karakter olabilir")]
        [Display(Name = "Duygu Durumu")]
        public string? Sentiment { get; set; }
        
        [Required(ErrorMessage = "Yorum metni zorunludur")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Yorum 10-250 karakter arasında olmalıdır")]
        [Display(Name = "Yorum")]
        required public string ReviewText { get; set; }
        
        [Required(ErrorMessage = "Yorum tarihi zorunludur")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Yorum Tarihi")]
        required public DateTime ReviewDate { get; set; }
    }

    public record ReviewListDto
    {
        required public int ReviewId { get; init; }
        required public string ProductName { get; init; }
        required public string CustomerName { get; init; }
        required public byte Rating { get; init; }
        public string? Sentiment { get; init; }
        required public string ReviewText { get; init; }
        required public DateTime ReviewDate { get; init; }
    }
}
