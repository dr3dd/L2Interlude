using System;
using System.Collections.Generic;
using Core.Module.CharacterData.Template.Class;
using L2Logger;

namespace Core.Module.CharacterData.Template
{
    public class TemplateInit
    {
        private readonly IDictionary<int, ITemplateHandler> _handlers;

        public TemplateInit()
        {
            _handlers = new Dictionary<int, ITemplateHandler>();
            RegisterTemplateHandler(new Fighter());
            RegisterTemplateHandler(new Mage());
        }

        private void RegisterTemplateHandler(ITemplateHandler templateHandler)
        {
            int classId = templateHandler.GetClassId();
            _handlers.Add(classId, templateHandler);
        }

        public ITemplateHandler GetTemplateByClassId(int classId)
        {
            try
            {
                return _handlers[classId];
            }
            catch (Exception ex)
            {
                LoggerManager.Error("TemplateInit:" + ex.Message);
                throw;
            }
        }
    }
}