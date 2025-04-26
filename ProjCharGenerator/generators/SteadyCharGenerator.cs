using System;

namespace generator;

public class SteadyCharGenerator : CharGenerator
{
	private string syms = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя";
	private char[] data;
	private int size;
	private Random random = new Random();
	public SteadyCharGenerator()
	{
		size = syms.Length;
		data = syms.ToCharArray();
	}

	public override char getNextChar(char? prev)
	{
		return data[random.Next(0, size)];
	}
}