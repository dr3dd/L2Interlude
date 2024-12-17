using Core.Module.CharacterData.Template.Race;
using Core.Module.SettingData;
using Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Core.Module.CharacterData.Template
{
    public abstract class OrcFighterApprentice : Orc
    {
        private const string BaseClass = "orc_fighter";
        private const int BasePhysicalAttack = 4;
        private const int BaseCritical = 4;
        private const string BaseAttackType = "fist";
        private const int BaseAttackSpeed = 300;
        //private const int BaseDefend = 80; //{Upper body; Lower body; Pitch; Boots; Gloves; Underwear; Mantle} {31;18;12;7;8;3;1}
        private const int BaseDefendUpperBody = 31;
        private const int BaseDefendLowerBody = 18;
        private const int BaseDefendPitch = 12;
        private const int BaseDefendBoots = 7;
        private const int BaseDefendGloves = 8;
        private const int BaseDefendUnderwear = 3;
        private const int BaseDefendMantle = 1;

        private const int BaseMagicAttack = 6;
        //private const int BaseMagicDefend = 41; //{The earring which rises; Left earring; The ring which rises; Left ring; Neck} {9;9;5;5;13}
        private const int BaseMagicDefendLeftEarring = 9;
        private const int BaseMagicDefendRightEarring = 9;
        private const int BaseMagicDefendLeftRing = 5;
        private const int BaseMagicDefendRightRing = 5;
        private const int BaseMagicDefendNecklace = 13;

        private const int BaseAttackRange = 25;
        private const int BaseDamageRange = 0;
        private const int BaseRandDam = 10;

        private const int BaseGroundLowSpeed = 70;
        private const int BaseGroundHighSpeed = 117;
        private const int BaseUnderWaterLowSpeed = 50;
        private const int BaseUnderWaterHighSpeed = 50;

        private const byte Int = 18;
        private const byte Str = 40;
        private const byte Con = 47;
        private const byte Men = 27;
        private const byte Dex = 26;
        private const byte Wit = 12;

        private IList<string> _initialEquipment;
        private IList<Location> _initialStartPoint;
        private SettingDataInit _settingDatainit;

        protected OrcFighterApprentice(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _settingDatainit = serviceProvider.GetRequiredService<SettingDataInit>();
            InitialEquipment();
            InitialStartPoint();
            InitialHpRegen();
        }
        
        private void InitialHpRegen()
        {
            _baseHpRegen = new List<float> { 6.0f, 7.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f };
        }
        
        private void InitialStartPoint()
        {
            _initialStartPoint = new List<Location>
            {
                new Location(-56733, -113459, -690),
                new Location(-56686, -113470, -690),
                new Location(-56728, -113610, -690),
                new Location(-56693, -113610, -690),
                new Location(-56743, -113757, -690),
                new Location(-56682, -113730, -690)
            };
        }

        private void InitialEquipment()
        {
            _initialEquipment = _settingDatainit.GetInitialEquipment(BaseClass);
        }

        public byte GetInt()
        {
            return Int;
        }
        public byte GetStr()
        {
            return Str;
        }
        public byte GetCon()
        {
            return Con;
        }
        public byte GetMen()
        {
            return Men;
        }
        public byte GetDex()
        {
            return Dex;
        }
        public byte GetWit()
        {
            return Wit;
        }

        public IEnumerable<string> GetInitialEquipment()
        {
            return _initialEquipment;
        }

        public Location GetInitialStartPoint()
        {
            var rndItem = Rnd.Next(6);
            return _initialStartPoint[rndItem];
        }

        public int GetBasePhysicalAttack()
        {
            return BasePhysicalAttack;
        }

        public int GetBaseAttackSpeed()
        {
            return BaseAttackSpeed;
        }

        public int GetBaseCritical()
        {
            return BaseCritical;
        }
        public int GetBaseDefendUpperBody()
        {
            return BaseDefendUpperBody;
        }

        public int GetBaseDefendLowerBody()
        {
            return BaseDefendLowerBody;
        }

        public int GetBaseDefendPitch()
        {
            return BaseDefendPitch;
        }

        public int GetBaseDefendBoots()
        {
            return BaseDefendBoots;
        }

        public int GetBaseDefendGloves()
        {
            return BaseDefendGloves;
        }

        public int GetBaseDefendUnderwear()
        {
            return BaseDefendUnderwear;
        }

        public int GetBaseDefendMantle()
        {
            return BaseDefendMantle;
        }

        public int GetBaseMagicDefendLeftEarring()
        {
            return BaseMagicDefendLeftEarring;
        }

        public int GetBaseMagicDefendRightEarring()
        {
            return BaseMagicDefendRightEarring;
        }

        public int GetBaseMagicDefendLeftRing()
        {
            return BaseMagicDefendLeftRing;
        }

        public int GetBaseMagicDefendRightRing()
        {
            return BaseMagicDefendRightRing;
        }

        public int GetBaseMagicDefendNecklace()
        {
            return BaseMagicDefendNecklace;
        }

        public int GetBaseMagicAttack()
        {
            return BaseMagicAttack;
        }

        public int GetBaseAttackRange()
        {
            return BaseAttackRange;
        }

        public int GetBaseGroundHighSpeed()
        {
            return BaseGroundHighSpeed;
        }

        public int GetBaseGroundLowSpeed()
        {
            return BaseGroundLowSpeed;
        }
        
        public int GetBaseUnderWaterLowSpeed()
        {
            return BaseUnderWaterLowSpeed;
        }

        public int GetBaseUnderWaterHighSpeed()
        {
            return BaseUnderWaterHighSpeed;
        }
    }
}