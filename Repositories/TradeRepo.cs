using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M7_Class_37_Work_01.Models;
using Microsoft.EntityFrameworkCore;

namespace M7_Class_37_Work_01.Repositories
{
    public class TradeRepo : ITradeRepo
    {
        CourseDbContext db;
        public TradeRepo(CourseDbContext db)
        {
            this.db = db;
        }

        public List<Trade> Get()
        {
            return db.Trades.ToList();
        }
        public List<Trade> GetWithChildred()
        {
            return db.Trades
                 .Include(x => x.Courses)
                 .ThenInclude(y => y.Students)
                 .ToList();
        }
        public Trade Get(int id)
        {
            return db.Trades
                .Include(x=> x.Courses)
                .FirstOrDefault(x => x.TradeId == id);
        }
        public bool Insert(Trade trade)
        {
            db.Trades.Add(trade);
            return db.SaveChanges() > 0;
        }

        public bool Update(Trade trade, bool childIncluded=false)
        {
           // db.Entry(trade).State = EntityState.Modified;
            var orignal = db.Trades.Include(x=> x.Courses).First(x => x.TradeId == trade.TradeId);
            orignal.TradeName = trade.TradeName;
            orignal.Description = trade.Description;
            orignal.FemaleAllowed = trade.FemaleAllowed;
            orignal.IndustrialAttachment = trade.IndustrialAttachment;
            if(trade.Courses != null && trade.Courses.Count > 0)
            {
                var courses = trade.Courses.ToArray();
                for (var i=0; i< courses.Length; i++)
                {
                    var temp =orignal.Courses.FirstOrDefault(c => c.CourseId == courses[i].CourseId);
                    if(temp != null)
                    {
                        temp.CourseName = courses[i].CourseName;
                        temp.Duration = courses[i].Duration;
                        
                    }
                    else
                    {
                        orignal.Courses.Add(courses[i]);
                    }
                }
                foreach(var c in orignal.Courses)
                {
                    var temp = trade.Courses.FirstOrDefault(t => t.CourseId == c.CourseId);
                    if (temp == null)
                        db.Entry(c).State = EntityState.Deleted;
                }
            }
           
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Entry(new Trade { TradeId = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }
    }
}
