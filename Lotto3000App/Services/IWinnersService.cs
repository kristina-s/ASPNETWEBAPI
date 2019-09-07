using DataAccess;
using DataModels;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IWinnersService
    {
        IEnumerable<WinnerModel> GetWinners(int sessionId);

    }
}
