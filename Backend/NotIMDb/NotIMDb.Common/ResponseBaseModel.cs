using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Common
{
    public class ResponseBaseModel<T>
    {
        public bool Success { get
            {
                return Errors == null ||  Errors.Count == 0;
            }
        }
        public List<string> Errors { get; set; }
        public T Result { get; set; } = default;

    }
}
