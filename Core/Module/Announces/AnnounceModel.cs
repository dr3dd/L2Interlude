using DataBase.Entities;
using System;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.01.2025 18:25:22

namespace Core.Module.Announces
{
    public class AnnounceModel : AnnounceEntity
    {
        public DateTime NextSendTime { get; set; }
    }
}
