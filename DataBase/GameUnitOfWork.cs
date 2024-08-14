using System;
using DataBase.Interfaces;

namespace DataBase
{
    public class GameUnitOfWork : IUnitOfWorkGame
    {
        public ICharacterRepository Characters { get; }
        public ISpawnListRepository SpawnList { get; }
        public IRaidBossSpawnListRepository RaidBossSpawnList { get; }
        public IUserItemRepository UserItems { get; }
        public IUserSkillRepository UserSkill { get; }
        
        public ISkillTreeRepository SkillTree { get; }
        public ICharacterSkillRepository CharacterSkill { get; }
        public IShortCutRepository ShortCut { get; }

        public GameUnitOfWork( 
            ICharacterRepository characterRepository,
            ISpawnListRepository spawnListRepository,
            IUserItemRepository userItemRepository,
            IUserSkillRepository userSkillRepository,
            ISkillTreeRepository skillTreeRepository,
            ICharacterSkillRepository characterSkillRepository,
            IShortCutRepository shortCutRepository,
            IRaidBossSpawnListRepository raidBossSpawnListRepository
            )
        {
            Characters = characterRepository;
            SpawnList = spawnListRepository;
            UserItems = userItemRepository;
            UserSkill = userSkillRepository;
            SkillTree = skillTreeRepository;
            CharacterSkill = characterSkillRepository;
            ShortCut = shortCutRepository;
            RaidBossSpawnList = raidBossSpawnListRepository;
        }
    }
}