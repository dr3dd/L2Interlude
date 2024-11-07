//CLR: 4.0.30319.42000
//USER: GL
//DATE: 10.08.2024 23:10:22

namespace Core.Module.Handlers
{
    public enum ChatType
    {
        GENERAL = 0,
        /// <summary>
        /// !
        /// </summary>
        SHOUT = 1,
        /// <summary>
        /// "
        /// </summary>
        WHISPER = 2,
        /// <summary>
        /// #
        /// </summary>
        PARTY = 3,
        /// <summary>
        /// @
        /// </summary>
        CLAN = 4,
        GM = 5,
        /// <summary>
        /// 
        /// </summary>
        PETITION_PLAYER = 6,
        /// <summary>
        /// *
        /// </summary>
        PETITION_GM = 7,
        /// <summary>
        /// +
        /// </summary>
        TRADE = 8,
        /// <summary>
        /// $
        /// </summary>
        ALLIANCE = 9,
        ANNOUNCEMENT = 10,
        BOAT = 11,
        FRIEND = 12,
        MSNCHAT = 13,
        PARTYMATCH_ROOM = 14,
        PARTYROOM_COMMANDER = 15,
        /// <summary>
        /// `
        /// </summary>
        PARTYROOM_ALL = 16,
        /// <summary>
        /// %
        /// </summary>
        HERO_VOICE = 17,
        CRITICAL_ANNOUNCE = 18,
        SCREEN_ANNOUNCE = 19,
        /// <summary>
        /// ^
        /// </summary>
        BATTLEFIELD = 20,
        MPCC_ROOM = 21,
        NPC_GENERAL = 22,
        NPC_SHOUT = 23,
        NPC_WHISPER = 24,
        /// <summary>
        /// &
        /// </summary>
        WORLD = 25
    }
}
