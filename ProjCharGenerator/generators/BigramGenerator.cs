using System;
using System.Collections.Generic;
using System.IO;

namespace generator;

public class BigramGenerator : Generator
{
	private Dictionary<char, Dictionary<char, int>> data = new();
	private Dictionary<char, int> sizes = new();
	private int totalSize
	{
		get
		{
			int sum = 0;
			foreach (var size in sizes.Values)
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

			if (data.ContainsKey(bigram[0]))
			{
				data[bigram[0]].Add(bigram[1], probability);
				sizes[bigram[0]] += probability;
			}
			else
			{
				data.Add(bigram[0], new Dictionary<char, int>() { { bigram[1], probability } });
				sizes.Add(bigram[0], probability);
			}
		}
	}

	public override string getNextPart(string prev)
	{
		if (prev == "")
		{
			int num = random.Next(0, totalSize);
			int current = 0;
			foreach (var item in data.Values)
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
			int num = random.Next(0, sizes[(char)prev[0]]);
			int current = 0;
			foreach (var key in data[(char)prev[0]].Keys)
			{
				current += data[(char)prev[0]][key];
				if (num <= current) return $"{key}";
			}
		}
		return "";
	}
	public override Example getExample(int length)
	{
		var stats = new SortedDictionary<string, double>();
		string result = getString(length + 1);
		for (int i = 0; i < length - 1; i++)
		{
			string bigram = $"{result[i]}{result[i + 1]}";
			if (stats.ContainsKey(bigram))
				stats[bigram]++;
			else
				stats.Add(bigram, 1);
		}

		return new Example { String = result, Stats = stats };
	}
}