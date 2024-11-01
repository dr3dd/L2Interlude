using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket;

public class StatusUpdate : Network.ServerPacket
{
	public const int Level = 0x01;
	public const int Exp = 0x02;
	public const int Str = 0x03;
	public const int Dex = 0x04;
	public const int Con = 0x05;
	public const int Int = 0x06;
	public const int Wit = 0x07;
	public const int Men = 0x08;

	public const int CurHp = 0x09;
	public const int MaxHp = 0x0a;
	public const int CurMp = 0x0b;
	public const int MaxMp = 0x0c;

	public const int Sp = 0x0d;
	public const int CurLoad = 0x0e;
	public const int MaxLoad = 0x0f;

	public const int PAtk = 0x11;
	public const int AtkSpd = 0x12;
	public const int PDef = 0x13;
	public const int Evasion = 0x14;
	public const int Accuracy = 0x15;
	public const int Critical = 0x16;
	public const int MAtk = 0x17;
	public const int CastSpd = 0x18;
	public const int MDef = 0x19;
	public const int PvpFlag = 0x1a;
	public const int Karma = 0x1b;

	public const int CurCp = 0x21;
	public const int MaxCp = 0x22;

	private readonly PlayerInstance _playerInstance;

	private readonly List<Attribute> _attributes;
	public readonly int _objectId;

	class Attribute
	{
		// id values 09 - current health 0a - max health 0b - current mana 0c - max mana
		public int Id;
		public int Value;
		
		public Attribute(int pId, int pValue)
		{
			Id = pId;
			Value = pValue;
		}
	}

	public StatusUpdate(PlayerInstance playerInstance)
	{
		_playerInstance = playerInstance;
	}

	public StatusUpdate(int objectId)
	{
		_attributes = new List<Attribute>();
		_objectId = objectId;
	}

	public void AddAttribute(int id, int value)
	{
		_attributes.Add(new Attribute(id, value));
	}

	public override async Task WriteAsync()
	{
		await WriteByteAsync(0x0e);

		if (_playerInstance != null)
		{
			await WriteIntAsync(_playerInstance.ObjectId);
			await WriteIntAsync(28); // all the attributes

			await WriteIntAsync(Level);
			await WriteIntAsync(_playerInstance.PlayerStatus().Level);
			await WriteIntAsync(Exp);
			await WriteIntAsync((int) _playerInstance.PlayerCharacterInfo().Exp);
			await WriteIntAsync(Str);
			await WriteIntAsync(_playerInstance.TemplateHandler().GetStr());
			await WriteIntAsync(Dex);
			await WriteIntAsync(_playerInstance.TemplateHandler().GetDex());
			await WriteIntAsync(Con);
			await WriteIntAsync(_playerInstance.TemplateHandler().GetCon());
			await WriteIntAsync(Int);
			await WriteIntAsync(_playerInstance.TemplateHandler().GetInt());
			await WriteIntAsync(Wit);
			await WriteIntAsync(_playerInstance.TemplateHandler().GetWit());
			await WriteIntAsync(Men);
			await WriteIntAsync(_playerInstance.TemplateHandler().GetMen());

			await WriteIntAsync(CurHp);
			await WriteIntAsync((int) _playerInstance.CharacterStatus().CurrentHp);
			await WriteIntAsync(MaxHp);
			await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxHp());
			await WriteIntAsync(CurMp);
			await WriteIntAsync((int) _playerInstance.CharacterStatus().CurrentMp);
			await WriteIntAsync(MaxMp);
			await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxMp());
			await WriteIntAsync(Sp);
			await WriteIntAsync(_playerInstance.PlayerCharacterInfo().Sp);
			await WriteIntAsync(CurLoad);
			await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetCurrentLoad()); //_actor.Stat.CurrentLoad()
			await WriteIntAsync(MaxLoad);
			await WriteIntAsync(_playerInstance.CharacterBaseStatus().GetMaxLoad()); //_actor.Stat.GetMaxLoad()

			await WriteIntAsync(PAtk);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalAttack());
			await WriteIntAsync(AtkSpd);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalAttackSpeed());
			await WriteIntAsync(PDef);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetPhysicalDefence());
			await WriteIntAsync(Evasion);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetEvasion());
			await WriteIntAsync(Accuracy);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetAccuracy());
			await WriteIntAsync(Critical);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetCriticalRate());
			await WriteIntAsync(MAtk);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetMagicalAttack());

			await WriteIntAsync(CastSpd);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetCastSpeed());
			await WriteIntAsync(MDef);
			await WriteIntAsync(_playerInstance.PlayerCombat().GetMagicalDefence());
			await WriteIntAsync(PvpFlag);
			await WriteIntAsync(0); //_actor.Stat.PvpKills
			await WriteIntAsync(Karma);
			await WriteIntAsync(0); //_actor.Stat.Karma
			await WriteIntAsync(CurCp);
			await WriteIntAsync((int) _playerInstance.PlayerStatus().CurrentCp);
			await WriteIntAsync(MaxCp);
			await WriteIntAsync(_playerInstance.PlayerStatus().GetMaxCp());
		}
		else
		{
			await WriteIntAsync(_objectId);
			await WriteIntAsync(_attributes.Count);
			
			foreach (var temp in _attributes)
			{
				await WriteIntAsync(temp.Id);
				await WriteIntAsync(temp.Value);
			}
		}
	}
}