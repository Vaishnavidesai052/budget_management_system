using System.Collections.Generic;
using BudgetManagementSystemNew.Model;
using BudgetManagementSystemNew.Services;
using BudgetManagementSystemNew.DataAccess;

namespace BudgetManagementSystemNew.Services
{
    public class ViewExpenseService
    {
        private readonly ViewExpenseRepository _repository;

        public ViewExpenseService(ViewExpenseRepository repository)
        {
            _repository = repository;
        }

        public List<ViewExpenseModel> GetViewExpenses(int yearId)
        {
            return _repository.GetViewExpenses(yearId);
        }
    }
}