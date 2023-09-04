namespace DotMatrix.Core;

public readonly record struct Vec2Int
{
    public int X { get; init; }
    public int Y { get; init; }

    public int Height => Y;
    public int Width => X;

    public static Vec2Int operator +(Vec2Int a, Vec2Int b) => new()
        {
            X = a.X + b.X,
            Y = a.Y + b.Y,
        };

    public static Vec2Int operator -(Vec2Int a, Vec2Int b) => new()
        {
            X = a.X - b.X,
            Y = a.Y - b.Y,
        };

    public static Vec2Int operator *(Vec2Int a, int b) => new()
        {
            X = a.X * b,
            Y = a.Y * b,
        };

    public static Vec2Int operator /(Vec2Int a, int b) => new()
        {
            X = a.X / b,
            Y = a.Y / b,
        };
}
