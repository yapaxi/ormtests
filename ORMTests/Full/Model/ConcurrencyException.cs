using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1.MarketplaceManagement.OrderReturnService.DataAccess.Model
{
    [Serializable]
    public class DbConcurrencyException : Exception
    {
        public DbConcurrencyException() { }
        public DbConcurrencyException(string message) : base(message) { }
        public DbConcurrencyException(string message, Exception inner) : base(message, inner) { }
        protected DbConcurrencyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
