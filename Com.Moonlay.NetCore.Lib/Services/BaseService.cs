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

    public abstract class BaseService : IService
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public virtual DbContext DbContext { get; set; }

        public BaseService(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public abstract int Create(object model);
        public abstract Task<int> CreateAsync(object model);
        public abstract int Delete(object id);
        public abstract Task<int> DeleteAsync(object id);
        public abstract IEnumerable<object> Get();
        public abstract object Get(object id);
        public abstract Task<object> GetAsync(object id);
        public abstract bool IsExists(object id);
        public abstract int Update(object id, object model);
        public abstract Task<int> UpdateAsync(object id, object model);
    }

    public abstract class BaseService<TDbContext, TModel, TKey> : BaseService, IService<TModel, TKey>
        where TDbContext : DbContext
        where TModel : class, IEntity, IValidatableObject
        where TKey : IConvertible
    {
        TDbContext _dbContext;
        DbSet<TModel> _dbSet;

        public BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override DbContext DbContext
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
        void Validate(TModel model)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(model, this.ServiceProvider, null);

            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                throw new ServiceValidationExeption(validationContext, validationResults);
        }

        public virtual void OnCreating(TModel model)
        {
        }
        public int Create(TModel model)
        {
            this.DbSet.Add(model);
            this.OnCreating(model);
            this.Validate(model);
            return this.DbContext.SaveChanges();
        }
        public Task<int> CreateAsync(TModel model)
        {
            this.DbSet.Add(model);
            this.OnCreating(model);
            this.Validate(model);
            return this.DbContext.SaveChangesAsync();
        }

        public virtual void OnDeleting(TModel entity)
        {
            this.DbSet.Remove(entity);
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

        public TModel Get(TKey id)
        {
            return this.DbSet.SingleOrDefault(e => e.Id.Equals(id));
        }

        public Task<TModel> GetAsync(TKey id)
        {
            return this.DbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public bool IsExists(TKey id)
        {
            return this.DbSet.Any(m => m.Id.Equals(id));
        }

        public virtual void OnUpdating(TKey id, TModel model)
        {
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

        IEnumerable<TModel> IService<TModel, TKey>.Get()
        {
            return this.DbSet;
        }


        public override int Create(object model)
        {
            return this.Create(model as TModel);
        }
        public override Task<int> CreateAsync(object model)
        {
            return this.CreateAsync(model as TModel);
        }
        public override int Delete(object id)
        {
            return this.Delete((TKey)id);
        }
        public override Task<int> DeleteAsync(object id)
        {
            return this.DeleteAsync((TKey)id);
        }
        public override IEnumerable<object> Get()
        {
            return this.Get();
        }
        public override object Get(object id)
        {
            return this.Get((TKey)id);
        }
        public override Task<object> GetAsync(object id)
        {
            return System.Threading.Tasks.Task.FromResult((object)Get((TKey)id));
        }
        public override bool IsExists(object id)
        {
            return this.IsExists((TKey)id);
        }
        public override int Update(object id, object model)
        {
            return this.Update((TKey)id, model as TModel);
        }
        public override Task<int> UpdateAsync(object id, object model)
        {
            return this.UpdateAsync((TKey)id, model as TModel);
        }
    }

}
