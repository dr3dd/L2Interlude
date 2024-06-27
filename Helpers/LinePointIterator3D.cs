using System;

namespace Helpers
{
    public class LinePointIterator3D
    {
        private int _srcX;
		private int _srcY;
		private int _srcZ;
		private int _dstX;
		private int _dstY;
		private int _dstZ;
		private long _dx;
		private long _dy;
		private long _dz;
		private int _sx;
		private int _sy;
		private int _sz;
		private long _error;
		private long _error2;
		private bool _first;
		
		public LinePointIterator3D(int srcX, int srcY, int srcZ, int dstX, int dstY, int dstZ)
		{
			_srcX = srcX;
			_srcY = srcY;
			_srcZ = srcZ;
			_dstX = dstX;
			_dstY = dstY;
			_dstZ = dstZ;
			_dx = Math.Abs((long) dstX - srcX);
			_dy = Math.Abs((long) dstY - srcY);
			_dz = Math.Abs((long) dstZ - srcZ);
			_sx = srcX < dstX ? 1 : -1;
			_sy = srcY < dstY ? 1 : -1;
			_sz = srcZ < dstZ ? 1 : -1;
			if ((_dx >= _dy) && (_dx >= _dz))
			{
				_error = _error2 = _dx / 2;
			}
			else if ((_dy >= _dx) && (_dy >= _dz))
			{
				_error = _error2 = _dy / 2;
			}
			else
			{
				_error = _error2 = _dz / 2;
			}
			
			_first = true;
		}
		
		public bool next()
		{
			if (_first)
			{
				_first = false;
				return true;
			}
			if ((_dx >= _dy) && (_dx >= _dz))
			{
				if (_srcX != _dstX)
				{
					_srcX += _sx;
					_error += _dy;
					if (_error >= _dx)
					{
						_srcY += _sy;
						_error -= _dx;
					}
					
					_error2 += _dz;
					if (_error2 >= _dx)
					{
						_srcZ += _sz;
						_error2 -= _dx;
					}
					
					return true;
				}
			}
			else if ((_dy >= _dx) && (_dy >= _dz))
			{
				if (_srcY != _dstY)
				{
					_srcY += _sy;
					_error += _dx;
					if (_error >= _dy)
					{
						_srcX += _sx;
						_error -= _dy;
					}
					
					_error2 += _dz;
					if (_error2 >= _dy)
					{
						_srcZ += _sz;
						_error2 -= _dy;
					}
					
					return true;
				}
			}
			else if (_srcZ != _dstZ)
			{
				_srcZ += _sz;
				_error += _dx;
				if (_error >= _dz)
				{
					_srcX += _sx;
					_error -= _dz;
				}
				
				_error2 += _dy;
				if (_error2 >= _dz)
				{
					_srcY += _sy;
					_error2 -= _dz;
				}
				
				return true;
			}
			
			return false;
		}
		
		public int x()
		{
			return _srcX;
		}
		
		public int y()
		{
			return _srcY;
		}
		
		public int z()
		{
			return _srcZ;
		}
    }
}