using generator;

namespace ProjCharGenerator.Tests;

public class BigramGeneratorTests
{
	[Fact]
	public void InitTest()
	{

		var exception = Record.Exception(() => { var generator = new BigramGenerator(); });
		Assert.Null(exception);
	}

	[Fact]
	public void CountTest()
	{
		var lines = File.ReadAllLines("samples/bigrams.txt");
		var generator = new BigramGenerator();
		int counter = 0;
		foreach (var item in generator.Data.Values)
		{
			foreach (var key in item.Keys)
			{
				counter++;
			}
		}
		Assert.Equal(lines.Length, counter);
	}

	[Fact]
	public void SizesTest()
	{
		var generator = new BigramGenerator();
		int sum = 0;
		foreach (var size in generator.Sizes.Values)
		{
			sum += size;
		}
		Assert.Equal(generator.TotalSize, sum);
	}

	[Fact]
	public void PartLengthTest()
	{
		var generator = new BigramGenerator();
		Assert.Equal(1, generator.getNextPart("").Length);
	}

	[Fact]
	public void StringLengthTest()
	{
		var generator = new BigramGenerator();
		Assert.Equal(1000, generator.getString(1000).Length);
	}

	[Fact]
	public void StatsSizeTest()
	{
		var generator = new BigramGenerator();
		int sum = 0;
		foreach (int statValue in generator.getExample(1000).Stats.Values)
		{
			sum += statValue;
		}
		Assert.Equal(999, sum);
	}
}
