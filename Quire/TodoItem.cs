using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Quire
{
    class TodoItem
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "Header")]
        public string Header { get; set; }

        [JsonProperty(PropertyName = "Time")]
        public DateTime Time { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "Checked")]
        public bool Checked { get; set; }

        [JsonProperty(PropertyName = "Visible")]
        public bool Visible { get; set; }

    }
}
