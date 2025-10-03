using Tasky.Domain.Common;

namespace Tasky.Domain.Projects.Events;

public sealed class ModifiedProjectEvent : DomainEvent
{

  public string Tag { get; set; } = string.Empty;
}