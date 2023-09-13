using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flab2Fab.App_Data
{
    public interface IUserDataAccess
    {
        decimal GetStartingWeight(string userId);
        DataTable GetUpdateableUserInfo(string userId);
        DataTable UpdateUserInfo(string userId, string name, string phoneNumber, bool isAnonymous, bool isPaid);
    }
}
