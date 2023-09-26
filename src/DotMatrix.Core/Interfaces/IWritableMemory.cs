// <copyright file="IWritableMemory.cs" company="PlaceholderCompany">Copyright (c) PlaceholderCompany. All rights reserved.</copyright>

namespace DotMatrix.Core;

public interface IWritableMemory
{
    public void Write8(byte data);

    public void Write16(byte data);
}
