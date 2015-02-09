using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace Shopee.Support
{
    public class APIResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }

    public class APIErrorResponse: APIResponse
    {
        public APIErrorResponse()
        {
            Code = 401;
        }
    }
    public class APISuccessResponse : APIResponse
    {
        public APISuccessResponse()
        {
            Code = 200;
        }
    }
    public class APISuccessSearchResponse : APISuccessResponse
    {
        public string SortColumn { get; set; }
        public string Order { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public APISuccessSearchResponse()
        {

        }
    }
}