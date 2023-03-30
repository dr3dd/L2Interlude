using Helpers;
using NpcAi.Ai.NpcType;

namespace NpcAi.Ai.NpcTeleporter
{
    public class EventTeleporterK : Teleporter
    {
        public override IList<TeleportList> Position => new List<TeleportList>
        {
            new("Field of Silence", 91088, 182384, -3192, 1000, 0 ),
            new("West of Field of Silence", 69748, 186111, -2872, 1000, 0 ),
            new("East of Field of Whispers", 97786, 209303, -3040, 1000, 0 ),
            new("Field of Whispers", 74592, 207656, -3032, 1000, 0 ),
            new("Garden of Eva", 84413, 234334, -3656, 1000, 0 ),
            new("Alligator Island", 115583, 192261, -3488, 1000, 0 ),
            new("Alligator Beach", 116132, 202102, -3312, 1000, 0 ),
            new("North of Alligator Island", 116732, 165938, -2448, 1000, 0 ),
            new("Entrance to Alligator Island", 113708, 178387, -3232, 1000, 0 ),
            new("Garden of Eva 2nd Room", 79248, 247390, -8816, 1000, 0 ),
            new("Garden of Eva 3rd Room", 77868, 250400, -9328, 1000, 0 ),
            new("Garden of Eva 4th Room", 78721, 253309, -9840, 1000, 0 ),
            new("Garden of Eva 5th Room", 82570, 252464, -10592, 1000, 0 ),
            new("Garden of Eva Lobby", 82693, 242220, -6712, 1000, 0 )
        };
    }
}
