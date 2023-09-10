namespace DotMatrix.Core;

public sealed class NoDisplay : IDisplay
{
    public NewFrameDelegate OnNewFrame { get; } = _ => { };

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
