using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.TaskManager;

public class MovementTaskManager
{
    private static readonly ConcurrentDictionary<int, Character> AllCharacters = new();
    private static volatile MovementTaskManager _instance;
    private const int TaskDelay = 100;
    
    public static MovementTaskManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            if (_instance == null)
                _instance = new MovementTaskManager();
            return _instance;
        }
    }
    
    protected MovementTaskManager()
    {
    }
    
    private class Movement : IRunnable
    {
        private readonly List<Character> _characters;

        public Movement(List<Character> characters)
        {
            _characters = characters;
        }

        public async Task Run()
        {
            if (_characters.Count == 0)
            {
                return;
            }

            var charactersToRemove = new List<Character>();
            foreach (var character in _characters)
            {
                try
                {
                    if (character == null)
                        continue;

                    if (await character.CharacterMovement().UpdatePosition())
                    {
                        charactersToRemove.Add(character);
                        character.CharacterNotifyEvent().NotifyEvent(CtrlEvent.EvtArrived);
                    }

                    await character.UpdateKnownObjects();
                    await character.RemoveKnownObjects();
                }
                catch (Exception e)
                {
                    charactersToRemove.Add(character);
                    LoggerManager.Error("MovementTaskManager: Problem updating position of " + character + e.StackTrace);
                }
            }
            foreach (var character in charactersToRemove)
            {
                AllCharacters.TryRemove(character.ObjectId, out _);
            }
        }
    }
    
    public async Task RegisterMovingObject(Character character)
    {
        if (AllCharacters.ContainsKey(character.ObjectId))
        {
            return;
        }

        AllCharacters.TryAdd(character.ObjectId, character);

        await Task.Run(async () =>
        {
            var characters = AllCharacters.Values.ToList();
            var movement = new Movement(characters);
            while (true)
            {
                await movement.Run();
                await Task.Delay(TaskDelay);
            }
        });
    }
}