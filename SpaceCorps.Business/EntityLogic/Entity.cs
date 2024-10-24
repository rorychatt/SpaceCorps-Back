namespace SpaceCorps.Business.EntityLogic;

public class Entity : IEntity
{
    public Position Position { get; set; } = new(0, 0, 0);
}