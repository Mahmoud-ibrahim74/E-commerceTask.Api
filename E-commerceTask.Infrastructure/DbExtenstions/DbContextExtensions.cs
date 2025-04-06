using System.Dynamic;
using System.Linq.Expressions;
using E_commerceTask.Domain.Enums;

namespace E_commerceTask.Infrastructure.DbExtenstions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// This extension  method to make soft delete for any prop name  then save changes 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_context"></param>
        /// <param name="_entity"></param>
        /// <param name="_softDeleteProperty"></param>
        /// <returns>number of rows affected</returns>
        public static async Task<int> SoftDeleteAsync<T>(this DbContext _context, T _entity, string _softDeleteProperty = "IsDeleted", bool IsDeleted = true, string DeletedBy = null)
        {
            if (_entity is null)
                return 0;

            var entry = _context.Entry(_entity);
            entry.Property(_softDeleteProperty).CurrentValue = IsDeleted;
            entry.Property("DeleteDate").CurrentValue = DateTime.Now;
            entry.Property("DeleteBy").CurrentValue = DeletedBy;
            entry.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// This extension method to begin - end transaction with commit and rollback 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_action"></param>
        public static async void ExecuteInTransaction(this DbContext _context, Action _action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _action();
                await _context.SaveChangesAsync();
                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }




        public static string DatabaseName(this DbContext _context)
        {

            var db = _context.Database.GetDbConnection().Database;
            return db;
        }

        /// <summary>
        /// Retrieves a query filtered and optionally paginated <typeparamref name="T"/> 
        /// from the database context. Allows for applying custom query filters, sorting, distinct selection, 
        /// and projection of specific fields using a selector.
        /// </summary>
        /// <typeparam name="T">The type of the entity to retrieve from the database.</typeparam>
        /// <param name="context">The <see cref="DbContext"/> instance to query the data from.</param>
        /// <param name="orderBySelector">An optional expression to specify the property for ordering.</param>
        /// <param name="filters">An optional array of expressions to filter the results based on specific conditions.</param>
        /// <param name="orderByTypes">Specifies the type of ordering to apply (ascending or descending).</param>
        /// <param name="pageNumber">The page number for pagination (default is 0).</param>
        /// <param name="pageSize">The number of items per page for pagination (default is 10).</param>
        /// <param name="isPaginated">A boolean indicating whether to paginate the results (default is false).</param>
        /// <param name="isActive">A boolean indicating whether to include only active records (default is true).</param>
        /// <param name="isDistinct">A boolean indicating whether to return distinct results (default is false).</param>
        /// <param name="take">An optional limit on the number of results to return. If 0, all results are returned.</param>
        /// <param name="tracking">Specify tracking entity or NoTracking</param>
        /// <param name="IgnoreQueryFilter"> Ignore query filter of entity based on Model Creating </param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, 
        /// returning a list of entities or projected results based on the provided selector.</returns>

        public static IQueryable<T> MasterQueryFilterQueryable<T>(this DbContext context,
            Expression<Func<T, object>> orderBySelector = null,
            OrderByTypesEnums orderByTypes = OrderByTypesEnums.Ascending,
            int pageNumber = 0,
            int pageSize = 10,
            bool isPaginated = false,
            bool isDistinct = false,
            int take = 0,
            bool tracking = true,
            bool IgnoreQueryFilter = false,
            params Expression<Func<T, bool>>[] filters
              ) where T : class
        {
            DbSet<T> dbSet = context.Set<T>();
            var query = tracking ? dbSet : dbSet.AsNoTracking();

            try
            {
                if (IgnoreQueryFilter)
                    query = query.IgnoreQueryFilters();

                query = ApplyFilters(query, filters);
                // query = ApplyPropertyFilter(query, nameof(ApplicationUser.IsActive), isActive);
                query = ApplyOrdering(query, orderBySelector, orderByTypes);
                query = ApplyPagination(query, isPaginated, pageNumber, pageSize, take);
                query = ApplyDistinct(query, isDistinct);
                return query;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error executing MasterQueryFilterQueryable", ex);
            }
        }

        #region ApplyFilter-Methods
        private static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, Expression<Func<T, bool>>[] filters) where T : class
        {
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            return query;
        }
        private static IQueryable<T> ApplyPropertyFilter<T>(IQueryable<T> query, string propertyName, bool value)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property != null && property.PropertyType == typeof(bool))
            {
                query = query.Where(e => EF.Property<bool>(e, propertyName) == value);
            }
            return query;
        }
        private static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, Expression<Func<T, object>> orderBySelector, OrderByTypesEnums orderByTypes) where T : class
        {
            if (orderBySelector != null)
            {
                query = orderByTypes == OrderByTypesEnums.Ascending
                    ? query.OrderBy(orderBySelector)
                    : query.OrderByDescending(orderBySelector);
            }
            return query;
        }
        private static IQueryable<T> ApplyPagination<T>(IQueryable<T> query, bool isPaginated, int pageNumber, int pageSize, int take) where T : class
        {
            if (isPaginated)
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else if (take > 0)
            {
                query = query.Take(take);
            }
            return query;
        }
        private static IQueryable<T> ApplyDistinct<T>(IQueryable<T> query, bool isDistinct) where T : class
        {
            if (isDistinct)
            {
                query = query.Distinct();
            }
            return query;
        }
        #endregion



        /// <summary>
        /// Retrieves a filtered and optionally paginated list of entities of type <typeparamref name="T"/> 
        /// from the database context. Allows for applying custom query filters, sorting, distinct selection, 
        /// and projection of specific fields using a selector, with an optional post-query transformation.
        /// </summary>
        /// <typeparam name="T">The type of the entity to retrieve from the database.</typeparam>
        /// <typeparam name="TResult">The type of the result to return, defined by the selector projection. 
        /// If no selector is provided, <typeparamref name="T"/> is used as the result type.</typeparam>
        /// <param name="context">The <see cref="DbContext"/> instance to query the data from.</param>
        /// <param name="orderBySelector">An optional expression to specify the property for ordering.</param>
        /// <param name="filters">An optional array of expressions to filter the results based on specific conditions.</param>
        /// <param name="selector">An optional expression to project specific fields of the entity. 
        /// When provided, the return type will be <typeparamref name="TResult"/>.</param>
        /// <param name="selectorAfterResult">An optional delegate to apply a transformation on the result after the query 
        /// is executed and the data is materialized into a list.</param>
        /// <param name="orderByTypes">Specifies the type of ordering to apply (ascending or descending).</param>
        /// <param name="pageNumber">The page number for pagination (default is 0).</param>
        /// <param name="pageSize">The number of items per page for pagination (default is 10).</param>
        /// <param name="isPaginated">A boolean indicating whether to paginate the results (default is false).</param>
        /// <param name="isActive">A boolean indicating whether to include only active records (default is true).</param>
        /// <param name="isDistinct">A boolean indicating whether to return distinct results (default is false).</param>
        /// <param name="take">An optional limit on the number of results to return. If 0, all results are returned.</param>
        /// <param name="tracking">Specifies whether the entities should be tracked by the context (default is true).</param>
        /// <param name="igonreQueryFilter"> Ignore query filter of entity based on Model Creating </param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation, 
        /// returning a list of entities or projected results based on the provided selector and post-query transformation.</returns>
        public static async Task<List<TResult>> MasterQueryFilterAsync<T, TResult>(
              this DbContext context,
              Expression<Func<T, object>> orderBySelector = null,
              OrderByTypesEnums orderByTypes = OrderByTypesEnums.Ascending,
              Expression<Func<T, TResult>> selector = null,  // Adjusted selector
              Func<TResult, TResult> selectorAfterResult = null,  // Selector after result
              int pageNumber = 0,
              int pageSize = 10,
              bool isPaginated = false,
              bool isDistinct = false,
              int take = 0,
              bool tracking = false,
              bool igonreQueryFilter = false,
              params Expression<Func<T, bool>>[] filters
              ) where T : class
        {
            var query = context.MasterQueryFilterQueryable(
                filters: filters,
                orderBySelector: orderBySelector,
                orderByTypes: orderByTypes,
                pageNumber: pageNumber,
                pageSize: pageSize,
                isPaginated: isPaginated,
                isDistinct: isDistinct,
                take: take,
                IgnoreQueryFilter: igonreQueryFilter,
                tracking: tracking
                );
            if (query is not null)
            {
                #region Apply selector or return full entity
                List<TResult> result = [];

                if (selector is not null)
                {
                    result = await query.Select(selector).ToListAsync();
                }
                else
                {
                    result = await query.Cast<TResult>().ToListAsync(); // Cast the entity type T to TResult
                }

                // Apply the selectorAfterResult projection
                if (selectorAfterResult is not null)
                    result = [.. result.Select(selectorAfterResult)];


                return result;
                #endregion
            }
            return [];
        }


        /// <summary>  
        /// Retrieves a filtered from the database context. Applies various filters such as is_deleted, is_active, and custom query filters.  
        /// </summary>  
        /// <typeparam name="T">The type of the entity to retrieve.</typeparam>  
        /// <param name="context">The DbContext instance from which to query the entities.</param>  
        /// <param name="filters">An optional array of expressions to filter the results.</param>  
        /// <param name="selector">An optional expression to select specific properties of the entities.</param>  
        /// <param name="tracking">Specify tracking entity or NoTracking</param>       
        /// <param name="igonreQueryFilter"> Ignore query filter of entity based on Model Creating </param>
        /// <returns>A Task that represents the asynchronous operation, with a entity of type <typeparamref name="T"/> as the result.</returns> 
        public static async Task<TResult> MasterQueryFilterFirstOrDefaultAsync<T, TResult>(this DbContext context,
              Expression<Func<T, TResult>> selector = null,  // Adjusted selector
              bool tracking = true,
              bool igonreQueryFilter = false,
             params Expression<Func<T, bool>>[] filters
              ) where T : class
        {

            var query = context.MasterQueryFilterQueryable(
                filters: filters,
                tracking: tracking,
                IgnoreQueryFilter: igonreQueryFilter
                );
            if (query is not null)
            {
                #region Apply selector or return full entity
                if (selector is not null)
                {
                    return (await query.Select(selector).FirstOrDefaultAsync())!;
                }
                else
                {
                    return (await query.Cast<TResult>().FirstOrDefaultAsync())!; // Cast the entity type T to TResult
                }
                #endregion
            }
            return default!;
        }
        /// <summary>
        /// Checks the existence of records in a DbSet with optional filtering conditions.
        /// This method applies the following filters:
        /// 1. An optional initial filter specified by an expression.
        /// 2. Filters based on `is_deleted` if such a property exists in the entity.
        /// 3. Applies an `is_active` filter for specific entity types (`DoctorProfile` or `HospitalClinicProfile`).
        /// 4. Checks for relationships to `DoctorProfile` or `HospitalClinicProfile` and applies `is_deleted`
        ///    and `is_active` filters on those related entities if present.
        ///
        /// Returns true if any record matches the filters, false otherwise.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="context">The database context.</param>
        /// <param name="igonreQueryFilter"> Ignore query filter of entity based on Model Creating </param>
        /// <returns>A task that returns true if any record matches the filters, otherwise false.</returns>

        public static Task<bool> MasterQueryFilterExistAsync<T>(this DbContext context,
                bool igonreQueryFilter = false,
                  params Expression<Func<T, bool>>[] filters
            ) where T : class
        {
            var query = context.MasterQueryFilterQueryable(
                            filters: filters,
                            IgnoreQueryFilter: igonreQueryFilter
                            );
            if (query is not null)
                return query.AnyAsync();
            else
                return Task.FromResult(false);
        }
        /// <summary>
        /// Asynchronously counts the number of entities of type <typeparamref name="T"/> 
        /// in the database, applying optional filters for deletion status and activity state.
        /// </summary>
        /// <typeparam name="T">The type of the entities to count.</typeparam>
        /// <param name="context">The DbContext instance used for database operations.</param>
        /// <param name="filters">Optional filters array of expression for the main entity.</param>
        /// <param name="isActive">Indicates whether to include active entities (default is true).</param>
        /// <returns>A task representing the asynchronous operation, with a value of type int 
        /// indicating the count of matching entities.</returns>
        public static Task<int> MasterQueryFilterCountAsync<T>(this DbContext context,
            bool igonreQueryFilter = false,
         params Expression<Func<T, bool>>[] filters
            ) where T : class
        {
            var query = context.MasterQueryFilterQueryable(
                            filters: filters,
                            IgnoreQueryFilter: igonreQueryFilter
                            );
            if (query is not null)
                return query.CountAsync();
            else
                return Task.FromResult(0);

        }

        /// <summary>
        /// Executes a dynamic SQL query against the specified DbContext and returns the results as a list of dynamic objects.
        /// This method supports any entity type without explicitly defining it, providing maximum flexibility for running
        /// arbitrary SQL queries directly on the database.
        /// </summary>
        /// <param name="context">The DbContext used to execute the query.</param>
        /// <param name="query">The SQL query string to execute.</param>

        public static async Task<List<dynamic>> RunDynamicQueryAsync(
            this DbContext context,
            string query)
        {
            var results = new List<dynamic>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                context.Database.OpenConnection();

                using var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    var row = new ExpandoObject() as IDictionary<string, object>;
                    for (var i = 0; i < result.FieldCount; i++)
                    {
                        row.Add(result.GetName(i), result.IsDBNull(i) ? null : result.GetValue(i));
                    }
                    results.Add(row);
                }
            }

            return results;
        }
    }
}