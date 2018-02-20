using Microsoft.EntityFrameworkCore;

namespace A01.Report.Models
{
    public class ResultsContext : DbContext
    {
        public ResultsContext(DbContextOptions<ResultsContext> options) : base(options) { }

        public DbSet<Run> Runs { get; set; }
        
        public DbSet<RunTask> Tasks{get;set;}
    }

}