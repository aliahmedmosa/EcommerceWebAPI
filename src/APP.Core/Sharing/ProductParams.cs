using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Core.Sharing
{
    //---------------------------This class is complex type for product get all parameters
    public class ProductParams
    {
        public int MaxPageSize { get; set; } = 15;
        private int pageSize = 3;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageNumber { get; set; }

        public string? Sort { get; set; }

        public int? CategoryId { get; set; }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
