using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace generator;

public abstract class CharGenerator
{
	abstract public char getNextChar(char? prev);
	public Example getExample(int length)
	{
		var stats = new SortedDictionary<char, double>();
		string result = "";
		char? lastChar = null;
		for (int i = 0; i < length; i++)
		{
			char ch = getNextChar(lastChar);
			result += ch;

			if (stats.ContainsKey(ch))
				stats[ch]++;
			else
				stats.Add(ch, 1);

			lastChar = ch;
		}

		foreach (var key in stats.Keys.ToList())
		{
			stats[key] = stats[key] / length;
		}

		return new Example { String = result, Stats = stats };
	}
}