using System.Linq.Expressions;
using Infrastructure.Database;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstracts;

public class Repository<T, TT> : IRepository<T> where T : DbEntity<TT>
{
    private readonly DatabaseContext _context;

    public Repository(DatabaseContext context)
    {
        _context = context;
    }

    public IQueryable<T> AsQueryable(bool tracking = true, params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return includes.Aggregate(query,
            (current, include) => current.Include(include)
        );
    }
}