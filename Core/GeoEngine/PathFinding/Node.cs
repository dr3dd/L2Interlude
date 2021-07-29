using System;
using Core.Module.CharacterData;

namespace Core.GeoEngine.PathFinding
{
    public class Node : Location, IComparable<Node>
    {
        // Node geodata values.
        private int _geoX;
        private int _geoY;
        private sbyte _nswe;
	
        // The cost G (movement cost done) and cost H (estimated cost to target).
        private int _costG;
        private int _costH;
        private int _costF;
	
        // Node parent (reverse path construction).
        private Node _parent;
        
        public Node(int x, int y, int z) : base(x, y, z)
        {
        }

        public Node(int x, int y, int z, int heading) : base(x, y, z, heading)
        {
        }
        
        public void SetGeo(int gx, int gy, int gz, sbyte nswe)
        {
            SetXYZ(GeoEngineInit.GetWorldX(gx), GeoEngineInit.GetWorldY(gy), gz);
            _geoX = gx;
            _geoY = gy;
            _nswe = nswe;
        }
        
        public void SetCost(Node parent, int weight, int costH)
        {
            _costG = weight;
            if (parent != null)
            {
                _costG += parent._costG;
            }
            _costH = costH;
            _costF = _costG + _costH;
            _parent = parent;
        }
        
        public int GetGeoX() => _geoX;
        public int GetGeoY() => _geoY;
        public sbyte GetNSWE() => _nswe;
        public int GetCostF() => _costF;
        public Node GetParent() => _parent;
        public int CompareTo(Node other)
        {
            return _costF - other._costF;
        }

        public void Clean()
        {
            base.Clean();
		
            _geoX = 0;
            _geoY = 0;
            _nswe = GeoStructure.CellFlagNone;
		
            _costG = 0;
            _costH = 0;
            _costF = 0;
		
            _parent = null;
        }
    }
}