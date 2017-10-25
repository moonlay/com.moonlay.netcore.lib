using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Moonlay.NetCore.Lib.Service
{
    public abstract class BaseEFService<TDbContext, TModel, TKey> : IBaseEFService<TDbContext, TModel, TKey>
        where TDbContext : DbContext
        where TModel : class, IEntity<TKey>, IValidatableObject
        where TKey : IConvertible
    {
        TDbContext _dbContext;
        DbSet<TModel> _dbSet;

        public IServiceProvider ServiceProvider { get; private set; }

        public BaseEFService(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public TDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = this.ServiceProvider.GetService<TDbContext>();
                return _dbContext;
            }
        }
        public DbSet<TModel> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = this.DbContext.Set<TModel>();
                return _dbSet;
            }
        }

        DbContext IBaseEFService.DbContext { get { return this.DbContext; } }

        void Validate(TModel model)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(model, this.ServiceProvider, null);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                throw new ServiceValidationExeption(validationContext, validationResults);
        }

        public virtual void OnCreating(TModel model) { }
        public virtual void OnUpdating(TKey id, TModel model) { }
        public virtual void OnDeleting(TModel entity) { this.DbSet.Remove(entity); }

        public TModel Find(params object[] keys)
        {
            return this.DbSet.Find(keys);
        }

        public Task<TModel> FindAsync(params object[] keys)
        {
            return this.DbSet.FindAsync(keys);
        }

        public IEnumerable<TModel> Get()
        {
            return this.DbSet;
        }

        public Task<TModel> GetAsync(TKey id)
        {
            return this.DbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public TModel Get(TKey id)
        {
            return this.DbContext.Set<TModel>().SingleOrDefault(e => e.Id.Equals(id));
        }

        public Task<int> UpdateAsync(TKey id, TModel model)
        {
            try
            {
                this.DbContext.Entry(model).State = EntityState.Modified;
                this.OnUpdating(id, model);
                this.Validate(model);
                return this.DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExists(id))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }
        public int Update(TKey id, TModel model)
        {
            try
            {
                this.DbContext.Entry(model).State = EntityState.Modified;
                this.OnUpdating(id, model);
                this.Validate(model);
                return this.DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExists(id))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }

        public Task<int> CreateAsync(TModel model)
        {
            this.DbSet.Add(model);
            this.OnCreating(model);
            this.Validate(model);
            return this.DbContext.SaveChangesAsync();
        }

        public int Create(TModel model)
        {
            this.DbSet.Add(model);
            this.OnCreating(model);
            this.Validate(model);
            return this.DbContext.SaveChanges();
        }

        public Task<int> DeleteAsync(TKey id)
        {
            var entity = this.Get(id);
            if (entity == null)
            {
                throw new Exception();
            }

            this.OnDeleting(entity);
            return this.DbContext.SaveChangesAsync();
        }

        public int Delete(TKey id)
        {
            var entity = this.Get(id);
            if (entity == null)
            {
                throw new Exception();
            }
            this.OnDeleting(entity);
            return this.DbContext.SaveChanges();
        }

        public bool IsExists(TKey id)
        {
            return this.DbSet.Any(m => m.Id.Equals(id));
        }

        IEnumerable<object> IService.Get()
        {
            return ((IService<TModel, TKey>)this).Get();
        }

        Task<object> IService.GetAsync(object id)
        {
            return System.Threading.Tasks.Task.FromResult((object)Get((TKey)id));
        }

        object IService.Get(object id)
        {
            return ((IService<TModel, TKey>)this).Get((TKey)id);
        }

        Task<object> IService.FindAsync(params object[] keys)
        {
            return System.Threading.Tasks.Task.FromResult((object)((IService<TModel, TKey>)this).FindAsync(keys));
        }

        object IService.Find(params object[] keys)
        {
            return ((IService<TModel, TKey>)this).Find(keys);
        }

        Task<int> IService.CreateAsync(object model)
        {
            return ((IService<TModel, TKey>)this).CreateAsync(model as TModel);
        }

        int IService.Create(object model)
        {
            return ((IService<TModel, TKey>)this).Create(model as TModel);
        }

        Task<int> IService.UpdateAsync(object id, object model)
        {
            return ((IService<TModel, TKey>)this).UpdateAsync((TKey)id, model as TModel);
        }

        int IService.Update(object id, object model)
        {
            return ((IService<TModel, TKey>)this).Update((TKey)id, model as TModel);
        }

        Task<int> IService.DeleteAsync(object id)
        {
            return ((IService<TModel, TKey>)this).DeleteAsync((TKey)id);
        }

        int IService.Delete(object id)
        {
            return ((IService<TModel, TKey>)this).Delete((TKey)id);
        }

        bool IService.IsExists(object id)
        {
            return ((IService<TModel, TKey>)this).IsExists((TKey)id);
        }
    }

}
