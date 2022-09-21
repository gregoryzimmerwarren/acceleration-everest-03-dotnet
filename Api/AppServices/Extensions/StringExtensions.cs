namespace AppServices.Extensions;

public static class StringExtensions
{
    public static string FormatCpf(this string cpf)
    {
        return cpf.Trim().Replace(".", "").Replace(",", "").Replace("-", "");
    }
}