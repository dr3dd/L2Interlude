using NpcService.Ai.NpcType;

namespace NpcService.Ai
{
    public class Doorkeeper : Citizen
    {
        public virtual string DoorName1 { get; set; } = "partisan001";
        public virtual string DoorName2 { get; set; } = "partisan002";
        public override string FnHi { get; set; } = "gludio_outter_doorman001.htm";
        public string FnNotMyLord { get; set; } = "gludio_outter_doorman002.htm";
        public string FnUnderSiege { get; set; } = "gludio_outter_doorman003.htm";
        public int PosX01 { get; set; } = 1;
        public int PosY01 { get; set; } = 1;
        public int PosZ01 { get; set; } = 1;
        public int PosX02 { get; set; } = 1;
        public int PosY02 { get; set; } = 1;
        public int PosZ02 { get; set; } = 1;

        public override void Talked(Talker talker)
        {
            base.Talked(talker);
        }

        public override void MenuSelected(Talker talker, int ask, int reply, string fhtml0)
        {
            switch (ask)
            {
                case -201 when MySelf.CastleIsUnderSiege():
                    MySelf.ShowPage(talker, FnUnderSiege);
                    break;
                case -201:
                    switch (reply)
                    {
                        case 1:
                            MySelf.CastleGateOpenClose2(DoorName1, 0);
                            MySelf.CastleGateOpenClose2(DoorName2, 0);
                            break;
                        case 2:
                            MySelf.CastleGateOpenClose2(DoorName1, 1);
                            MySelf.CastleGateOpenClose2(DoorName2, 1);
                            break;
                    }
                    break;
                case -202:
                    switch (reply)
                    {
                        case 1:
                            MySelf.InstantTeleport(talker, PosX01, PosY01, PosZ01);
                            break;
                        case 2:
                            MySelf.InstantTeleport(talker, PosX02, PosY02, PosZ02);
                            break;
                    }
                    break;
            }
        }
    }
}