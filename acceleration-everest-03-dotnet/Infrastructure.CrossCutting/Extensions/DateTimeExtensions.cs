using System;

namespace Infrastructure.CrossCutting.Extensions;

public static class DateTimeExtensions
{
    public static bool IsOver18(this DateTime dataNascimento)
    {
        if(DateTime.Now.Year - dataNascimento.Year >= 18)
            if (DateTime.Now.DayOfYear - dataNascimento.DayOfYear >= 0)
                return true;
        return false;
    }
}