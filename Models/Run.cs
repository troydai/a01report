using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace A01.Report.Models
{
    [Table("run", Schema = "public")]
    public class Run
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("settings")]
        public string Settings { get; set; }

        [Column("details")]
        public string Details { get; set; }

        [Column("creation")]
        public DateTime Creation { get; set; }

        public List<RunTask> Tasks { get; set; }
    }

    public class RunViewModel
    {
        private readonly Run _data;
        private readonly Lazy<JObject> _details;
        private readonly Lazy<JObject> _settings;

        public RunViewModel(Run data)
        {
            _data = data;
            _details = new Lazy<JObject>(() => JObject.Parse(_data.Details));
        }

        public bool IsOfficial
        {
            get
            {
                JToken remark;
                if (_details.Value.TryGetValue("remark", StringComparison.InvariantCulture, out remark))
                {
                    if (string.Compare((string)remark, "Official", true) == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public Run Data => _data;
    }
}