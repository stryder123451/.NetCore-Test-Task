using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.Models
{
    public class JsonRequest
    {
        public string Author { get; set; } 
        public string ProjectName { get; set; }
        public int StarGazers { get; set; }
        public int Watchers { get; set; }
        public string Url { get; set; }
    }
}
