using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RunningLogger.Models;
using RunningLogger.Repositories;
using System.Data;

namespace RunningLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LogsRepository _logsRepository;
        private readonly UnitsRepository _unitsRepo;
        public List<Log> Logs { get; set; } = new();
        public List<LogChartDataItem> LogChartData { get; set; } = new();

        public IndexModel(LogsRepository logsRepo, UnitsRepository unitsRepo)
        {
            _logsRepository = logsRepo;
            _unitsRepo = unitsRepo;
        }

        public ActionResult OnGet()
        {
            Logs = _logsRepository.GetAll();
            LogChartData = PrepareLogChartDataWithMostCommonUnit(Logs);
            return Page();
        }

        private List<LogChartDataItem> PrepareLogChartDataWithMostCommonUnit(List<Log> logs)
        {
            var allUnits = _unitsRepo.GetAll();

            var standarisedLogsList = new List<Log>();

            var mostCommonUnitGrouping = logs.GroupBy(l => l.UnitName).MaxBy(grouping => grouping.Count());
            var mostCommonUnitName = mostCommonUnitGrouping?.Key ?? RunningUnit.Kilometers;

            return logs.Select(log => new LogChartDataItem
            {
                StartDateTime = log.StartDateTime,
                Quantity = Unit.StandardiseLogQuantityToUnit(log.Quantity, log.UnitName, mostCommonUnitName),
                UnitName = mostCommonUnitName
            }).ToList();
        }
    }

    public class LogChartDataItem
    {
        public string UnitName { get; set; } = "";
        public decimal Quantity { get; set; }
        public DateTime StartDateTime { get; set; }
    }
}
