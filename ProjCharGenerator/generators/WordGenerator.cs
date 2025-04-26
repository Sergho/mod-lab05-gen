using System;
using System.Collections.Generic;
using System.IO;

namespace generator;

public class WordGenerator : Generator
{
	private Dictionary<string, int> data = new();
	private int size = 0;
	private Random random = new Random();
	public WordGenerator()
	{
		var lines = File.ReadAllLines("samples/words.txt");
		foreach (var line in lines)
		{
			var args = line.Split(" ");
			string word = args[0];
			int probability = int.Parse(args[1]);

			data.Add(word, probability);
			size += probability;
		}
	}

	public override string getNextPart(string prev)
	{
		string prefix = "";
		if (prev != "") prefix = " ";

		int num = random.Next(0, size);
		int current = 0;
		foreach (var key in data.Keys)
		{
			current += data[key];
			if (num <= current) return $"{prefix}{key}";
		}

		return "";
	}
	public override Example getExample(int length)
	{
		var stats = new SortedDictionary<string, double>();
		string result = getString(length + 1);
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