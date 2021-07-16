using System;
using System.Collections.Generic;
using Core.Module.CharacterData.Template.Class;
using L2Logger;

namespace Core.Module.CharacterData.Template
{
    public class TemplateInit
    {
        private readonly IDictionary<byte, ITemplateHandler> _handlers;

        public TemplateInit()
        {
            _handlers = new Dictionary<byte, ITemplateHandler>();
            RegisterTemplateHandler(new Fighter());
            RegisterTemplateHandler(new Mage());
        }

        private void RegisterTemplateHandler(ITemplateHandler templateHandler)
        {
            byte classId = templateHandler.GetClassId();
            _handlers.Add(classId, templateHandler);
        }

        public ITemplateHandler GetTemplateByClassId(byte classId)
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