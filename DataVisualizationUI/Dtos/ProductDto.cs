using System.ComponentModel.DataAnnotations;

namespace DataVisualizationUI.Dtos
{
    public record ProductDto
    {
        required public int ProductId { get; init; }
        
        [Display(Name = "Ürün Adı")]
        required public string ProductName { get; init; }
        
        [Display(Name = "Açıklama")]
        required public string ProductDescription { get; init; }
        
        [Display(Name = "Birim Fiyat")]
        [DataType(DataType.Currency)]
        required public decimal UnitPrice { get; init; }
        
        [Display(Name = "Menşei Ülke")]
        required public string CountryOfOrigin { get; init; }
        
        [Display(Name = "Ürün Görseli")]
        required public string ProductImageUrl { get; init; }
        
        [Display(Name = "Stok Miktarı")]
        required public int StockQuantity { get; init; }
        
        [Display(Name = "Kategori")]
        required public int CategoryId { get; init; }
        
        required public string CategoryName { get; init; }
    }

    public record CreateProductDto
    {
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ürün adı 2-100 karakter arasında olmalıdır")]
        [Display(Name = "Ürün Adı")]
        required public string ProductName { get; set; }
        
        [Required(ErrorMessage = "Ürün açıklaması zorunludur")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Açıklama 10-250 karakter arasında olmalıdır")]
        [Display(Name = "Ürün Açıklaması")]
        required public string ProductDescription { get; set; }
        
        [Required(ErrorMessage = "Birim fiyat zorunludur")]
        [Range(0.01, 1000000, ErrorMessage = "Fiyat 0.01 ile 1.000.000 arasında olmalıdır")]
        [DataType(DataType.Currency)]
        [Display(Name = "Birim Fiyat (₺)")]
        required public decimal UnitPrice { get; set; }
        
        [Required(ErrorMessage = "Menşei ülke zorunludur")]
        [StringLength(50, ErrorMessage = "Menşei ülke en fazla 50 karakter olabilir")]
        [Display(Name = "Menşei Ülke")]
        required public string CountryOfOrigin { get; set; }
        
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Ürün Görseli URL")]
        public string? ProductImageUrl { get; set; }
        
        [Required(ErrorMessage = "Stok miktarı zorunludur")]
        [Range(0, 100000, ErrorMessage = "Stok miktarı 0-100000 arasında olmalıdır")]
        [Display(Name = "Stok Miktarı")]
        required public int StockQuantity { get; set; }
        
        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir kategori seçiniz")]
        [Display(Name = "Kategori")]
        required public int CategoryId { get; set; }
    }

    public record UpdateProductDto
    {
        [Required]
        required public int ProductId { get; init; }
        
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ürün adı 2-100 karakter arasında olmalıdır")]
        [Display(Name = "Ürün Adı")]
        required public string ProductName { get; set; }
        
        [Required(ErrorMessage = "Ürün açıklaması zorunludur")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Açıklama 10-250 karakter arasında olmalıdır")]
        [Display(Name = "Ürün Açıklaması")]
        required public string ProductDescription { get; set; }
        
        [Required(ErrorMessage = "Birim fiyat zorunludur")]
        [Range(0.01, 1000000, ErrorMessage = "Fiyat 0.01 ile 1.000.000 arasında olmalıdır")]
        [DataType(DataType.Currency)]
        [Display(Name = "Birim Fiyat (₺)")]
        required public decimal UnitPrice { get; set; }
        
        [Required(ErrorMessage = "Menşei ülke zorunludur")]
        [StringLength(50, ErrorMessage = "Menşei ülke en fazla 50 karakter olabilir")]
        [Display(Name = "Menşei Ülke")]
        required public string CountryOfOrigin { get; set; }
        
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Ürün Görseli URL")]
        public string? ProductImageUrl { get; set; }
        
        [Required(ErrorMessage = "Stok miktarı zorunludur")]
        [Range(0, 100000, ErrorMessage = "Stok miktarı 0-100000 arasında olmalıdır")]
        [Display(Name = "Stok Miktarı")]
        required public int StockQuantity { get; set; }
        
        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir kategori seçiniz")]
        [Display(Name = "Kategori")]
        required public int CategoryId { get; set; }
    }

    public record ProductListDto
    {
        required public int ProductId { get; init; }
        required public string ProductName { get; init; }
        
        [DataType(DataType.Currency)]
        required public decimal UnitPrice { get; init; }
        
        required public int StockQuantity { get; init; }
        required public string CategoryName { get; init; }
        required public string ProductImageUrl { get; init; }
        required public int TotalOrders { get; init; }
        required public double AverageRating { get; init; }
    }
}
