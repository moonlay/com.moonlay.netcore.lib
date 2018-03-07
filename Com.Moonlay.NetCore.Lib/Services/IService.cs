using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Moonlay.NetCore.Lib.Service
{
    //public interface IService
    //{

    //    IEnumerable<object> Get();
    //    object Get(params object[] keys);
    //    Task<object> GetAsync(params object[] keys);

    //    object Find(params object[] keys);
    //    Task<object> FindAsync(params object[] keys);

    //    int Create(object model);
    //    Task<int> CreateAsync(object model);

    //    int Update(object model, params object[] keys);
    //    Task<int> UpdateAsync(object model, params object[] keys);

    //    int Delete(params object[] keys);
    //    Task<int> DeleteAsync(params object[] keys);

    //    bool IsExists(params object[] keys);
    //}

    public interface IService<TModel, TKey>
        where TModel : IEntity<TKey>
    {
        IEnumerable<TModel> Get();
        TModel Get(params object[] keys);
        Task<TModel> GetAsync(params object[] keys);

        TModel Find(params object[] keys);
        Task<TModel> FindAsync(params object[] keys);

        int Update(TModel model, params object[] keys);
        Task<int> UpdateAsync(TModel model, params object[] keys);

        int Create(TModel model);
        Task<int> CreateAsync(TModel model);
        int Delete(params object[] keys);
        Task<int> DeleteAsync(params object[] keys);

        bool IsExists(params object[] keys);

    }
}
