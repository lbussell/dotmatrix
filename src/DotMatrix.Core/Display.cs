namespace DotMatrix.Core;

public class Display : IDisplay
{
    public NewFrameDelegate OnNewFrame { get; }

    /*
     * Each GameBoy pixel can be one of 4 shades: white, light gray, dark gray, and black. We will represent this with
     * 4 bits. This means we can fit 2 pixels into each byte. So, we need an array of bytes divided by 2.
     */
    private byte[] _displayData = new byte[DotMatrixConsoleSpecs.DisplaySize.Y * DotMatrixConsoleSpecs.DisplaySize.X / 2];

    public Display(NewFrameDelegate onNewFrame)
    {
        OnNewFrame = onNewFrame;
    }

    public void SetPixel()
    {
    }

    public void RequestRefresh()
    {
    }

    public void WaitForRefresh()
    {
    }
}
