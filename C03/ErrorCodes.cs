namespace CleanCodeProject.C03
{
    public enum ErrorCode
    {
        E_OK = 0,           // 操作成功
        E_ERROR = 1,        // 一般錯誤
        E_NOT_FOUND = 2,    // 找不到資源
        E_UNAUTHORIZED = 3,  // 未經授權
        E_INVALID_INPUT = 4, // 無效的輸入
        E_DATABASE = 5,      // 資料庫錯誤
        E_NETWORK = 6,       // 網路錯誤
        E_TIMEOUT = 7,       // 操作逾時
        E_SYSTEM = 8,        // 系統錯誤
        E_UNKNOWN = 99       // 未知錯誤
    }
}