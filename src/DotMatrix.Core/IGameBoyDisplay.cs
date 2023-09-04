// <copyright file="IGameBoyDisplay.cs" company="PlaceholderCompany">Copyright (c) PlaceholderCompany. All rights reserved.</copyright>

namespace DotMatrix.Core;

public delegate void NewFrameDelegate(byte[] frameData);

public interface IGameBoyDisplay
{
    public NewFrameDelegate OnNewFrame { get; }
    public void SetPixel();
    public void RequestRefresh();
    public void WaitForRefresh();
}
