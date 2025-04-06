using E_commerceTask.Application.Interfaces;
using E_commerceTask.Domain.Enums;
using E_commerceTask.Infrastructure.Data;
using E_commerceTask.Infrastructure.DbExtenstions;

namespace E_commerceTask.Infrastructure.Repositories;

public class BaseRepository<T>(ECommerceTaskContext context) : IBaseRepository<T> where T : class
{
    internal DbSet<T> dbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(int id)
    {
        var res = await context.FindAsync<T>(id);
        return res!;
    }
    public async Task<T> GetEntityWithIncludeAsync
        (Expression<Func<T, bool>> filter, string include) =>
      (await dbSet.Include(include).FirstOrDefaultAsync(filter))!;
    public async Task<T> GetByIdWithNoTrackingAsync(int id)
    {
        return await dbSet.AsNoTracking().FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
    }
    public async Task<List<T>> GetListByIdWithNoTrackingAsync(int id)
    {
        return await dbSet.AsNoTracking().Where(entity => EF.Property<int>(entity, "Id") == id).ToListAsync();
    }



    public async Task<IQueryable<TType>> GetSpecificSelectAsQuerableAsync<TType>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, TType>> select,
        string includeProperties = null!,
        int? skip = null,
        int? take = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!
       ) where TType : class
    {


        IQueryable<T> query = dbSet.AsNoTracking();

        if (includeProperties != null)
        {
            query.AsSplitQuery();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty).IgnoreQueryFilters();
        }



        if (filter != null)
            query = query.Where(filter);
        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);

        return query.Select(select);
    }


    public async Task<IEnumerable<TResult>> GetGrouped<TKey, TResult>(
        Expression<Func<T, TKey>> groupingKey,
        Expression<Func<IGrouping<TKey, T>, TResult>> resultSelector,
        string includeProperties = null!,
        int? skip = null,
        int? take = null,
        Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!)
    {
        var query = dbSet.AsQueryable();

        if (includeProperties != null)
        {
            query.AsSplitQuery();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty).DefaultIfEmpty().IgnoreQueryFilters();
        }

        if (filter != null)
            query = query.Where(filter);
        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);

        return await query.GroupBy(groupingKey).Select(resultSelector).ToListAsync();
    }






    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null!,
     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, Expression<Func<T, bool>> includeFilter = null!,
     string includeProperties = null!,
     int? skip = null,
     int? take = null)
    {
        IQueryable<T> query = dbSet.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }



    public async Task<bool> ExistAsync(int id) =>
        await dbSet.FindAsync(id) is not null;

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!
      )
    {
        IQueryable<T> query = dbSet.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);
        var querytest = query.ToQueryString();
        return await query.FirstOrDefaultAsync() is not null;
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null!,
        string includeProperties = null!

        )
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty).IgnoreQueryFilters().AsSplitQuery().AsNoTracking();

        return (await query.FirstOrDefaultAsync())!;
    }

    public async Task<IEnumerable<T>> GetWithJoinAsync(Expression<Func<T, bool>> predicate
        , string includeProperties)
    {
        IQueryable<T> query = dbSet;

        foreach (var includeProperty in includeProperties.Split(','))
        {
            query = query.Include(includeProperty).DefaultIfEmpty();
        }

        return await query.Where(predicate).ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await dbSet.AddRangeAsync(entities);
        return entities;
    }

    public T Remove(T entity)
    {
        dbSet.Remove(entity);
        return entity;
    }

    public T Update(T entity)
    {
        dbSet.Update(entity);
        return entity;
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!)
    {
        IQueryable<T> query = dbSet.AsNoTracking();



        if (filter != null)
            query = query.Where(filter);
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return await query.CountAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null!)
    {
        IQueryable<T> query = dbSet.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        return await query.AnyAsync();
    }

    public void RemoveRange(IEnumerable<T> entities) =>
            dbSet.RemoveRange(entities);

    public void UpdateRange(IEnumerable<T> entities) =>
        dbSet.UpdateRange(entities);

    //public async Task<T> ExcuteUpdateAsync(Expression<Func<T, bool>> filter)
    //{
    //    IQueryable<T> query = dbSet;

    //    bool isSucceded = await query.Where(filter).ExecuteUpdate(setters => setters);

    //    return entity;
    //}

    public async Task<bool> ExecuteDeleteAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;

        bool isSucceded = await query.Where(filter).ExecuteDeleteAsync() > 0;
        return isSucceded;
    }

    public Task<List<T>> GetAllWithIncludeAsync(string includeProperties)
    {
        IQueryable<T> query = dbSet.AsNoTracking();
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);
        return query.ToListAsync();
    }
    public async Task<T> GetLast(Expression<Func<T, object>> orderby)
    {
        return await dbSet.OrderBy(orderby).LastAsync();
    }


    public async Task<IEnumerable<TType>> GetSpecificSelectAsync<TType>(Expression<Func<T, bool>> filter, Expression<Func<T, TType>> select, string includeProperties = null, int? skip = null, int? take = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where TType : class
    {
        IQueryable<T> query = dbSet.AsNoTracking();

        if (includeProperties != null)
        {
            query.AsSplitQuery();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty).IgnoreQueryFilters();
        }

        if (filter != null)
            query = query.Where(filter);
        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);

        var querystring = query.ToQueryString();
        return query.Select(select).ToList();
    }

    public async Task<IQueryable<TType>> GetSpecificSelectTrackingAsync<TType>(Expression<Func<T, bool>> filter, Expression<Func<T, TType>> select, string includeProperties = null, int? skip = null, int? take = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where TType : class
    {
        IQueryable<T> query = dbSet;

        if (includeProperties != null)
        {
            query.AsSplitQuery();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty).IgnoreQueryFilters();
        }



        if (filter != null)
            query = query.Where(filter);
        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);

        return query.Select(select);
    }

    public async Task<int> MaxInCloumn(Expression<Func<T, int>> selector)
    {
        return await dbSet.MaxAsync(selector);
    }


    #region MasterQueryFilter
    public IQueryable<T> MasterQueryFilterQueryable(
        Expression<Func<T, object>> orderBySelector = null,
        OrderByTypesEnums orderByTypes = OrderByTypesEnums.Ascending,
        int pageNumber = 0,
        int pageSize = 10,
        bool isPaginated = false,
        bool isDistinct = false,
        int take = 0,
        bool tracking = true,
        bool igonreQueryFilter = false,
        params Expression<Func<T, bool>>[] filters)
    {
        return context.MasterQueryFilterQueryable(
           orderBySelector: orderBySelector,
           orderByTypes: orderByTypes,
            pageNumber: pageNumber,
            pageSize: pageSize,
            isPaginated: isPaginated,
            take: take,
            tracking: tracking,
            isDistinct: isDistinct,
            filters: filters,
            IgnoreQueryFilter: igonreQueryFilter
            );
    }

    public async Task<List<TResult>> MasterQueryFilterAsync<TResult>(
        Expression<Func<T, object>> orderBySelector = null,
        OrderByTypesEnums orderByTypes = OrderByTypesEnums.Ascending,
        Expression<Func<T, TResult>> selector = null,
        Func<TResult, TResult> selectorAfterResult = null,
        int pageNumber = 0,
        int pageSize = 10,
        bool isPaginated = false,
        bool isDistinct = false,
        int take = 0,
        bool tracking = false,
        bool igonreQueryFilter = false,
        params Expression<Func<T, bool>>[] filters)
    {
        return await context.MasterQueryFilterAsync(
           orderBySelector: orderBySelector,
           orderByTypes: orderByTypes,
            selector: selector,
            selectorAfterResult: selectorAfterResult,
            pageNumber: pageNumber,
            pageSize: pageSize,
            isPaginated: isPaginated,
            isDistinct:isDistinct,
            take: take,
            tracking: tracking,
            filters: filters,
            igonreQueryFilter:igonreQueryFilter
            );
    }

    public async Task<TResult> MasterQueryFilterFirstOrDefaultAsync<TResult>(
        Expression<Func<T, TResult>> selector = null,
        bool tracking = true,
        bool igonreQueryFilter = false,
        params Expression<Func<T, bool>>[] filters)
    {
        return await context.MasterQueryFilterFirstOrDefaultAsync(
          selector:  selector,
           tracking: tracking,
           filters: filters,
           igonreQueryFilter: igonreQueryFilter
            );
    }

    public async Task<bool> MasterQueryFilterExistAsync(
        bool igonreQueryFilter = true,
        params Expression<Func<T, bool>>[] filters)
    {
        return await context.MasterQueryFilterExistAsync(
            igonreQueryFilter:igonreQueryFilter,
            filters
            );
    }

    public async Task<int> MasterQueryFilterCountAsync(
        bool igonreQueryFilter = true,
        params Expression<Func<T, bool>>[] filters)
    {
        return await context.MasterQueryFilterCountAsync(
            igonreQueryFilter,
            filters);
    }
    #endregion
}