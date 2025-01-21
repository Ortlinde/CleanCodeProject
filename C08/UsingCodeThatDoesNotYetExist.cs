namespace CleanCodeProject.C08;

// 我們希望擁有的界面
public interface ITransmitter
{
    void Transmit(double frequency, Stream dataStream);
}

// 未來真實的發射器 API（目前還不存在）
public interface ITransmitterAPI
{
    // 這個界面將來會由發射器團隊定義
}

// 轉接器模式，用於橋接我們的界面和實際的 API
public class TransmitterAdapter : ITransmitter
{
    private readonly ITransmitterAPI _transmitterAPI;

    public TransmitterAdapter(ITransmitterAPI transmitterAPI)
    {
        _transmitterAPI = transmitterAPI;
    }

    public void Transmit(double frequency, Stream dataStream)
    {
        // 將來實作與實際 API 的互動
    }
}

// 通訊控制器
public class CommunicationsController
{
    private readonly ITransmitter _transmitter;

    public CommunicationsController(ITransmitter transmitter)
    {
        _transmitter = transmitter;
    }

    public void SendData(double frequency, Stream data)
    {
        _transmitter.Transmit(frequency, data);
    }
}

// 用於測試的假發射器
public class FakeTransmitter : ITransmitter
{
    public void Transmit(double frequency, Stream dataStream)
    {
        // 用於測試的模擬實作
    }
}