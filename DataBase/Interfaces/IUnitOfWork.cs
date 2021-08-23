﻿namespace DataBase.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        ICharacterRepository Characters { get; }
        ISpawnListRepository SpawnList { get; }
        IRaidBossSpawnListRepository RaidBossSpawnList { get; }
        IUserItemRepository UserItems { get; }
        IUserSkillRepository UserSkill { get; }
        ISkillTreeRepository SkillTree { get; }
        ICharacterSkillRepository CharacterSkill { get; }
        IShortCutRepository ShortCut { get; }
    }
}