using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Movies4All.App.Data;
using Movies4All.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Movies4All.Data.Repositories
{
    public class BaseRepository<TEntity>:IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string[] includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
                foreach(var entity in includes)
                    query=query.Include(entity);
                
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(criteria);
            if (includes != null)
                foreach (var entity in includes)
                    query = query.Include(entity);

            return await query.ToListAsync();
        }
        public bool isValidEntity(Expression<Func<TEntity,bool>> criteria)
        {
            return _context.Set<TEntity>().Any(criteria);
        }
        public async Task<TEntity> GetByIdAsync(int id) 
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public TEntity GetById(Expression<Func<TEntity,bool>> criteria, string[] includes = null) 
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
                foreach (var entity in includes)
                    query = query.Include(entity);
            return query.SingleOrDefault(criteria);
        }
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }
    }
}