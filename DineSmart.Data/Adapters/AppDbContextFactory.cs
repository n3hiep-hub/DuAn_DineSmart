using DuAn_DineSmart.DAL;

namespace DineSmart.Data.Adapters;

public interface IAppDbContextFactory
{
    AppDbContext Create();
}

public class AppDbContextFactory : IAppDbContextFactory
{
    public AppDbContext Create() => new();
}
