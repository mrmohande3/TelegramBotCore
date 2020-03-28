using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelegramBotCore.Models.Context;

namespace TelegramBotCore.Repositories
{
    public class RepositoryBase<T> :IRepositoryAsync<T>, IRepository<T> where T : Entity
    {
        private readonly BotContext _context;

        public RepositoryBase(BotContext context)
        {
            _context = context;
        }
        public T AddEntry(T item)
        {
            (item).CreationTime = DateTime.Now;
            _context.Add(item);
            _context.SaveChanges();
            _context.Entry(item).State = EntityState.Detached;
            return item;
        }
        public IEnumerable<T> AddRangEntry(IEnumerable<T> items)
        {
            foreach (var entity in items)
            {
                entity.CreationTime = DateTime.Now;
            }
            _context.AddRange(items);
            _context.SaveChanges();
            return items;
        }
        public T UpdateEntry(T item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }
        public T GetEntry(object id)
        {
            var ent = _context.Find<T>(id);
            if (!ent.IsRemoved)
            {
                _context.Entry(ent).State = EntityState.Detached;
                return ent;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<T> Where(Func<T, bool> expression)
        {
            return _context.Set<T>().AsNoTracking().Where(expression).ToList();
        }
        public T GetEntry(Expression<Func<T, bool>> expression)
        {
            var user = _context.Set<T>().AsNoTracking().FirstOrDefault(expression);
            if(user!=null)
                _context.Entry(user).State = EntityState.Detached;
            return user;
        }
        public T RemoveEntry(T item)
        {
            var ent = _context.Entry(item);
            ent.State = EntityState.Detached;
            ent.State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }
        public IEnumerable<T> GetEntries()
        {
            return _context.Set<T>().Where(e => e.IsRemoved == false).AsNoTracking().ToList();
        }

        public async Task<T> AddEntryAsync(T item)
        {
            (item).CreationTime = DateTime.Now;
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> AddRangEntryAsync(IEnumerable<T> items)
        {
            foreach (var entity in items)
            {
                entity.CreationTime = DateTime.Now;
            }
            await _context.AddRangeAsync(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<T> UpdateEntryAsync(T item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> GetEntryAsync(object id)
        {
            var ent = await _context.FindAsync<T>(id);
            if (!ent.IsRemoved)
            {
                _context.Entry(ent).State = EntityState.Detached;
                return ent;
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetEntryAsync(Expression<Func<T, bool>> expression)
        {
            var user = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
            if (user != null)
                _context.Entry(user).State = EntityState.Detached;
            return user;
        }

        public async Task<IEnumerable<T>> WhereAsync(Func<T, bool> expression)
        {
            IEnumerable<T> datas = new List<T>();
            await Task.Run(() =>
            {
                datas = _context.Set<T>().AsNoTracking().Where(expression);
            });
            return datas.ToList();
        }

        public async Task<T> RemoveEntryAsync(T item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> GetEntriesAsync()
        {
            return await _context.Set<T>().Where(e => e.IsRemoved == false).AsNoTracking().ToListAsync();
        }
    }
}
