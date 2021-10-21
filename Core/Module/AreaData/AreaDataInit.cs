using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using Core.Module.WorldData;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Module.AreaData
{
    public sealed class AreaDataInit : BaseParse
    {
        private readonly IDictionary<string, string> _defaultSettings;
        private readonly IDictionary<string, Type> _areaCollection;
        private readonly IList<BaseArea> _areas;
        private readonly WorldInit _worldInit;
        private readonly IParse _parse;
        
        public AreaDataInit(IServiceProvider provider) : base(provider)
        {
            _defaultSettings = new Dictionary<string, string>();
            _areaCollection = new Dictionary<string, Type>
                {
                    { "water", typeof(WaterArea) }, 
                    { "mother_tree", typeof(MotherTree) },
                    { "peace_zone", typeof(PeaceZone) },
                    { "damage", typeof(Damage) },
                    { "swamp", typeof(Swamp) },
                    { "no_restart", typeof(NoRestart) },
                    { "poison", typeof(Poison) },
                    { "ssq_zone", typeof(SsqZone) },
                    { "battle_zone", typeof(BattleZone) },
                    { "instant_skill", typeof(InstantSkill) },
                };
            _worldInit = provider.GetRequiredService<WorldInit>();
            _areas = new List<BaseArea>();
            _parse = new ParseAreaData();
        }

        public override void Run()
        {
            try
            {
                LoggerManager.Info("AreaData start...");
                IResult result = Parse("areadata.txt", _parse);
                foreach (var (type, collection) in result.GetResult())
                {
                    InitAreas(type.ToString(), collection);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Info(GetType().Name + ": " + ex.Message);
            }
            LoggerManager.Info("Loaded Areas: " + _areas.Count);
        }

        private void InitAreas(string type, object value)
        {
            try
            {
                var worldRegions = _worldInit.GetAllWorldRegions();
                var areaType = _areaCollection[type];
                if (areaType == typeof(WaterArea))
                {
                    InitWaterArea(value, areaType, worldRegions);
                    return;
                }
                InitZonesArea(value, areaType, worldRegions);
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
        }

        private void InitZonesArea(object value, Type areaType, WorldRegionData[,] worldRegions)
        {
            var areaZoneValue = (Dictionary<string, IList<IDictionary<string, int>>>)value;
            foreach (var (name, baseRange) in areaZoneValue)
            {
                var zoneArea = (BaseArea)Activator.CreateInstance(areaType, name.RemoveBrackets(), areaType);
                var aX = new int[baseRange.Count];
                var aY = new int[baseRange.Count];
                var minZ = 0;
                var maxZ = 0;
                for (var i = 0; i < baseRange.Count; i++)
                {
                    aX[i] = baseRange[i]["X"];
                    aY[i] = baseRange[i]["Y"];
                    minZ = baseRange[i]["MinZ"];
                    maxZ = baseRange[i]["MaxZ"];
                }
                zoneArea.X = aX;
                zoneArea.Y = aY;
                zoneArea.MinZ = minZ;
                zoneArea.MaxZ = maxZ;
                
                zoneArea.Zone = new ZoneNPoly(zoneArea.X, zoneArea.Y, zoneArea.MinZ, zoneArea.MaxZ);
                AddAreaZone(worldRegions, zoneArea);
            }
        }

        private void InitWaterArea(object value, Type areaType, WorldRegionData[,] worldRegions)
        {
            var waterValue = (IDictionary<string, IDictionary<string, double>>)value;
            foreach (var (name, data) in waterValue)
            {
                var waterZone = (WaterArea)Activator.CreateInstance(areaType, name.RemoveBrackets(), areaType);
                waterZone.MinX = (int) data["MinX"]; 
                waterZone.MinY = (int) data["MinY"];
                waterZone.MinZ = (int) data["MinZ"];
            
                waterZone.MaxX = (int) data["MaxX"];
                waterZone.MaxY = (int) data["MaxY"];
                waterZone.MaxZ = (int) data["MaxZ"];
                
                waterZone.Zone = new ZoneCuboid(waterZone.MinX, waterZone.MaxX, waterZone.MinY,
                    waterZone.MaxY, waterZone.MinZ, waterZone.MaxZ);

                AddAreaZone(worldRegions, waterZone);
            }
        }

        private void AddAreaZone(WorldRegionData[,] worldRegions, BaseArea baseArea)
        {
            int rows = worldRegions.GetUpperBound(0) + 1;
            int columns = worldRegions.Length / rows;
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (baseArea.Zone.IntersectsRectangle(
                        (x - _worldInit.OffsetX) << _worldInit.ShiftBy,
                        ((x + 1) - _worldInit.OffsetX) << _worldInit.ShiftBy,
                        (y - _worldInit.OffsetY) << _worldInit.ShiftBy,
                        ((y + 1) - _worldInit.OffsetY) << _worldInit.ShiftBy))
                    {
                        worldRegions[x, y].AddZone(baseArea);
                    }
                }
            }
        }
    }
}