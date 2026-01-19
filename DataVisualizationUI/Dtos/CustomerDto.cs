using System.ComponentModel.DataAnnotations;

namespace DataVisualizationUI.Dtos
{
    public record CustomerDto
    {
        required public int CustomerId { get; init; }
        
        [Display(Name = "Ad")]
        required public string CustomerName { get; init; }
        
        [Display(Name = "Soyad")]
        required public string CustomerSurname { get; init; }
        
        [Display(Name = "E-posta")]
        required public string CustomerEmail { get; init; }
        
        [Display(Name = "Telefon")]
        required public string CustomerPhone { get; init; }
        
        [Display(Name = "Profil Resmi")]
        public string? CustomerImageUrl { get; init; }
        
        [Display(Name = "Ülke")]
        required public string CustomerCountry { get; init; }
        
        [Display(Name = "Şehir")]
        required public string CustomerCity { get; init; }
        
        [Display(Name = "İlçe")]
        public string? CustomerDistrict { get; init; }
        
        [Display(Name = "Adres")]
        required public string CustomerAddress { get; init; }
        
        public string FullName => $"{CustomerName} {CustomerSurname}";
    }


    public record CreateCustomerDto
    {
        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 karakter arasında olmalıdır")]
        [Display(Name = "Ad")]
        required public string CustomerName { get; set; }
        
        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 karakter arasında olmalıdır")]
        [Display(Name = "Soyad")]
        required public string CustomerSurname { get; set; }
        
        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir")]
        [Display(Name = "E-posta")]
        required public string CustomerEmail { get; set; }
        
        [Required(ErrorMessage = "Telefon zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(20, ErrorMessage = "Telefon en fazla 20 karakter olabilir")]
        [Display(Name = "Telefon")]
        required public string CustomerPhone { get; set; }
        
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Profil Resmi URL")]
        public string? CustomerImageUrl { get; set; }
        
        [Required(ErrorMessage = "Ülke zorunludur")]
        [StringLength(50, ErrorMessage = "Ülke en fazla 50 karakter olabilir")]
        [Display(Name = "Ülke")]
        required public string CustomerCountry { get; set; }
        
        [Required(ErrorMessage = "Şehir zorunludur")]
        [StringLength(50, ErrorMessage = "Şehir en fazla 50 karakter olabilir")]
        [Display(Name = "Şehir")]
        required public string CustomerCity { get; set; }
        
        [StringLength(50, ErrorMessage = "İlçe en fazla 50 karakter olabilir")]
        [Display(Name = "İlçe")]
        public string? CustomerDistrict { get; set; }
        
        [Required(ErrorMessage = "Adres zorunludur")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Adres 10-250 karakter arasında olmalıdır")]
        [Display(Name = "Adres")]
        required public string CustomerAddress { get; set; }
    }

    public record UpdateCustomerDto
    {
        [Required]
        required public int CustomerId { get; init; }
        
        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 karakter arasında olmalıdır")]
        [Display(Name = "Ad")]
        required public string CustomerName { get; set; }
        
        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 karakter arasında olmalıdır")]
        [Display(Name = "Soyad")]
        required public string CustomerSurname { get; set; }
        
        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir")]
        [Display(Name = "E-posta")]
        required public string CustomerEmail { get; set; }
        
        [Required(ErrorMessage = "Telefon zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(20, ErrorMessage = "Telefon en fazla 20 karakter olabilir")]
        [Display(Name = "Telefon")]
        required public string CustomerPhone { get; set; }
        
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Profil Resmi URL")]
        public string? CustomerImageUrl { get; set; }
        
        [Required(ErrorMessage = "Ülke zorunludur")]
        [StringLength(50, ErrorMessage = "Ülke en fazla 50 karakter olabilir")]
        [Display(Name = "Ülke")]
        required public string CustomerCountry { get; set; }
        
        [Required(ErrorMessage = "Şehir zorunludur")]
        [StringLength(50, ErrorMessage = "Şehir en fazla 50 karakter olabilir")]
        [Display(Name = "Şehir")]
        required public string CustomerCity { get; set; }
        
        [StringLength(50, ErrorMessage = "İlçe en fazla 50 karakter olabilir")]
        [Display(Name = "İlçe")]
        public string? CustomerDistrict { get; set; }
        
        [Required(ErrorMessage = "Adres zorunludur")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Adres 10-250 karakter arasında olmalıdır")]
        [Display(Name = "Adres")]
        required public string CustomerAddress { get; set; }
    }

    public record CustomerListDto
    {
        required public int CustomerId { get; init; }
        required public string FullName { get; init; }
        required public string CustomerEmail { get; init; }
        required public string CustomerCountry { get; init; }
        required public string CustomerCity { get; init; }
        public string? CustomerImageUrl { get; init; }
        required public int TotalOrders { get; init; }
    }
}
