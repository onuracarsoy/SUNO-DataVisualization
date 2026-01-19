using System.ComponentModel.DataAnnotations;

namespace DataVisualizationUI.Dtos
{
    public record OrderDto
    {
        required public int OrderId { get; init; }
        
        [Display(Name = "Ürün")]
        required public int ProductId { get; init; }
        
        required public string ProductName { get; init; }
        
        [Display(Name = "Müşteri")]
        required public int CustomerId { get; init; }
        
        required public string CustomerName { get; init; }
        
        [Display(Name = "Miktar")]
        required public int Quantity { get; init; }
        
        [Display(Name = "Ödeme Yöntemi")]
        required public string PaymentMethod { get; init; }
        
        [Display(Name = "Sipariş Durumu")]
        required public string OrderStatus { get; init; }
        
        [Display(Name = "Sipariş Tarihi")]
        required public DateTime OrderDate { get; init; }
        
        [Display(Name = "Notlar")]
        public string? OrderNotes { get; init; }
        
        [Display(Name = "Toplam Tutar")]
        required public decimal TotalPrice { get; init; }
    }

    public record CreateOrderDto
    {
        [Required(ErrorMessage = "Ürün seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ürün seçiniz")]
        [Display(Name = "Ürün")]
        required public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Müşteri seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir müşteri seçiniz")]
        [Display(Name = "Müşteri")]
        required public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(1, 10000, ErrorMessage = "Miktar 1-10000 arasında olmalıdır")]
        [Display(Name = "Miktar")]
        required public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Ödeme yöntemi zorunludur")]
        [StringLength(50, ErrorMessage = "Ödeme yöntemi en fazla 50 karakter olabilir")]
        [Display(Name = "Ödeme Yöntemi")]
        required public string PaymentMethod { get; set; }
        
        [Required(ErrorMessage = "Sipariş durumu zorunludur")]
        [StringLength(50, ErrorMessage = "Sipariş durumu en fazla 50 karakter olabilir")]
        [Display(Name = "Sipariş Durumu")]
        required public string OrderStatus { get; set; }
        
        [Required(ErrorMessage = "Sipariş tarihi zorunludur")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Sipariş Tarihi")]
        required public DateTime OrderDate { get; set; }
        
        [StringLength(250, ErrorMessage = "Notlar en fazla 250 karakter olabilir")]
        [Display(Name = "Sipariş Notları")]
        public string? OrderNotes { get; set; }
    }

    public record UpdateOrderDto
    {
        [Required]
        required public int OrderId { get; init; }
        
        [Required(ErrorMessage = "Ürün seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ürün seçiniz")]
        [Display(Name = "Ürün")]
        required public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Müşteri seçimi zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir müşteri seçiniz")]
        [Display(Name = "Müşteri")]
        required public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Miktar zorunludur")]
        [Range(1, 10000, ErrorMessage = "Miktar 1-10000 arasında olmalıdır")]
        [Display(Name = "Miktar")]
        required public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Ödeme yöntemi zorunludur")]
        [StringLength(50, ErrorMessage = "Ödeme yöntemi en fazla 50 karakter olabilir")]
        [Display(Name = "Ödeme Yöntemi")]
        required public string PaymentMethod { get; set; }
        
        [Required(ErrorMessage = "Sipariş durumu zorunludur")]
        [StringLength(50, ErrorMessage = "Sipariş durumu en fazla 50 karakter olabilir")]
        [Display(Name = "Sipariş Durumu")]
        required public string OrderStatus { get; set; }
        
        [Required(ErrorMessage = "Sipariş tarihi zorunludur")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Sipariş Tarihi")]
        required public DateTime OrderDate { get; set; }
        
        [StringLength(250, ErrorMessage = "Notlar en fazla 250 karakter olabilir")]
        [Display(Name = "Sipariş Notları")]
        public string? OrderNotes { get; set; }
    }

    public record OrderListDto
    {
        required public int OrderId { get; init; }
        required public string ProductName { get; init; }
        required public string CustomerName { get; init; }
        required public int Quantity { get; init; }
        required public decimal TotalPrice { get; init; }
        required public string PaymentMethod { get; init; }
        required public string OrderStatus { get; init; }
        required public DateTime OrderDate { get; init; }
    }
}
