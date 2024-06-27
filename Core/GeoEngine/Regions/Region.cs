using System;
using System.IO;
using Core.GeoEngine.Blocks;

namespace Core.GeoEngine.Regions;

public class Region : RegionAbstract
{
    private readonly IBlock[] _blocks = new IBlock[REGION_BLOCKS];

    public Region(BinaryReader reader)
    {
        for (int blockOffset = 0; blockOffset < REGION_BLOCKS; blockOffset++)
        {
            int blockType = reader.ReadInt16();
            switch (blockType)
            {
                case IBlock.TYPE_FLAT:
                    _blocks[blockOffset] = new FlatBlock(reader);
                    break;
                case IBlock.TYPE_COMPLEX:
                    _blocks[blockOffset] = new ComplexBlock(reader);
                    break;
                default:
                    _blocks[blockOffset] = new MultilayerBlock(reader);
                    break;
            }
        }
    }
    
    private IBlock GetBlock(int geoX, int geoY)
    {
        return _blocks[(((geoX / IBlock.BLOCK_CELLS_X) % REGION_BLOCKS_X) * REGION_BLOCKS_Y) + ((geoY / IBlock.BLOCK_CELLS_Y) % REGION_BLOCKS_Y)];
    }

    public override bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return GetBlock(geoX, geoY).CheckNearestNswe(geoX, geoY, worldZ, nswe);
    }

    public override void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        IBlock block = GetBlock(geoX, geoY);
		
        // Flat block cells are enabled by default on all directions.
        if (block is FlatBlock)
        {
            // convertFlatToComplex(block, geoX, geoY);
            return;
        }
		
        GetBlock(geoX, geoY).SetNearestNswe(geoX, geoY, worldZ, nswe);
    }

    public override void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        IBlock block = GetBlock(geoX, geoY);
		
        // Flat blocks are by default enabled on all locations.
        if (block is FlatBlock)
        {
            ConvertFlatToComplex(block, geoX, geoY);
        }
		
        GetBlock(geoX, geoY).UnsetNearestNswe(geoX, geoY, worldZ, nswe);
    }
    
    private void ConvertFlatToComplex(IBlock block, int geoX, int geoY)
    {
        short currentHeight = ((FlatBlock)block).GetHeight();
        short encodedHeight = (short)((currentHeight << 1) & 0xffff);
        short combinedData = (short)(encodedHeight | Cell.NSWE_ALL);
    
        byte[] buffer = new byte[IBlock.BLOCK_CELLS * 2];
        using (MemoryStream memoryStream = new MemoryStream(buffer))
        {
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                for (int i = 0; i < IBlock.BLOCK_CELLS; i++)
                {
                    writer.Write(combinedData);
                }
            }
        }
        int index = (((geoX / IBlock.BLOCK_CELLS_X) % REGION_BLOCKS_X) * REGION_BLOCKS_Y) + ((geoY / IBlock.BLOCK_CELLS_Y) % REGION_BLOCKS_Y);
        _blocks[index] = new ComplexBlock(new BinaryReader(new MemoryStream(buffer)));
    }

    public override int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return GetBlock(geoX, geoY).GetNearestZ(geoX, geoY, worldZ);
    }

    public override int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        return GetBlock(geoX, geoY).GetNextLowerZ(geoX, geoY, worldZ);
    }

    public override int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        return GetBlock(geoX, geoY).GetNextHigherZ(geoX, geoY, worldZ);
    }

    public override bool HasGeo()
    {
        return true;
    }

    public override bool SaveToFile(string fileName)
    {
        throw new NotImplementedException();
    }
}