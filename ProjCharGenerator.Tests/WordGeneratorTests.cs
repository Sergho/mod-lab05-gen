using generator;

namespace ProjCharGenerator.Tests;

public class WordGeneratorTests
{
	[Fact]
	public void InitTest()
	{

		var exception = Record.Exception(() => { var generator = new WordGenerator(); });
		Assert.Null(exception);
	}

	[Fact]
	public void CountTest()
	{
		var lines = File.ReadAllLines("samples/words.txt");
		var generator = new WordGenerator();
		int counter = 0;
		foreach (var key in generator.Data.Keys)
		{
			counter++;
		}
		Assert.Equal(lines.Length, counter);
	}

	[Fact]
	public void SizeTest()
	{
		var generator = new WordGenerator();
		int sum = 0;
		foreach (var probability in generator.Data.Values)
		{
			sum += probability;
		}
		Assert.Equal(generator.Size, sum);
	}

	[Fact]
	public void PartTest()
	{
		var generator = new WordGenerator();
		Assert.NotEqual(' ', generator.getNextPart("")[0]);
		Assert.Equal(' ', generator.getNextPart("и")[0]);
	}

	[Fact]
	public void StringLengthTest()
	{
		var generator = new WordGenerator();
		Assert.Equal(1000, generator.getString(1000).Split(" ").Length);
	}

	[Fact]
	public void StatsSizeTest()
	{
		var generator = new WordGenerator();
		int sum = 0;
		foreach (int statValue in generator.getExample(1000).Stats.Values)
		{
			sum += statValue;
		}
		Assert.Equal(1000, sum);
	}
}
