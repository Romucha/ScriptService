using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.Paging
{
    public class RequestParams
    {
        private int maxPaxSize = 50;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPaxSize ? maxPaxSize : value;
        }
    }
}
