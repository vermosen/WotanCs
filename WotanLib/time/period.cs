using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class period : IComparable
    {
        public enum timeUnit
        {
            days        = 1,
            weeks       = 2,
            months      = 3,
            quarters    = 4,
            years       = 5,
            unknown     = 0
        }

        private int length_;
        private timeUnit unit_;

        // IComparable interface
        public int CompareTo(object obj)
        {
            if (this < (period)obj)
                return -1;
            else if (this == (period)obj)
                return 0;
            else return 1;
        }

        // ctors
        public period(int length, timeUnit unit)
        {
            length_ = length; unit_ = unit;
        }

        public period()
        {
            length_ = 0; unit_ = timeUnit.days;
        }

        public period(string str)
        {
            str = str.ToUpper();
            length_ = int.Parse(str.Substring(0, str.Length - 1));
            string freq = str.Substring(str.Length - 1, 1);
            switch (freq)
            {
                case "D":
                    unit_ = timeUnit.days;
                    break;
                case "W":
                    unit_ = timeUnit.weeks;
                    break;
                case "M":
                    unit_ = timeUnit.months;
                    break;
                case "Q":
                    unit_ = timeUnit.quarters;
                    break;
                case "Y":
                    unit_ = timeUnit.years;
                    break;
                default:
                    throw new ArgumentException("Unknown TimeUnit: " + freq);
            }
        }

        // accessors
        public int length { get { return length_; } }
        public timeUnit unit { get { return unit_; } }

        public void normalize()
        {
            if (length_ != 0)
                switch (unit_)
                {
                    case timeUnit.days:
                        if ((length_ % 7) == 0)
                        {
                            length_ /= 7;
                            unit_ = timeUnit.weeks;
                        }
                        break;
                    case timeUnit.months:
                        if ((length_ % 12) == 0)
                        {
                            length_ /= 12;
                            unit_ = timeUnit.years;
                        }
                        else if ((length_ % 3) == 0)
                        {
                            length_ /= 3;
                            unit_ = timeUnit.quarters;
                        }
                        break;
                    case timeUnit.quarters:
                        if ((length_ % 4) == 0)
                        {
                            length_ /= 4;
                            unit_ = timeUnit.years;
                        }
                        break;
                    case timeUnit.weeks:
                    case timeUnit.years:
                        break;
                    default:
                        throw new ArgumentException("Unknown TimeUnit: " + unit_);
                }
        }

        public static period operator +(period p1, period p2)
        {
            int length_ = p1.length;
            timeUnit units_ = p1.unit;

            if (length_ == 0)
            {
                length_ = p2.length;
                units_ = p2.unit;
            }
            else if (units_ == p2.unit)
            {
                // no conversion needed
                length_ += p2.length;
            }
            else
            {
                switch (units_)
                {
                    case timeUnit.years:
                        switch (p2.unit)
                        {
                            case timeUnit.months:
                                units_ = timeUnit.months;
                                length_ = length_ * 12 + p2.length;
                                break;
                            case timeUnit.quarters:
                                units_ = timeUnit.quarters;
                                length_ = length_ * 4 + p2.length;
                                break;
                            case timeUnit.weeks:
                            case timeUnit.days:
                                if (p1.length != 0)
                                    throw new Exception(
                                          "impossible addition between " + p1 +
                                          " and " + p2);
                                break;
                            default:
                                throw new Exception("unknown time unit ("
                                      + p2.unit + ")");
                        }
                        break;
                    case timeUnit.quarters:
                        switch (p2.unit)
                        {
                            case timeUnit.years:
                                length_ += p2.length * 4;
                                break;
                            case timeUnit.months :
                                length_ += p2.length * 4;
                                break;
                            case timeUnit.weeks:
                            case timeUnit.days:
                                if (p1.length != 0)
                                    throw new Exception(
                                          "impossible addition between " + p1 +
                                          " and " + p2);
                                break;
                            default:
                                throw new Exception("unknown time unit ("
                                      + p2.unit + ")");
                        }
                        break;
                    case timeUnit.months:
                        switch (p2.unit)
                        {
                            case timeUnit.years:
                                length_ += p2.length * 12;
                                break;
                            case timeUnit.weeks:
                            case timeUnit.days:
                                if (p1.length != 0)
                                    throw new Exception(
                                          "impossible addition between " + p1 +
                                          " and " + p2);
                                break;
                            default:
                                throw new Exception("unknown time unit ("
                                      + p2.unit + ")");
                        }
                        break;

                    case timeUnit.weeks:
                        switch (p2.unit)
                        {
                            case timeUnit.days:
                                units_ = timeUnit.days;
                                length_ = length_ * 7 + p2.length;
                                break;
                            case timeUnit.years:
                            case timeUnit.months:
                                if (p1.length != 0)
                                    throw new Exception(
                                          "impossible addition between " + p1 +
                                          " and " + p2);
                                break;
                            default:
                                throw new Exception("unknown time unit ("
                                      + p2.unit + ")");
                        }
                        break;

                    case timeUnit.days:
                        switch (p2.unit)
                        {
                            case timeUnit.weeks:
                                length_ += p2.length * 7;
                                break;
                            case timeUnit.years:
                            case timeUnit.months:
                                if (p1.length != 0)
                                    throw new Exception(
                                          "impossible addition between " + p1 +
                                          " and " + p2);
                                break;
                            default:
                                throw new Exception("unknown time unit ("
                                      + p2.unit + ")");
                        }
                        break;

                    default:
                        throw new Exception("unknown time unit (" + units_ + ")");
                }
            }
            return new period(length_, units_);
        }
        public static period operator -(period p1, period p2)
        {
            return p1 + (-p2);
        }
        public static period operator -(period p) { return new period(-p.length, p.unit); }
        public static period operator *(int n, period p) { return new period(n * p.length, p.unit); }
        public static period operator *(period p, int n) { return new period(n * p.length, p.unit); }
        public static bool operator ==(period p1, period p2)
        {
            if ((object)p1 == null && (object)p2 == null)
                return true;
            else if ((object)p1 == null || (object)p2 == null)
                return false;
            else
                return !(p1 < p2 || p2 < p1);
        }
        public static bool operator !=(period p1, period p2) { return !(p1 == p2); }
        public static bool operator <=(period p1, period p2) { return !(p1 > p2); }
        public static bool operator >=(period p1, period p2) { return !(p1 < p2); }
        public static bool operator >(period p1, period p2) { return p2 < p1; }
        public static bool operator <(period p1, period p2)
        {
            // special cases
            if (p1.length == 0) return (p2.length > 0);
            if (p2.length == 0) return (p1.length < 0);

            // exact comparisons
            if (p1.unit == p2.unit) return p1.length < p2.length;
            if (p1.unit == timeUnit.quarters && p2.unit == timeUnit.years) return p1.length < 4 * p2.length;
            if (p1.unit == timeUnit.years && p2.unit == timeUnit.quarters) return 4 * p1.length < p2.length;
            if (p1.unit == timeUnit.quarters && p2.unit == timeUnit.months) return 3 * p1.length < p2.length;
            if (p1.unit == timeUnit.months && p2.unit == timeUnit.quarters) return p1.length < 3 * p2.length;
            if (p1.unit == timeUnit.months && p2.unit == timeUnit.years) return p1.length < 12 * p2.length;
            if (p1.unit == timeUnit.years && p2.unit == timeUnit.months) return 12 * p1.length < p2.length;
            if (p1.unit == timeUnit.days && p2.unit == timeUnit.weeks) return p1.length < 7 * p2.length;
            if (p1.unit == timeUnit.weeks && p2.unit == timeUnit.days) return 7 * p1.length < p2.length;
            
            // inexact comparisons (handled by converting to days and using limits)
            pair p1lim = new pair(p1), p2lim = new pair(p2);
            if (p1lim.hi < p2lim.lo || p2lim.hi < p1lim.lo)
                return p1lim.hi < p2lim.lo;
            else
                throw new ArgumentException("Undecidable comparison between " + p1.ToString() + " and " + p2.ToString());
        }

        // required by operator <
        struct pair
        {
            public int lo, hi;
            public pair(period p)
            {
                switch (p.unit)
                {
                    case timeUnit.days:
                        lo = hi = p.length; break;
                    case timeUnit.weeks:
                        lo = hi = 7 * p.length; break;
                    case timeUnit.months:
                        lo = 28 * p.length; hi = 31 * p.length; break;
                    case timeUnit.quarters:
                        lo = 90 * p.length; hi = 92 * p.length; break;
                    case timeUnit.years:
                        lo = 365 * p.length; hi = 366 * p.length; break;
                    default:
                        throw new ArgumentException("Unknown TimeUnit: " + p.unit);
                }
            }
        }
        public override bool Equals(object o) { return this == (period)o; }
        public override int GetHashCode() { return 0; }
        public override string ToString()
        {
            string result = "";
            int n = length;
            int m = 0;
            switch (unit)
            {
                case timeUnit.days:
                    if (n >= 7)
                    {
                        m = n / 7;
                        result += m + "W";
                        n = n % 7;
                    }
                    if (n != 0 || m == 0)
                        return result + n + "D";
                    else
                        return result;
                case timeUnit.weeks:
                    return result + n + "W";
                case timeUnit.months:
                    if (n >= 12)
                    {
                        m = n / 12;
                        result += n / 12 + "Y";
                        n = n % 12;
                    }
                    if (n != 0 || m == 0)
                        return result + n + "M";
                    else
                        return result;
                case timeUnit.years:
                    return result + n + "Y";
                default:
                    throw new Exception("unknown time unit (" + unit + ")");
            }
        }
    }
}