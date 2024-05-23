namespace IdentityService.Entities.Abstracts
{
    public class BaseEntity<T> : IEntity
    {
        public T Id { get; }
    }
}
