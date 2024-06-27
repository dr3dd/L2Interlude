using System;
using Core.GeoEngine.Blocks;

namespace Core.GeoEngine.Regions;

public abstract class RegionAbstract
{
	public static int REGION_BLOCKS_X = 256;

	/** Blocks in a region on the y axis. */
	public static int REGION_BLOCKS_Y = 256;

	/** Blocks in a region. */
	public static int REGION_BLOCKS = REGION_BLOCKS_X * REGION_BLOCKS_Y;

	/** Cells in a region on the x axis. */
	public static int REGION_CELLS_X = REGION_BLOCKS_X * IBlock.BLOCK_CELLS_X;

	/** Cells in a region on the y axis. */
	public static int REGION_CELLS_Y = REGION_BLOCKS_Y * IBlock.BLOCK_CELLS_Y;

	/** Cells in a region. */
	public int REGION_CELLS = REGION_CELLS_X * REGION_CELLS_Y;

	public abstract bool CheckNearestNswe(int geoX, int geoY, int worldZ, int nswe);
	public abstract void SetNearestNswe(int geoX, int geoY, int worldZ, byte nswe);
	public abstract void UnsetNearestNswe(int geoX, int geoY, int worldZ, byte nswe);
	public abstract int GetNearestZ(int geoX, int geoY, int worldZ);
	public abstract int GetNextLowerZ(int geoX, int geoY, int worldZ);
	public abstract int GetNextHigherZ(int geoX, int geoY, int worldZ);
	public abstract bool HasGeo();
	public abstract bool SaveToFile(String fileName);
}