using System.ComponentModel.DataAnnotations;

namespace DataVisualizationUI.Dtos
{
    // View DTO - All properties are init (read-only) (Görünüm DTO'su - Tüm özellikler init (salt okunur))
    public record CategoryDto
    {
        required public int CategoryId { get; init; }
        
        [Display(Name = "Kategori Adı")]
        required public string CategoryName { get; init; }
    }

    // Create DTO - User input fields are set (Oluşturma DTO'su - Kullanıcı giriş alanları ayarlandı)
    public record CreateCategoryDto
    {
        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kategori adı 2-100 karakter arasında olmalıdır")]
        [Display(Name = "Kategori Adı")]
        required public string CategoryName { get; set; }
    }

    // Update DTO - ID is init (cannot change), other fields are set (Güncelleme DTO'su - Kimlik init (değiştirilemez), diğer alanlar ayarlandı)
    public record UpdateCategoryDto
    {
        [Required]
        required public int CategoryId { get; init; }
        
        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kategori adı 2-100 karakter arasında olmalıdır")]
        [Display(Name = "Kategori Adı")]
        required public string CategoryName { get; set; }
    }
}
