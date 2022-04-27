namespace PUSL2020.Domain.Entities;

public class ImageResource
{
    public ImageResource()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }
    public string CreatedAt { get; set; }
}