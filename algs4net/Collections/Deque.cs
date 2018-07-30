using System;

namespace algs4net.Collections
{
    public class Deque<T> :
        LinkedList<T>
        where T : IComparable<T>
    {
        public Deque()
        {
        }

        public virtual T PopLeft()
        {
            var item = _head;
            if (item == null)
            {
                throw new RankException("Collection has no elements.");
            }

            item.Previous.Next = item.Next;
            item.Next.Previous = item.Previous;

            _head = item.Next;
            _count--;

            return item.Value;
        }

        public virtual T PopRight()
        {
            var item = _head?.Previous ?? _head;
            if (item == null)
            {
                throw new RankException("Collection has no elements.");
            }

            item.Previous.Next = item.Next;
            item.Next.Previous = item.Previous;

            if (item == _head)
            {
                _head = null;
            }

            _count--;

            return item.Value;
        }

        public virtual void PushLeft(T item)
        {
            if (_head == null)
            {
                _head = new Node { Value = item };
                _head.Next = _head;
                _head.Previous = _head;
            }
            else
            {
                var next = _head;
                _head = new Node { Value = item, Previous = next.Previous, Next = next };
                next.Previous.Next = _head;
                next.Previous = _head;
            }
            _count++;
        }

        public virtual void PushRight(T item)
        {
            if (_head == null)
            {
                _head = new Node { Value = item };
                _head.Next = _head;
                _head.Previous = _head;
            }
            else
            {
                var node = new Node { Value = item, Previous = _head.Previous, Next = _head };
                _head.Previous.Next = node;
                _head.Previous = node;
            }
            _count++;
        }
    }
}
