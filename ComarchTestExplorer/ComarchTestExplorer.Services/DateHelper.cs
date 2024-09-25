using ComarchTestExplorer.Services.Interfaces;

namespace ComarchTestExplorer.Services;

public class DateHelper : IDateHelper
{
    public bool AreDatesWithinRange(DateTime dateFrom, DateTime dateTo, List<DateTime> dates)
    {
        return dates.All(date => date >= dateFrom && date <= dateTo);
    }
}
