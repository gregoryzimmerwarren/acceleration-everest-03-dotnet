using System;

namespace AppServices.Extensions;

public static class DateTimeExtensions
{
    public static bool BeOver18(this DateTime dataNascimento)
    {
        return DateTime.Now.Year - dataNascimento.Year >= 18;
    }
}