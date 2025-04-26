using System;
using System.Collections.Generic;
using System.Linq;

namespace generator;

public class SteadyGenerator : Generator
{
	private string syms = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя";
	private char[] data;
	private int size;
	private Random random = new Random();
	public SteadyGenerator()
	{
		size = syms.Length;
		data = syms.ToCharArray();
	}

	public override char getNextChar(char? prev)
	{
		return data[random.Next(0, size)];
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

		return new Example { String = result, Stats = stats };
	}
}