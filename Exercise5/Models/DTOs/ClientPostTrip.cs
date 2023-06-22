using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Exercise5.Models.DTOs
{
    public class ClientPostTrip
    {
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [BindRequired]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [BindRequired]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [BindRequired]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [BindRequired]
        public string Telephone { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [BindRequired]
        public string Pesel { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        [BindRequired]
        public string tripName { get; set; } = null!;
        public DateTime? PaymentDate { get; set; }
    }
}
