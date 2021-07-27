using System;
using System.IO;

namespace Core.GeoEngine
{
    public class BlockComplex : ABlock
    {
        private readonly sbyte[] _buffer;

        public BlockComplex(BinaryReader binaryReader)
        {
            // Initialize buffer.
            _buffer = new sbyte[GeoStructure.BlockCells * 3];
            for (int i = 0; i < GeoStructure.BlockCells; i++)
            {
                var data = binaryReader.ReadInt16();
                        
                // Get nswe.
                _buffer[i * 3] = (sbyte) (data & 0x000F);
			
                // Get height.
                data = (short) ((short) (data & 0xFFF0) >> 1);
                _buffer[(i * 3) + 1] = (sbyte) (data & 0x00FF);
                _buffer[(i * 3) + 2] = (sbyte) (data >> 8);
            }
        }

        public override bool HasGeoPos()
        {
            return true;
        }

        public override short GetHeightNearest(int geoX, int geoY, int worldZ)
        {
            // Get cell index.
            int index = (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)) * 3;
            // Get height.
            return (short) ((_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8));
        }

        public override sbyte GetNsweNearest(int geoX, int geoY, int worldZ)
        {
            // Get cell index.
            int index = (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)) * 3;
            // Get nswe.
            return _buffer[index];
        }

        public override int GetIndexNearest(int geoX, int geoY, int worldZ)
        {
            return (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)) * 3;
        }

        public override int GetIndexAbove(int geoX, int geoY, int worldZ)
        {
            // Get cell index.
            int index = (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)) * 3;
            // Get height.
            int height = (_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8);
            // Check height and return nswe.
            return height > worldZ ? index : -1;
        }

        public override int GetIndexBelow(int geoX, int geoY, int worldZ)
        {
            // Get cell index.
            int index = (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)) * 3;
		
            // Get height.
            int height = (_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8);
		
            // Check height and return nswe.
            return height < worldZ ? index : -1;
        }

        public override short GetHeight(int index)
        {
            // Get height.
            return (short) ((_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8));
        }

        public override sbyte GetNswe(int index)
        {
            // Get nswe.
            return _buffer[index];
        }
    }
}