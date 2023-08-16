﻿using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Exceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid employeeId) : base($"The employee with id: { employeeId }doesn't exits in the database.")
        {
        }
    }
}
