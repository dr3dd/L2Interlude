using System;
using System.Collections.Generic;
using System.Globalization;

namespace Core.Module.ParserEngine
{
    public class ParsePcParameter : IParse
    {
        private bool _isLevelBonusBegin;
        private bool _isStrBonusBegin;
        private bool _isIntBonusBegin;
        private bool _isConBonusBegin;
        private bool _isMenBonusBegin;
        private bool _isDexBonusBegin;
        private bool _isWitBonusBegin;
        private readonly IDictionary<byte, float> _levelBonus;
        private readonly IDictionary<byte, short> _strBonus;
        private readonly IDictionary<byte, short> _intBonus;
        private readonly IDictionary<byte, short> _conBonus;
        private readonly IDictionary<byte, short> _menBonus;
        private readonly IDictionary<byte, short> _dexBonus;
        private readonly IDictionary<byte, short> _witBonus;
        private readonly IResult _result;

        public ParsePcParameter()
        {
            _result = new Result();
            _levelBonus = new Dictionary<byte, float>();
            _strBonus = new Dictionary<byte, short>();
            _intBonus = new Dictionary<byte, short>();
            _conBonus = new Dictionary<byte, short>();
            _menBonus = new Dictionary<byte, short>();
            _dexBonus = new Dictionary<byte, short>();
            _witBonus = new Dictionary<byte, short>();
        }
        public void ParseLine(string line)
        {
            switch (line)
            {
                case "level_bonus_begin":
                    _isLevelBonusBegin = true;
                    return;
                case "level_bonus_end":
                    _isLevelBonusBegin = false;
                    _result.AddItem("levelBonus", _levelBonus);
                    return;
                case "str_bonus_begin":
                    _isStrBonusBegin = true;
                    return;
                case "str_bonus_end":
                    _isStrBonusBegin = false;
                    _result.AddItem("strBonus", _strBonus);
                    return;
                case "int_bonus_begin":
                    _isIntBonusBegin = true;
                    return;
                case "int_bonus_end":
                    _isIntBonusBegin = false;
                    _result.AddItem("intBonus", _intBonus);
                    return;
                case "con_bonus_begin":
                    _isConBonusBegin = true;
                    return;
                case "con_bonus_end":
                    _isConBonusBegin = false;
                    _result.AddItem("conBonus", _conBonus);
                    return;
                case "men_bonus_begin":
                    _isMenBonusBegin = true;
                    return;
                case "men_bonus_end":
                    _isMenBonusBegin = false;
                    _result.AddItem("menBonus", _menBonus);
                    return;
                case "dex_bonus_begin":
                    _isDexBonusBegin = true;
                    return;
                case "dex_bonus_end":
                    _isDexBonusBegin = false;
                    _result.AddItem("dexBonus", _dexBonus);
                    return;
                case "wit_bonus_begin":
                    _isWitBonusBegin = true;
                    return;
                case "wit_bonus_end":
                    _isWitBonusBegin = false;
                    _result.AddItem("witBonus", _witBonus);
                    return;
            }
            PrepareData(line);
        }

        private void PrepareData(string line)
        {
            if (_isLevelBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][4..].Trim();
                var parseValue = parseRow[1].Trim();
                _levelBonus.Add(Convert.ToByte(parseKey),
                    Convert.ToSingle(parseValue, CultureInfo.InvariantCulture.NumberFormat));
            }

            if (_isStrBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][10..].Trim();
                var parseValue = parseRow[1].Trim();
                _strBonus.Add(Convert.ToByte(parseKey), Convert.ToInt16(parseValue));
            }

            if (_isIntBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][10..].Trim();
                var parseValue = parseRow[1].Trim();
                _intBonus.Add(Convert.ToByte(parseKey), Convert.ToInt16(parseValue));
            }

            if (_isConBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][10..].Trim();
                var parseValue = parseRow[1].Trim();
                _conBonus.Add(Convert.ToByte(parseKey), Convert.ToInt16(parseValue));
            }

            if (_isMenBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][10..].Trim();
                var parseValue = parseRow[1].Trim();
                _menBonus.Add(Convert.ToByte(parseKey), Convert.ToInt16(parseValue));
            }

            if (_isDexBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][10..].Trim();
                var parseValue = parseRow[1].Trim();
                _dexBonus.Add(Convert.ToByte(parseKey), Convert.ToInt16(parseValue));
            }

            if (_isWitBonusBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][10..].Trim();
                var parseValue = parseRow[1].Trim();
                _witBonus.Add(Convert.ToByte(parseKey), Convert.ToInt16(parseValue));
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}