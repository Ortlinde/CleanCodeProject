using System.Diagnostics;

namespace CleanCodeProject.C08
{
    public class ThirdPartyLibraryTests
    {
        public void WhenUsingSpecificFeature_ShouldBehaveAsExpected()
        {
            // 安排
            var thirdPartyObject = new ThirdPartyLibrary.SomeClass();

            // 操作
            var result = thirdPartyObject.SomeMethod("test input");

            // 斷言
            Debug.Assert(result == "expected output");
        }
    }
}

namespace ThirdPartyLibrary
{
    public class SomeClass
    {
        public string SomeMethod(string input)
        {
            return "expected output";
        }
    }
}
