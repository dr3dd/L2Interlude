using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.CharacterData.Template
{
    public class BasicStatBonusInit : BaseParse
    {
        private IDictionary<byte, float> _levelBonus;
        private IDictionary<byte, short> _strBonus;
        private IDictionary<byte, short> _intBonus;
        private IDictionary<byte, short> _conBonus;
        private IDictionary<byte, short> _menBonus;
        private IDictionary<byte, short> _dexBonus;
        private IDictionary<byte, short> _witBonus;
        
        private readonly IParse _parse;

        public BasicStatBonusInit()
        {
            _parse = new ParsePcParameter();
        }

        public float GetLevelBonus(byte level) => _levelBonus[level];
        public short GetStrBonus(byte str) => _strBonus[str];
        public short GetIntBonus(byte intelligence) => _intBonus[intelligence];
        public short GetConBonus(byte con) => _conBonus[con];
        public short GetMenBonus(byte men) => _menBonus[men];
        public short GetDexBonus(byte dex) => _dexBonus[dex];
        public short GetWitBonus(byte wit) => _witBonus[wit];
        public override void Run()
        {
            try
            {
                LoggerManager.Info("PC_parameter start...");
                IResult result = Parse("PC_parameter.txt", _parse);
                InitData(result);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        private void InitData(IResult result)
        {
            var data = result.GetResult();
            if (data.ContainsKey("levelBonus"))
            {
                _levelBonus = (IDictionary<byte, float>) data["levelBonus"];
            }

            if (data.ContainsKey("strBonus"))
            {
                _strBonus = (IDictionary<byte, short>) data["strBonus"];
            }

            if (data.ContainsKey("intBonus"))
            {
                _intBonus = (IDictionary<byte, short>) data["intBonus"];
            }

            if (data.ContainsKey("conBonus"))
            {
                _conBonus = (IDictionary<byte, short>) data["conBonus"];
            }

            if (data.ContainsKey("menBonus"))
            {
                _menBonus = (IDictionary<byte, short>) data["menBonus"];
            }

            if (data.ContainsKey("dexBonus"))
            {
                _dexBonus = (IDictionary<byte, short>) data["dexBonus"];
            }

            if (data.ContainsKey("witBonus"))
            {
                _witBonus = (IDictionary<byte, short>) data["witBonus"];
            }
        }
    }
}