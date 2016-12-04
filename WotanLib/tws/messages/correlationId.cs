using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wotan
{
    public class correlationId<T> : IEquatable<correlationId<T> > where T : IEquatable<T>
    {
        public correlationId(T id)
        {
            this.id = id;
        }

        public T id { get; private set; }

        public bool Equals(correlationId<T> other)
        {
            if (other == null)
                return false;

            if (id.Equals(other.id))
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            correlationId<T> corrObj = obj as correlationId<T>;
            if (corrObj == null)
                return false;
            else
                return Equals(corrObj);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public static bool operator ==(correlationId<T> id1, correlationId<T> id2)
        {
            if (((object)id1) == null || ((object)id2) == null)
                return Equals(id1, id2);

            return id1.Equals(id2);
        }

        public static bool operator !=(correlationId<T> id1, correlationId<T> id2)
        {
            if (((object)id1) == null || ((object)id2) == null)
                return !Equals(id1, id2);

            return !(id1.Equals(id2));
        }
    }
}
