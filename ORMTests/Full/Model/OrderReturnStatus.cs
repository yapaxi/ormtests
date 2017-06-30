using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    public enum OrderReturnStatus
    {
        None = 0,

        Pending = 1,
        Completed = 2,

        BusinessFailed = 3
    }
}
