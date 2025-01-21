namespace CleanCodeProject.C09;

public class First
{
    // 1. 先寫測試
    [Fact]
    public void Calculator_Add_ShouldReturnSum()
    {
        var calculator = new Calculator();
        var result = calculator.Add(2, 3);
        Assert.Equal(5, result);
    }

    // 2. 再實現功能
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}