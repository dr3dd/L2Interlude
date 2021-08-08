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
        
        private bool _isCpTableBegin;
        private bool _isHpTableBegin;
        private bool _isMpTableBegin;
        private string _className;
        
        private readonly IDictionary<byte, float> _levelBonus;
        private readonly IDictionary<byte, short> _strBonus;
        private readonly IDictionary<byte, short> _intBonus;
        private readonly IDictionary<byte, short> _conBonus;
        private readonly IDictionary<byte, short> _menBonus;
        private readonly IDictionary<byte, short> _dexBonus;
        private readonly IDictionary<byte, short> _witBonus;
        private readonly IDictionary<byte, float> _cpTable;
        private readonly IDictionary<byte, float> _hpTable;
        private readonly IDictionary<byte, float> _mpTable;
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
            _cpTable = new Dictionary<byte, float>();
            _hpTable = new Dictionary<byte, float>();
            _mpTable = new Dictionary<byte, float>();
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
            
            //Init CP
            if (line.EndsWith("cp_table_begin"))
            {
                _isCpTableBegin = true;
                _className = line.Replace("_cp_table_begin", "");
                return;
            }
            if (line.EndsWith("cp_table_end"))
            {
                _isCpTableBegin = false;
                _result.AddItem(_className + "_cp", new Dictionary<byte, float>(_cpTable));
                _cpTable.Clear();
            }

            //Init HP
            if (line.EndsWith("hp_table_begin"))
            {
                _isHpTableBegin = true;
                _className = line.Replace("_hp_table_begin", "");
                return;
            }
            if (line.EndsWith("hp_table_end"))
            {
                _isHpTableBegin = false;
                _result.AddItem(_className + "_hp", new Dictionary<byte, float>(_hpTable));
                _hpTable.Clear();
            }
            
            //Init MP
            if (line.EndsWith("mp_table_begin"))
            {
                _isMpTableBegin = true;
                _className = line.Replace("_mp_table_begin", "");
                return;
            }
            if (line.EndsWith("mp_table_end"))
            {
                _isMpTableBegin = false;
                _result.AddItem(_className + "_mp", new Dictionary<byte, float>(_mpTable));
                _mpTable.Clear();
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
            
            if (_isCpTableBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][4..].Trim();
                var parseValue = parseRow[1].Trim();
                _cpTable.Add(Convert.ToByte(parseKey),
                    Convert.ToSingle(parseValue, CultureInfo.InvariantCulture.NumberFormat));
            }

            if (_isHpTableBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][4..].Trim();
                var parseValue = parseRow[1].Trim();
                _hpTable.Add(Convert.ToByte(parseKey),
                    Convert.ToSingle(parseValue, CultureInfo.InvariantCulture.NumberFormat));
            }
            
            if (_isMpTableBegin)
            {
                var parseRow = line.Replace("\t", "").Split("=");
                var parseKey = parseRow[0][4..].Trim();
                var parseValue = parseRow[1].Trim();
                _mpTable.Add(Convert.ToByte(parseKey),
                    Convert.ToSingle(parseValue, CultureInfo.InvariantCulture.NumberFormat));
            }
        }

        public IResult GetResult()
        {
            return _result;
        }
    }
}