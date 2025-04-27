using System;
using System.Collections.Generic;
using System.IO;

namespace generator;

public class BigramGenerator : Generator
{
	public Dictionary<char, Dictionary<char, int>> Data { get; private set; } = new();
	public Dictionary<char, int> Sizes { get; private set; } = new();
	public int TotalSize
	{
		get
		{
			int sum = 0;
			foreach (var size in Sizes.Values)
			{
				sum += size;
			}
			return sum;
		}
	}
	private Random random = new Random();
	public BigramGenerator()
	{
		var lines = File.ReadAllLines("samples/bigrams.txt");
		foreach (var line in lines)
		{
			var args = line.Split(" ");
			string bigram = args[0];
			int probability = int.Parse(args[1]);

			if (Data.ContainsKey(bigram[0]))
			{
				Data[bigram[0]].Add(bigram[1], probability);
				Sizes[bigram[0]] += probability;
			}
			else
			{
				Data.Add(bigram[0], new Dictionary<char, int>() { { bigram[1], probability } });
				Sizes.Add(bigram[0], probability);
			}
		}
	}

	public override string getNextPart(string prev)
	{
		if (prev == "")
		{
			int num = random.Next(0, TotalSize);
			int current = 0;
			foreach (var item in Data.Values)
			{
				foreach (var key in item.Keys)
				{
					current += item[key];
					if (num <= current) return $"{key}";
				}
			}
		}
		else
		{
			int num = random.Next(0, Sizes[(char)prev[0]]);
			int current = 0;
			foreach (var key in Data[(char)prev[0]].Keys)
			{
				current += Data[(char)prev[0]][key];
				if (num <= current) return $"{key}";
			}
		}
		return "";
	}
	public override Example getExample(int length)
	{
		var stats = new SortedDictionary<string, double>();
		string result = getString(length + 1);
		for (int i = 0; i < length; i++)
		{
			string bigram = $"{result[i]}{result[i + 1]}";
			if (stats.ContainsKey(bigram))
				stats[bigram]++;
			else
				stats.Add(bigram, 1);
		}

		return new Example { String = result, Stats = stats, Length = length };
	}
}