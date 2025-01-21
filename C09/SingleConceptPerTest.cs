namespace CleanCodeProject.C09;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public class SingleConceptPerTest
{
    /// <summary>
    /// Miscellaneous tests for the AddMonths method.
    /// </summary>
    [TestMethod]
    public void AddOneMonth_To31DayMonth_ShouldReturnLastDayOfNextMonth()
    {
        SerialDate d1 = SerialDate.CreateInstance(31, 5, 2004);
        SerialDate result = SerialDate.AddMonths(1, d1);

        Assert.AreEqual(30, result.GetDayOfMonth());
        Assert.AreEqual(6, result.GetMonth());
        Assert.AreEqual(2004, result.GetYYYY());
    }

    [TestMethod]
    public void AddTwoMonths_To31DayMonth_ShouldReturnSameDayOfSecondMonth()
    {
        SerialDate d1 = SerialDate.CreateInstance(31, 5, 2004);
        SerialDate result = SerialDate.AddMonths(2, d1);

        Assert.AreEqual(31, result.GetDayOfMonth());
        Assert.AreEqual(7, result.GetMonth());
        Assert.AreEqual(2004, result.GetYYYY());
    }

    [TestMethod]
    public void AddTwoMonths_OneAtATime_ShouldMatchAddingTwoMonthsAtOnce()
    {
        SerialDate d1 = SerialDate.CreateInstance(31, 5, 2004);
        SerialDate result = SerialDate.AddMonths(1, SerialDate.AddMonths(1, d1));

        Assert.AreEqual(30, result.GetDayOfMonth());
        Assert.AreEqual(7, result.GetMonth());
        Assert.AreEqual(2004, result.GetYYYY());
    }
}