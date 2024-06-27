using System;

namespace Helpers
{
    public class LinePointIterator
    {
        // src is moved towards dst in next()
        private int _srcX;
        private int _srcY;
        private readonly int _dstX;
        private readonly int _dstY;

        private readonly long _dx;
        private readonly long _dy;
        private readonly int _sx;
        private readonly int _sy;
        private long _error;

        private bool _first;

        public LinePointIterator(int srcX, int srcY, int dstX, int dstY)
        {
            _srcX = srcX;
            _srcY = srcY;
            _dstX = dstX;
            _dstY = dstY;
            _dx = Math.Abs((long)dstX - srcX);
            _dy = Math.Abs((long)dstY - srcY);
            _sx = srcX < dstX ? 1 : -1;
            _sy = srcY < dstY ? 1 : -1;
            if (_dx >= _dy)
            {
                _error = _dx / 2;
            }
            else
            {
                _error = _dy / 2;
            }

            _first = true;
        }

        public bool Next()
        {
            if (_first)
            {
                _first = false;
                return true;
            }
            if (_dx >= _dy)
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
                    return true;
                }
            }
            else if (_srcY != _dstY)
            {
                _srcY += _sy;
                _error += _dx;
                if (_error >= _dy)
                {
                    _srcX += _sx;
                    _error -= _dy;
                }

                return true;
            }

            return false;
        }

        public int X()
        {
            return _srcX;
        }

        public int Y()
        {
            return _srcY;
        }
    }
}