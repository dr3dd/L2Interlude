using System.Collections.Generic;

namespace Core.GeoEngine
{
    internal sealed  class MoveDirection
    {
        public static readonly MoveDirection N = new MoveDirection("N", InnerEnum.N, 0, -1);
        public static readonly MoveDirection S = new MoveDirection("S", InnerEnum.S, 0, 1);
        public static readonly MoveDirection W = new MoveDirection("W", InnerEnum.W, -1, 0);
        public static readonly MoveDirection E = new MoveDirection("E", InnerEnum.E, 1, 0);
        public static readonly MoveDirection NW = new MoveDirection("NW", InnerEnum.NW, -1, -1);
        public static readonly MoveDirection SW = new MoveDirection("SW", InnerEnum.SW, -1, 1);
        public static readonly MoveDirection NE = new MoveDirection("NE", InnerEnum.NE, 1, -1);
        public static readonly MoveDirection SE = new MoveDirection("SE", InnerEnum.SE, 1, 1);

        private static readonly List<MoveDirection> valueList = new List<MoveDirection>();
        
        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;

        // Step and signum.
        private readonly int _stepX;
        private readonly int _stepY;
        private readonly int _signumX;
        private readonly int _signumY;

        // Cell offset.
        private readonly int _offsetX;
        private readonly int _offsetY;

        // Direction flags.
        private readonly sbyte _directionX;
        private readonly sbyte _directionY;
        private readonly string _symbolX;
        private readonly string _symbolY;

        public int StepX => _stepX;
        public int StepY => _stepY;
        public int SignumX => _signumX;
        public int SignumY => _signumY;
        public int OffsetX => _offsetX;
        public int OffsetY => _offsetY;
        public sbyte DirectionX => _directionX;
        public sbyte DirectionY => _directionY;
        public string SymbolX => _symbolX;
        public string SymbolY => _symbolY;

        static MoveDirection()
        {
            valueList.Add(N);
            valueList.Add(S);
            valueList.Add(W);
            valueList.Add(E);
            valueList.Add(NW);
            valueList.Add(SW);
            valueList.Add(NE);
            valueList.Add(SE);
        }

        public enum InnerEnum
        {
            N,
            S,
            W,
            E,
            NW,
            SW,
            NE,
            SE
        }
        
        private MoveDirection(string name, InnerEnum innerEnum, int signumX, int signumY)
        {
            // Get step (world -16, 0, 16) and signum (geodata -1, 0, 1) coordinates.
            _stepX = signumX * GeoStructure.CellSize;
            _stepY = signumY * GeoStructure.CellSize;
            _signumX = signumX;
            _signumY = signumY;

            // Get border offsets in a direction of iteration.
            _offsetX = signumX >= 0 ? GeoStructure.CellSize - 1 : 0;
            _offsetY = signumY >= 0 ? GeoStructure.CellSize - 1 : 0;

            // Get direction NSWE flag and symbol.
            _directionX = (sbyte) (signumX < 0 ? GeoStructure.CellFlagW : signumX == 0 ? 0 : GeoStructure.CellFlagE);
            _directionY = (sbyte) (signumY < 0 ? GeoStructure.CellFlagN : signumY == 0 ? 0 : GeoStructure.CellFlagS);
            _symbolX = signumX < 0 ? "W" : signumX == 0 ? "-" : "E";
            _symbolY = signumY < 0 ? "N" : signumY == 0 ? "-" : "S";

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }

        public static MoveDirection GetDirection(int gdx, int gdy)
        {
            if (gdx == 0)
            {
                return (gdy < 0) ? N : S;
            }
		
            if (gdy == 0)
            {
                return (gdx < 0) ? W : E;
            }
		
            if (gdx > 0)
            {
                return (gdy < 0) ? NE : SE;
            }
		
            return (gdy < 0) ? NW : SW;
        }
    }
}