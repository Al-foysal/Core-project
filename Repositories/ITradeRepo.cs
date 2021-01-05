using M7_Class_37_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M7_Class_37_Work_01.Repositories
{
    public interface ITradeRepo
    {
        List<Trade> Get();
        List<Trade> GetWithChildred();
        Trade Get(int id);
        bool Insert(Trade trade);
        bool Update(Trade trade, bool childIncluded=false);
        bool Delete(int id);
    }
}
