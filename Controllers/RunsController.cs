using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A01.Report.Models;

namespace A01.Report.Controllers
{
    public class RunsController : Controller
    {
        private readonly ResultsContext _context;

        public RunsController(ResultsContext context) => this._context = context;

        [HttpGet]
        [Route("/r")]
        public async Task<IActionResult> Index() =>
            View((await _context.Runs.OrderByDescending(run => run.Id).ToListAsync()).Select(each => new RunViewModel(each)));

        [HttpGet]
        [Route("/r/{id}")]
        public async Task<IActionResult> Details(int id) =>
            View(new RunViewModel(await _context.Runs.Where(r => r.Id == id).Include(r => r.Tasks).FirstAsync()));
    }
}