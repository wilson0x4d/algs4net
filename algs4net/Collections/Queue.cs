using System;

namespace algs4net.Collections
{
    public class Queue<T> :
        LinkedList<T>,
        IQueue<T>
        where T : IComparable<T>
    {
        protected readonly QueueType _queueType;

#if DEBUG
        protected ulong _dequeues = 0L;

        protected ulong _enqueues = 0L;
#endif

        public Queue()
            : this(QueueType.FIFO)
        {
        }

        public Queue(QueueType queueType)
        {
            _queueType = queueType;
        }

        public virtual T Dequeue()
        {
            if (_head == null)
            {
                throw new RankException("Collection contained no elements.");
            }
            Node node;
            if (_queueType == QueueType.LIFO)
            {
                node = _head.Previous;
                if (node == _head)
                {
                    _head = null;
                }
                else
                {
                    node.Previous.Next = node.Next;
                    node.Next.Previous = node.Previous;
                }
            }
            else
            {
                node = _head;
                if (_count == 1)
                {
                    _head = null;
                }
                else
                {
                    _head = node.Next;
                    _head.Previous = node.Previous;
                    _head.Previous.Next = _head;
                }
            }
            _count--;
#if DEBUG
            _enqueues++;
#endif
            return node.Value;
        }

        public virtual void Enqueue(T value)
        {
#if DEBUG
            _enqueues++;
#endif
            base.Add(value);
        }

#if DEBUG
        public override string ToString()
        {
            return $"queue:{_queueType}, {base.ToString()}, enqueues:{_enqueues}, dequeues:{_dequeues}";
        }
#endif

        public enum QueueType
        {
            LIFO,
            FIFO
        }
    }
}
