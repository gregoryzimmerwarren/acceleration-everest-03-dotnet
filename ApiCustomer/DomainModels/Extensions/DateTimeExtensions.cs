﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Extensions;

public static class DateTimeExtensions
{
    public static bool BeOver18(this DateTime dataNascimento)
    {
        if (DateTime.Now.Year - dataNascimento.Year == 18)
        {
            return true;
        }

        return false;
    }
}
