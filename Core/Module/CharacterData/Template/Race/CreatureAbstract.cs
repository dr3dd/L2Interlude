using System.Collections.Generic;

namespace Core.Module.CharacterData.Template.Race
{
    public class CreatureAbstract
    {
        protected IList<float> _baseHpRegen;
        
        public float GetBaseHpRegen(int level)
        {
            return level switch
            {
                >= 1 and <= 10 => _baseHpRegen[0],
                >= 11 and <= 20 => _baseHpRegen[1],
                >= 21 and <= 30 => _baseHpRegen[2],
                >= 31 and <= 40 => _baseHpRegen[3],
                >= 41 and <= 50 => _baseHpRegen[4],
                >= 51 and <= 60 => _baseHpRegen[5],
                >= 61 and <= 70 => _baseHpRegen[6],
                >= 71 and <= 80 => _baseHpRegen[7],
                _ => 0
            };
        }
    }
}