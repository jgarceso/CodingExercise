﻿using Application.Dtos;
using Domain.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInvestmentPerformanceService
    {
        List<UserInvestment> GetInvestmentsListByUserId(int userId);
        List<object> GetInvestmentDetailsById(int investmentId);
    }
}
