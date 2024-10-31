using System;
using System.Threading.Tasks;
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

	public override async Task WriteAsync()
	{
		await WriteByteAsync(0x04);

		await WriteIntAsync(_playerInstance.GetX());
		await WriteIntAsync(_playerInstance.GetY());
		await WriteIntAsync(_playerInstance.GetZ());
		await WriteIntAsync(0); //heading
		await WriteIntAsync(_playerInstance.ObjectId);

		await WriteStringAsync(_playerAppearance.CharacterName);

		await WriteIntAsync(_template.GetRaceId());
		await WriteIntAsync(_playerAppearance.Gender);
		await WriteIntAsync(_template.GetClassId());
		await WriteIntAsync(_level); //level
		await WriteLongAsync(_characterInfo.Exp); //exp

		//stats
		await WriteIntAsync(_template.GetStr());
		await WriteIntAsync(_template.GetDex());
		await WriteIntAsync(_template.GetCon());
		await WriteIntAsync(_template.GetInt());
		await WriteIntAsync(_template.GetWit());
		await WriteIntAsync(_template.GetMen());

		await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxHp()); //maxHp
		await WriteIntAsync(_playerInstance.CharacterStatus().CurrentHp); //_playerInstance.Status.GetCurrentHp()
		await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxMp()); //_playerInstance.Stat.GetMaxMp()
		await WriteIntAsync(_playerInstance.CharacterStatus().CurrentMp); //_playerInstance.Status.GetCurrentMp()
		await WriteIntAsync(_characterInfo.Sp); //_playerInstance.Stat.Sp
		await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetCurrentLoad()); //_playerInstance.PlayerInventory().GetCurrentLoad()
		await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxLoad()); //_playerInstance.Stat.GetMaxLoad()

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

		await WriteIntAsync(40); // 20 no weapon, 40 weapon equipped

		//Object ids of items
		await WriteIntAsync(underWear.ObjectId); //Under
		await WriteIntAsync(leftEar.ObjectId); //Left Earning
		await WriteIntAsync(rightEar.ObjectId); //Right Earning
		await WriteIntAsync(neck.ObjectId); //Necklace
		await WriteIntAsync(leftFinger.ObjectId); //Left Finger
		await WriteIntAsync(rightFinger.ObjectId); //Right Finger
		await WriteIntAsync(head.ObjectId); //Head
		await WriteIntAsync(rightHand.ObjectId); //Right hand
		await WriteIntAsync(leftHand.ObjectId); //Left hand
		await WriteIntAsync(gloves.ObjectId); //Gloves
		await WriteIntAsync(chest.ObjectId); //Chest
		await WriteIntAsync(legs.ObjectId); //Legs
		await WriteIntAsync(feet.ObjectId); //Feet
		await WriteIntAsync(back.ObjectId); //Back
		await WriteIntAsync(bothHand.ObjectId); //Left Right Hand
		await WriteIntAsync(face.ObjectId); //Face 
		await WriteIntAsync(hair.ObjectId); //Hair

		//item ids
		await WriteIntAsync(underWear.ItemId); //Under
		await WriteIntAsync(leftEar.ItemId); //Left Earning
		await WriteIntAsync(rightEar.ItemId); //Right Earning
		await WriteIntAsync(neck.ItemId); //Necklace
		await WriteIntAsync(leftFinger.ItemId); //Left Finger
		await WriteIntAsync(rightFinger.ItemId); //Right Finger
		await WriteIntAsync(head.ItemId); //Head
		await WriteIntAsync(rightHand.ItemId); //Right hand
		await WriteIntAsync(leftHand.ItemId); //Left hand
		await WriteIntAsync(gloves.ItemId); //Gloves
		await WriteIntAsync(chest.ItemId); //Chest
		await WriteIntAsync(legs.ItemId); //Legs
		await WriteIntAsync(feet.ItemId); //Feet
		await WriteIntAsync(back.ItemId); //Back
		await WriteIntAsync(bothHand.ItemId); //Left Right Hand
		await WriteIntAsync(face.ItemId); //Face 
		await WriteIntAsync(hair.ItemId); //Hair

		// c6 new h's
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
		await WriteShortAsync(0x00);
		await WriteShortAsync(0x00);
		await WriteIntAsync(0x00); // PAPERDOLL_RHAND
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
		await WriteIntAsync(0x00); //PAPERDOLL_LRHAND
		await WriteShortAsync(0x00);
		await WriteShortAsync(0x00);
		await WriteShortAsync(0x00);
		await WriteShortAsync(0x00);    
		// end of c6 new h's

		await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalAttack()); //_playerInstance.Stat.GetPAtk()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalAttackSpeed()); //_playerInstance.Stat.GetPAtkSpd()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalDefence()); //_playerInstance.Stat.GetPDef()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetEvasion()); //_playerInstance.Stat.GetEvasionRate()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetAccuracy()); //_playerInstance.Stat.GetAccuracy()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetCriticalRate()); //_playerInstance.Stat.GetCriticalHit()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetMagicalAttack()); //_playerInstance.Stat.GetMAtk()

		await WriteIntAsync(_playerInstance.PlayerCombat().GetCastSpeed()); //_playerInstance.Stat.GetMAtkSpd()
		await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalAttackSpeed()); //_playerInstance.Stat.GetPAtkSpd()

		await WriteIntAsync(_playerInstance.PlayerCombat().GetMagicalDefence()); //_playerInstance.Stat.GetMDef()

		await WriteIntAsync(0); // getPvpFlag 0-non-pvp 1-pvp = violett name
		await WriteIntAsync(0); //getKarma

		await WriteIntAsync(_highSpeed); // base run speed
		await WriteIntAsync(_lowSpeed); // base walk speed
		await WriteIntAsync(_highSpeed); // swim run speed (calculated by getter)
		await WriteIntAsync(_lowSpeed); // swim walk speed (calculated by getter)
		await WriteIntAsync(0);
		await WriteIntAsync(0);
		await WriteIntAsync(_flyRunSpd);
		await WriteIntAsync(_flyWalkSpd);
		await WriteDoubleAsync(_moveMultiplier); // _playerInstance.Stat.GetMovementSpeedMultiplier() run speed multiplier

		//this is very mandatory option. To avoid player paralyzed when trying to physical attack
		await WriteDoubleAsync(1.21); //_playerInstance.Stat.GetAttackSpeedMultiplier() attack speed multiplier 

		await WriteDoubleAsync(_playerInstance.CharacterCombat().GetCollisionRadius()); //getCollisionRadius
		await WriteDoubleAsync(_playerInstance.CharacterCombat().GetCollisionHeight()); //getCollisionHeight

		await WriteIntAsync(_playerAppearance.HairStyle);
		await WriteIntAsync(_playerAppearance.HairColor);
		await WriteIntAsync(_playerAppearance.Face);
		await WriteIntAsync(_playerInstance.IsGM ? 0x01 : 0x00); // _playerInstance.isGM() builder level

		await WriteStringAsync(""); //_playerInstance.Stat.Title

		await WriteIntAsync(0);//_player.ClanId
		await WriteIntAsync(0);//_player.ClanCrestId
		await WriteIntAsync(0);//_player.AllianceId
		await WriteIntAsync(0);//_player.AllianceCrestId

		await WriteIntAsync(_relation);

		await WriteByteAsync(0); // mount type
		await WriteByteAsync(0); //getPrivateStoreType
		await WriteByteAsync(0); //hasDwarvenCraft

		await WriteIntAsync(0); //_playerInstance.Stat.PkKills 
		await WriteIntAsync(0); //_playerInstance.Stat.PvpKills

		await WriteShortAsync(0);//_player.Cubics.Count

		await WriteByteAsync(0); //1-isInPartyMatchRoom

		await WriteIntAsync(0); //_player.AbnormalBitMask

		await WriteByteAsync(0x00);

		await WriteIntAsync(0);//_player.ClanPrivs

		await WriteShortAsync(0); // c2 recommendations remaining
		await WriteShortAsync(0); // c2 recommendations received
		await WriteIntAsync(0x00); // _player.getMountNpcId() > 0 ? _player.getMountNpcId() + 1000000 : 0
		await WriteShortAsync(80); //_player.getInventoryLimit()

		await WriteIntAsync(_template.GetClassId());
		await WriteIntAsync(0x00); // special effects? circles around player...
		await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxCp()); //_playerInstance.Stat.GetMaxCp()
		await WriteIntAsync(_playerInstance.PlayerStatus().CurrentCp); //_playerInstance.Status.GetCurrentCp()
		await WriteByteAsync(0); //_player.isMounted() ? 0 : _player.getEnchantEffect()

		await WriteByteAsync(0x00); // team circle around feet 1= Blue, 2 = red

		await WriteIntAsync(0); //_player.getClanCrestLargeId()
		await WriteByteAsync(0); // _player.isNoble() 0x01: symbol on char menu ctrl+I
		await WriteByteAsync(0); // 0x01: Hero Aura

		await WriteByteAsync(0); // _player.isFishing() Fishing Mode
		await WriteIntAsync(0); // fishing x
		await WriteIntAsync(0); // fishing y
		await WriteIntAsync(0); // fishing z
		await WriteIntAsync(0xFFFFFF); //_playerInstance.PlayerAppearance().NameColor /** The hexadecimal Color of players name (white is 0xFFFFFF) */

		// new c5
		await WriteByteAsync(_isHighSpeed ? 0x01 : 0x00); //_playerInstance.Movement().IsRunning() changes the Speed display on Status Window

		await WriteIntAsync(0); // _player.getPledgeClass() changes the text above CP on Status Window
		await WriteIntAsync(0); //_player.getPledgeType()

		await WriteIntAsync(0); //_playerInstance.PlayerAppearance().TitleColor

		await WriteIntAsync(0x00); //_player.isCursedWeaponEquiped()
	}
}