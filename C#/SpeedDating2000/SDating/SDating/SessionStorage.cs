using Microsoft.AspNetCore.Http;
using SDating.Models;

namespace SDating
{
    public interface ISessionStorage
    {
        void SaveInSession(HttpContext context, DatingSession model);
        DatingSession LoadFromSession(HttpContext context);
    }

    public class SessionStorage : ISessionStorage
    {
        private ISessionConverter converter = new SessionConverter();
        public void SaveInSession(HttpContext context, DatingSession model)
        {
            context.Session.Set("session", converter.SessionToBytes(model));
        }

        public DatingSession LoadFromSession(HttpContext context)
        {
            byte[] modelBytes = new byte[] { };
            context.Session.TryGetValue("session", out modelBytes);

            return converter.BytesToSession(modelBytes);
        }
    }
}
