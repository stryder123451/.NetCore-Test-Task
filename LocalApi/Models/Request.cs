using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string RequestUrl { get; set; }
        public string RequestText { get; set; }
    }
}
