//using Com.Moonlay.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Com.Moonlay.NetCore.Lib.Service
//{
//    public abstract class BaseEFService<TDbContext, TModel, TKey> : IService<TModel, TKey>
//        where TDbContext : DbContext
//        where TModel : StandardEntity<TKey>, IValidatableObject
//    {
//        TDbContext _dbContext;
//        DbSet<TModel> _dbSet;
//        string Actor;
//        public IServiceProvider ServiceProvider { get; private set; }

//        public BaseEFService(IServiceProvider serviceProvider)
//        {
//            this.ServiceProvider = serviceProvider;
//        }
//        public TDbContext DbContext
//        {
//            get
//            {
//                if (_dbContext == null)
//                    _dbContext = this.ServiceProvider.GetService<TDbContext>();
//                return _dbContext;
//            }
//        }
//        public DbSet<TModel> DbSet
//        {
//            get
//            {
//                if (_dbSet == null)
//                    _dbSet = this.DbContext.Set<TModel>();
//                return _dbSet;
//            }
//        } 

//        public virtual void OnCreating(TModel model) { }
//        public int Create(TModel model)
//        {
//            this.DbSet.Add(model);
//            this.OnCreating(model);
//            this.Validate(model);
//            model.FlagForCreate(this.Actor, string.Empty);

//            return this.DbContext.SaveChanges();
//        }
//        public Task<int> CreateAsync(TModel model)
//        {
//            this.DbSet.Add(model);
//            this.OnCreating(model);
//            this.Validate(model);
//            model.FlagForCreate(this.Actor, string.Empty);

//            return this.DbContext.SaveChangesAsync();
//        }


//        public virtual void OnDeleting(TModel entity) { this.DbSet.Remove(entity); }
//        public int Delete(params object[] keys)
//        {
//            var target = this.Get(keys);
//            if (target == null)
//                throw new Exception("Delete failed: data not found");

//            this.OnDeleting(target);
//            target.FlagForDelete(this.Actor, string.Empty);
//            return this.DbContext.SaveChanges();
//        }
//        public Task<int> DeleteAsync(params object[] keys)
//        {
//            return this.GetAsync(keys).ContinueWith(task =>
//            {
//                TModel target = task.Result;
//                if (target == null)
//                    throw new Exception("Delete failed: data not found");

//                this.OnDeleting(target);
//                target.FlagForDelete(this.Actor, string.Empty);

//                return this.DbContext.SaveChangesAsync();
//            }).Unwrap();
//        }


//        public TModel Find(params object[] keys)
//        {
//            return this.DbSet.Find(keys);
//        }
//        public Task<TModel> FindAsync(params object[] keys)
//        {
//            return this.DbSet.FindAsync(keys);
//        }


//        public IEnumerable<TModel> Get()
//        {
//            return this.DbSet;
//        }
//        public TModel Get(params object[] keys)
//        {
//            TModel data = this.DbSet.Find(keys);
//            if (data.IsDeleted)
//                return null;
//            return data;
//        }
//        public Task<TModel> GetAsync(params object[] keys)
//        {
//            return this.DbSet.FindAsync(keys)
//                .ContinueWith(task =>
//                {
//                    TModel data = task.Result;
//                    if (data.IsDeleted)
//                        return null;
//                    return data;
//                });
//        }


//        public virtual void OnUpdating(TModel model, TModel delta) { }
//        public int Update(TModel model, params object[] keys)
//        {
//            try
//            {
//                TModel target = this.DbSet.Find(keys);
//                if (target == null)
//                    throw new Exception("Update failed: data not found");

//                this.OnUpdating(target, model);
//                this.Validate(target);
//                target.FlagForUpdate(this.Actor, string.Empty);

//                return this.DbContext.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                throw;
//            }
//        }
//        public Task<int> UpdateAsync(TModel model, params object[] keys)
//        {
//            try
//            {
//                return this.DbSet.FindAsync(keys).ContinueWith(task =>
//                    {
//                        TModel target = task.Result;
//                        if (target == null)
//                            throw new Exception("Update failed: data not found");

//                        this.OnUpdating(target, model);
//                        this.Validate(target);
//                        target.FlagForUpdate(this.Actor, string.Empty);

//                        return this.DbContext.SaveChangesAsync();
//                    }).Unwrap();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                throw;
//            }
//        }
        

//        public bool IsExists(params object[] keys)
//        {
//            return this.DbSet.Find(keys) != null;
//        }
//        void Validate(TModel model)
//        {
//            List<ValidationResult> validationResults = new List<ValidationResult>();
//            ValidationContext validationContext = new ValidationContext(model, this.ServiceProvider, null);

//            if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
//                throw new ServiceValidationExeption(validationContext, validationResults);
//        }
//    }
//}