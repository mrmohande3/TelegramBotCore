using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TelegramBotCore.Repositories
{
    public interface IRepository<T>
    {
        T AddEntry(T item);
        IEnumerable<T> AddRangEntry(IEnumerable<T> items);
        T UpdateEntry(T item);
        T GetEntry(object id);
        T GetEntry(Expression<Func<T, bool>> expression);
        IEnumerable<T> Where(Func<T, bool> expression);
        T RemoveEntry(T item);
        IEnumerable<T> GetEntries();
    }
}
