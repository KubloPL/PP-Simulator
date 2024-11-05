namespace Simulator;

public static class Validator
{
    public static int Limiter(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    public static string Shortener(string value, int min, int max, char placeholder)
    {
        value = value.Trim();

        if (value.Length < min)
        {
            value = value.PadRight(min, placeholder);
        }
        else if (value.Length > max)
        {
            value = value.Substring(0, max).TrimEnd();
        }

        if (value.Length < min)
        {
            value = value.PadRight(min, placeholder);
        }

        if (char.IsLower(value[0]))
        {
            value = char.ToUpper(value[0]) + value.Substring(1);
        }

        return value;
    }
}