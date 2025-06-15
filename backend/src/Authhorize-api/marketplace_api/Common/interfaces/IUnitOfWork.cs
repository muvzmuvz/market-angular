namespace marketplace_api.Common.interfaces;

public interface IUnitOfWork
{
  public Task commitChange();
}
