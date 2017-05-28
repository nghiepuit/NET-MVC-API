using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Web.Infrastructure.Core
{
    public class PaginationSet<T>
    {
        public int MaxPage { set; get; }
        public int TotalCount { set; get; }
        public int TotalPages { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { get; set; }
        public int TotalRows { set; get; }
        public IEnumerable<T> Items { set; get; }
        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
            set
            {

            }
        }
    }
}