using System;
using System.Collections.Generic;
using System.IO;

namespace generator;

public class WordGenerator : Generator
{
	public Dictionary<string, int> Data { get; private set; } = new();
	public int Size { get; private set; } = 0;
	private Random random = new Random();
	public WordGenerator()
	{
		var lines = File.ReadAllLines("samples/words.txt");
		foreach (var line in lines)
		{
			var args = line.Split(" ");
			string word = args[0];
			int probability = int.Parse(args[1]);

			Data.Add(word, probability);
			Size += probability;
		}
	}

	public override string getNextPart(string prev)
	{
		string prefix = "";
		if (prev != "") prefix = " ";

		int num = random.Next(0, Size);
		int current = 0;
		foreach (var key in Data.Keys)
		{
			current += Data[key];
			if (num <= current) return $"{prefix}{key}";
		}

		return "";
	}
	public override Example getExample(int length)
	{
		var stats = new SortedDictionary<string, double>();
		string result = getString(length);
		var words = result.Split(" ");
		foreach (string word in words)
		{
			if (stats.ContainsKey(word))
				stats[word]++;
			else
				stats.Add(word, 1);
		}

		return new Example { String = result, Stats = stats, Length = length };
	}
}