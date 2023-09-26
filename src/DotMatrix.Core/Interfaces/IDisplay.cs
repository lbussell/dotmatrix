namespace DotMatrix.Core;

public delegate void NewFrameDelegate(byte[] frameData);

public interface IDisplay
{
    /*
         Each GameBoy pixel can be one of 4 shades: white, light gray, dark gray, and black. We will represent this with
         4 bits. This means we can fit 2 pixels into each byte. So, we need an array of bytes divided by 2.

         private byte[] _displayData =
            new byte[DotMatrixConsoleSpecs.DisplaySize.Y * DotMatrixConsoleSpecs.DisplaySize.X / 2];
     */

    public NewFrameDelegate OnNewFrame { get; }

    public void SetPixel();

    public void RequestRefresh();

    public void WaitForRefresh();
}
