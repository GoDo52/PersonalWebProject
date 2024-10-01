﻿using Personal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.DataAccess.Repository.IRepository
{
    public interface ISpendingRepository : IRepository<Spending>
    {
        void Update(Spending obj);
    }
}
