// <copyright file="IGenericRepository.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.DL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(string includeProperties = "");

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        
        void Add(TEntity entity);
        
        void Delete(TEntity entity);
        
        void Update(TEntity entity);
        
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        int ExecuteSqlCommand(string sql, params object[] parmaters);
    }
}
