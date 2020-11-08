using System;

namespace BL.Models
{
    public class LoginResponseModel
    {
        public User User { get; set; }
        public Exception Ex { get; set; }
        public bool Succeed { get; set; }
    }
}
