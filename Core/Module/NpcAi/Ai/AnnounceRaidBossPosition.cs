using Helpers;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Module.NpcAi.Ai;

public class AnnounceRaidBossPosition : Citizen
{
    public virtual IList<TeleportList> Position => new List<TeleportList>
    {
        {new("Talking Island Village", -84169, 244693, -3729, 100000, 0 )}
    };

    public IList<TeleportList> RaidBossList20_29 => new List<TeleportList>
    {
        new("Discarded Guardian (lv20)", 48000, 243376, -6611, 0, 0),
        new("Zombie Lord Farakelsus (lv20)", 22500, 80300, -2772, 0, 0),
        new("Madness Beast (lv20)", -54096, 84288, -3512, 0, 0),
        new("Serpent Demon Bifrons (lv21)", -13056, 215680, -3760, 0, 0),
        new("Sukar Wererat Chief (lv21)", -3456, 112864, -3456, 0, 0),
        new("Malex Herald of Dagoniel  (lv21)", 9649, 77467, -3808, 0, 0),
        new("Kaysha Herald of Icarus (lv21)", -47367, 51548, -5904, 0, 0),
        new("Greyclaw Kutus (lv23)", -54416, 146480, -2887, 0, 0),
        new("Tracker Leader Sharuk (lv23)", -55920, 186768, -3336, 0, 0),
        new("Kuroboros' Priest (lv23)", -62368, 179440, -3594, 0, 0),
        new("Unrequited Kael (lv24)", -60428, 188264, -4512, 0, 0),
        new("Langk Matriarch Rashkos (lv24)", -47552, 219232, -2413, 0, 0),
        new("Pan Dryad (lv25)", 7376, 169376, -3600, 0, 0),
        new("Princess Molrang (lv25)", -60976, 127552, -2960, 0, 0),
        new("Zombie Lord Crowl (lv25)", -12656, 138176, -3584, 0, 0),
        new("Ikuntai (lv25)", -21800, 152000, -2900, 0, 0),
        new("Soul Scavenger (lv25)", -45616, 111024, -3808, 0, 0),
        new("Betrayer of Urutu Freki (lv25)", -18048, -101264, -2112, 0, 0),
        new("Mammon Collector Talos (lv25)", 172064, -214752, -3565, 0, 0),
        new("Tiger Hornet (lv26)", 29216, 179280, -3624, 0, 0),
        new("Patriarch Kuroboros (lv26)", -62000, 190256, -3687, 0, 0),
        new("Tirak (lv28)", -57360, 186272, -4967, 0, 0),
        new("Partisan Leader Talakin (lv28)", 49248, 127792, -3552, 0, 0),
        new("Elf Renoa (lv29)", -37856, 198128, -2672, 0, 0)
    };
    public IList<TeleportList> RaidBossList30_39 => new List<TeleportList>
    {
        new("Turek Mercenary Captain (lv30)", -94208, 100240, -3520, 0, 0),
        new("Cat's Eye Bandit (lv30)", 53712, 102656, -1072, 0, 0),
        new("Agent of Beres,  Meana (lv30)", 116128, 139392, -3640, 0, 0),
        new("Ragraman (lv30)", -54464, 170288, -3136, 0, 0),
        new("Apepi (lv30)", 88256, 176208, -3488, 0, 0),
        new("Giant Wasteland Basilisk (lv30)", -16912, 174912, -3264, 0, 0),
        new("Boss Akata (lv30)", 48575, -106191, -1568, 0, 0),
        new("Captain of Queen's Royal Guards (lv32)", 29928, 107160, -3708, 0, 0),
        new("Skyla (lv32)", 117808, 102880, -3600, 0, 0),
        new("Vuku Grand Seer Gharmash (lv33)", 17696, 179056, -3520, 0, 0),
        new("Nurka's Messenger (lv33)", 45600, 120592, -2455, 0, 0),
        new("Corsair Captain Kylon (lv33)", 35992, 191312, -3104, 0, 0),
        new("Breka Warlock Pastu (lv34)", 90384, 125568, -2128, 0, 0),
        new("Stakato Queen Zyrnna (lv34)", 27280, 101744, -3696, 0, 0),
        new("Cronos's Servitor Mumu (lv34)", 68832, 203024, -3547, 0, 0),
        new("Revenant of Sir Calibus (lv34)", 51632, 153920, -3552, 0, 0),
        new("Remmel (lv35)", 10416, 126880, -3676, 0, 0),
        new("Chertuba of Great Soul (lv35)", -91024, 116304, -3466, 0, 0),
        new("Sejarr's Servitor (lv35)", 123536, 133504, -3584, 0, 0),
        new("Guilotine,  Warden of the Execution Grounds (lv35)", 50896, 146576, -3645, 0, 0),
        new("Flame Lord Shadar (lv35)", 43872, 123968, -2928, 0, 0),
        new("Tasaba Patriarch Hellena (lv35)", 88123, 166312, -3412, 0, 0),
        new("Soul Collector Acheron (lv35)", 43152, 152352, -2848, 0, 0),
        new("Gargoyle Lord Sirocco (lv35)", -16096, 184288, -3817, 0, 0),
        new("Red Eye Captain Trakia (lv35)", 40128, 101920, -1241, 0, 0),
        new("Eye of Beleth (lv35)", 5000, 189000, -3728, 0, 0),
        new("Sebek (lv36)", 76352, 193216, -3648, 0, 0),
        new("Evil Spirit Tempest (lv36)", 53600, 143472, -3872, 0, 0),
        new("Rayito the Looter (lv37)", 127900, -160600, -1100, 0, 0),
        new("Lizardmen Leader Hellion (lv38)", 26064, 121808, -3738, 0, 0),
        new("Premo Prime (lv38)", 101888, 200224, -3680, 0, 0),
        new("Leader of Cat Gang (lv39)", 88512, 140576, -3483, 0, 0),
        new("Nellis' Vengeful Spirit (lv39)", 123000, -141000, -1100, 0, 0)
    };
    public IList<TeleportList> RaidBossList40_49 => new List<TeleportList>
    {
        new("Queen Ant (lv40)", -21610, 181594, -5734, 0, 0),
        new("Wizard of Storm Teruk (lv40)", 92528, 84752, -3703, 0, 0),
        new("Icarus Sample 1 (lv40)", 94000, 197500, -3300, 0, 0),
        new("Leto Chief Talkin (lv40)", 87536, 75872, -3591, 0, 0),
        new("Shaman King Selu (lv40)", 73520, 66912, -3728, 0, 0),
        new("Water Couatle Ateka (lv40)", 73776, 201552, -3760, 0, 0),
        new("Fafurion's Page Sika (lv40)", 112112, 209936, -3616, 0, 0),
        new("Road Scavenger Leader (lv40)", 72192, 125424, -3657, 0, 0),
        new("Nakondas (lv40)", 128352, 138464, -3467, 0, 0),
        new("Water Spirit Lian (lv40)", 83056, 183232, -3616, 0, 0),
        new("Gwindorr (lv40)", 86528, 216864, -3584, 0, 0),
        new("Retreat Spider Cletu (lv42)", 124240, 75376, -2800, 0, 0),
        new("Crazy Mechanic Golem (lv43)", 90848, 16368, -5296, 0, 0),
        new("Earth Protector Panathen (lv43)", 125920, 190208, -3291, 0, 0),
        new("Timak Orc Chief Ranger (lv44)", 66944, 67504, -3704, 0, 0),
        new("Rotten Tree Repiro (lv44)", 64048, 16048, -3536, 0, 0),
        new("Dread Avenger Kraven (lv44)", 62416, 8096, -3376, 0, 0),
        new("Flamestone Golem (lv44)", 79648, 18320, -5232, 0, 0),
        new("Thief Kelbar (lv44)", 107000, 92000, -2272, 0, 0),
        new("Biconne of Blue Sky (lv45)", 107056, 168176, -3456, 0, 0),
        new("Shacram (lv45)", 113840, 84256, -2480, 0, 0),
        new("Tiger King Karuta (lv45)", 75968, 110784, -2512, 0, 0),
        new("Iron Giant Totem (lv45)", 93120, 19440, -3607, 0, 0),
        new("Archon Suscepter (lv45)", 15000, 119000, -11900, 0, 0),
        new("Timak Orc Gosmos (lv45)", 67296, 64128, -3723, 0, 0),
        new("Evil Spirit Cyrion (lv45)", 111440, 82912, -2912, 0, 0),
        new("Fafurion's Henchman Istary (lv45)", 126624, 174448, -3056, 0, 0),
        new("Barion (lv47)", 107792, 27728, -3488, 0, 0),
        new("Necrosentinel Royal Guard (lv47)", 81920, 113136, -3056, 0, 0),
        new("King Tarlk (lv48)", 77104, 5408, -3088, 0, 0),
        new("Orfen's Handmaiden (lv48)", 42032, 24128, -4704, 0, 0),
        new("Katu Van Leader Atui (lv49)", 92976, 7920, -3914, 0, 0),
        new("Karte (lv49)", 116352, 27648, -3319, 0, 0),
        new("Mirror of Oblivion (lv49)", 133632, 87072, -3623, 0, 0)
    };
    public IList<TeleportList> RaidBossList50_59 => new List<TeleportList>
    {
        new("Core (lv50)", 17726, 108915, -6480, 0, 0),
        new("Orfen (lv50)", 55024, 17368, -5412, 0, 0),
        new("Ghost of Peasant Leader (lv50)", 169744, 11920, -2732, 0, 0),
        new("Messenger of Fairy Queen Berun (lv50)", 121872, 64032, -3536, 0, 0),
        new("Carnage Lord Gato (lv50)", 75488, -9360, -2720, 0, 0),
        new("Cursed Clara (lv50)", 89904, 105712, -3292, 0, 0),
        new("Carnamakos (lv50)", 23800, 119500, -8976, 0, 0),
        new("Lilith's Witch Marilion (lv50)", 54651, 180269, -4976, 0, 0),
        new("Zaken's Chief Mate Tillion (lv50)", 43160, 220463, -3680, 0, 0),
        new("Verfa (lv51)", 125520, 27216, -3632, 0, 0),
        new("Deadman Ereve (lv51)", 150304, 67776, -3688, 0, 0),
        new("Captain of Red Flag Shaka (lv52)", 94992, -23168, -2176, 0, 0),
        new("Grave Robber Kim (lv52)", 175712, 29856, -3776, 0, 0),
        new("Fafurion's Envoy Pingolpin (lv52)", 88300, 258000, -10200, 0, 0),
        new("Gigantic Chaos Golem (lv52)", 96524, -111070, -3335, 0, 0),
        new("Atraiban (lv53)", 54941, 206705, -3728, 0, 0),
        new("Magus Kenishee (lv53)", 53517, 205413, -3728, 0, 0),
        new("Dark Shaman Varangka  (lv53)", 74000, -102000, 900, 0, 0),
        new("Paniel the Unicorn (lv54)", 124984, 43200, -3625, 0, 0),
        new("Furious Thieles (lv55)", 113920, 52960, -3735, 0, 0),
        new("Enchanted Forest Watcher Ruell (lv55)", 125600, 50100, -3600, 0, 0),
        new("Sorcerer Isirr (lv55)", 135872, 94592, -3735, 0, 0),
        new("Beleth's Seer Sephia (lv55)", 125280, 102576, -3305, 0, 0),
        new("Black Lily (lv55)", 92544, 115232, -3200, 0, 0),
        new("Harit Hero Tamash (lv55)", 165984, 88048, -2384, 0, 0),
        new("Zaken's Butcher Krantz (lv55)", 42050, 208107, -3752, 0, 0),
        new("Pagan Watcher Cerberon (lv55)", 85622, 88766, -5120, 0, 0),
        new("Bandit Leader Barda (lv55)", 104096, -16896, -1803, 0, 0),
        new("Ghost Knight Kabed (lv55)", 183568, 24560, -3184, 0, 0),
        new("Eva's Spirit Niniel (lv55)", 83174, 254428, -10873, 0, 0),
        new("Refugee Hopeful Leo (lv56)", 86300, -8200, -3000, 0, 0),
        new("Harit Guardian Garangky (lv56)", 166288, 68096, -3264, 0, 0),
        new("Timak Seer Ragoth (lv57)", 66672, 46704, -3920, 0, 0),
        new("Eva's Guardian Millenu (lv58)", 88532, 245798, -10376, 0, 0),
        new("Soulless Wild Boar (lv59)", 165424, 93776, -2992, 0, 0),
        new("Demon Kurikups (lv59)", 41966, 215417, -3728, 0, 0),
        new("Abyss Brukunt (lv59)", 155000, 85400, -3200, 0, 0),
        new("Captain of the Ice Queen's Royal Guard (lv59)", 106000, -128000, -3000, 0, 0)
    };
    public IList<TeleportList> RaidBossList60_69 => new List<TeleportList>
    {
        new("Zaken (lv60)", 55312, 219168, -3223, 0, 0),
        new("The 3rd Underwater Guardian (lv60)", 76787, 245775, -10376, 0, 0),
        new("Ghost of the Well Lidia (lv60)", 173880, -11412, -2880, 0, 0),
        new("Giant Marpanak (lv60)", 194107, 53884, -4368, 0, 0),
        new("Guardian of the Statue of Giant Karum (lv60)", 181814, 52379, -4344, 0, 0),
        new("Ancient Weird Drake (lv60)", 120080, 111248, -3047, 0, 0),
        new("Taik High Prefect Arak (lv60)", 170320, 42640, -4832, 0, 0),
        new("Lord Ishka  (lv60)", 115072, 112272, -3018, 0, 0),
        new("Ice Fairy Sirra (lv60)", 102800, -126000, -2500, 0, 0),
        new("Fairy Queen Timiniel (lv61)", 113600, 47120, -4640, 0, 0),
        new("Roaring Lord Kastor (lv62)", 104240, -3664, -3392, 0, 0),
        new("Gorgolos (lv64)", 186192, 61472, -4160, 0, 0),
        new("Rahha (lv65)", 117760, -9072, -3264, 0, 0),
        new("Fierce Tiger King Angel (lv65)", 170656, 85184, -2000, 0, 0),
        new("Hekaton Prime (lv65)", 191975, 56959, -7616, 0, 0),
        new("Gargoyle Lord Tiphon (lv65)", 170048, -24896, -3440, 0, 0),
        new("Enmity Ghost Ramdal (lv65)", 113232, 17456, -4384, 0, 0),
        new("Shilen's Priest Hisilrome (lv65)", 168288, 28368, -3632, 0, 0),
        new("Demon's Agent Falston (lv66)", 93296, -75104, -1824, 0, 0),
        new("Last Titan Utenus (lv66)", 186896, 56276, -4576, 0, 0),
        new("Kernon's Faithful Servant Kelone (lv67)", 144400, -28192, -1920, 0, 0),
        new("Bloody Priest Rudelto (lv69)", 143265, 110044, -3944, 0, 0),
        new("Spirit of Andras,  the Betrayer (lv69)", 185800, -26500, -2000, 0, 0)
    };
    public IList<TeleportList> RaidBossList70_79 => new List<TeleportList>
    {
        new("Shilen's Messenger Cabrio (lv70)", 180968, 12035, -2720, 0, 0),
        new("Korim (lv70)", 116151, 16227, 1944, 0, 0),
        new("Roaring Skylancer (lv70)", 130500, 59098, 3584, 0, 0),
        new("Fafurion's Herald Lokness (lv70)", 102656, 157424, -3735, 0, 0),
        new("Palibati Queen Themis (lv70)", 192376, 22087, -3608, 0, 0),
        new("Beast Lord Behemoth (lv70)", 123504, -23696, -3481, 0, 0),
        new("Anakim's Nemesis Zakaron (lv70)", 151053, 88124, -5424, 0, 0),
        new("Flame of Splendor Barakiel (lv70)", 91008, -85904, -2736, 0, 0),
        new("Meanas Anor (lv70)", 156704, -6096, -4185, 0, 0),
        new("Eilhalder von Hellmann (lv71)", 59331, -42403, -3003, 0, 0),
        new("Immortal Savior Mardil (lv71)", 113200, 17552, -1424, 0, 0),
        new("Water Dragon Seer Sheshark (lv72)", 108096, 157408, -3688, 0, 0),
        new("Vanor Chief Kandra (lv72)", 116400, -62528, -3264, 0, 0),
        new("Doom Blade Tanatos (lv72)", 127903, -13399, -3720, 0, 0),
        new("Death Lord Hallate (lv73)", 113551, 17083, -2120, 0, 0),
        new("Plague Golem (lv73)", 170000, -60000, -3500, 0, 0),
        new("Antharas Priest Cloe (lv74)", 152660, 110387, -5520, 0, 0),
        new("Krokian Padisha Sobekk (lv74)", 119760, 157392, -3744, 0, 0),
        new("Icicle Emperor Bumbalump (lv74)", 158352, -121088, -2240, 0, 0),
        new("Baium (lv75)", 115213, 16623, 10080, 0, 0),
        new("Kernon (lv75)", 113432, 16403, 3960, 0, 0),
        new("Storm Winged Naga (lv75)", 137568, -19488, -3552, 0, 0),
        new("Last Lesser Giant Olkuth (lv75)", 187360, 45840, -5856, 0, 0),
        new("Palatanos of Horrific Power (lv75)", 147104, -20560, -3377, 0, 0),
        new("Bloody Empress Decarbia (lv75)", 188983, 13647, -2672, 0, 0),
        new("Death Lord Ipos (lv75)", 154088, -14116, -3736, 0, 0),
        new("Death Lord Shax (lv75)", 179311, -7632, -4896, 0, 0),
        new("Benom (lv75)", 11882, -49216, -3008, 0, 0),
        new("Ocean Flame Ashakiel (lv76)", 123808, 153408, -3671, 0, 0),
        new("Flamestone Giant (lv76)", 144600, -5500, -4100, 0, 0),
        new("Fire of Wrath Shuriel (lv78)", 113102, 16002, 6992, 0, 0),
        new("Last Lesser Giant Glaki (lv78)", 172000, 55000, -5400, 0, 0),
        new("Daimon the White-Eyed (lv78)", 186304, -43744, -3193, 0, 0),
        new("Hestia,  Guardian Deity of the Hot Springs (lv78)", 134672, -115600, -1216, 0, 0),
        new("Antharas (lv79)", 185708, 114298, -8221, 0, 0),
        new("Longhorn Golkonda (lv79)", 116263, 15916, 6992, 0, 0),
        new("Cherub Galaxia (lv79)", 113600, 15104, 9559, 0, 0)
    };
    public IList<TeleportList> RaidBossList80_89 => new List<TeleportList>
    {
        new("Lilith (lv80)", 184410, -10111, -5488, 0, 0),
        new("Anakim (lv80)", 185000, -13000, -5488, 0, 0),
        new("Ketra's Hero Hekaton (lv80)", 148160, -73808, -4919, 0, 0),
        new("Varka's Hero Shadith (lv80)", 115552, -39200, -2480, 0, 0),
        new("Queen Shyeed (lv80)", 80000, -55000, -6000, 0, 0),
        new("Ketra's Commander Tayr (lv84)", 145504, -81664, -6016, 0, 0),
        new("Varka's Commander Mos (lv84)", 109216, -36160, -938, 0, 0),
        new("Valakas (lv85)", 213896, -115436, -1644, 0, 0),
        new("Ember (lv85)", 184542, -106330, -6304, 0, 0),
        new("Uruka (lv86)", 3776, -6768, -32, 0, 0),
        new("Ketra's Chief Brakki (lv87)", 145008, -84992, -6240, 0, 0),
        new("Soul of Fire Nastron (lv87)", 142368, -82512, -6487, 0, 0),
        new("Varka's Chief Horus (lv87)", 105584, -43024, -1728, 0, 0),
        new("Soul of Water Ashutar (lv87)", 105452, -36775, -1050, 0, 0),
        new("Master Anays (lv87)", 113000, -76000, 200, 0, 0),
        new("High Priestess van Halter (lv87)", -16382, -53450, -10432, 0, 0),
        new("Sailren (lv87)", 26528, -8244, -20, 0, 0)
    };
    public IList<TeleportList> RaidBossList90_99 => new List<TeleportList>
    {
        new("Scarlet van Halisha (lv90)", 174238, -89792, -5002, 0, 0)
    };

    public virtual IList<TeleportList> GetPositionList(int hashCode)
    {
        if (RaidBossList20_29.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList20_29;
        }
        else if (RaidBossList30_39.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList30_39;
        }
        else if (RaidBossList40_49.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList40_49;
        }
        else if (RaidBossList50_59.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList50_59;
        }
        else if (RaidBossList60_69.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList60_69;
        }
        else if (RaidBossList70_79.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList70_79;
        }
        else if (RaidBossList80_89.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList80_89;
        }
        else if (RaidBossList90_99.GetHashCodeByValue() == hashCode)
        {
            return RaidBossList90_99;
        }

        return Position;
    }

    public override async Task MenuSelected(Talker talker, int ask, int reply, string fhtml0)
    {
        if (ask == -18)
        {
            switch (reply)
            {
                case 1:
                    await MySelf.ShowQuestInfoList(talker);
                    break;
                case 2:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList20_29);
                    break;
                case 3:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList30_39);
                    break;
                case 4:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList40_49);
                    break;
                case 5:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList50_59);
                    break;
                case 6:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList60_69);
                    break;
                case 7:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList70_79);
                    break;
                case 8:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList80_89);
                    break;
                case 9:
                    await MySelf.ShowTelPosListPage(talker, RaidBossList90_99);
                    break;
            }
        }
        await base.MenuSelected(talker, ask, reply, fhtml0);
    }
}