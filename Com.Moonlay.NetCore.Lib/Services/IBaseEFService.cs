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
//    public interface IBaseEFService : IService
//    {
//        IServiceProvider ServiceProvider { get; }
//        DbContext DbContext { get; }
//    }

//    public interface IBaseEFService<TDbContext, TModel, TKey> : IBaseEFService, IService<TModel, TKey>
//        where TDbContext : DbContext
//        where TModel : class, IEntity<TKey>
//        where TKey : IConvertible
//    {
//        new TDbContext DbContext { get; }
//    }

//}
