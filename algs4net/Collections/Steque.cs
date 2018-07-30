using System;

namespace algs4net.Collections
{
    public class Steque<T> :
        Queue<T>,
        IQueue<T>,
        IStack<T>
        where T : IComparable<T>
    {
        public Steque() :
            base(QueueType.FIFO)
        {
        }

        public virtual T Pop()
        {
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
            base.Enqueue(value);
        }
    }
}
