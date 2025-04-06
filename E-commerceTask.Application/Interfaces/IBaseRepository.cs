
using E_commerceTask.Domain.Enums;

namespace E_commerceTask.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdWithNoTrackingAsync(int id);
        Task<T> GetLast(Expression<Func<T, object>> orderby);
        Task<IQueryable<TType>> GetSpecificSelectTrackingAsync<TType>(
              Expression<Func<T, bool>> filter,
              Expression<Func<T, TType>> select,
              string includeProperties = null!,
              int? skip = null,
              int? take = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!
             ) where TType : class;
        public Task<T> GetEntityWithIncludeAsync(Expression<Func<T, bool>> filter, string include);
        Task<IEnumerable<TType>> GetSpecificSelectAsync<TType>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TType>> select,
            string includeProperties = null!,
            int? skip = null,
            int? take = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!) where TType : class;


        Task<IQueryable<TType>> GetSpecificSelectAsQuerableAsync<TType>(
          Expression<Func<T, bool>> filter,
          Expression<Func<T, TType>> select,
          string includeProperties = null!,
          int? skip = null,
          int? take = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!) where TType : class;

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null!,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, Expression<Func<T, bool>> includeFilter = null!,
         string includeProperties = null!,
         int? skip = null,
         int? take = null);

        public Task<List<T>> GetAllWithIncludeAsync(string includeProperties);

        Task<bool> ExistAsync(int id);
        public Task<List<T>> GetListByIdWithNoTrackingAsync(int id);

        Task<bool> ExistAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!);
        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null!,
            string includeProperties = null!
        );

        public Task<int> MaxInCloumn(Expression<Func<T, int>> selector);
        Task<IEnumerable<TResult>> GetGrouped<TKey, TResult>(
            Expression<Func<T, TKey>> groupingKey,
            Expression<Func<IGrouping<TKey, T>, TResult>> resultSelector,
            string includeProperties = null!,
            int? skip = null,
            int? take = null,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!);
        Task<IEnumerable<T>> GetWithJoinAsync(Expression<Func<T, bool>> predicate
            , string includeProperties);
        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Remove(T entity);

        T Update(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null!);
        void RemoveRange(IEnumerable<T> entities);
        Task<bool> ExecuteDeleteAsync(Expression<Func<T, bool>> filter);
        void UpdateRange(IEnumerable<T> entities);


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
            params Expression<Func<T, bool>>[] filters);

        public Task<List<TResult>> MasterQueryFilterAsync<TResult>(
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
            params Expression<Func<T, bool>>[] filters);

        public Task<TResult> MasterQueryFilterFirstOrDefaultAsync<TResult>(
            Expression<Func<T, TResult>> selector = null,
            bool tracking = true,
            bool igonreQueryFilter = false,
            params Expression<Func<T, bool>>[] filters);

        public Task<bool> MasterQueryFilterExistAsync(
            bool igonreQueryFilter = true,
            params Expression<Func<T, bool>>[] filters);

        public Task<int> MasterQueryFilterCountAsync(
            bool igonreQueryFilter = true,
            params Expression<Func<T, bool>>[] filters);
        #endregion

    }
}