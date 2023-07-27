namespace eShop.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; private init; }

    protected Entity() { }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;
        if (other?.GetType() != this.GetType())
            return false;
        if (this.Id != other?.Id)
            return false;

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && obj is Entity && Equals(obj as Entity);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}

