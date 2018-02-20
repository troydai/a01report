using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace A01.Report.Models
{
    [Table("task", Schema = "public")]
    public class RunTask
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("annotation")]
        public string Annotation { get; set; }

        [Column("settings")]
        public string Settings { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("result_details")]
        public string ResultDetails { get; set; }

        [Column("result")]
        public string Result { get; set; }

        [ForeignKey("run_id")]
        public Run Run { get; set; }
    }

    public class RunTaskViewModel
    {
        private static readonly string logTemplate = "https://azureclia01log.file.core.windows.net/k8slog/{0}?sv=2017-04-17&ss=f&srt=o&sp=r&se=2019-01-01T00:00:00Z&st=2018-01-04T10:21:21Z&spr=https&sig=I9Ajm2i8Knl3hm1rfN%2Ft2E934trzj%2FNnozLYhQ%2Bb7TE%3D";

        private readonly RunTask _task;
        private readonly Lazy<JObject> _details;
        private readonly Lazy<JObject> _settings;

        public RunTaskViewModel(RunTask task)
        {
            _task = task;
            _details = new Lazy<JObject>(() => JObject.Parse(_task.ResultDetails));
            _settings = new Lazy<JObject>(() => JObject.Parse(_task.Settings));
        }

        public RunTask Data => _task;

        public async Task<string> GetLog()
        {
            var filePath = string.Format(logTemplate, $"{Data.Run.Id}/task_{Data.Id}.log");
            var client = new HttpClient();
            var resp = await client.GetAsync(filePath);
            return await resp.Content.ReadAsStringAsync();
        }
    }
}