using Dapper.Contrib.Extensions;
using System.Collections.Generic;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 22:21:31

namespace DataBase.Entities
{
    [Table("user_macroses")]
    public class UserMacrosEntity
    {
        public int MacrosId { get; set; }
        public int UserMacrosId { get; set; }
        public int CharacterObjectId { get; set; }
        public int Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Acronym { get; set; }
        public string Commands { get; set; }
    }
}
