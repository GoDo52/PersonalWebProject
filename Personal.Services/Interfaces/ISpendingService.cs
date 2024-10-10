using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Services.Interfaces
{
    public interface ISpendingService
    {
        decimal? TotalCurrentMonthSpending();
        string? TopSpenderCurrentMonth();
        string? LastFoodSpender();
        decimal? AccountTotalSpendingCurrentMonth(int userId);
        decimal? AccountTotalSpendingPreviousMonth(int userId);
    }
}
