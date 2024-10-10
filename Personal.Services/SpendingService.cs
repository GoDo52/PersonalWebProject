using Personal.DataAccess.Repository;
using Personal.DataAccess.Repository.IRepository;
using Personal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Services
{
    public class SpendingService : ISpendingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _currentYear = DateTime.Now.Year;
        private readonly int _currentMonth = DateTime.Now.Month;

        public SpendingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string? LastFoodSpender()
        {
            // Find the category ID for "Food" and check if null
            var foodCategory = _unitOfWork.Category.Get(c => c.Name == "Food");
            if (foodCategory == null)
            {
                return null;
            }

            var lastFoodSpending = _unitOfWork.Spending
                .GetAll(includeProperties: "User,Category")
                .Where(s => s.CategoryId == foodCategory.Id)
                .OrderByDescending(s => s.DateTime)
                .FirstOrDefault();

            return lastFoodSpending?.User.UserName;
        }

        public string? TopSpenderCurrentMonth()
        {
            var topSpenderCurrentMonth = _unitOfWork.Spending
                .GetAll(includeProperties: "User")
                .Where(s => s.DateTime.Year == _currentYear && s.DateTime.Month == _currentMonth)
                .OrderByDescending(s => s.Amount)
                .FirstOrDefault();

            return topSpenderCurrentMonth?.User.UserName;
        }

        public decimal? TotalCurrentMonthSpending()
        {
            decimal? totalCurrentMonthSpending = _unitOfWork.Spending
                .GetAll()
                .Where(s => s.DateTime.Year == _currentYear && s.DateTime.Month == _currentMonth)
                .Sum(s => s.Amount);

            return totalCurrentMonthSpending;
        }

        public decimal? AccountTotalSpendingCurrentMonth(int userId)
        {
            decimal? accountTotalSpendingCurrentMonth = _unitOfWork.Spending
                .GetAll(includeProperties: "User")
                .Where(s => s.User.Id == userId && s.DateTime.Month == _currentMonth && s.DateTime.Year == _currentYear)
                .Sum (s => (decimal?)s.Amount) ?? 0;

            return accountTotalSpendingCurrentMonth;
        }

        public decimal? AccountTotalSpendingPreviousMonth(int userId)
        {
            var previousMonth = _currentMonth - 1;
            var previousYear = _currentYear;
            if (previousMonth == 0)
            {
                previousMonth = 12;
                previousYear -= 1;
            }

            decimal? accountTotalSpendingPreviousMonth = _unitOfWork.Spending
                .GetAll(includeProperties: "User")
                .Where(s => s.User.Id == userId && s.DateTime.Month == previousMonth && s.DateTime.Year == previousYear)
                .Sum(s => (decimal?)s.Amount) ?? 0;

            return accountTotalSpendingPreviousMonth;
        }
    }
}
