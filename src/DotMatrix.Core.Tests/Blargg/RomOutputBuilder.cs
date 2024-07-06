using System.Text;

namespace DotMatrix.Core.Tests.Blargg;

internal class RomOutputBuilder(
    CancellationTokenSource cancellationTokenSource,
    string success = "Passed",
    string failure = "Failed")
{
    private readonly StringBuilder _inner = new();
    private readonly CancellationTokenSource _cancellationTokenSource = cancellationTokenSource;
    private readonly string _success = success;
    private readonly string _failure = failure;

    public RomOutputBuilder Append(string? value)
    {
        _inner.Append(value);
        string currentOutput = _inner.ToString();

        if (currentOutput.Contains(_success)
            || currentOutput.Contains(_failure))
        {
            _cancellationTokenSource.Cancel();
        }

        return this;
    }

    public override string ToString()
    {
        return _inner.ToString();
    }
}
