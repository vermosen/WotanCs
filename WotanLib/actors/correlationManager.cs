using System;
using Akka.Actor;

namespace Wotan.actors
{
    public abstract class correlationManagerBase<T> : TypedActor where T : IEquatable<T>, new()
    {
        public abstract correlationId<T> next();
    }

    public class correlationManager : correlationManagerBase<int>, ISingleton<correlationManager>
    {
        private static int previous_ = 0;

        private static correlationManager instance_;

        public correlationManager instance()
        {
            if (instance_ == null)
            {
                instance_ = new correlationManager();
            }

            return instance_;
        }

        public override correlationId<int> next()
        {
            return instance().nextImpl();
        }

        private correlationId<int> nextImpl()
        {
            return new correlationId<int>(previous_ + 1);
        }
    }
}
