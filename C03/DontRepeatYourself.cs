namespace CleanCodeProject.C03
{
    public class DontRepeatYourself
    {
        private string _name;
        private string _address;
        private string _description;

        public DontRepeatYourself()
        {
            this._name = string.Empty;
            this._address = string.Empty;
            this._description = string.Empty;
        }

        private void SetValue<T>(ref T field, T value)
        {
            if (value != null)
            {
                field = value;
            }
        }

        public void SetName(string name)
        {
            SetValue(ref _name, name);
        }

        public void SetAddress(string address)
        {
            SetValue(ref _address, address);
        }

        public void SetDescription(string description)
        {
            SetValue(ref _description, description);
        }

        // C# 風格的屬性
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public string Address
        {
            get => _address;
            set => SetValue(ref _address, value);
        }

        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value);
        }
    }
}