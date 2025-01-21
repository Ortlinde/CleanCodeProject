namespace CleanCodeProject.C03
{
    public abstract class CommandQuerySeparation
    {
        public void ExampleFuncBefore()
        {
            // 不知道這個方法的布林回傳值是什麼意思
            if (Set("username", "unclebob"))
            {
                // Do something.
            }
        }

        public bool Set(string attribute, string value)
        {
            // Do something.
            return true;
        }

        public void ExampleFuncAfter()
        {
            // 明確的表示先確認 attribute 是否存在，再設定 attribute 的值
            if (AttributeExists("username"))
            {
                SetAttribute("username", "unclebob");
            }
        }

        private bool AttributeExists(string attribute)
        {
            return true;
        }

        private void SetAttribute(string attribute, string value)
        {
            // Do something.
        }
    }
}