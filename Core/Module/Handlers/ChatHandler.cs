using Core.Attributes;
using Core.Module.Handlers.Chat;
using Core.Module.Player;
using Helpers;
using L2Logger;
using System;
using System.Collections.Generic;
using System.Reflection;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:13:53

namespace Core.Module.Handlers
{
    public class ChatHandler : IChatHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SortedList<ChatType, AbstractChatMessage> chatTypes = new SortedList<ChatType, AbstractChatMessage>();
        public ChatHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            IEnumerable<Type> typelist = Utility.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Core.Module.Handlers.Chat");
            foreach (Type t in typelist)
            {
                if (!t.Name.StartsWith("Abstract"))
                {
                    Register(Activator.CreateInstance(t));
                }
            }
            LoggerManager.Info($"ChatHandler: Loaded {chatTypes.Count} handlers.");
        }
        public void Chat(PlayerInstance player, ChatType type, string target, string text)
        {
            if (!chatTypes.ContainsKey(type))
            {
                LoggerManager.Warn($"ChatHandler: hander {type} not exists.  Player: {player.CharacterName} Target: {target} Text: {text}");
                return;
            }

            AbstractChatMessage processor = chatTypes[type];
            try
            {
                processor.Chatting(player, type, text, target);
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"ChatHandler: {ex.Message} {ex.StackTrace}");
            }
        }

        public void Register(object processor)
        {
            ChatTypeAttribute attribute =
                (ChatTypeAttribute)processor.GetType().GetCustomAttribute(typeof(ChatTypeAttribute));
            chatTypes.Add(attribute.Type, (AbstractChatMessage)processor);
        }
    }
}
