using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExtensions;
namespace CustomExtensions
{
    public static class DateTimeExtension
    {
        public static DateTime endOfMonth(this DateTime d)
        {
            return d.AddDays(-d.Day).AddDays(DateTime.DaysInMonth(d.Year, d.Month));
        }
        public static DateTime advance(this DateTime d, Wotan.period p)
        {
            switch (p.unit)
            {
                case Wotan.period.timeUnit.days:
                    return d.AddDays(p.length);
                case Wotan.period.timeUnit.weeks:
                    return d.AddDays(p.length * 7);
                case Wotan.period.timeUnit.months:
                    return d.AddMonths(p.length);
                case Wotan.period.timeUnit.quarters:
                    return d.AddMonths(p.length * 3);
                case Wotan.period.timeUnit.years:
                    return d.AddYears(p.length);
                default:
                    throw new ArgumentException("Unknown time unit: " + p.unit);
            }
        }
    }
}

namespace Wotan
{
    public class calendar
    {
        public enum businessDayConvention
        {
            following = 1,
            modifiedFollowing = 2,
            preceding = 3,
            unadjusted = 4,
            unknown = 0
        }

        protected calendar innerCalendar_;

        public calendar innerCalendar
        {
            get { return innerCalendar_; }
            set { innerCalendar_ = value; }
        }

        public calendar() {}
        public calendar(calendar inner) { innerCalendar_ = inner; }
        public virtual string name() { return innerCalendar_.name(); }
        public virtual bool isBusinessDay(DateTime t)
        {
            return innerCalendar_.isBusinessDay(t);
        }
        public virtual bool isWeekend(DayOfWeek w)
        {
            return innerCalendar_.isWeekend(w);
        }
        public bool empty()
        {
            return innerCalendar_ == null;
        }
        public bool isHoliday(DateTime d)
        {
            return !isBusinessDay(d);
        }
        public DateTime adjust(DateTime d, businessDayConvention c = businessDayConvention.following)
        {
            return innerCalendar_.adjust(d, c);
        }
        public bool isEndOfMonth(DateTime d)
        {
            return (d.Month != adjust(d.AddDays(1)).Month);
        }

        public DateTime endOfMonth(DateTime d)
        {
            return adjust(d.endOfMonth(), businessDayConvention.preceding);
        }
        public DateTime previousBusinessDay(DateTime d, uint nDays = 1)
        {
            for (uint i = 0; i < nDays; i++)
            {
                d = d.AddDays(-1.0);

                while (!this.isBusinessDay(d))
                {
                    d = d.AddDays(-1.0);
                }
            }

            return d;
        }
        public DateTime nextBusinessDay(DateTime d, uint nDays = 1)
        {
            for (uint i = 0; i < nDays; i++)
            {
                d = d.AddDays(1);

                while (!this.isBusinessDay(d))
                {
                    d = d.AddDays(1);
                }
            }

            return d;
        }

        public class westernImpl : calendar
        {
            public westernImpl() { }
            public westernImpl(calendar c) : base(c) { }

            private int[] easterMonday_ = {

            };

            public override bool isWeekend(DayOfWeek w)
            {
                return (w == DayOfWeek.Saturday || w == DayOfWeek.Sunday);
            }

            public int easterMonday(int year) { return easterMonday_[year - 1901]; }
        }
    }
}
