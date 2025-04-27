using generator;

namespace ProjCharGenerator.Tests;

public class SteadyGeneratorTests
{
    [Fact]
    public void SizeTest()
    {
        var generator = new SteadyGenerator();
        Assert.Equal(33, generator.Size);
    }

    [Fact]
    public void DataSizeTest()
    {
        var generator = new SteadyGenerator();
        Assert.Equal(33, generator.Data.Length);
    }

    [Fact]
    public void PartLengthTest()
    {
        var generator = new SteadyGenerator();
        Assert.Equal(1, generator.getNextPart("").Length);
    }

    [Fact]
    public void StringLengthTest()
    {
        var generator = new SteadyGenerator();
        Assert.Equal(1000, generator.getString(1000).Length);
    }

    [Fact]
    public void StatsSizeTest()
    {
        var generator = new SteadyGenerator();
        int sum = 0;
        foreach (int statValue in generator.getExample(1000).Stats.Values)
        {
            sum += statValue;
        }
        Assert.Equal(1000, sum);
    }
}
