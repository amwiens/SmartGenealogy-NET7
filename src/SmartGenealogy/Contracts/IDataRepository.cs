using System.Collections.Generic;

using SmartGenealogy.Persistence.Models;

namespace SmartGenealogy.Contracts;

/// <summary>
/// Data repository interface.
/// </summary>
public interface IDataRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Get all records.
    /// </summary>
    /// <returns>Records of the given type.</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Get an entity.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>Entity</returns>
    TEntity Get(long id);

    /// <summary>
    /// Add a new record.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Update an existing entity.
    /// </summary>
    /// <param name="dbEntity">The database entity.</param>
    /// <param name="entity">The new entity.</param>
    /// <returns>Entity</returns>
    TEntity Update(TEntity dbEntity, TEntity entity);

    /// <summary>
    /// Deletes and existing record.
    /// </summary>
    /// <param name="entity">Entity</param>
    void Delete(TEntity entity);
}