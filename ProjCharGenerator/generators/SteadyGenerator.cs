using System;
using System.Collections.Generic;

namespace generator;

public class SteadyGenerator : Generator
{
	private readonly string syms = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя";
	public char[] Data { get; private set; }
	public int Size { get; private set; }
	private Random random = new Random();
	public SteadyGenerator()
	{
		Size = syms.Length;
		Data = syms.ToCharArray();
	}

	public override string getNextPart(string prev)
	{
		return $"{Data[random.Next(0, Size)]}";
	}
	public override Example getExample(int length)
	{
		var stats = new SortedDictionary<string, double>();
		string result = getString(length);
		for (int i = 0; i < length; i++)
		{
			if (stats.ContainsKey($"{result[i]}"))
				stats[$"{result[i]}"]++;
			else
				stats.Add($"{result[i]}", 1);
		}

		return new Example { String = result, Stats = stats, Length = length };
	}
}