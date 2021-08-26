using System.Collections.Generic;
using Core.Module.SkillData.Effect;

namespace Core.Module.SkillData
{
    public class EffectInit
    {
        private readonly IDictionary<string, Effects> _handlers;
        public EffectInit()
        {
            _handlers = new Dictionary<string, Effects>();

            RegisterSkillHandler("p_speed", new PSpeed());
        }
        
        private void RegisterSkillHandler(string key, Effects handler)
        {
            _handlers.Add(key, handler);
        }

        public Effects GetEffectHandler(string key)
        {
            return _handlers[key];
        }
        
    }
}