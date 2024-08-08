//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 0:01:41

namespace Config
{
    public class DebugConfig
    {
        public const string DebugSection = "DebugSection";
        /// <summary>
        /// Показывать заголовок пакета
        /// </summary>
        public bool ShowHeaderPacket { get; set; } = false;
        /// <summary>
        /// Показывать название пакета
        /// </summary>
        public bool ShowNamePacket { get; set; } = false;
        /// <summary>
        /// Показывать тело пакета
        /// </summary>
        public bool ShowPacket { get; set; } = false;
    }
}
