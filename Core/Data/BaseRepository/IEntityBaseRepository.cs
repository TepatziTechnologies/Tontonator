using System;
namespace Tontonator.Core.Data.BaseRepository
{
    public interface IBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task Read(T entity);
    }
}