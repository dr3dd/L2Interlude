using Core.Module.CharacterData.Template.Race;

namespace Core.Module.CharacterData.Template
{
    public class HumanMagician : Human
    {
        private const int Int = 41;
        private const int Str = 22;
        private const int Con = 27;
        private const int Men = 39;
        private const int Dex = 21;
        private const int Wit = 20;
        
        public int GetInt()
        {
            return Int;
        }

        public int GetStr()
        {
            return Str;
        }

        public int GetCon()
        {
            return Con;
        }

        public int GetMen()
        {
            return Men;
        }

        public int GetDex()
        {
            return Dex;
        }

        public int GetWit()
        {
            return Wit;
        }
    }
}