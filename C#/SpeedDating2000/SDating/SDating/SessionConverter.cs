using SDating.Models;
using System.Text;

namespace SDating
{
    public interface ISessionConverter
    {
        byte[] SessionToBytes(DatingSession ds);
        DatingSession BytesToSession(byte[] array);

    }

    public class SessionConverter : ISessionConverter
    {
        public byte[] SessionToBytes(DatingSession ds)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ds);
            return Encoding.UTF8.GetBytes(json);
        }

        public DatingSession BytesToSession(byte[] array)
        {
            var model = Encoding.UTF8.GetString(array);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DatingSession>(model);
        }
    }
}
