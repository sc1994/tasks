﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aspire.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata;

namespace Aspire.Domain.Repositories
{
    public interface IRepositoryEfCore<TEntity> : IRepositoryEfCore<TEntity, long>
         where TEntity : BaseEntity
    {

    }

    public interface IRepositoryEfCore<TEntity, TId> : IRepository<TEntity, TId>
         where TEntity : BaseEntity<TId>
    {
        IQueryable<TEntity> Query { get; }

        IEntityType EntityType { get; }

        Task<bool> UpdateAsync(Action<TEntity> updater, TId id);

        Task<int> UpdateRangeAsync(Action<TEntity> updater, params TId[] ids);

        Task<int> UpdateRangeAsync(Action<TEntity> updater, IEnumerable<TId> ids);
    }
}
