using System;
using System.Collections;
using System.Collections.Generic;

namespace algs4net.Collections
{
    /// <summary>
    /// An implementation of <see cref="IIndex{TKey, TValue}"/> which uses a
    /// Binary Search Tree (BST) internally.
    /// </summary>
    public class BinaryIndex<TKey, TValue> :
        IIndex<TKey, TValue>,
        IBag<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        protected readonly IComparer<TKey> _comparer;

        protected readonly IEqualityComparer<TKey> _equalityComparer;

        protected Node _root;

        public int Count => _root == null ? 0 : _root.Count + 1;

        public BinaryIndex()
            : this(default(IComparer<TKey>))
        {
        }

        public BinaryIndex(IComparer<TKey> comparer)
            : this(comparer, default(IEqualityComparer<TKey>))
        {
        }

        public BinaryIndex(IComparer<TKey> comparer, IEqualityComparer<TKey> equalityComparer)
        {
            _comparer = comparer ?? Comparers<TKey>.DefaultComparer;
            _equalityComparer = equalityComparer ?? Comparers<TKey>.DefaultEqualityComparer;
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                if (TryGet(key, out TValue value))
                {
                    return value;
                }
                throw new Exception("Key not found.");
            }
            set
            {
                Set(key, value);
            }
        }

        public virtual void Add(TKey key, TValue value)
        {
            if (TryGet(key, out TValue existingValue))
            {
                throw new InvalidOperationException("Key already exists.");
            }
            else
            {
                Set(key, value);
            }
        }

        public TKey Ceil(TKey key)
        {
            if (_root == null)
            {
                throw new Exception("Collection is empty.");
            }
            return Ceil(_root as BinaryNode, key).Key;
        }

        public TKey Floor(TKey key)
        {
            if (_root == null)
            {
                throw new Exception("Collection is empty.");
            }
            return Floor(_root as BinaryNode, key).Key;
        }

        public virtual IEnumerator<TKey> GetEnumerator()
        {
            if (_root == null)
            {
                yield break;
            }
            else
            {
                // TODO: non-recursive enumerator (better run time&space)
                foreach (var node in EnumerateRecursive(_root as BinaryNode))
                {
                    yield return node.Key;
                }
            }
        }

        public int IndexOf(TKey key)
        {
            if (_root == null)
            {
                throw new Exception("Collection is empty.");
            }
            return IndexOf(_root as BinaryNode, key);
        }

        public TKey Max()
        {
            if (_root == null)
            {
                throw new Exception("Collection is empty.");
            }
            return Max(_root as BinaryNode).Key;
        }

        public TKey Min()
        {
            if (_root == null)
            {
                throw new Exception("Collection is empty.");
            }
            return Min(_root as BinaryNode).Key;
        }

        public virtual bool TryGet(TKey key, out TValue value)
        {
            return TryGetRecursive(_root as BinaryNode, key, out value);
        }

        public bool TryGetByIndex(int index, out TKey key)
        {
            return TryGetByIndexRecursive(_root as BinaryNode, index, out key);
        }

        public virtual bool TryRemove(TKey key, out TValue value)
        {
            var success = TryRemoveRecursive(
                (_root as BinaryNode),
                key,
                out value,
                out BinaryNode successor);

            if (success && _equalityComparer.Equals(key, _root.Key))
            {
                // if target of removal was the root of the tree, swap
                _root = successor;
            }

            return success;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Set the existing <paramref name="key"/> to the specified
        /// <paramref name="value"/>, or add a new entri to the index.
        /// </summary>
        /// <remarks>
        /// non-recursive because Binary insertion is trivial to implement.
        /// </remarks>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected virtual void Set(TKey key, TValue value)
        {
            var node = new BinaryNode(key, value);
            if (_root == null)
            {
                _root = node;
            }
            else
            {
                var stack = new Stack<BinaryNode>();
                var current = _root as BinaryNode;
                while (true)
                {
                    stack.Push(current);
                    if (_equalityComparer.Equals(key, current.Key))
                    {
                        current.Value = value;
                        return;
                    }
                    if (key.CompareTo(current.Key) < 0)
                    {
                        if (current.Left == null)
                        {
                            current.Left = node;
                            break;
                        }
                        else
                        {
                            current = current.Left;
                        }
                    }
                    else
                    {
                        if (current.Right == null)
                        {
                            current.Right = node;
                            break;
                        }
                        else
                        {
                            current = current.Right;
                        }
                    }
                }
                while (stack.Count > 0)
                {
                    current = stack.Pop();
                    current.Count++;
                }
            }
        }

        private BinaryNode Ceil(BinaryNode node, TKey key)
        {
            return node.Left != null && _comparer.Compare(node.Left.Key, key) >= 0
                ? Ceil(node.Left, key)
                : node.Right == null || _comparer.Compare(node.Key, key) >= 0
                    ? node
                    : Ceil(node.Right, key);
        }

        private IEnumerable<BinaryNode> EnumerateRecursive(BinaryNode node)
        {
            if (node.Left != null)
            {
                foreach (var next in EnumerateRecursive(node.Left))
                {
                    yield return next;
                }
            }
            yield return node;
            if (node.Right != null)
            {
                foreach (var next in EnumerateRecursive(node.Right))
                {
                    yield return next;
                }
            }
        }

        private BinaryNode Floor(BinaryNode node, TKey key)
        {
            return node.Right != null && _comparer.Compare(node.Right.Key, key) <= 0
                ? Floor(node.Right, key)
                : node.Left == null || _comparer.Compare(node.Key, key) >= 0
                    ? node
                    : Floor(node.Left, key);
        }

        private int IndexOf(BinaryNode node, TKey key)
        {
            if (node == null)
            {
                throw new Exception("Key not found.");
            }
            if (_equalityComparer.Equals(key, node.Key))
            {
                return (node.Left != null)
                    ? node.Left.Count + 1
                    : 0;
            }
            var cmp = _comparer.Compare(key, node.Key);
            if (cmp < 0)
            {
                return node.Left != null
                    ? IndexOf(node.Left, key)
                    : IndexOf(node.Right, key);
            }
            else
            {
                return node.Left != null
                    ? node.Left.Count + 2 + IndexOf(node.Right, key)
                    : 1 + IndexOf(node.Right, key);
            }
        }

        private BinaryNode Max(BinaryNode node)
        {
            return (node?.Right == null)
                ? node
                : Max(node.Right);
        }

        private BinaryNode Min(BinaryNode node)
        {
            return (node?.Left == null)
                ? node
                : Min(node.Left);
        }

        private BinaryNode RemoveInternal(BinaryNode node)
        {
            // removal logic separate from search logic for readability and clarity
            if (node.Left != null && node.Right != null)
            {
                var parent = default(BinaryNode);
                var it = node.Right;
                while (it.Left != null)
                {
                    parent = it;
                    it = it.Left;
                }
                it.Left = node.Left;
                if (parent != null)
                {
                    var rnode = it.Right;
                    it.Right = node.Right;
                    parent.Left = rnode;
                }
                return it;
            }
            else if (node.Left != null)
            {
                return node.Left;
            }
            else
            {
                return node.Right;
            }
        }

        private bool TryGetByIndexRecursive(BinaryNode node, int index, out TKey key)
        {
            var precedingCount = node.Left != null
                ? node.Left.Count + 1
                : 0;

            if (precedingCount == index)
            {
                key = node.Key;
                return true;
            }
            else if (precedingCount < index)
            {
                return TryGetByIndexRecursive(node.Right, index - precedingCount - 1, out key);
            }
            else
            {
                return TryGetByIndexRecursive(node.Left, index, out key);
            }
        }

        private bool TryGetRecursive(BinaryNode node, TKey key, out TValue value)
        {
            if (node == null)
            {
                value = default;
                return false;
            }
            if (_equalityComparer.Equals(key, node.Key))
            {
                value = node.Value;
                return true;
            }
            if (_comparer.Compare(key, node.Key) < 0)
            {
                return TryGetRecursive(node.Left, key, out value);
            }
            else
            {
                return TryGetRecursive(node.Right, key, out value);
            }
        }

        private bool TryRemoveRecursive(BinaryNode node, TKey key, out TValue value, out BinaryNode successor)
        {
            if (node == null)
            {
                successor = default;
                value = default;
                return false;
            }

            if (_equalityComparer.Equals(key, node.Key))
            {
                successor = RemoveInternal(node);
                value = node.Value;
                return true;
            }
            else if (_comparer.Compare(key, node.Key) < 0)
            {
                if (TryRemoveRecursive(node.Left, key, out value, out successor))
                {
                    if (successor != null)
                    {
                        node.Left = successor;
                        successor = null;
                    }
                    return true;
                }
            }
            else
            {
                if (TryRemoveRecursive(node.Right, key, out value, out successor))
                {
                    if (successor != null)
                    {
                        node.Right = successor;
                        successor = null;
                    }
                    return true;
                }
            }

            return false;
        }

        protected class BinaryNode :
            Node,
            IComparable<BinaryNode>
        {
            public BinaryNode Left;

            public BinaryNode Right;

            public BinaryNode(TKey key, TValue value)
                : base(key, value)
            {
            }

            public int CompareTo(BinaryNode other)
            {
                return base.CompareTo(other);
            }

            public override string ToString()
            {
                // ie. "key:value[L:(null),R:(null)]"
                return $"{base.ToString()}" +
                    $"[L:{(Left != null ? Convert.ToString(Left.Key) : "")}," +
                    $"R:{(Right != null ? Convert.ToString(Right.Key) : "")}]";
            }
        }

        protected abstract class Node :
            IComparable<Node>
        {
            public readonly TKey Key;

            public int Count;

            public TValue Value;

            public Node(
                TKey key,
                TValue value)
            {
                Key = key;
                Value = value;
            }

            public int CompareTo(Node other)
            {
                return Key.CompareTo(other.Key);
            }

            public override string ToString()
            {
                return $"{Key}:{Value}";
            }
        }
    }
}
