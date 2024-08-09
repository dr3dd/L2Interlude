//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 23:43:08

namespace Core.Module.Player.ShortCuts
{
    public class ShortCut
    {
        public int Id { get; }
        public int Slot { get; }
        public int Page { get; }
        public ShortCutType Type { get; }
        public int Level { get; set; }

    public ShortCut(int slotId, int pageId, ShortCutType shortcutType, int shortcutId, int shortcutLevel = -1)
    {
        Slot = slotId;
        Page = pageId;
        Type = shortcutType;
        Id = shortcutId;
        Level = shortcutLevel;
    }

}
}
