﻿using Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IHelperWrapper
    {
        public CommonValidationHelper CHelper { get; }

    }
}
