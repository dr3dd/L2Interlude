using Core.Module.ParserEngine;
using L2Logger;
using System;
using System.Collections.Generic;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 17.12.2024 1:22:00

namespace Core.Module.SettingData
{
    public class SettingDataInit : BaseParse
    {
        private readonly IParse _parse;
        private IResult _result;
        private readonly IServiceProvider _serviceProvider;

        private IDictionary<string, List<string>> _initialEquipment;
        public SettingDataInit(IServiceProvider provider) : base(provider)
        {
            _serviceProvider = provider;
            _parse = new ParseSetting(new Result());
        }

        public List<string> GetInitialEquipment(string base_class) => _initialEquipment.ContainsKey(base_class) ? _initialEquipment[base_class] : new List<string>();

        public override void Run()
        {
            try
            {
                LoggerManager.Info("Setting start...");
                _result = Parse("setting.txt", _parse);
                InitData();
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        private void InitData()
        {
            var data = _result.GetResult();
            if (data.ContainsKey("initial_equipment"))
            {
                _initialEquipment = (IDictionary<string, List<string>>) data["initial_equipment"];
            }
        }
    }
}
