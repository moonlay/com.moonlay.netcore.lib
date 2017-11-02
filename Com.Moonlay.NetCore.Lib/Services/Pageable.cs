using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Com.Moonlay.NetCore.Lib
{
    public interface IPageable<TModel>
    {
        IPagedList<TModel> Data { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }

        void ApplySource(IQueryable<TModel> source);
    }

    [Serializable]
    public class Pageable<TModel> : IPageable<TModel>
    {

        public Pageable(IQueryable<TModel> source, int pageIndex, int pageSize)
        {
            Data = new PagedList<TModel>(source, pageIndex, pageSize);
        }

        public Pageable(IEnumerable<TModel> source, int pageIndex, int pageSize)
        {
            Data = new PagedList<TModel>(source, pageIndex, pageSize);
        }

        public void ApplySource(IQueryable<TModel> source)
        {
            Data = new PagedList<TModel>(source, 0, PageSize);
        }

        [DataMember]
        public IPagedList<TModel> Data { get; private set; }

        [DataMember]
        public int PageIndex { get { return Data.PageIndex; } }

        [DataMember]
        public int PageSize { get { return Data.PageSize; } }

        [DataMember]
        public int TotalCount { get { return Data.TotalCount; } }

        [DataMember]
        public int TotalPages { get { return Data.TotalPages; } }

        public bool HasPreviousPage { get { return Data.HasPreviousPage; } }

        public bool HasNextPage { get { return Data.HasNextPage; } }
    }
}
