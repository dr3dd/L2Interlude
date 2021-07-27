using System.IO;

namespace Core.GeoEngine
{
    public class BlockFlat : ABlock
    {
        private readonly short _height;
        private readonly byte _nswe;
        public BlockFlat(BinaryReader binaryReader)
        {
            // Get height and nswe.
            _height = binaryReader.ReadInt16();
            _nswe = GeoStructure.CellFlagAll;
		
            // Read dummy data.
            binaryReader.ReadInt16();
        }

        public override bool HasGeoPos() => true;

        public override short GetHeightNearest(int geoX, int geoY, int worldZ) => _height;

        public override byte GetNsweNearest(int geoX, int geoY, int worldZ) => _nswe;

        public override int GetIndexNearest(int geoX, int geoY, int worldZ) => 0;

        public override int GetIndexAbove(int geoX, int geoY, int worldZ) => _height > worldZ ? 0 : -1;

        public override int GetIndexBelow(int geoX, int geoY, int worldZ) => _height < worldZ ? 0 : -1;

        public override short GetHeight(int index) => _height;

        public override byte GetNswe(int index) => _nswe;
    }
}