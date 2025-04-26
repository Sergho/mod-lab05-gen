namespace generator;

public abstract class Generator
{
	abstract public string getNextPart(string prev);
	abstract public Example getExample(int length);
	public string getString(int length)
	{
		string result = "";
		string lastChar = "";
		for (int i = 0; i < length; i++)
		{
			lastChar = getNextPart(lastChar);
			result += lastChar;
		}
		return result;
	}
}