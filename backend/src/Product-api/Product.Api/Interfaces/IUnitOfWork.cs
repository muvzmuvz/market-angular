namespace Products.Api.Interfaces;

public interface IUnitOfWork
{
  public Task commitChange();
}
