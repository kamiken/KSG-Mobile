using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Mobile.Framework.Extensions;


namespace Mobile.Framework
{
    public class PagedResult<T>
    {
        [NonSerialized]
        private IQueryable<T> _source;
        private List<T> _sourcePaged;

        public PagedResult() { }
        private PagingInput PagingInput { get; set; }

        public PagedResult(IQueryable<T> source)
        {
            _source = source;
        }

        public List<T> Results
        {
            get { return _sourcePaged ?? (_sourcePaged = _source.OrderBy(PagingInput.SortPropertyName, PagingInput.SortDirection)
                .Skip(CurrentPageIndex * PageSize).Take(PageSize).ToList()); }
        }
        public int CurrentPageIndex
        {
            get { return PagingInput.PageIndex; }
        }
        public long PageCount
        {
            get { return (RowCount - 1) / PageSize + 1; }
        }
        public int PageSize
        {
            get { return PagingInput.PageSize; }
        }
        public long RowCount { get; private set; }

        public void SetPaging(PagingInput paging)
        {
            PagingInput = paging;
            _sourcePaged = _source.OrderBy(PagingInput.SortPropertyName, PagingInput.SortDirection)
                .Skip(CurrentPageIndex * PageSize).Take(PageSize).ToList();
            RowCount = _source.LongCount();
        }
    }

    public class PagingInput
    {
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
        public SortDirection SortDirection { get; set; }
        public string SortPropertyName { get; set; }
    }

    
}
