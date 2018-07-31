using System;

namespace algs4net.Collections
{
    public class Stack<T> :
        LinkedList<T>,
        IStack<T>
        where T : IComparable<T>
    {
#if DEBUG
        protected ulong _pops = 0L;

        protected ulong _pushes = 0L;
#endif

        public virtual T Pop()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("Collection contained no elements.");
            }
            Node node = _head.Previous;
            if (node == _head)
            {
                _head = null;
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
            }
            _count--;
#if DEBUG
            _pops++;
#endif
            return node.Value;
        }

        public virtual void Push(T value)
        {
#if DEBUG
            _pushes++;
#endif
            base.Add(value);
        }

#if DEBUG

        public override string ToString()
        {
            return $"{base.ToString()}, pushes:{_pushes}, pops:{_pops}";
        }

#endif
    }
}
