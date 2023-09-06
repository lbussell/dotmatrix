namespace DotMatrix.Core;

public class Bus
{
    public Cartridge Cartridge { get; init;  }
    public Memory Memory { get; init;  }

    public byte this[ushort addr]
    {
        get =>
            addr switch
            {
                _ => throw new NotImplementedException()
            };
        set
        {
            throw new NotImplementedException();
            switch (addr)
            {
            }
        }
    }
}
