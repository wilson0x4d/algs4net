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

        public int Count => _root == null ? 0 : _root.Count;

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
                if (TryGetValue(key, out TValue value))
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
            if (TryGetValue(key, out TValue existingValue))
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
            if (_root != null)
            {
                foreach (var node in Enumerate(_root as BinaryNode))
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

        public bool TryGetByIndex(int index, out TKey key)
        {
            if (TryGetByIndex(_root as BinaryNode, index, out BinaryNode result))
            {
                key = result.Key;
                return true;
            }
            else
            {
                key = default;
                return false;
            }
        }

        public bool TryGetRange(TKey from, TKey to, out IEnumerable<TKey> keys)
        {
            // NOTE: there are a few ways to implement this call, this
            // variant is sufficiently fast without being excessively complex
            var lo = IndexOf(from);
            var hi = IndexOf(to);
            var inc = (hi >= lo) ? 1 : -1;
            var len = (inc * (hi - lo)) + 1;
            if (len == 0)
            {
                keys = default;
                return false;
            }
            var L_keys = new TKey[len];
            var k = 0;
            for (int i = lo; i <= hi; i += inc)
            {
                TryGetByIndex(i, out TKey key);
                L_keys[k] = key;
                k++;
            }
            keys = L_keys;
            return true;
        }

        public bool TryGetRangeValues(TKey from, TKey to, out IEnumerable<TValue> values)
        {
            // NOTE: there are a few ways to implement this call, this
            // variant is sufficiently fast without being excessively complex
            var lo = IndexOf(from);
            var hi = IndexOf(to);
            var inc = (hi >= lo) ? 1 : -1;
            var len = (inc * (hi - lo)) + 1;
            if (len == 0)
            {
                values = default;
                return false;
            }
            var L_values = new TValue[len];
            var k = 0;
            for (int i = lo; i <= hi; i += inc)
            {
                TryGetValueByIndex(i, out TValue value);
                L_values[k] = value;
                k++;
            }
            values = L_values;
            return true;
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            if (TryGetRecursive(_root as BinaryNode, key, out BinaryNode result))
            {
                value = result.Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public virtual bool TryGetValueByIndex(int index, out TValue value)
        {
            if (TryGetByIndex(_root as BinaryNode, index, out BinaryNode result))
            {
                value = result.Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
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
                    var cmp = key.CompareTo(current.Key);
                    if (cmp < 0)
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
                    else if (cmp > 0)
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
                    else
                    {
                        current.Value = value;
                        return;
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

        private IEnumerable<BinaryNode> Enumerate(BinaryNode node)
        {
            var stack = new Stack<BinaryNodeEnumeratorState>();
            var state = new BinaryNodeEnumeratorState { Node = node, Direction = BinaryNodeEnumeratorState.EnumeratorStage.Explore };
            while (state != null)
            {
                node = state.Node;
                if (state.Direction == BinaryNodeEnumeratorState.EnumeratorStage.Yield)
                {
                    yield return node;
                }
                else
                {
                    do
                    {
                        if (node.Right != null)
                        {
                            stack.Push(new BinaryNodeEnumeratorState { Node = node.Right, Direction = BinaryNodeEnumeratorState.EnumeratorStage.Explore });
                        }
                        stack.Push(new BinaryNodeEnumeratorState { Node = node, Direction = BinaryNodeEnumeratorState.EnumeratorStage.Yield });
                        node = node.Left;
                    } while (node != null);
                }
                state = stack.Count > 0
                    ? stack.Pop()
                    : null;
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
            var cmp = _comparer.Compare(key, node.Key);
            if (cmp < 0)
            {
                return node.Left != null
                    ? IndexOf(node.Left, key)
                    : IndexOf(node.Right, key);
            }
            else if (cmp > 0)
            {
                return node.Left != null
                    ? node.Left.Count + 1 + IndexOf(node.Right, key)
                    : 1 + IndexOf(node.Right, key);
            }
            else
            {
                return (node.Left != null)
                    ? node.Left.Count
                    : 0;
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
            if (node.Right == null)
            {
                return node.Left;
            }
            else if (node.Left == null)
            {
                return node.Right;
            }
            else
            {
                var parent = default(BinaryNode);
                var it = node.Right;
                while (it.Left != null)
                {
                    it.Count--;
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
                it.Count = 1;
                if (it.Left != null)
                {
                    it.Count += it.Left.Count;
                }
                if (it.Right != null)
                {
                    it.Count += it.Right.Count;
                }
                return it;
            }
        }

        private bool TryGetByIndex(BinaryNode node, int index, out BinaryNode target)
        {
            while (true)
            {
                var precedingCount = node.Left != null
                    ? node.Left.Count
                    : 0;
                if (precedingCount == index)
                {
                    target = node;
                    return true;
                }
                else if (precedingCount < index)
                {
                    index = index - precedingCount - 1;
                    node = node.Right;
                }
                else
                {
                    node = node.Left;
                }
            }
        }

        private bool TryGetRecursive(BinaryNode node, TKey key, out BinaryNode target)
        {
            if (node == null)
            {
                target = default;
                return false;
            }
            var cmp = _comparer.Compare(key, node.Key);
            if (cmp < 0)
            {
                return TryGetRecursive(node.Left, key, out target);
            }
            else if (cmp > 0)
            {
                return TryGetRecursive(node.Right, key, out target);
            }
            else
            {
                target = node;
                return true;
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

            var cmp = _comparer.Compare(key, node.Key);
            if (cmp < 0)
            {
                var org = node.Left.Count;
                if (TryRemoveRecursive(node.Left, key, out value, out successor))
                {
                    node.Count -= org;
                    node.Left = successor;
                    if (node.Left != null)
                    {
                        node.Count += node.Left.Count;
                    }
                    successor = node;
                    return true;
                }
            }
            else if (cmp > 0)
            {
                var org = node.Right.Count;
                if (TryRemoveRecursive(node.Right, key, out value, out successor))
                {
                    node.Count -= org;
                    node.Right = successor;
                    if (node.Right != null)
                    {
                        node.Count += node.Right.Count;
                    }
                    successor = node;
                    return true;
                }
            }
            else
            {
                successor = RemoveInternal(node);
                value = node.Value;
                return true;
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
                Count = 1;
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

        /// <summary> A state helper so we can avoid stack fill && spill
        /// during in-order enumeration. </summary>
        private sealed class BinaryNodeEnumeratorState :
            IComparable<BinaryNodeEnumeratorState>
        {
            public EnumeratorStage Direction { get; set; }

            public BinaryNode Node { get; set; }

            public int CompareTo(BinaryNodeEnumeratorState other)
            {
                return Node.Key.CompareTo(other.Node.Key);
            }

            internal enum EnumeratorStage
            {
                /// <summary>
                /// Explore the Node on Next Step
                /// </summary>
                Explore,

                /// <summary>
                /// Yield the Node on Next Step
                /// </summary>
                Yield
            }
        }
    }
}
