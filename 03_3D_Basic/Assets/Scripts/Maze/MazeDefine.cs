using System;

[Flags]
public enum PathDirection : byte
{
    None = 0,   // 0000
    North = 1,  // 0001
    East = 2,   // 0010
    South = 4,  // 0100
    West = 8,   // 1000
}


[Flags]
public enum CornerMask : byte
{
    None = 0,
    NorthWest = 1,
    NorthEast = 2,
    SouthEast = 4,
    SouthWest = 8,
}