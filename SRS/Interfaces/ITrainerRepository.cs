﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Models;

namespace SRS.Interfaces
{
    public interface ITrainerRepository
    {
        Trainer GetTrainerByName(string name);
        void AddTrainer(Trainer trainer);
    }
}