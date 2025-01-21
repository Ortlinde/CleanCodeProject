namespace CleanCodeProject.C09;
using System.Text;

public class ADualStandard
{
    // 模擬硬體狀態
    private bool heater;
    private bool blower;
    private bool cooler;
    private bool hiTempAlarm;
    private bool loTempAlarm;

    // 在測試環境中使用的非效率但易讀的狀態方法
    public string GetState()
    {
        string state = "";
        state += heater ? "H" : "h";
        state += blower ? "B" : "b";
        state += cooler ? "C" : "c";
        state += hiTempAlarm ? "H" : "h";
        state += loTempAlarm ? "L" : "l";
        return state;
    }

    // 在生產環境中使用的高效能版本
    public string GetStateOptimized()
    {
        return new StringBuilder()
            .Append(heater ? "H" : "h")
            .Append(blower ? "B" : "b")
            .Append(cooler ? "C" : "c")
            .Append(hiTempAlarm ? "H" : "h")
            .Append(loTempAlarm ? "L" : "l")
            .ToString();
    }
}