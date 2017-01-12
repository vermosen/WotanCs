using System;
using System.Collections.Generic;

namespace Wotan
{
    public interface iFactory<K, T> where K : IComparable
    {
        T create(K key);
    }

    public interface iFactoryElement
    {
        object _new();
    }

    public class factoryElement<T> : iFactoryElement where T : new()
    {
        public object _new()
        {
            return new T();
        }
    }
    public class factory<K, T> : iFactory<K, T> where K : IComparable
    {
        Dictionary<K, iFactoryElement> map_ = new Dictionary<K, iFactoryElement>();

        public void add<V>(K key) where V : T, new()
        {
            map_.Add(key, new factoryElement<V>());
        }

        public T create(K key)
        {
            if (map_.ContainsKey(key))
            {
                return (T)map_[key]._new();
            }
            else throw new ArgumentException();

        }
    }
}
