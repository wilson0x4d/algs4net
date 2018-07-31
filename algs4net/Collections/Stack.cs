using System;

namespace algs4net.Collections
{
    public class Stack<T> :
        Queue<T>,
        IStack<T>
        where T : IComparable<T>
    {
#if DEBUG
        protected ulong _pops = 0L;

        protected ulong _pushes = 0L;
#endif

        public Stack()
            : base(QueueType.LIFO)
        {
        }

        public virtual T Pop()
        {
#if DEBUG
            _pops++;
#endif
            return base.Dequeue();
        }

        public virtual void Push(T item)
        {
#if DEBUG
            _pushes++;
#endif
            base.Enqueue(item);
        }

#if DEBUG
        public override string ToString()
        {
            return $"{base.ToString()}, pushes:{_pushes}, pops:{_pops}";
        }
#endif
    }
}
