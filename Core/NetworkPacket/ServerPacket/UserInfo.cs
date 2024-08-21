using System;
using Core.Module.CharacterData;
using Core.Module.CharacterData.Template;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket;

public class UserInfo : Network.ServerPacket
{
	private readonly PlayerInstance _playerInstance;
	private readonly double _moveMultiplier;
	private readonly int _highSpeed;
	private readonly int _lowSpeed;
	private readonly int _flyRunSpd;
	private readonly int _flyWalkSpd;
	private readonly int _relation;
	private readonly int _level;
	private readonly bool _isHighSpeed;
	private readonly ITemplateHandler _template;
	private readonly PlayerCharacterInfo _characterInfo;
	private readonly Location _location;
	private readonly PlayerAppearance _playerAppearance;
	private readonly PlayerInventory _playerInventory;

	public UserInfo(PlayerInstance playerInstance)
	{
		_playerInstance = playerInstance;
		_characterInfo = _playerInstance.PlayerCharacterInfo();
		_level = _playerInstance.PlayerStatus().Level;
		_template = _playerInstance.TemplateHandler();
		_location = _playerInstance.Location;
		_playerAppearance = _playerInstance.PlayerAppearance();
		_playerInventory = _playerInstance.PlayerInventory();
		_moveMultiplier = _playerInstance.PlayerCombat().GetMovementSpeedMultiplier();
		_highSpeed = (int) (Math.Round(_playerInstance.PlayerCombat().GetCharacterSpeed() / _moveMultiplier));
		_lowSpeed = (int) (Math.Round(_playerInstance.PlayerCombat().GetGroundLowSpeed() / _moveMultiplier));
		_flyRunSpd = 0;
		_flyWalkSpd = 0;
		_relation = 0;
		var movementStatus = _playerInstance.CharacterMovement().CharacterMovementStatus();
		_isHighSpeed = movementStatus.IsGroundHigh();
	}

	public override void Write()
	{
		WriteByte(0x04);

		WriteInt(_playerInstance.GetX());
		WriteInt(_playerInstance.GetY());
		WriteInt(_playerInstance.GetZ());
		WriteInt(0); //heading
		WriteInt(_playerInstance.ObjectId);

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

		WriteInt(_playerInstance.CharacterBaseStatus().GetMaxHp()); //maxHp
		WriteInt(_playerInstance.CharacterStatus().CurrentHp); //_playerInstance.Status.GetCurrentHp()
		WriteInt(_playerInstance.CharacterBaseStatus().GetMaxMp()); //_playerInstance.Stat.GetMaxMp()
		WriteInt(_playerInstance.CharacterStatus().CurrentMp); //_playerInstance.Status.GetCurrentMp()
		WriteInt(_characterInfo.Sp); //_playerInstance.Stat.Sp
		WriteInt(_playerInstance.CharacterBaseStatus().GetCurrentLoad()); //_playerInstance.PlayerInventory().GetCurrentLoad()
		WriteInt(_playerInstance.CharacterBaseStatus().GetMaxLoad()); //_playerInstance.Stat.GetMaxLoad()

		var underWear = _playerInventory.GetItemInstance(_characterInfo.StUnderwear);
		var leftEar = _playerInventory.GetItemInstance(_characterInfo.StLeftEar);
		var rightEar = _playerInventory.GetItemInstance(_characterInfo.StRightEar);
		var neck = _playerInventory.GetItemInstance(_characterInfo.StNeck);
		var leftFinger = _playerInventory.GetItemInstance(_characterInfo.StLeftFinger);
		var rightFinger = _playerInventory.GetItemInstance(_characterInfo.StRightFinger);
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

		WriteInt(40); // 20 no weapon, 40 weapon equipped

		//Object ids of items
		WriteInt(underWear.ObjectId); //Under
		WriteInt(leftEar.ObjectId); //Left Earning
		WriteInt(rightEar.ObjectId); //Right Earning
		WriteInt(neck.ObjectId); //Necklace
		WriteInt(leftFinger.ObjectId); //Left Finger
		WriteInt(rightFinger.ObjectId); //Right Finger
		WriteInt(head.ObjectId); //Head
		WriteInt(rightHand.ObjectId); //Right hand
		WriteInt(leftHand.ObjectId); //Left hand
		WriteInt(gloves.ObjectId); //Gloves
		WriteInt(chest.ObjectId); //Chest
		WriteInt(legs.ObjectId); //Legs
		WriteInt(feet.ObjectId); //Feet
		WriteInt(back.ObjectId); //Back
		WriteInt(bothHand.ObjectId); //Left Right Hand
		WriteInt(face.ObjectId); //Face 
		WriteInt(hair.ObjectId); //Hair

		//item ids
		WriteInt(underWear.ItemId); //Under
		WriteInt(leftEar.ItemId); //Left Earning
		WriteInt(rightEar.ItemId); //Right Earning
		WriteInt(neck.ItemId); //Necklace
		WriteInt(leftFinger.ItemId); //Left Finger
		WriteInt(rightFinger.ItemId); //Right Finger
		WriteInt(head.ItemId); //Head
		WriteInt(rightHand.ItemId); //Right hand
		WriteInt(leftHand.ItemId); //Left hand
		WriteInt(gloves.ItemId); //Gloves
		WriteInt(chest.ItemId); //Chest
		WriteInt(legs.ItemId); //Legs
		WriteInt(feet.ItemId); //Feet
		WriteInt(back.ItemId); //Back
		WriteInt(bothHand.ItemId); //Left Right Hand
		WriteInt(face.ItemId); //Face 
		WriteInt(hair.ItemId); //Hair

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

		WriteInt(_highSpeed); // base run speed
		WriteInt(_lowSpeed); // base walk speed
		WriteInt(_highSpeed); // swim run speed (calculated by getter)
		WriteInt(_lowSpeed); // swim walk speed (calculated by getter)
		WriteInt(0);
		WriteInt(0);
		WriteInt(_flyRunSpd);
		WriteInt(_flyWalkSpd);
		WriteDouble(_moveMultiplier); // _playerInstance.Stat.GetMovementSpeedMultiplier() run speed multiplier

		//this is very mandatory option. To avoid player paralyzed when trying to physical attack
		WriteDouble(1.21); //_playerInstance.Stat.GetAttackSpeedMultiplier() attack speed multiplier 

		WriteDouble(_playerInstance.CharacterCombat().GetCollisionRadius()); //getCollisionRadius
		WriteDouble(_playerInstance.CharacterCombat().GetCollisionHeight()); //getCollisionHeight

		WriteInt(_playerAppearance.HairStyle);
		WriteInt(_playerAppearance.HairColor);
		WriteInt(_playerAppearance.Face);
		WriteInt(_playerInstance.IsGM ? 0x01 : 0x00); // _playerInstance.isGM() builder level

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
		WriteInt(_playerInstance.CharacterBaseStatus().GetMaxCp()); //_playerInstance.Stat.GetMaxCp()
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
		WriteByte(_isHighSpeed ? 0x01 : 0x00); //_playerInstance.Movement().IsRunning() changes the Speed display on Status Window

		WriteInt(0); // _player.getPledgeClass() changes the text above CP on Status Window
		WriteInt(0); //_player.getPledgeType()

		WriteInt(0); //_playerInstance.PlayerAppearance().TitleColor

		WriteInt(0x00); //_player.isCursedWeaponEquiped()
	}
}