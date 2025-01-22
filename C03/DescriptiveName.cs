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

        public bool AssertExpectedEqualsActual(decimal expected, decimal actual)
        {
            // 當進行除法運算時，參數順序會影響結果
            // 例如：10/2 = 5，但 2/10 = 0.2
            return (expected / actual) == 1;
        }
    }
}