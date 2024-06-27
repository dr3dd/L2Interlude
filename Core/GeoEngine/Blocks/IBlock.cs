namespace Core.GeoEngine.Blocks;

public interface IBlock
{
    const int TYPE_FLAT = 0;
    const int TYPE_COMPLEX = 64;

    const int BLOCK_CELLS_X = 8;
    const int BLOCK_CELLS_Y = 8;
    const int BLOCK_CELLS = BLOCK_CELLS_X * BLOCK_CELLS_Y;
    
    bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe);
	
    void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe);
	
    void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe);
	
    int GetNearestZ(int geoX, int geoY, int worldZ);
	
    int GetNextLowerZ(int geoX, int geoY, int worldZ);
	
    int GetNextHigherZ(int geoX, int geoY, int worldZ);
}