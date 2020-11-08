using BL.DAL;
using BL.Models;
using System.Threading;

namespace BL
{
    public class Administration
    {
        private IdbAuthorization dbAuth { get; set; }
        private IdbChat dbCh { get; set; }

        public Administration(IdbAuthorization dbAuth, IdbChat dbCh)
        {
            this.dbAuth = dbAuth;
            this.dbCh = dbCh;
        }

        public void DeleteUser(int id)
        {
            dbAuth.DeleteUser(id);
        }

        public void DeleteChat(int id, string table)
        {
            dbCh.DeleteChat(id, table);
        }

        public bool CheckChatTableWasCreated(Chat chat)
        {
            return dbCh.CheckChatTableWasCreated(chat);
        }

        public void DeleteChatTable(Chat chat)
        {
            dbCh.DeleteChatTable(chat);
        }
    }
}
