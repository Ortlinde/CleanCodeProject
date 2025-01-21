namespace CleanCodeProject.C07;

using System;
using System.Collections.Generic;

public class DontReturnNull
{
    public IReadOnlyList<Employee> GetEmployees()
    {
        return Array.Empty<Employee>();
    }

    public void RegisterItem(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        var registry = peristentStore.GetItemRegistry()
            ?? throw new InvalidOperationException("Item registry not found");

        var existing = registry.GetItem(item.GetID());
        if (existing?.GetBillingPeriod()?.HasRetailOwner() == true)
        {
            existing.Register(item);
        }
    }

    public class MetricsCalculator
    {
        public double XProjection(Point p1, Point p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);

            return (p2.X - p1.X) * 1.5;
        }
    }
}

public class NullEmployee : Employee
{
    public override decimal GetPay() => 0;
    // 其他方法實現預設行為
}