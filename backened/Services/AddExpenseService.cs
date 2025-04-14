//using BudgetManagementSystemNew.Model;
//using ExpenseManagementAPI.Repositories;

//namespace BudgetManagementSystemNew.Services
//{
//    public class AddExpenseService
//    {
//        private readonly AddExpenseRepository _repository;

//        public AddExpenseService(AddExpenseRepository repository)
//        {
//            _repository = repository;
//        }

//        public List<AddExpense> GetExpenses(int year, int month)
//        {
//            return _repository.GetExpenses(year, month);
//        }

//        // New method to update only ActualExpense
//        public bool UpdateActualExpense(int id, decimal actualExpense)
//        {
//            return _repository.UpdateActualExpense(id, actualExpense);
//        }

//        // Optional: Method to update both ActualExpense and Remarks (if needed)
//        public bool UpdateExpense(int id, AddExpense updatedExpense)
//        {
//            return _repository.UpdateExpense(id, updatedExpense);
//        }
//    }
//}
//using BudgetManagementSystemNew.Model;
//using ExpenseManagementAPI.Repositories;

//namespace BudgetManagementSystemNew.Services
//{
//    public class AddExpenseService
//    {
//        private readonly AddExpenseRepository _repository;

//        public AddExpenseService(AddExpenseRepository repository)
//        {
//            _repository = repository;
//        }

//        public List<AddExpense> GetExpenses(int year, int month)
//        {
//            return _repository.GetExpenses(year, month);
//        }

//        // New method to update only ActualExpense
//        public bool UpdateActualExpense(int id, decimal actualExpense)
//        {
//            return _repository.UpdateActualExpense(id, actualExpense);
//        }

//        // Optional: Method to update both ActualExpense and Remarks (if needed)
//        public bool UpdateExpense(int id, AddExpense updatedExpense)
//        {
//            return _repository.UpdateExpense(id, updatedExpense);
//        }
//    }
//}using System;
using System.Collections.Generic;
using BudgetManagementSystemNew.Controllers;
using BudgetManagementSystemNew.Model;
using ExpenseManagementAPI.Repositories;

namespace BudgetManagementSystemNew.Services
{
    public class AddExpenseService
    {
        private readonly AddExpenseRepository _repository;

        public AddExpenseService(AddExpenseRepository repository)
        {
            _repository = repository;
        }

        public List<AddExpense> GetExpenses(int year, int month)
        {
            return _repository.GetExpenses(year, month);
        }

        public bool UpdateMultipleActualExpenses(List<UpdateExpenseRequest> updateRequests)
        {
            bool allUpdatesSuccessful = true;

            foreach (var request in updateRequests)
            {
                bool success = _repository.UpdateActualExpense(request.Id, request.ActualExpense);
                if (!success)
                {
                    allUpdatesSuccessful = false;
                    // Optionally, you can log the failure or handle it as needed
                }
            }

            return allUpdatesSuccessful;
        }
    }
}