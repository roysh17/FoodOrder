using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Repository.Dtos.Foods;

public class Food
{
    [Key]
    public int FoodId { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
