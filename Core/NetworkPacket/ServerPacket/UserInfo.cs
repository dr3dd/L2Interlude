using System;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
    public class UserInfo : Network.ServerPacket
    {
        private readonly PlayerInstance _playerInstance;
        private float _moveMultiplier;
        private readonly int _runSpd;
        private readonly int _walkSpd;
        private readonly int _flyRunSpd;
        private readonly int _flyWalkSpd;
        private readonly int _relation;
        private readonly int _level;
        private readonly ITemplateHandler _template;
        private readonly PlayerCharacterInfo _characterInfo;
        private readonly Location _location;
        private readonly PlayerAppearance _playerAppearance;

        public UserInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _characterInfo = _playerInstance.PlayerCharacterInfo();
            _level = _playerInstance.PlayerStatus().Level;
            _template = _playerInstance.TemplateHandler();
            _location = _playerInstance.Location;
            _playerAppearance = _playerInstance.PlayerAppearance();
            //_moveMultiplier = playerInstance.Stat.GetMovementSpeedMultiplier();
            //_runSpd = Convert.ToInt32(Math.Round(playerInstance.Stat.GetRunSpeed() / _moveMultiplier));
            //_walkSpd = Convert.ToInt32(Math.Round(playerInstance.Stat.GetWalkSpeed() / _moveMultiplier));
            _moveMultiplier = _playerInstance.PlayerCombat().GetMovementSpeedMultiplier();
            _runSpd = (int) (Math.Round(_playerInstance.PlayerCombat().GetGroundHighSpeed() / _moveMultiplier));
            _walkSpd = (int) (Math.Round(_playerInstance.PlayerCombat().GetGroundLowSpeed() / _moveMultiplier));
            _flyRunSpd = 0;
            _flyWalkSpd = 0;
            _relation = 0;
        }
        
        public override void Write()
        {
            WriteByte(0x04);
            
            WriteInt(_location.GetX());
            WriteInt(_location.GetY());
            WriteInt(_location.GetZ());
            WriteInt(0); //heading
            WriteInt(_characterInfo.CharacterId);
            
            WriteString(_playerAppearance.CharacterName);
            
            WriteInt(_template.GetRaceId());
            WriteInt(_playerAppearance.Gender);
            WriteInt(_template.GetClassId());
            WriteInt(_level); //level
            WriteLong(_characterInfo.Exp); //exp

            //stats
            WriteInt(_template.GetStr());
            WriteInt(_template.GetDex());
            WriteInt(_template.GetCon());
            WriteInt(_template.GetInt());
            WriteInt(_template.GetWit());
            WriteInt(_template.GetMen());
            
            WriteInt(_playerInstance.PlayerStatus().GetMaxHp()); //maxHp
            WriteInt(_playerInstance.PlayerStatus().CurrentHp); //_playerInstance.Status.GetCurrentHp()
            WriteInt(_playerInstance.PlayerStatus().GetMaxMp()); //_playerInstance.Stat.GetMaxMp()
            WriteInt(_playerInstance.PlayerStatus().CurrentMp); //_playerInstance.Status.GetCurrentMp()
            WriteInt(_characterInfo.Sp); //_playerInstance.Stat.Sp
            WriteInt(0); //_playerInstance.PlayerInventory().GetCurrentLoad()
            WriteInt(100); //_playerInstance.Stat.GetMaxLoad()

            WriteInt(40); // 20 no weapon, 40 weapon equipped
            
            //inventory
            //for (byte id = 0; id < 17; id++)
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(2369);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
	            WriteInt(0);
           
            //for (byte id = 0; id < 17; id++)
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(2369);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
	           WriteInt(0);
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
            
            WriteInt(_playerInstance.PlayerCombat().GetPhysicalAttack()); //_playerInstance.Stat.GetPAtk()
            WriteInt(_playerInstance.PlayerCombat().GetPhysicalAttackSpeed()); //_playerInstance.Stat.GetPAtkSpd()
            WriteInt(_playerInstance.PlayerCombat().GetPhysicalDefence()); //_playerInstance.Stat.GetPDef()
            WriteInt(_playerInstance.PlayerCombat().GetEvasion()); //_playerInstance.Stat.GetEvasionRate()
            WriteInt(_playerInstance.PlayerCombat().GetAccuracy()); //_playerInstance.Stat.GetAccuracy()
            WriteInt(_playerInstance.PlayerCombat().GetCriticalRate()); //_playerInstance.Stat.GetCriticalHit()
            WriteInt(_playerInstance.PlayerCombat().GetMagicalAttack()); //_playerInstance.Stat.GetMAtk()
		
            WriteInt(_playerInstance.PlayerCombat().GetCastSpeed()); //_playerInstance.Stat.GetMAtkSpd()
            WriteInt(_playerInstance.PlayerCombat().GetPhysicalAttackSpeed()); //_playerInstance.Stat.GetPAtkSpd()
		
            WriteInt(_playerInstance.PlayerCombat().GetMagicalDefence()); //_playerInstance.Stat.GetMDef()
		
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
            WriteDouble(_moveMultiplier); // _playerInstance.Stat.GetMovementSpeedMultiplier() run speed multiplier
            
            //this is very mandatory option. To avoid player paralyzed when trying to physical attack
            WriteDouble(1.21); //_playerInstance.Stat.GetAttackSpeedMultiplier() attack speed multiplier 

            WriteDouble(7); //getCollisionRadius
            WriteDouble(24); //getCollisionHeight
            
            WriteInt(_playerAppearance.HairStyle);
            WriteInt(_playerAppearance.HairColor);
            WriteInt(_playerAppearance.Face);
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
            
            WriteInt(_template.GetClassId());
            WriteInt(0x00); // special effects? circles around player...
            WriteInt(_playerInstance.PlayerStatus().GetMaxCp()); //_playerInstance.Stat.GetMaxCp()
            WriteInt(_playerInstance.PlayerStatus().CurrentCp); //_playerInstance.Status.GetCurrentCp()
            WriteByte(0); //_player.isMounted() ? 0 : _player.getEnchantEffect()
		
            WriteByte(0x00); // team circle around feet 1= Blue, 2 = red
		
            WriteInt(0); //_player.getClanCrestLargeId()
            WriteByte(0); // _player.isNoble() 0x01: symbol on char menu ctrl+I
            WriteByte(0); // 0x01: Hero Aura
		
            WriteByte(0); // _player.isFishing() Fishing Mode
            WriteInt(0); // fishing x
            WriteInt(0); // fishing y
            WriteInt(0); // fishing z
            WriteInt(0xFFFFFF); //_playerInstance.PlayerAppearance().NameColor /** The hexadecimal Color of players name (white is 0xFFFFFF) */
		
            // new c5
            WriteByte(0x01); //_playerInstance.Movement().IsRunning() changes the Speed display on Status Window
		
            WriteInt(0); // _player.getPledgeClass() changes the text above CP on Status Window
            WriteInt(0); //_player.getPledgeType()
		
            WriteInt(0); //_playerInstance.PlayerAppearance().TitleColor
		
            WriteInt(0x00); //_player.isCursedWeaponEquiped()
        }
    }
}