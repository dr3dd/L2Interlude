using System;
using System.Threading.Tasks;
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
        private readonly double _moveMultiplier;
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
            _runSpd = Convert.ToInt32(Math.Round(_playerInstance.PlayerCombat().GetCharacterSpeed() / _moveMultiplier));
            _walkSpd = Convert.ToInt32(Math.Round(_playerInstance.PlayerCombat().GetGroundLowSpeed() / _moveMultiplier));
            _flyRunSpd = 0;
            _flyWalkSpd = 0;
            
            _playerAppearance = _playerInstance.PlayerAppearance();
            _playerInventory = _playerInstance.PlayerInventory();
        }
        public override async Task WriteAsync()
        {
            await WriteByteAsync(0x03);
            await WriteIntAsync(_x);
            await WriteIntAsync(_y);
            await WriteIntAsync(_z);
            await WriteIntAsync(0); //getBoat
            await WriteIntAsync(_playerInstance.ObjectId);
            await WriteStringAsync(_playerAppearance.CharacterName);
            await WriteIntAsync(_template.GetRaceId());
            await WriteIntAsync(_playerAppearance.Gender);
            await WriteIntAsync(_template.GetClassId());
            
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

            await WriteIntAsync(hairAll.ItemId); //Hair All
            await WriteIntAsync(head.ItemId); //Head
            await WriteIntAsync(rightHand.ItemId); //Right hand
            await WriteIntAsync(leftHand.ItemId); //Left hand
            await WriteIntAsync(gloves.ItemId); //Gloves
            await WriteIntAsync(chest.ItemId); //Chest
            await WriteIntAsync(legs.ItemId); //Legs
            await WriteIntAsync(feet.ItemId); //Feet
            await WriteIntAsync(back.ItemId); //Back
            await WriteIntAsync(bothHand.ItemId); //Left Right Hand
            await WriteIntAsync(hair.ItemId); //Hair
            await WriteIntAsync(face.ItemId); //Face
            
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteIntAsync(0);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteIntAsync(0);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            await WriteShortAsync(0x00);
            
            await WriteIntAsync(0); // getPvpFlag 0-non-pvp 1-pvp = violett name
            await WriteIntAsync(0); //getKarma
            
            await WriteIntAsync(_mAtkSpd);
            await WriteIntAsync(_pAtkSpd);
            
            await WriteIntAsync(0); // getPvpFlag 0-non-pvp 1-pvp = violett name
            await WriteIntAsync(0); //getKarma
            
            await WriteIntAsync(_runSpd); // base run speed
            await WriteIntAsync(_walkSpd); // base walk speed
            await WriteIntAsync(_runSpd); // swim run speed (calculated by getter)
            await WriteIntAsync(_walkSpd); // swim walk speed (calculated by getter)
            await WriteIntAsync(_flyRunSpd);
            await WriteIntAsync(_flyWalkSpd);
            await WriteIntAsync(_flyRunSpd);
            await WriteIntAsync(_flyWalkSpd);
            await WriteDoubleAsync(_moveMultiplier); // run speed multiplier
            
            //this is very mandatory option. To avoid player paralyzed when trying to physical attack
            await WriteDoubleAsync(_attackSpeedMultiplier); // attack speed multiplier
            
            await WriteDoubleAsync(7); //getCollisionRadius
            await WriteDoubleAsync(24); //getCollisionHeight
			
            await WriteIntAsync(_playerInstance.PlayerAppearance().HairStyle);
            await WriteIntAsync(_playerInstance.PlayerAppearance().HairColor);
            await WriteIntAsync(_playerInstance.PlayerAppearance().Face);
			
            await WriteStringAsync(""); //_playerInstance.Stat.Title
	
            await WriteIntAsync(0);//_player.ClanId
            await WriteIntAsync(0);//_player.ClanCrestId
            await WriteIntAsync(0);//_player.AllianceId
            await WriteIntAsync(0);//_player.AllianceCrestId
			// In UserInfo leader rights and siege flags, but here found nothing??
			// Therefore RelationChanged packet with that info is required
			await WriteIntAsync(0);
			
			await WriteByteAsync(1); // standing = 1 sitting = 0
			await WriteByteAsync(1); // running = 1 walking = 0
			await WriteByteAsync(0); //isInCombat
			await WriteByteAsync(0); //IsAlikeDead
			
			await WriteByteAsync(0x00); // if the charinfo is written means receiver can see the char

			await WriteByteAsync(0); // 1 on strider 2 on wyvern 0 no mount
			await WriteByteAsync(0); // 1 - sellshop
			
			await WriteShortAsync(0);

			await WriteByteAsync(0);
			
			await WriteIntAsync(0); //_player.getAbnormalEffect()
			
			await WriteByteAsync(0); //getRecomLeft
			await WriteShortAsync(0); //getRecomHave  Blue value for name (0 = white, 255 = pure blue)
			await WriteIntAsync(_template.GetClassId());
			
			await WriteIntAsync(_playerInstance.PlayerStatus().GetMaxCp()); //_playerInstance.Stat.GetMaxCp()
			await WriteIntAsync(_playerInstance.PlayerStatus().CurrentCp); //_playerInstance.Status.GetCurrentCp()
			await WriteByteAsync(0);
			
			await WriteByteAsync(0x00); // team circle around feet 1= Blue, 2 = red
			
			await WriteIntAsync(0);
			await WriteByteAsync(0); // isNoble Symbol on char menu ctrl+I
			await WriteByteAsync(0); // isHero  Hero Aura
			
			await WriteByteAsync(0); // 0x01: Fishing Mode (Cant be undone by setting back to 0)
			await WriteIntAsync(0);
			await WriteIntAsync(0);
			await WriteIntAsync(0);
			
			await WriteIntAsync(0xFFFFFF); //_playerInstance.PlayerAppearance().NameColor /** The hexadecimal Color of players name (white is 0xFFFFFF) */
			
			await WriteIntAsync(_heading);
			
			await WriteIntAsync(0); //getPledgeClass
			await WriteIntAsync(0); //_player.getPledgeType())
			
			await WriteIntAsync(0); //_playerInstance.PlayerAppearance().TitleColor
			
			await WriteIntAsync(0x00); //isCursedWeaponEquiped
        }
    }
}