//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 0:01:41

namespace Config
{
    public class DebugConfig
    {
        public const string DebugSection = "DebugSection";
        /// <summary>
        /// Show packet header
        /// Показывать заголовок пакета
        /// </summary>
        public bool ShowHeaderPacket { get; set; } = false;
        /// <summary>
        /// Show packet name
        /// Показывать название пакета
        /// </summary>
        public bool ShowNamePacket { get; set; } = false;
        /// <summary>
        /// Show packet body
        /// Показывать тело пакета
        /// </summary>
        public bool ShowPacket { get; set; } = false;
        /// <summary>
        /// Show packet auth>game
        /// </summary>
        public bool ShowPacketToGame { get; set; } = false;
        /// <summary>
        /// Show packet game>auth
        /// </summary>
        public bool ShowPacketToAuth { get; set; } = false;
        /// <summary>
        /// Show packet server>client
        /// </summary>
        public bool ShowPacketToClient { get; set; } = false;
        /// <summary>
        /// Show packet client>server
        /// </summary>
        public bool ShowPacketFromClient { get; set; } = false;
    }
}
