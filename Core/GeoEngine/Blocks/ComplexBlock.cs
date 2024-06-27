using System.IO;

namespace Core.GeoEngine.Blocks;

public class ComplexBlock : IBlock
{
    private readonly short[] _data;
    public ComplexBlock(BinaryReader reader)
    {
        _data = new short[IBlock.BLOCK_CELLS];
        for (var cellOffset = 0; cellOffset < IBlock.BLOCK_CELLS; cellOffset++)
        {
            _data[cellOffset] = reader.ReadInt16();
        }
    }
    private short GetCellData(int geoX, int geoY)
    {
        return _data[((geoX % IBlock.BLOCK_CELLS_X) * IBlock.BLOCK_CELLS_Y) + (geoY % IBlock.BLOCK_CELLS_Y)];
    }
	
    private byte GetCellNswe(int geoX, int geoY)
    {
        return (byte) (GetCellData(geoX, geoY) & 0x000F);
    }
	
    private int GetCellHeight(int geoX, int geoY)
    {
        return (short) (GetCellData(geoX, geoY) & 0x0FFF0) >> 1;
    }
    
    public bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe)
    {
        return (GetCellNswe(geoX, geoY) & nswe) == nswe;
    }
    
    public void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        var currentNswe = GetCellNswe(geoX, geoY);
        if ((currentNswe & nswe) == 0)
        {
            var currentHeight = (short) GetCellHeight(geoX, geoY);
            var encodedHeight = (short) (currentHeight << 1); // Shift left by 1 bit.
            var newNswe = (short) (currentNswe | nswe); // Add NSWE.
            var newCombinedData = (short) (encodedHeight | newNswe); // Combine height and NSWE.
            _data[((geoX % IBlock.BLOCK_CELLS_X) * IBlock.BLOCK_CELLS_Y) + (geoY % IBlock.BLOCK_CELLS_Y)] = (short) (newCombinedData & 0xffff);
        }
    }

    public void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe)
    {
        var currentNswe = GetCellNswe(geoX, geoY);
        if ((currentNswe & nswe) != 0)
        {
            var currentHeight = (short) GetCellHeight(geoX, geoY);
            var encodedHeight = (short) (currentHeight << 1); // Shift left by 1 bit.
            var newNswe = (short) (currentNswe & ~nswe); // Subtract NSWE.
            var newCombinedData = (short) (encodedHeight | newNswe); // Combine height and NSWE.
            _data[((geoX % IBlock.BLOCK_CELLS_X) * IBlock.BLOCK_CELLS_Y) + (geoY % IBlock.BLOCK_CELLS_Y)] = (short) (newCombinedData & 0xffff);
        }
    }

    public int GetNearestZ(int geoX, int geoY, int worldZ)
    {
        return GetCellHeight(geoX, geoY);
    }

    public int GetNextLowerZ(int geoX, int geoY, int worldZ)
    {
        var cellHeight = GetCellHeight(geoX, geoY);
        return cellHeight <= worldZ ? cellHeight : worldZ;
    }

    public int GetNextHigherZ(int geoX, int geoY, int worldZ)
    {
        var cellHeight = GetCellHeight(geoX, geoY);
        return cellHeight >= worldZ ? cellHeight : worldZ;
    }
    
    public short[] GetData()
    {
        return _data;
    }
}