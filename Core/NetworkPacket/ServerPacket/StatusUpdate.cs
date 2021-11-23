using System.Collections.Generic;
using Core.Module.Player;

namespace Core.NetworkPacket.ServerPacket
{
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
        
        public void AddAttribute(int id, int level)
        {
	        _attributes.Add(new Attribute(id, level));
        }
        
        public override void Write()
        {
	        WriteByte(0x0e);
	        
	        if (_playerInstance != null)
	        {
		        WriteInt(_playerInstance.ObjectId);
		        WriteInt(28); // all the attributes
			
		        WriteInt(Level);
		        WriteInt(_playerInstance.PlayerStatus().Level);
		        WriteInt(Exp);
		        WriteInt((int) _playerInstance.PlayerCharacterInfo().Exp);
		        WriteInt(Str);
		        WriteInt(_playerInstance.TemplateHandler().GetStr());
		        WriteInt(Dex);
		        WriteInt(_playerInstance.TemplateHandler().GetDex());
		        WriteInt(Con);
		        WriteInt(_playerInstance.TemplateHandler().GetCon());
		        WriteInt(Int);
		        WriteInt(_playerInstance.TemplateHandler().GetInt());
		        WriteInt(Wit);
		        WriteInt(_playerInstance.TemplateHandler().GetWit());
		        WriteInt(Men);
		        WriteInt(_playerInstance.TemplateHandler().GetMen());
			
		        WriteInt(CurHp);
		        WriteInt((int) _playerInstance.PlayerStatus().CurrentHp);
		        WriteInt(MaxHp);
		        WriteInt(_playerInstance.PlayerStatus().GetMaxHp());
		        WriteInt(CurMp);
		        WriteInt((int) _playerInstance.PlayerStatus().CurrentMp);
		        WriteInt(MaxMp);
		        WriteInt(_playerInstance.PlayerStatus().GetMaxMp());
		        WriteInt(Sp);
		        WriteInt(_playerInstance.PlayerCharacterInfo().Sp);
		        WriteInt(CurLoad);
		        WriteInt(0); //_actor.Stat.CurrentLoad()
		        WriteInt(MaxLoad);
		        WriteInt(100); //_actor.Stat.GetMaxLoad()
			
		        WriteInt(PAtk);
		        WriteInt(_playerInstance.PlayerCombat().GetPhysicalAttack());
		        WriteInt(AtkSpd);
		        WriteInt(_playerInstance.PlayerCombat().GetPhysicalAttackSpeed());
		        WriteInt(PDef);
		        WriteInt(_playerInstance.PlayerCombat().GetPhysicalDefence());
		        WriteInt(Evasion);
		        WriteInt(_playerInstance.PlayerCombat().GetEvasion());
		        WriteInt(Accuracy);
		        WriteInt(_playerInstance.PlayerCombat().GetAccuracy());
		        WriteInt(Critical);
		        WriteInt(_playerInstance.PlayerCombat().GetCriticalRate());
		        WriteInt(MAtk);
		        WriteInt(_playerInstance.PlayerCombat().GetMagicalAttack());
			
		        WriteInt(CastSpd);
		        WriteInt(_playerInstance.PlayerCombat().GetCastSpeed());
		        WriteInt(MDef);
		        WriteInt(_playerInstance.PlayerCombat().GetMagicalDefence());
		        WriteInt(PvpFlag);
		        WriteInt(0); //_actor.Stat.PvpKills
		        WriteInt(Karma);
		        WriteInt(0); //_actor.Stat.Karma
		        WriteInt(CurCp);
		        WriteInt((int) _playerInstance.PlayerStatus().CurrentCp);
		        WriteInt(MaxCp);
		        WriteInt(_playerInstance.PlayerStatus().GetMaxCp());
	        }
	        else
	        {
		        WriteInt(_objectId);
		        WriteInt(_attributes.Count);
			
		        foreach (var temp in _attributes)
		        {
			        WriteInt(temp.Id);
			        WriteInt(temp.Value);
		        }
	        }
        }
    }
}