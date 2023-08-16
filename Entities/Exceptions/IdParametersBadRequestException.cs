﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Exceptions
{
    public class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Parameter ids is null")
        {
        }
    }
}
