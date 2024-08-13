using Core.Module.ItemData;
using Core.Module.Player.ShortCuts;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 23:35:18

namespace Core.Module.Player
{
    public class PlayerShortCut
    {
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerCharacterInfo _characterInfo;
        private readonly IList<ShortCut> _shortCuts;
        private readonly IShortCutRepository _shortCutRepository;

        public PlayerShortCut(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _characterInfo = _playerInstance.PlayerCharacterInfo();
            _shortCuts = new List<ShortCut>();
            _shortCutRepository = Initializer.ServiceProvider.GetRequiredService<IUnitOfWorkGame>().ShortCut;
        }

        public IList<ShortCut> GetAllShortCuts()
        {
            return _shortCuts;
        }

        /// <summary>
        /// RegisterShortCut
        /// </summary>
        /// <param name="shortCut"></param>
        public async Task RegisterShortCut(ShortCut shortcut)
        {
            switch (shortcut.Type)
            {
                case ShortCutType.NONE:
                case ShortCutType.ITEM:
                case ShortCutType.ACTION:
                case ShortCutType.MACRO:
                case ShortCutType.RECIPE:
                case ShortCutType.BOOKMARK:
                    break;
                case ShortCutType.SKILL:
                    int level = _playerInstance.PlayerSkill().GetSkillLevel(shortcut.Id);
                    if (level > 0)
                    {
                        shortcut.Level = level;
                    }
                    else { 
                        return; 
                    }
                    break;
            }

            var shortCutEntity = ToEntity(shortcut);
            ShortCut currentShortCut = _shortCuts.FirstOrDefault(sc => (sc.Slot == shortcut.Slot) && (sc.Page == shortcut.Page));
            if (currentShortCut != null)
            {
                _shortCuts.Remove(shortcut);
                await _shortCutRepository.DeleteShortCutAsync(shortCutEntity);
            }

            await _playerInstance.SendPacketAsync(new ShortCutRegister(shortcut));

            _shortCuts.Add(shortcut);
            await _shortCutRepository.AddAsync(shortCutEntity);
        }

        /// <summary>
        /// DeleteShortCut
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task DeleteShortCutAsync(int slot, int page)
        {
            ShortCut currentShortCut = _shortCuts.FirstOrDefault(sc => (sc.Slot == slot) && (sc.Page == page));
            if (currentShortCut == null)
            {
                return;
            }
            _shortCuts.Remove(currentShortCut);
            await _shortCutRepository.DeleteShortCutAsync(ToEntity(currentShortCut));

            if (currentShortCut.Type == ShortCutType.ITEM)
            {
                ItemInstance item = _playerInstance.PlayerInventory().GetInventoryItemByObjectId(currentShortCut.Id);
                //TODO add soulshot
            }
            await _playerInstance.SendPacketAsync(new ShortCutInit(_playerInstance));
        }

        private ShortCutEntity ToEntity(ShortCut shortcut)
        {
            ShortCutEntity shortCutEntity = new ShortCutEntity
            {
                CharacterObjectId = _characterInfo.CharacterId,
                Level = shortcut.Level,
                Page = shortcut.Page,
                Slot = shortcut.Slot,
                Type = (int)shortcut.Type,
                ClassIndex = _playerInstance.TemplateHandler().GetClassId(),
                ShortcutId = shortcut.Id
            };
            return shortCutEntity;
        }

        public async Task RestoreShortCuts()
        {
            var shortCuts = await _shortCutRepository.GetShortCutsByOwnerIdAsync(_characterInfo.CharacterId,
                _playerInstance.TemplateHandler().GetClassId());
            foreach (var shortCutEntity in shortCuts)
            {
                ShortCut sc = new ShortCut(shortCutEntity.Slot, shortCutEntity.Page, (ShortCutType)shortCutEntity.Type,
                    shortCutEntity.ShortcutId, shortCutEntity.Level);
                _shortCuts.Add(sc);
            }
        }
    }
}
