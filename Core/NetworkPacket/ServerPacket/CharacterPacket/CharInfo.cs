using System;
using Core.Module.CharacterData.Template;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket.CharacterPacket
{
    internal class CharInfo : Network.ServerPacket
    {
        private readonly PlayerInstance _playerInstance;
        private readonly int _x;
        private readonly int _y;
        private readonly int _z;
        private readonly int _heading;
        private readonly int _mAtkSpd;
        private readonly int _pAtkSpd;
        private readonly int _runSpd;
        private readonly int _walkSpd;
        private readonly int _flyRunSpd;
        private readonly int _flyWalkSpd;
        private readonly float _moveMultiplier;
        private readonly float _attackSpeedMultiplier;
        private readonly ITemplateHandler _template;
        private readonly PlayerAppearance _playerAppearance;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerCharacterInfo _characterInfo;

        public CharInfo(PlayerInstance playerInstance)
        {
            _playerInstance = playerInstance;
            _template = _playerInstance.TemplateHandler();
            _characterInfo = _playerInstance.PlayerCharacterInfo();
            
            _x = playerInstance.GetX();
            _y = playerInstance.GetY();
            _z = playerInstance.GetZ();
            _heading = playerInstance.Heading;
            _mAtkSpd = playerInstance.PlayerCombat().GetCastSpeed();
            _pAtkSpd = playerInstance.PlayerCombat().GetPhysicalAttackSpeed();
            
            _attackSpeedMultiplier = playerInstance.PlayerCombat().GetAttackSpeedMultiplier();
            _moveMultiplier = _playerInstance.PlayerCombat().GetMovementSpeedMultiplier();
            _runSpd = Convert.ToInt32(Math.Round(_playerInstance.PlayerCombat().GetGroundHighSpeed() / _moveMultiplier));
            _walkSpd = Convert.ToInt32(Math.Round(_playerInstance.PlayerCombat().GetGroundLowSpeed() / _moveMultiplier));
            _flyRunSpd = 0;
            _flyWalkSpd = 0;
            
            _playerAppearance = _playerInstance.PlayerAppearance();
            _playerInventory = _playerInstance.PlayerInventory();
        }
        public override void Write()
        {
            WriteByte(0x03);
            WriteInt(_x);
            WriteInt(_y);
            WriteInt(_z);
            WriteInt(0); //getBoat
            WriteInt(_playerInstance.ObjectId);
            WriteString(_playerAppearance.CharacterName);
            WriteInt(_template.GetRaceId());
            WriteInt(_playerAppearance.Gender);
            WriteInt(_template.GetClassId());
            
            var head = _playerInventory.GetItemInstance(_characterInfo.StHead);
            var rightHand = _playerInventory.GetItemInstance(_characterInfo.StRightHand);
            var leftHand = _playerInventory.GetItemInstance(_characterInfo.StLeftHand);
            var gloves = _playerInventory.GetItemInstance(_characterInfo.StGloves);
            var chest = _playerInventory.GetItemInstance(_characterInfo.StChest);
            var legs = _playerInventory.GetItemInstance(_characterInfo.StLegs);
            var feet = _playerInventory.GetItemInstance(_characterInfo.StFeet);
            var back = _playerInventory.GetItemInstance(_characterInfo.StBack);
            var bothHand = _playerInventory.GetItemInstance(_characterInfo.StBothHand);
            var face = _playerInventory.GetItemInstance(_characterInfo.StFace);
            var hair = _playerInventory.GetItemInstance(_characterInfo.StHair);
            var hairAll = _playerInventory.GetItemInstance(_characterInfo.StHairAll);

            WriteInt(hairAll.ItemId); //Hair All
            WriteInt(head.ItemId); //Head
            WriteInt(rightHand.ItemId); //Right hand
            WriteInt(leftHand.ItemId); //Left hand
            WriteInt(gloves.ItemId); //Gloves
            WriteInt(chest.ItemId); //Chest
            WriteInt(legs.ItemId); //Legs
            WriteInt(feet.ItemId); //Feet
            WriteInt(back.ItemId); //Back
            WriteInt(bothHand.ItemId); //Left Right Hand
            WriteInt(hair.ItemId); //Hair
            WriteInt(face.ItemId); //Face
            
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteInt(0);
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
            WriteInt(0);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            WriteShort(0x00);
            
            WriteInt(0); // getPvpFlag 0-non-pvp 1-pvp = violett name
            WriteInt(0); //getKarma
            
            WriteInt(_mAtkSpd);
            WriteInt(_pAtkSpd);
            
            WriteInt(0); // getPvpFlag 0-non-pvp 1-pvp = violett name
            WriteInt(0); //getKarma
            
            WriteInt(_runSpd); // base run speed
            WriteInt(_walkSpd); // base walk speed
            WriteInt(_runSpd); // swim run speed (calculated by getter)
            WriteInt(_walkSpd); // swim walk speed (calculated by getter)
            WriteInt(_flyRunSpd);
            WriteInt(_flyWalkSpd);
            WriteInt(_flyRunSpd);
            WriteInt(_flyWalkSpd);
            WriteDouble(_moveMultiplier); // run speed multiplier
            
            //this is very mandatory option. To avoid player paralyzed when trying to physical attack
            WriteDouble(_attackSpeedMultiplier); // attack speed multiplier
            
            WriteDouble(7); //getCollisionRadius
            WriteDouble(24); //getCollisionHeight
			
            WriteInt(_playerInstance.PlayerAppearance().HairStyle);
            WriteInt(_playerInstance.PlayerAppearance().HairColor);
            WriteInt(_playerInstance.PlayerAppearance().Face);
			
            WriteString(""); //_playerInstance.Stat.Title
	
            WriteInt(0);//_player.ClanId
            WriteInt(0);//_player.ClanCrestId
            WriteInt(0);//_player.AllianceId
            WriteInt(0);//_player.AllianceCrestId
			// In UserInfo leader rights and siege flags, but here found nothing??
			// Therefore RelationChanged packet with that info is required
			WriteInt(0);
			
			WriteByte(1); // standing = 1 sitting = 0
			WriteByte(1); // running = 1 walking = 0
			WriteByte(0); //isInCombat
			WriteByte(0); //IsAlikeDead
			
			WriteByte(0x00); // if the charinfo is written means receiver can see the char

			WriteByte(0); // 1 on strider 2 on wyvern 0 no mount
			WriteByte(0); // 1 - sellshop
			
			WriteShort(0);

			WriteByte(0);
			
			WriteInt(0); //_player.getAbnormalEffect()
			
			WriteByte(0); //getRecomLeft
			WriteShort(0); //getRecomHave  Blue value for name (0 = white, 255 = pure blue)
			WriteInt(_template.GetClassId());
			
			WriteInt(_playerInstance.PlayerStatus().GetMaxCp()); //_playerInstance.Stat.GetMaxCp()
			WriteInt(_playerInstance.PlayerStatus().CurrentCp); //_playerInstance.Status.GetCurrentCp()
			WriteByte(0);
			
			WriteByte(0x00); // team circle around feet 1= Blue, 2 = red
			
			WriteInt(0);
			WriteByte(0); // isNoble Symbol on char menu ctrl+I
			WriteByte(0); // isHero  Hero Aura
			
			WriteByte(0); // 0x01: Fishing Mode (Cant be undone by setting back to 0)
			WriteInt(0);
			WriteInt(0);
			WriteInt(0);
			
			WriteInt(0xFFFFFF); //_playerInstance.PlayerAppearance().NameColor /** The hexadecimal Color of players name (white is 0xFFFFFF) */
			
			WriteInt(_heading);
			
			WriteInt(0); //getPledgeClass
			WriteInt(0); //_player.getPledgeType())
			
			WriteInt(0); //_playerInstance.PlayerAppearance().TitleColor
			
			WriteInt(0x00); //isCursedWeaponEquiped
        }
    }
}