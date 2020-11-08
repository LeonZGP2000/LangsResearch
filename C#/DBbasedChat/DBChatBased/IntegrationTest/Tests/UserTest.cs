namespace IntegrationTest.Tests
{
    public class UserTest : BaseTest
    {
        public void Start()
        {
            var user = GenerateRandomUser();

            if(user == null || user.id == default) 
                ThrowException("Error creating user");

            var authResult = auth.MakeLogin(login1, login1password);
            if (authResult == null || !authResult.Succeed)
                ThrowException("Auth. failed for user1");

            //negative test
            try
            {
                authResult = auth.MakeLogin("nouser", "nopassword12354465");
                if (authResult.Succeed)
                    ThrowException("Auth. should be failed for user2");
            }
            catch
            {
                // Exceptin was expected
            }
        }
    } 
}
