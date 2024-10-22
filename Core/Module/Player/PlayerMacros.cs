using Core.Module.NpcAi.Ai.NpcMonrace;
using Core.Module.Player.Macroses;
using Core.Module.Player.ShortCuts;
using Core.NetworkPacket.ServerPacket;
using DataBase.Entities;
using DataBase.Interfaces;
using DataBase.Repositories;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.09.2024 22:42:20

namespace Core.Module.Player
{
    public class PlayerMacros
    {
        private readonly PlayerInstance _playerInstance;
        private readonly PlayerCharacterInfo _characterInfo;
        private readonly PlayerShortCut _playerShortCut;
        private readonly List<MacrosModel> _macroses;
        private readonly IMacrosRepository _macrosRepository;
        private int _userMacrosId;

        public PlayerMacros(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _characterInfo = _playerInstance.PlayerCharacterInfo();
            _playerShortCut = _playerInstance.PlayerShortCut();
            _macroses = new List<MacrosModel>();
            _userMacrosId = 1000;
            _macrosRepository = Initializer.ServiceProvider.GetRequiredService<IUnitOfWorkGame>().Macros;
        }

        public IList<MacrosModel> GetAllMacroses()
        {
            return _macroses;
        }

        public async Task RegisterMacros(MacrosModel macros) {

            macros.CharacterObjectId = _characterInfo.CharacterId;
            MacroUpdateType macroUpdateType = MacroUpdateType.ADD;

            if (macros.UserMacrosId.Equals(0))
            {
                int maxId = _macroses.Count > 0 ? _macroses.Max(m => m.UserMacrosId) : _userMacrosId;
                macros.UserMacrosId = ++maxId;
                macros.MacrosId = await _macrosRepository.AddAsync(macros);
                _macroses.Add(macros);
            }
            else
            {
                macroUpdateType = MacroUpdateType.MODIFY;
                foreach (MacrosModel macrosOld in _macroses)
                {
                    if (macrosOld.UserMacrosId == macros.UserMacrosId)
                    {
                        macrosOld.Name = macros.Name;
                        macrosOld.Description = macros.Description;
                        macrosOld.Acronym = macros.Acronym;
                        macrosOld.Icon = macros.Icon;
                        macrosOld.CommandArray = macros.CommandArray;
                        macros.MacrosId = macrosOld.MacrosId;
                        await _macrosRepository.UpdateAsync(macrosOld);
                        break;
                    }
                }
            }

            if (macros.MacrosId != 0)
            {
                foreach (MacrosModel macros_u in _macroses)
                {
                    await _playerInstance.SendPacketAsync(new MacroList(_macroses.Count, macros_u, macroUpdateType));
                }
            }
            

        }

        public async Task RegisterMacros_(MacrosModel macros)
        {
            int id = -1;
            MacroUpdateType macroUpdateType = MacroUpdateType.ADD;
            if (macros.UserMacrosId.Equals(0))
            {
                int maxId = _macroses.Count > 0 ? _macroses.Max(m => m.UserMacrosId) : _userMacrosId;
                macros.UserMacrosId = ++maxId;
                _macroses.Add(macros);
                id = await _macrosRepository.AddAsync(macros);
                macros.MacrosId = id;
            }
            else
            {
                macroUpdateType = MacroUpdateType.MODIFY;
                MacrosModel macrosOld = _macroses.FirstOrDefault(m => m.UserMacrosId == macros.UserMacrosId);
                if (macrosOld != null)
                {
                    macrosOld.Name = macros.Name;
                    macrosOld.Description = macros.Description;
                    macrosOld.Acronym = macros.Acronym;
                    macrosOld.Icon = macros.Icon;
                    macrosOld.CommandArray = macros.CommandArray;
                    await _macrosRepository.UpdateAsync(macrosOld);
                    id = macrosOld.MacrosId;
                }

            }

            if (id != -1)
            {
                await _playerInstance.SendPacketAsync(new MacroList(_macroses.Count, macros, macroUpdateType));
            }
        }

        public async Task RestoreMacroses()
        {
            var macrosesList = await _macrosRepository.GetMarcosesByOwnerIdAsync(_characterInfo.CharacterId);
            foreach (var macros_item in macrosesList)
            {
                MacrosModel macros = new MacrosModel()
                {
                    Acronym = macros_item.Acronym,
                    CharacterObjectId = macros_item.CharacterObjectId,
                    Description = macros_item.Description,
                    Icon = macros_item.Icon,
                    MacrosId = macros_item.MacrosId,
                    Name = macros_item.Name,
                    UserMacrosId = macros_item.UserMacrosId,
                    Commands = macros_item.Commands
                };
                _macroses.Add(macros);
            }
        }

        public async Task SendAllMacros()
        {

            if (_macroses.Count > 0)
            {   //todo
                foreach (MacrosModel macros in _macroses)
                {
                    await _playerInstance.SendPacketAsync(new MacroList(_macroses.Count, macros, MacroUpdateType.LIST));
                }
            }
            else
            {
                await _playerInstance.SendPacketAsync(new MacroList(0, null, MacroUpdateType.LIST));
            }
        }

        public async Task DeleteMacros(int userMacrosId)
        {
            for (int i = _macroses.Count - 1; i >= 0; i--)
            {
                if (_macroses[i].UserMacrosId == userMacrosId)
                {
                    int id = await _macrosRepository.DeleteMarcosAsync(_macroses[i]);
                    

                    _macroses.Remove(_macroses[i]);

                    if (id != 0)
                    {
                        await SendAllMacros();

                        var shortCuts = _playerShortCut.GetAllShortCuts().ToList().Where(shortCut => shortCut.Type == ShortCutType.MACRO && shortCut.Id == userMacrosId);
                        foreach (var shortCut in shortCuts)
                        {
                            LoggerManager.Info(shortCut.Id.ToString());
                            await _playerShortCut.DeleteShortCutAsync(shortCut.Slot, shortCut.Page);
                        }
                    }

                    break;
                }
            }
        }
    }
}
