namespace PUSL2020.Application.Services;

public interface IUnitOfWork
{
    Task CompleteAsync();
}