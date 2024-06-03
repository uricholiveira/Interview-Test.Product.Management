using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> AsQueryable(bool tracking = true, params Expression<Func<T, object>>[] includes);
}