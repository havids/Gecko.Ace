using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gecko.Application.Areas.SystemSecurity.Models
{
    [Serializable]
    public class NodeType
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string ntype { get; set; }
        [JsonProperty(PropertyName = "checked")]
        public bool? Checked { get; set; }
        public string suburl { get; set; }
        public string tag { get; set; }
        public IList children { get; set; }
    }
}