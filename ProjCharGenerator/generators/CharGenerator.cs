using System.Collections.Generic;
using System.Linq;

namespace generator;

public abstract class CharGenerator
{
	abstract public char getNextChar(char? prev);
	abstract public Example getExample(int length);
	public string getString(int length)
	{
		string result = "";
		char? lastChar = null;
		for (int i = 0; i < length; i++)
		{
			lastChar = getNextChar(lastChar);
			result += lastChar;
		}
		return result;
	}
}