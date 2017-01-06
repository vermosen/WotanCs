using System;

namespace Wotan
{
    public class correlation<T> : IEquatable<correlation<T> > where T : IEquatable<T>
    {
        public correlation(T id)
        {
            this.id = id;
        }

        public T id { get; private set; }

        public bool Equals(correlation<T> other)
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

            correlation<T> corrObj = obj as correlation<T>;
            if (corrObj == null)
                return false;
            else
                return Equals(corrObj);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public static bool operator ==(correlation<T> id1, correlation<T> id2)
        {
            if (((object)id1) == null || ((object)id2) == null)
                return Equals(id1, id2);

            return id1.Equals(id2);
        }

        public static bool operator !=(correlation<T> id1, correlation<T> id2)
        {
            if (((object)id1) == null || ((object)id2) == null)
                return !Equals(id1, id2);

            return !(id1.Equals(id2));
        }
    }
}
