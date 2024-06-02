namespace Infrastructure.Abstracts;

public class DbEntity<T>
{
    public required T Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}