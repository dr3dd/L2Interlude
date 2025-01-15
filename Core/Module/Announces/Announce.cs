using Config;
using Core.Enums;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Core.TaskManager;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 14.01.2025 16:56:18

namespace Core.Module.Announces
{
    public class Announce
    {
        private readonly List<AnnounceModel> _loginAnnonces;
        private readonly List<AnnounceModel> _intervalAnnonces;
        private GameServerConfig serverConfig;
        private readonly IAnnounceRepository _announceRepository;
        private Task _announceTask;
        private CancellationTokenSource _intervalAnnounceCancellationToken;
        public Announce(IServiceProvider serviceProvider)
        {
            _loginAnnonces = new List<AnnounceModel>();
            _intervalAnnonces = new List<AnnounceModel>();
            _announceRepository = serviceProvider.GetRequiredService<IUnitOfWorkGame>().Announce;
            serverConfig = serviceProvider.GetService<GameConfig>().ServerConfig;
            _ = Load();
            StartIntervalAnnounceTask();
        }

        public async Task Load()
        {
            var loginAnnonces = await _announceRepository.GetLoginAnnounces();
            foreach (var annonce in loginAnnonces)
            {
                _loginAnnonces.Add(new AnnounceModel() {
                    AnnounceId = annonce.AnnounceId,
                    AnnounceMsg = annonce.AnnounceMsg,
                    Interval = annonce.Interval
                });
            }

            var intervalAnnonces = await _announceRepository.GetIntervalAnnounces();
            foreach (var annonce in intervalAnnonces)
            {
                _intervalAnnonces.Add(new AnnounceModel()
                {
                    AnnounceId = annonce.AnnounceId,
                    AnnounceMsg = annonce.AnnounceMsg,
                    Interval = annonce.Interval,
                    NextSendTime = DateTime.Now.AddSeconds(annonce.Interval)
                });
            }

            LoggerManager.Info($"Announce: Loaded {_loginAnnonces.Count} login and {_intervalAnnonces.Count} interval announces");
        }

        public async Task Reload() {
            _loginAnnonces.Clear();
            _intervalAnnonces.Clear();
            await Load();
        }

        public async Task ShowLoginAnnounces(PlayerInstance player)
        {
            foreach (var annonce in _loginAnnonces)
            {
                await player.SendPacketAsync(new Say2(null, player.CharacterName, ChatType.ANNOUNCEMENT, annonce.AnnounceMsg));
            }
            
        }

        private void IntervalAnnounceShow()
        {
            foreach (var annonce in _intervalAnnonces)
            {
                if (DateTime.Compare(DateTime.Now, annonce.NextSendTime) > 0)
                {
                    _ = Initializer.ChatHandler().Chat(null, ChatType.ANNOUNCEMENT, null, annonce.AnnounceMsg);
                    annonce.NextSendTime = DateTime.Now.AddSeconds(annonce.Interval);
                }
            }
        }

        public bool AnnounceTaskIsRun()
        {
            if (_announceTask is null || _announceTask.Status == TaskStatus.RanToCompletion || _announceTask.Status == TaskStatus.Canceled)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void StartIntervalAnnounceTask()
        {
            if (!AnnounceTaskIsRun())
            {
                _intervalAnnounceCancellationToken = new CancellationTokenSource();
                var token = _intervalAnnounceCancellationToken.Token;
                _announceTask = TaskManagerScheduler.ScheduleAtFixedRate(IntervalAnnounceShow, 1000, 1000, _intervalAnnounceCancellationToken.Token);
                LoggerManager.Info("Announce: IntervalAnnounceTask started");
            }
        }

        public void StopIntervalAnnounceTask()
        {
            _intervalAnnounceCancellationToken?.Cancel();
            _intervalAnnounceCancellationToken = null;
            LoggerManager.Info("Announce: IntervalAnnounceTask stopped");
        }
    }
}
