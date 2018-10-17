public class MinMax
{
    public float Min { private set; get; }
    public float Max { private set; get; }

    public MinMax()
    {
        this.Reset();
    }

    public void AddValue(float value)
    {
        if (value < Min)
            Min = value;

        if (value > Max)
            Max = value;
    }

    public void Reset()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
    }
}