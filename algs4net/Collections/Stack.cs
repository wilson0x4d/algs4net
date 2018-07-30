using System;

namespace algs4net.Collections
{
    public class Stack<T> :
        Queue<T>,
        IStack<T>
        where T : IComparable<T>
    {
        public Stack()
            : base(QueueType.LIFO)
        {
        }

        public virtual T Pop()
        {
            return base.Dequeue();
        }

        public virtual void Push(T item)
        {
            base.Enqueue(item);
        }
    }
}
