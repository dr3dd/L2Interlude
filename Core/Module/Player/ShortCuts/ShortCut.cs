//CLR: 4.0.30319.42000
//USER: GL
//DATE: 09.08.2024 23:43:08

namespace Core.Module.Player.ShortCuts
{
    public class ShortCut
    {
        public int Id { get; }
        public int SlotNum { get; }
        public ShortCutType ShortcutType { get; }
        public int ShortcutMacro { get; set; }

        public ShortCut(int slotNum, ShortCutType shortcutType, int shortcutId, int shortcutMacro = -1)
        {
            SlotNum = slotNum;
            ShortcutType = shortcutType;
            Id = shortcutId;
            ShortcutMacro = shortcutMacro;
        }

    }
}
