using System;
using System.Data;

namespace Flab2Fab.App_Data
{
    public interface IWeightDataAccess
    {
        DataTable GetRecentWeightForAllUsers();
        DataTable GetWeightsByUserId(string userId);
        DataTable InsertWeight(string userId, decimal weight, DateTime date);
        DataTable GetSimpleLeaderBoard();

    }
}
