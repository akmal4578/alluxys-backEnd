using System.ComponentModel.DataAnnotations;

namespace Services.Product.DTO
{
    public class Product
    {
        public Guid IdProduct { get; set; }
        [Required, Display(Name ="Please enter a name")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }
        public string AliasName { get; set; }
        public string Description { get; set; }
        public Guid? IdUserCreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? IdUserUpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long IdRefObjectState { get; set; }
    }
}
