using BudgetManagementSystemNew.Model;
using BudgetManagementSystemNew.Services;
using System.Collections.Generic;
using BudgetManagementSystemNew.DataAccess;
namespace BudgetManagementSystemNew.Services;

public class YearService
{
    private readonly YearRepository _repository;

    public YearService(YearRepository repository)
    {
        _repository = repository;
    }

    public List<Year> GetAllYears()
    {
        return _repository.GetAllYears();
    }
}