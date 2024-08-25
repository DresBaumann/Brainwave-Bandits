namespace BrainwaveBandits.WinerR.Application.Helpers;
public static class StringExtensions
{
    public static bool InvariantEquals(this string? a, string? b)
    {
        return string.Equals(a, b, StringComparison.InvariantCultureIgnoreCase);
    }   

    public static bool InvariantContains(this string? a, string? b)
    {
        if (a == null || b == null)
        {
            return false;
        }

        return a?.IndexOf(b, StringComparison.InvariantCultureIgnoreCase) >= 0;
    }
}
