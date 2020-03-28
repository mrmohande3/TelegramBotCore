using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TelegramBotCore.Repositories
{
    public interface IRepositoryAsync<T>
    {
        Task<T> AddEntryAsync(T item);
        Task<IEnumerable<T>> AddRangEntryAsync(IEnumerable<T> items);
        Task<T> UpdateEntryAsync(T item);
        Task<T> GetEntryAsync(object id);
        Task<T> GetEntryAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> WhereAsync(Func<T, bool> expression);
        Task<T> RemoveEntryAsync(T item);
        Task<IEnumerable<T>> GetEntriesAsync();
    }
}
