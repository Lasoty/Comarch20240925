

namespace ComarchTestExplorer.Services.Interfaces;

public interface IDateHelper
{
    /// <summary>
    /// Checks if all dates are within the range.
    /// </summary>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="dates"></param>
    /// <returns></returns>
    bool AreDatesWithinRange(DateTime dateFrom, DateTime dateTo, List<DateTime> dates);
}
