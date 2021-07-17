using System;
using Network;

namespace Core.Module.Player.Response
{
    public class UserInfo : ServerPacket
    {
        private readonly PlayerInstance _playerInstance;
        private float _moveMultiplier;
        private readonly int _runSpd;
        private readonly int _walkSpd;
        private readonly int _flyRunSpd;
        private readonly int _flyWalkSpd;
        private readonly int _relation;

        public UserInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            //_moveMultiplier = playerInstance.Stat.GetMovementSpeedMultiplier();
            //_runSpd = Convert.ToInt32(Math.Round(playerInstance.Stat.GetRunSpeed() / _moveMultiplier));
            //_walkSpd = Convert.ToInt32(Math.Round(playerInstance.Stat.GetWalkSpeed() / _moveMultiplier));
            _moveMultiplier = 1;
            _runSpd = 115;
            _walkSpd = 80;
            _flyRunSpd = 0;
            _flyWalkSpd = 0;
            _relation = 0;
        }
        
        public override void Write()
        {
            WriteByte(0x04);
            
            WriteInt(-71338);
            WriteInt(258271);
            WriteInt(-3104);
            WriteInt(0);
            WriteInt(_playerInstance.CharacterId);
            
            WriteString(_playerInstance.PlayerAppearance().CharacterName);
            
            WriteInt(_playerInstance.TemplateHandler().GetRaceId());
            WriteInt(_playerInstance.PlayerAppearance().Gender);
            WriteInt(_playerInstance.TemplateHandler().GetClassId());
            WriteInt(1); //level
            WriteLong(0); //exp

            //stats
            WriteInt(_playerInstance.TemplateHandler().GetStr());
            WriteInt(_playerInstance.TemplateHandler().GetDex());
            WriteInt(_playerInstance.TemplateHandler().GetCon());
            WriteInt(_playerInstance.TemplateHandler().GetInt());
            WriteInt(_playerInstance.TemplateHandler().GetWit());
            WriteInt(_playerInstance.TemplateHandler().GetMen());
            
            WriteInt(100); //maxHp
            WriteInt(100); //_playerInstance.Status.GetCurrentHp()
            WriteInt(100); //_playerInstance.Stat.GetMaxMp()
            WriteInt(100); //_playerInstance.Status.GetCurrentMp()
            WriteInt(0); //_playerInstance.Stat.Sp
            WriteInt(0); //_playerInstance.PlayerInventory().GetCurrentLoad()
            WriteInt(100); //_playerInstance.Stat.GetMaxLoad()

            WriteInt(20); // 20 no weapon, 40 weapon equipped
            
            //inventory
            for (byte id = 0; id < 17; id++)
	            WriteInt(0);
           
            for (byte id = 0; id < 17; id++)
	           WriteInt(0);
           
            // c6 new h's
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteInt(0x00); // PAPERDOLL_RHAND
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteInt(0x00); //PAPERDOLL_LRHAND
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);    
            // end of c6 new h's
            
            WriteInt(1); //_playerInstance.Stat.GetPAtk()
            WriteInt(1); //_playerInstance.Stat.GetPAtkSpd()
            WriteInt(1); //_playerInstance.Stat.GetPDef()
            WriteInt(1); //_playerInstance.Stat.GetEvasionRate()
            WriteInt(1); //_playerInstance.Stat.GetAccuracy()
            WriteInt(1); //_playerInstance.Stat.GetCriticalHit()
            WriteInt(1); //_playerInstance.Stat.GetMAtk()
		
            WriteInt(1); //_playerInstance.Stat.GetMAtkSpd()
            WriteInt(1); //_playerInstance.Stat.GetPAtkSpd()
		
            WriteInt(1); //_playerInstance.Stat.GetMDef()
		
            WriteInt(0); // getPvpFlag 0-non-pvp 1-pvp = violett name
            WriteInt(0); //getKarma
		
            WriteInt(_runSpd); // base run speed
            WriteInt(_walkSpd); // base walk speed
            WriteInt(_runSpd); // swim run speed (calculated by getter)
            WriteInt(_walkSpd); // swim walk speed (calculated by getter)
            WriteInt(0);
            WriteInt(0);
            WriteInt(_flyRunSpd);
            WriteInt(_flyWalkSpd);
            WriteDouble(1.09); // _playerInstance.Stat.GetMovementSpeedMultiplier() run speed multiplier
            
            //this is very mandatory option. To avoid player paralyzed when trying to physical attack
            WriteDouble(1.21); //_playerInstance.Stat.GetAttackSpeedMultiplier() attack speed multiplier 

            WriteDouble(7); //getCollisionRadius
            WriteDouble(24); //getCollisionHeight
            
            WriteInt(_playerInstance.PlayerAppearance().HairStyle);
            WriteInt(_playerInstance.PlayerAppearance().HairColor);
            WriteInt(_playerInstance.PlayerAppearance().Face);
            WriteInt(0); // _playerInstance.isGM() builder level
            
            WriteString(""); //_playerInstance.Stat.Title
            
            WriteInt(0);//_player.ClanId
            WriteInt(0);//_player.ClanCrestId
            WriteInt(0);//_player.AllianceId
            WriteInt(0);//_player.AllianceCrestId

            WriteInt(_relation);
            
            WriteByte(0); // mount type
            WriteByte(0); //getPrivateStoreType
            WriteByte(0); //hasDwarvenCraft
            
            WriteInt(0); //_playerInstance.Stat.PkKills 
            WriteInt(0); //_playerInstance.Stat.PvpKills
            
            WriteShort(0);//_player.Cubics.Count
            
            WriteByte(0); //1-isInPartyMatchRoom
            
            WriteInt(0); //_player.AbnormalBitMask
            
            WriteByte(0x00);
            
            WriteInt(0);//_player.ClanPrivs
            
            WriteShort(0); // c2 recommendations remaining
            WriteShort(0); // c2 recommendations received
            WriteInt(0x00); // _player.getMountNpcId() > 0 ? _player.getMountNpcId() + 1000000 : 0
            WriteShort(80); //_player.getInventoryLimit()
            
            WriteInt(_playerInstance.TemplateHandler().GetClassId());
            WriteInt(0x00); // special effects? circles around player...
            WriteInt(100); //_playerInstance.Stat.GetMaxCp()
            WriteInt(100); //_playerInstance.Status.GetCurrentCp()
            WriteByte(0); //_player.isMounted() ? 0 : _player.getEnchantEffect()
		
            WriteByte(0x00); // team circle around feet 1= Blue, 2 = red
		
            WriteInt(0); //_player.getClanCrestLargeId()
            WriteByte(0); // _player.isNoble() 0x01: symbol on char menu ctrl+I
            WriteByte(0); // 0x01: Hero Aura
		
            WriteByte(0); // _player.isFishing() Fishing Mode
            WriteInt(0); // fishing x
            WriteInt(0); // fishing y
            WriteInt(0); // fishing z
            WriteInt(0); //_playerInstance.PlayerAppearance().NameColor
		
            // new c5
            WriteByte(0x01); //_playerInstance.Movement().IsRunning() changes the Speed display on Status Window
		
            WriteInt(0); // _player.getPledgeClass() changes the text above CP on Status Window
            WriteInt(0); //_player.getPledgeType()
		
            WriteInt(16777079); //_playerInstance.PlayerAppearance().TitleColor
		
            WriteInt(0x00); //_player.isCursedWeaponEquiped()
        }
    }
}