namespace Core.Entities.Base;


public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
