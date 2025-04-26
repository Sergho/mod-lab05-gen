using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ScottPlot;

namespace generator;

public class Logger
{
	public string[] Outputs { get; set; }
	public readonly string[] Inputs = { "samples/steady.txt", "samples/bigrams.txt", "samples/words.txt" };
	public Logger(string[] outputs)
	{
		if (outputs.Length != Inputs.Length) throw new Exception("Wrong outputs size");
		Outputs = outputs;
	}
	public void LogTxt(Example[] examples)
	{
		if (examples.Length != Inputs.Length) throw new Exception("Wrong examples size");
		for (int i = 0; i < examples.Length; i++)
		{
			string result = examples[i].String;
			File.WriteAllText($"{Outputs[i]}.txt", result);
			Console.WriteLine($"{Outputs[i]}.txt written");
		}
	}
	public void LogPlot(Example[] examples)
	{
		if (examples.Length != Inputs.Length) throw new Exception("Wrong examples size");

		var expected = GetExpected(examples);
		for (int i = 0; i < examples.Length; i++)
		{
			var stats = examples[i].Stats.Select(stat => stat.Value).ToArray();
			var exp = expected[i].Where(item => examples[i].Stats.ContainsKey(item.Key)).Select(item => item.Value).ToArray();
			double[] positions = Enumerable.Range(0, stats.Length).Select(x => (double)x * 3).ToArray();
			string[] labels = examples[i].Stats.Select(item => item.Key).ToArray();

			Plot plot = new();

			var actualBars = plot.Add.Bars(positions.Select(x => x - 0.5), exp);
			actualBars.LegendText = "Реальная частота";
			actualBars.Color = Colors.Blue;

			var expectedBars = plot.Add.Bars(positions.Select(x => x + 0.5), stats);
			expectedBars.LegendText = "Ожидаемая частота";
			expectedBars.Color = Colors.Red;

			plot.Axes.Bottom.SetTicks(positions.Select(x => x + 0.2).ToArray(), labels);

			plot.Title($"gen-{i} Length: {examples[i].Length}", size: 60);
			plot.YLabel("Частота", size: 40);
			plot.Axes.AutoScale();
			plot.Axes.SetLimitsY(0, plot.Axes.GetLimits().YRange.Max);

			plot.ShowLegend(Alignment.MiddleRight);

			plot.SavePng($"{Outputs[i]}.png", 7000, 500);

			Console.WriteLine($"{Outputs[i]}.png written");
		}
	}
	private SortedDictionary<string, int>[] GetExpected(Example[] examples)
	{
		if (examples.Length != Inputs.Length) throw new Exception("Wrong examples size");

		var expected = new List<SortedDictionary<string, int>>();
		for (int i = 0; i < Inputs.Length; i++)
		{
			string[] lines = File.ReadAllLines(Inputs[i]);
			var dict = new SortedDictionary<string, int>();
			int sum = 0;
			foreach (var line in lines)
			{
				var arr = line.Split(" ");
				int value = int.Parse(arr[1]);
				dict.Add(arr[0], value);
				sum += value;
			}
			foreach (var key in dict.Keys.ToList())
			{
				dict[key] = (int)Math.Round((double)dict[key] / sum * examples[i].Length);
			}
			expected.Add(dict);
		}

		return expected.ToArray();
	}
}