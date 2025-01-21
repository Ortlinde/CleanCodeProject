namespace CleanCodeProject.C07;

using System.Collections.Generic;

public class DefineTheNormalFlow
{
    public void UseExceptionExample()
    {
        try
        {
            MealExpenses expenses = ExpenseReportDAO.GetMeals(employee.ID);
            mTotal += expenses.GetTotal();
        }
        catch (MealExpensesNotFoundException e)
        {
            mTotal += GetMealPerDiem();
        }
    }

    public void UseSpecialCaseExample()
    {
        MealExpenses expenses = ExpenseReportDAO.GetMeals(employee.ID);
        mTotal += expenses.GetTotal();
    }

    public class PerDiemMealExpenses : MealExpenses
    {
        private const double PerDiemDefault = 50.0;

        public override double GetTotal()
        {
            return PerDiemDefault;
        }
    }
}

public class ExpenseReportDAO
{
    private static readonly Dictionary<int, MealExpenses> _mealExpensesDatabase = new();

    public static MealExpenses GetMeals(int employeeId)
    {
        // 嘗試從資料庫中獲取餐費記錄
        if (_mealExpensesDatabase.TryGetValue(employeeId, out MealExpenses? expenses))
        {
            return expenses;
        }

        // 若找不到記錄，返回特例物件而不是拋出例外
        return new PerDiemMealExpenses();
    }

    // 用於測試和示範的方法
    public static void AddMealExpenses(int employeeId, MealExpenses expenses)
    {
        _mealExpensesDatabase[employeeId] = expenses;
    }
}

// 基礎餐費類別
public abstract class MealExpenses
{
    public abstract double GetTotal();
}

// 一般餐費實作
public class RegularMealExpenses : MealExpenses
{
    private readonly double _total;

    public RegularMealExpenses(double total)
    {
        _total = total;
    }

    public override double GetTotal()
    {
        return _total;
    }
}