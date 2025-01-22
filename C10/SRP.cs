namespace CleanCodeProject.C10;

/// <summary>
/// 此檔案展示單一職責原則 (Single Responsibility Principle, SRP)
/// SRP 表示一個類別應該只有一個改變的理由，即只負責一個特定的功能或職責
/// </summary>
public class SRP
{
    /// <summary>
    /// SuperDashboard 類別現在只專注於 UI 相關的職責
    /// 它將焦點管理和版本管理的職責委派給專門的類別處理
    /// 這樣的設計使得每個類別都有明確的單一職責
    /// </summary>
    public abstract class SuperDashboard : JFrame, MetaDataUser
    {
        private readonly FocusManager focusManager;
        private readonly Version version;

        protected SuperDashboard(Version version)
        {
            this.version = version;
            this.focusManager = new FocusManager();
        }

        public Component GetLastFocusedComponent() => focusManager.GetLastFocusedComponent();
        public void SetLastFocused(Component lastFocused) => focusManager.SetLastFocused(lastFocused);
        public int GetMajorVersionNumber() => version.GetMajorVersionNumber();
        public int GetMinorVersionNumber() => version.GetMinorVersionNumber();
        public int GetBuildNumber() => version.GetBuildNumber();
    }

    /// <summary>
    /// FocusManager 類別專門負責管理 UI 組件的焦點狀態
    /// </summary>
    public abstract class FocusManager
    {
        public abstract Component GetLastFocusedComponent();

        public abstract void SetLastFocused(Component lastFocused);
    }

    /// <summary>
    /// Version 類別專門負責版本資訊的管理
    /// </summary>
    public abstract class Version
    {
        public abstract int GetMajorVersionNumber();

        public abstract int GetMinorVersionNumber();

        public abstract int GetBuildNumber();
    }
}