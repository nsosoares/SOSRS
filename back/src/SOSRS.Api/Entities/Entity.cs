namespace SOSRS.Api.Entities;

public class Entity
{
    protected Entity(int id)
    {
        Id = id;
    }

    //Ef
    protected Entity() { }

    public int Id { get; protected set; } = default!;
}