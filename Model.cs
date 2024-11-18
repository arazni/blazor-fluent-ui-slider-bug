namespace SliderTest;

public class Model
{
	public int DataValue { get; set; }

	public const int MaxValue = 3;

	public void Reset()
	{
		DataValue = 0;
		Console.WriteLine("Data Value: " + DataValue);
	}

	public void ChangeBy2()
	{
		if (DataValue < 2)
			DataValue += 2;
		else
			DataValue -= 2;

		Console.WriteLine("Data Value: " + DataValue);
	}
}
