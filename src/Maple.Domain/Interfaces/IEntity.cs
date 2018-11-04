namespace Maple.Domain
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}
