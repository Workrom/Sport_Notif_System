﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS
{
    public interface IClientRepository
    {
        Client GetClientByName(string name);

    }
}