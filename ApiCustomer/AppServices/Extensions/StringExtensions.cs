using System;
using System.Linq;

namespace AppServices.Extensions;

public static class StringExtensions
{
    public static string FormatCpf(this string cpf)
    {
        return cpf.Trim().Replace(".", "").Replace(",", "").Replace("-", "");
    }

    public static bool BeValidCpf(this string cpf)
    {
        cpf = cpf.CpfFormatter();

        if (cpf.Length != 11)
            return false;

        if (cpf.All(character => character == cpf.First()))
            return false;

        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string digit;
        int sum;
        int rest;

        sum = 0;

        for (int i = 0; i < 9; i++)
            sum += Convert.ToInt32(cpf[i].ToString()) * multiplier1[i];

        rest = sum % 11;

        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();

        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += Convert.ToInt32(cpf[i].ToString()) * multiplier2[i];

        rest = sum % 11;

        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cpf.EndsWith(digit);
    }        
}