using BL;
using BL.DAL;
using Unity;

namespace ChatClient
{
    /// <summary>
    /// Unity
    /// </summary>
    public static class UContainer
    {
        public static IUnityContainer services = new UnityContainer();

        public static void Registration()
        {
            services.RegisterType<IdbAuthorization, dbAuthorization>();
            services.RegisterType<IdbChat, dbChat>();

            services.RegisterType<IAuthorization, Authorization>();
            services.RegisterType<IChatOperations, ChatOperations>();
        }
    }
}
