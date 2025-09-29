using System.ComponentModel.DataAnnotations.Schema;

namespace Tasky.Domain.Common;

public abstract class Entity
{
  public Guid Id { get; protected set; }

  private readonly List<DomainEvent> _domainEvents = new();

  [NotMapped]
  public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();


  protected Entity() { }

  protected Entity(Guid guid)
  {
    Id = guid == Guid.Empty ? Guid.NewGuid() : guid;
  }

  public void AddDomainEvent(DomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }
  public void RemoveDomainEvent(DomainEvent domainEvent)
  {
    _domainEvents.Remove(domainEvent);
  }
  public void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }

}