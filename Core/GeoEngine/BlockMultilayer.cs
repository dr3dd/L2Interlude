using System;
using System.IO;
using L2Logger;

namespace Core.GeoEngine
{
    public class BlockMultilayer : ABlock
    {
        private static readonly int MaxLayers = byte.MaxValue;
        private readonly byte[] _buffer;
	
        private static MemoryStream _temp;

        public static void Initialize()
        {
            // Initialize temporary buffer and sorting mechanism.
            _temp = new MemoryStream(GeoStructure.BlockCells * MaxLayers * 3);
        }
        
        public static void Release()
        {
            _temp = null;
        }

        public BlockMultilayer(BinaryReader binaryReader)
        {
            for (int cell = 0; cell < GeoStructure.BlockCells; cell++)
            {
                byte layers = (byte) binaryReader.ReadInt16();
                // Add layers count.
                _temp.WriteByte(layers);
                // Loop over layers.
                for (byte layer = 0; layer < layers; layer++)
                {
                    // Get data.
                    short data = binaryReader.ReadInt16();
                    // Add nswe and height.
                    _temp.WriteByte((byte) (data & 0x000F));
                    _temp.WriteByte((byte) ((short) (data & 0xFFF0) >> 1));
                }
            }
            _buffer = new byte[_temp.Position];
            // Initialize buffer.
            Array.Copy(_temp.ToArray(), _buffer, _temp.Position);
            _temp.SetLength(0);
        }
        
        public override bool HasGeoPos() => true;

        public override short GetHeightNearest(int geoX, int geoY, int worldZ)
        {
            // Get cell index.
            int index = GetIndexNearest(geoX, geoY, worldZ);
            // Get height.
            return (short) ((_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8));
        }

        public override byte GetNsweNearest(int geoX, int geoY, int worldZ)
        {
            // Get cell index.
            int index = GetIndexNearest(geoX, geoY, worldZ);
            // Get nswe.
            return _buffer[index];
        }

        public override int GetIndexNearest(int geoX, int geoY, int worldZ)
        {
            // Move index to the cell given by coordinates.
            int index = 0;
            try
            {
                var dd = (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) +
                          (geoY % GeoStructure.BlockCellsY));
                for (int i = 0; i < dd; i++)
                {
                    // Move index by amount of layers for this cell.
                    index += (_buffer[index] * 3) + 1;
                }

                // Get layers count and shift to last layer data (first from bottom).
                byte layers = _buffer[index++];

                // Loop though all cell layers, find closest layer to given worldZ.
                int limit = int.MaxValue;
                while (layers-- > 0)
                {
                    // Get layer height.
                    int height = (_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8);

                    // Get Z distance and compare with limit.
                    // Note: When 2 layers have same distance to worldZ (worldZ is in the middle of them):
                    // > Returns bottom layer.
                    // >= Returns upper layer.
                    int distance = Math.Abs(height - worldZ);
                    if (distance > limit)
                    {
                        break;
                    }

                    // Update limit and move to next layer.
                    limit = distance;
                    index += 3;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
            // Return layer index.
            return index - 3;
        }

        public override int GetIndexAbove(int geoX, int geoY, int worldZ)
        {
            // Move index to the cell given by coordinates.
            int index = 0;
            for (int i = 0; i < (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)); i++)
            {
                // Move index by amount of layers for this cell.
                index += (_buffer[index] * 3) + 1;
            }
		
            // Get layers count and shift to last layer data (first from bottom).
            byte layers = _buffer[index++];
            index += (layers - 1) * 3;
		
            // Loop though all layers, find first layer above worldZ.
            while (layers-- > 0)
            {
                // Get layer height.
                int height = (_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8);
			
                // Layer height is higher than worldZ, return layer index.
                if (height > worldZ)
                {
                    return index;
                }
			
                // Move index to next layer.
                index -= 3;
            }
		
            // No layer found.
            return -1;
        }

        public override int GetIndexBelow(int geoX, int geoY, int worldZ)
        {
            // Move index to the cell given by coordinates.
            int index = 0;
            for (int i = 0; i < (((geoX % GeoStructure.BlockCellsX) * GeoStructure.BlockCellsY) + (geoY % GeoStructure.BlockCellsY)); i++)
            {
                // Move index by amount of layers for this cell.
                index += (_buffer[index] * 3) + 1;
            }
		
            // Get layers count and shift to first layer data (first from top).
            byte layers = _buffer[index++];
		
            // Loop though all layers, find first layer below worldZ.
            while (layers-- > 0)
            {
                // Get layer height.
                int height = (_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8);
			
                // Layer height is lower than worldZ, return layer index.
                if (height < worldZ)
                {
                    return index;
                }
			
                // Move index to next layer.
                index += 3;
            }
		
            // No layer found.
            return -1;
        }

        public override short GetHeight(int index)
        {
            // Get height.
            return (short) ((_buffer[index + 1] & 0x00FF) | (_buffer[index + 2] << 8));
        }

        public override byte GetNswe(int index)
        {
            // Get nswe.
            return _buffer[index];
        }
    }
}