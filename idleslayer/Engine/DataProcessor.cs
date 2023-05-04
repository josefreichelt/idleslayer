namespace idleslayer;

public class DataProcessor
{
    public static float ExponentialGrowth(float value, float time, float rate = 0.15f)
    {
        return (float)(value * Math.Pow((1 + rate), time));
    }
}
