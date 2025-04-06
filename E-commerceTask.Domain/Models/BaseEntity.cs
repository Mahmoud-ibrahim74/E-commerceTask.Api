namespace E_commerceTask.Domain.Models;

public class BaseEntity
{
    public DateTime? Add_date { get; set; } 
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }

    public int? Added_by { get; set; }
    public int? UpdateBy { get; set; }
}
