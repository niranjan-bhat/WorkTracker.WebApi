using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WorkTracker.Database;

internal class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal WorkTrackerContext context;
    internal DbSet<TEntity> dbSet;

    public BaseRepository(WorkTrackerContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }


    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null) query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);


        if (orderBy != null)
            return orderBy(query);
        return query;
    }

    public virtual TEntity GetByID(object id)
    {
        return dbSet.Find(id);
    }

    public virtual TEntity Insert(TEntity entity)
    {
        dbSet.Add(entity);
        return entity;
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached) dbSet.Attach(entityToDelete);
        dbSet.Remove(entityToDelete);
    }

    public virtual TEntity Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);

        return entityToUpdate;
    }
}