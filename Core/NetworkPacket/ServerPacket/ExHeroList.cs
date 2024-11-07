using L2Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 07.11.2024 12:24:49

namespace Core.NetworkPacket.ServerPacket
{
    public class ExHeroList : Network.ServerPacket
    {
        private readonly List<string> _heroList;

        public ExHeroList()
        {
            _heroList = new List<string>();
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0xFE);
            await WriteShortAsync(0x23);
            await WriteIntAsync(_heroList.Count);
            LoggerManager.Warn("ExHeroList need impl");
            /*
            foreach (var hero in _heroList)
            {
                await WriteStringAsync(hero.getString(Olympiad.CHAR_NAME));
                await WriteIntAsync(hero.getInt(Olympiad.CLASS_ID));
                await WriteStringAsync(hero.getString(Hero.CLAN_NAME, ""));
                await WriteIntAsync(hero.getInt(Hero.CLAN_CREST, 0));
                await WriteStringAsync(hero.getString(Hero.ALLY_NAME, ""));
                await WriteIntAsync(hero.getInt(Hero.ALLY_CREST, 0));
                await WriteIntAsync(hero.getInt(Hero.COUNT));
                await WriteIntAsync(0);
            }
            */
        }
    }
}
