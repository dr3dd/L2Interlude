using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class EventTeleporterC : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Dark Forest", -22224, 14168, -3232, 50, 0 ),
            new("Spider Nest", -61095, 75104, -3383, 50, 0 ),
            new("Swampland", -21472, 36244, -2794, 50, 0 ),
            new("East of School of Dark Arts", -30777, 49750, -3552, 50, 0 ),
            new("South of Spider Nest", -56532, 78321, -2960, 50, 0 ),
            new("Black Rock Hill", -23520, 68688, -3648, 50, 0 ),
            new("South of Scholl of Dark Arts", -47087, 59571, -3328, 50, 0 ),
            new("School of Dark Arts", -49185, 49441, -5912, 50, 0 )
        };
    }
}
