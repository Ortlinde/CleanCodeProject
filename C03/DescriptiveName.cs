namespace CleanCodeProject.C03
{
    public class DescriptiveName
    {
        private Cryptographer cryptographer;

        public bool CheckPassword(string userName, string password)
        {
            User user = UserGateway.FindByName(userName);
            if (user != User.NULL)
            {
                string codedPhrase = user.GetPhraseEncodedByPassword();
                string phrase = cryptographer.Decrypt(codedPhrase, password);
                if ("Valid Password" == phrase)
                {
                    Session.Initialize(); // 方法宣稱檢查密碼卻初始化Session
                    return true;
                }
            }
            return false;
        }

        public bool AssertExpectedEqualsActual(string expected, string actual)
        {
            return expected == actual;
        }
    }
}