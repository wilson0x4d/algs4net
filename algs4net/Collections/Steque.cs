using System;

namespace algs4net.Collections
{
    public class Steque<T> :
        Queue<T>,
        IQueue<T>,
        IStack<T>
        where T : IComparable<T>
    {
#if DEBUG
        protected ulong _pops = 0L;

        protected ulong _pushes = 0L;
#endif

        public Steque() :
            base(QueueType.FIFO)
        {
        }

        public virtual T Pop()
        {
#if DEBUG
            _pops++;
#endif
            if (_head == null)
            {
                throw new RankException("Collection contained no elements.");
            }

            var node = _head.Previous;
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            if (_head == node)
            {
                _head = null;
            }

            return node.Value;
        }

        public virtual void Push(T value)
        {
#if DEBUG
            _pushes++;
#endif
            base.Enqueue(value);
        }

#if DEBUG
        public override string ToString()
        {
            return $"{base.ToString()}, pushes:{_pushes}, pops:{_pops}";
        }
#endif
    }
}
