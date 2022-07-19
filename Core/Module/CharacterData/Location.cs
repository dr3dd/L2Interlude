namespace Core.Module.CharacterData
{
    public class Location
    {
        private int _x;
        private int _y;
        private int _z;
        private int _heading = 0;

        public Location(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Location(int x, int y, int z, int heading)
        {
            _x = x;
            _y = y;
            _z = z;
            _heading = heading;
        }
        
        public int GetX()
        {
            return _x;
        }
        
        public int GetY()
        {
            return _y;
        }
	
        public int GetZ()
        {
            return _z;
        }
	
        public int GetHeading()
        {
            return _heading;
        }
	
        public void SetX(int x)
        {
            _x = x;
        }
	
        public void SetY(int y)
        {
            _y = y;
        }
	
        public void SetZ(int z)
        {
            _z = z;
        }
	
        public void SetHeading(int head)
        {
            _heading = head;
        }
        
        public Location Clone()
        {
            return new Location(_x, _y, _z);
        }

        public void SetXYZ(int mXDestination, int mYDestination, int mZDestination)
        {
            _x = mXDestination;
            _y = mYDestination;
            _z = mZDestination;
        }

        public virtual void Clean()
        {
            _z = 0;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Location)
            {
                Location loc = (Location) obj;
                return (GetX() == loc.GetX()) && (GetY() == loc.GetY()) && (GetZ() == loc.GetZ()) && (GetHeading() == loc.GetHeading());
            }
            return false;
        }
        
        public override string ToString()
        {
            return "[" + GetType().Name + "] X: " + _x + " Y: " + _y + " Z: " + _z + " Heading: " + _heading;
        }
    }
}