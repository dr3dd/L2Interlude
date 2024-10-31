using System;
using System.IO;
using System.Threading.Tasks;

namespace Network;

public abstract class ServerPacket
{
    private readonly MemoryStream _stream = new();

    protected void WriteBytesArray(byte[] value)
    {
        _stream.Write(value);
    }
        
    protected Task WriteBytesArrayAsync(byte[] value)
    {
        return _stream.WriteAsync(value, 0, value.Length);
    }
        
    protected Task WriteBytesArrayAsync(byte[] value, int offset, int length)
    {
        return _stream.WriteAsync(value, offset, length);
    }

    protected void WriteBytesArray(byte[] value, int offset, int length)
    {
        _stream.Write(value, offset, length);
    }

    protected void WriteInt(uint value = 0)
    {
        WriteBytesArray(BitConverter.GetBytes(value));
    }

    protected void WriteInt(int value = 0)
    {
        WriteBytesArray(BitConverter.GetBytes(value));
    }
        
    protected Task WriteIntAsync(int value = 0)
    {
        return WriteBytesArrayAsync(BitConverter.GetBytes(value));
    }

    protected void WriteInt(double value = 0.0)
    {
        WriteBytesArray(BitConverter.GetBytes((int)value));
    }
        
    protected Task WriteIntAsync(double value = 0.0)
    {
        return WriteBytesArrayAsync(BitConverter.GetBytes((int)value));
    }

    protected void WriteShort(ushort value = 0)
    {
        WriteBytesArray(BitConverter.GetBytes(value));
    }

    protected void WriteShort(short value = 0)
    {
        WriteBytesArray(BitConverter.GetBytes(value));
    }

    protected void WriteShort(int value = 0)
    {
        WriteBytesArray(BitConverter.GetBytes((short)value));
    }

    public void WriteShort(bool value)
    {
        WriteShort(value ? 1 : 0);
    }

    protected Task WriteShortAsync(int value = 0)
    {
        return WriteBytesArrayAsync(BitConverter.GetBytes((short)value));
    }
    
    protected Task WriteShortAsync(bool value)
    {
        return WriteShortAsync(value ? 1 : 0);
    }

    protected void WriteByte(byte value)
    {
        _stream.WriteByte(value);
    }

    protected void WriteByte(bool value = false)
    {
        _stream.WriteByte((byte)(value.Equals(true) ? 1 : 0));
    }

    protected void WriteByte(int value)
    {
        _stream.WriteByte((byte)value);
    }
    
    protected Task WriteByteAsync(byte value)
    {
        return _stream.WriteAsync(new byte[] { value }, 0, 1);
    }
    
    protected Task WriteByteAsync(bool value = false)
    {
        return _stream.WriteAsync(new byte[] { (byte)(value.Equals(true) ? 1 : 0) }, 0, 1);
    }
    
    protected Task WriteByteAsync(int value)
    {
        return _stream.WriteAsync(new byte[] { (byte)value }, 0, 1);
    }

    protected void WriteDouble(double value)
    {
        WriteBytesArray(BitConverter.GetBytes(value));
    }
        
    protected Task WriteDoubleAsync(double value)
    {
        return WriteBytesArrayAsync(BitConverter.GetBytes(value));
    }

    protected void WriteString(string value)
    {
        if (value != null)
            WriteBytesArray(System.Text.Encoding.Unicode.GetBytes(value));

        _stream.WriteByte(0);
        _stream.WriteByte(0);
    }
    
    protected async Task WriteStringAsync(string value)
    {
        if (value != null)
        {
            var bytes = System.Text.Encoding.Unicode.GetBytes(value);
            await WriteBytesArrayAsync(bytes);
        }
        await WriteByteAsync(0);
        await WriteByteAsync(0);            
    }
        
    protected void WriteLong(long value)
    {
        WriteBytesArray(BitConverter.GetBytes(value));
    }
        
    protected Task WriteLongAsync(long value)
    {
        return WriteBytesArrayAsync(BitConverter.GetBytes(value));
    }

    public byte[] ToByteArray()
    {
        return _stream.ToArray();
    }

    public byte[] GetBuffer()
    {
        return _stream.GetBuffer();
    }

    public long Length => _stream.Length;

    public abstract Task WriteAsync();
}