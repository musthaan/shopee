using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopee.Areas.Admin.Controllers
{
   public class SearchOptions
    {
        public string SearchText { get; set; }
        public string SortColumn { get; set; }
        public string Order { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
