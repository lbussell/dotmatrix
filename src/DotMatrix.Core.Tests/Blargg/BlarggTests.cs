namespace DotMatrix.Core.Tests.Blargg;

public class BlarggTests
{
    private const string BlarggTestDataDir = "Blargg/Roms";

    public static IEnumerable<object[]> GetBlarggTestData() =>
    [
        ["cpu_instrs/individual/01-special.gb"],
        ["cpu_instrs/individual/02-interrupts.gb"],
        ["cpu_instrs/individual/03-op sp,hl.gb"],
        ["cpu_instrs/individual/04-op r,imm.gb"],
        ["cpu_instrs/individual/05-op rp.gb"],
        ["cpu_instrs/individual/06-ld r,r.gb"],
        ["cpu_instrs/individual/07-jr,jp,call,ret,rst.gb"],
        ["cpu_instrs/individual/08-misc instrs.gb"],
        ["cpu_instrs/individual/09-op r,r.gb"],
        ["cpu_instrs/individual/10-bit ops.gb"],
        ["cpu_instrs/individual/11-op a,(hl).gb"],
    ];

    [Theory]
    [MemberData(nameof(GetBlarggTestData))]
    public void CpuInstrs(string romPath) => ExecuteTest(GetBlarggRomData(romPath));

    private static void ExecuteTest(byte[] rom)
    {
        CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        RomOutputBuilder romOutputBuilder = new(cancellationTokenSource);
        DotMatrixConsole console =
            DotMatrixConsole.CreateInstance(rom, null, LoggingType.Serial, s => romOutputBuilder.Append(s));
        console.Run(cancellationToken);
        string output = romOutputBuilder.ToString();
        output.Should().Contain("Passed");
    }

    private static byte[] GetBlarggRomData(string romName) =>
        GetRomDataInternal(Path.Combine(BlarggTestDataDir, romName));

    private static byte[] GetRomDataInternal(string romPath) =>
        File.ReadAllBytes(romPath);
}
