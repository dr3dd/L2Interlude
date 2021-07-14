using System;

namespace DataBase.Entities
{
    [Dapper.Contrib.Extensions.Table("user_auth")]
    public class UserAuthEntity
    {
        public int AccountId { get; set; }
        
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } 
        
        public DateTime LastLogin { get; set; }
        
        public byte LastWorld { get; set; }
        
        public UserAuthEntity()
        {
            LastLogin = DateTime.Now;
            Email = "test@test.com";
            LastWorld = 1;
        }
    }
}