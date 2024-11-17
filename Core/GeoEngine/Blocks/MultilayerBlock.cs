using System;
using System.IO;
using System.Linq;
using L2Logger;

namespace Core.GeoEngine.Blocks;

public class MultilayerBlock : IBlock
{
    private readonly sbyte[] _data;

    public MultilayerBlock(BinaryReader reader)
    {
        var start = reader.BaseStream.Position;
        
        var totalDataLength = 0;
        for (var blockCellOffset = 0; blockCellOffset < IBlock.BLOCK_CELLS; blockCellOffset++)
        {
            var layers = (byte) reader.ReadInt16();
            if (layers <= 0 || layers > 125)
            {
                throw new Exception("Geo file corrupted! Invalid layers count!");
            }
            totalDataLength += 1 + layers * 2;
            reader.BaseStream.Position += layers * 2;
        }
        
        var end = reader.BaseStream.Position;
        var dataLength = end - start;
        
        _data = new sbyte[totalDataLength]; //create array without 0
        reader.BaseStream.Position = start;
        _data = Enumerable.Range(0, (int)dataLength)
            .Select(_ => reader.ReadSByte())
            .Where(value => value != 0)
            .ToArray();
    }
    
    private short GetNearestLayer(int geoX, int geoY, int worldZ)
    {
        var startOffset = GetCellDataOffset(geoX, geoY);
        var nLayers = _data[startOffset];
        var endOffset = startOffset + 1 + (nLayers * 2);
		
        // One layer at least was required on loading so this is set at least once on the loop below.
        var nearestDZ = 0;
        short nearestData = 0;
        for (var offset = startOffset + 1; offset < endOffset; offset += 2)
        {
            var layerData = ExtractLayerData(offset);
            var layerZ = ExtractLayerHeight(layerData);
            if (layerZ == worldZ)
            {
                // Exact z.
                return layerData;
            }
			
            var layerDZ = Math.Abs(layerZ - worldZ);
            if ((offset == (startOffset + 1)) || (layerDZ < nearestDZ))
            {
                nearestDZ = layerDZ;
                nearestData = layerData;
            }
        }
		
        return nearestData;
    }
    
    private int GetCellDataOffset(int geoX, int geoY)
    {
        var cellLocalOffset = ((geoX % IBlock.BLOCK_CELLS_X) * IBlock.BLOCK_CELLS_Y) + (geoY % IBlock.BLOCK_CELLS_Y);
        var cellDataOffset = 0;
        try
        {
            // Move index to cell, we need to parse on each request, OR we parse on creation and save indexes.
            for (var i = 0; i < cellLocalOffset; i++)
            {
                cellDataOffset += 1 + (_data[cellDataOffset] * 2);
            }
            // Now the index points to the cell we need.
        }
        catch (Exception ex)
        {
            LoggerManager.Error(ex.Message + " cellLocalOffset: " + cellLocalOffset);
        }
        return cellDataOffset;
    }
    
    private short ExtractLayerData(int dataOffset)
    {
        return (short) ((_data[dataOffset] & 0xff) | (_data[dataOffset + 1] << 8));
    }
    
    private static int ExtractLayerNswe(short layer)
    {
        return (sbyte) (layer & 0x000f);
    }
	
    private static int ExtractLayerHeight(short layer)
    {
        return (short) (layer & 0x0fff0) >> 1;
    }
    
    private int GetNearestNswe(int geoX, int geoY, int worldZ)
    {
        return ExtractLayerNswe(GetNearestLayer(geoX, geoY, worldZ));
    }

    public bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return (GetNearestNswe(geoX, geoY, worldZ) & nswe) == nswe;
    }

    public void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        var startOffset = GetCellDataOffset(geoX, geoY);
        var nLayers = _data[startOffset];
        var endOffset = startOffset + 1 + (nLayers * 2);
		
        var nearestDZ = 0;
        var nearestLayerZ = 0;
        var nearestOffset = 0;
        short nearestLayerData = 0;
        for (var offset = startOffset + 1; offset < endOffset; offset += 2)
        {
            var layerData = ExtractLayerData(offset);
            var layerZ = ExtractLayerHeight(layerData);
            if (layerZ == worldZ)
            {
                nearestLayerZ = layerZ;
                nearestOffset = offset;
                nearestLayerData = layerData;
                break;
            }
			
            var layerDZ = Math.Abs(layerZ - worldZ);
            if ((offset == (startOffset + 1)) || (layerDZ < nearestDZ))
            {
                nearestDZ = layerDZ;
                nearestLayerZ = layerZ;
                nearestOffset = offset;
            }
        }
		
        var currentNswe = (short) ExtractLayerNswe(nearestLayerData);
        if ((currentNswe & nswe) == 0)
        {
            var encodedHeight = (short) (nearestLayerZ << 1); // Shift left by 1 bit.
            var newNswe = (short) (currentNswe | nswe); // Combine NSWE.
            var newCombinedData = (short) (encodedHeight | newNswe); // Combine height and NSWE.
            _data[nearestOffset] = (sbyte) (newCombinedData & 0xff); // Update the first byte at offset.
            _data[nearestOffset + 1] = (sbyte) ((newCombinedData >> 8) & 0xff); // Update the second byte at offset + 1.
        }
    }

    public void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        var startOffset = GetCellDataOffset(geoX, geoY);
        var nLayers = _data[startOffset];
        var endOffset = startOffset + 1 + (nLayers * 2);
		
        var nearestDZ = 0;
        var nearestLayerZ = 0;
        var nearestOffset = 0;
        short nearestLayerData = 0;
        for (int offset = startOffset + 1; offset < endOffset; offset += 2)
        {
            var layerData = ExtractLayerData(offset);
            var layerZ = ExtractLayerHeight(layerData);
            if (layerZ == worldZ)
            {
                nearestLayerZ = layerZ;
                nearestOffset = offset;
                nearestLayerData = layerData;
                break;
            }
			
            var layerDZ = Math.Abs(layerZ - worldZ);
            if ((offset == (startOffset + 1)) || (layerDZ < nearestDZ))
            {
                nearestDZ = layerDZ;
                nearestLayerZ = layerZ;
                nearestOffset = offset;
            }
        }
		
        var currentNswe = (short) ExtractLayerNswe(nearestLayerData);
        if ((currentNswe & nswe) != 0)
        {
            var encodedHeight = (short) (nearestLayerZ << 1); // Shift left by 1 bit.
            var newNswe = (short) (currentNswe & ~nswe); // Subtract NSWE.
            var newCombinedData = (short) (encodedHeight | newNswe); // Combine height and NSWE.
            _data[nearestOffset] = (sbyte) (newCombinedData & 0xff); // Update the first byte at offset.
            _data[nearestOffset + 1] = (sbyte) ((newCombinedData >> 8) & 0xff); // Update the second byte at offset + 1.
        }
    }

    public int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return ExtractLayerHeight(GetNearestLayer(geoX, geoY, worldZ));
    }

    public int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        var startOffset = GetCellDataOffset(geoX, geoY);
        var nLayers = _data[startOffset];
        var endOffset = startOffset + 1 + (nLayers * 2);
		
        var lowerZ = int.MinValue;
        for (int offset = startOffset + 1; offset < endOffset; offset += 2)
        {
            var layerData = ExtractLayerData(offset);
			
            var layerZ = ExtractLayerHeight(layerData);
            if (layerZ == worldZ)
            {
                // Exact z.
                return layerZ;
            }
			
            if ((layerZ < worldZ) && (layerZ > lowerZ))
            {
                lowerZ = layerZ;
            }
        }
		
        return lowerZ == int.MinValue ? worldZ : lowerZ;
    }

    public int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        var startOffset = GetCellDataOffset(geoX, geoY);
        var nLayers = _data[startOffset];
        var endOffset = startOffset + 1 + (nLayers * 2);
		
        var higherZ = int.MaxValue;
        for (var offset = startOffset + 1; offset < endOffset; offset += 2)
        {
            var layerData = ExtractLayerData(offset);
			
            var layerZ = ExtractLayerHeight(layerData);
            if (layerZ == worldZ)
            {
                // Exact z.
                return layerZ;
            }
			
            if ((layerZ > worldZ) && (layerZ < higherZ))
            {
                higherZ = layerZ;
            }
        }
		
        return higherZ == int.MaxValue ? worldZ : higherZ;
    }
    
    public sbyte[] GetData()
    {
        return _data;
    }
}