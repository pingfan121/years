using GameDb.Util;
using GameLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocketLib.data
{
    /// <summary>
    /// 双向链表节点类
    /// </summary>
    /// <typeparam name="T">节点中的存放的数据类型</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// 当前节点的数据
        /// </summary>
        T data;
        /// <summary>
        /// 节点中存放的数据
        /// </summary>
        public T Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        /// <summary>
        /// 当前节点的下一个节点
        /// </summary>
        Node<T> next;
        /// <summary>
        /// 下一个节点
        /// </summary>
        public Node<T> Next
        {
            get { return this.next; }
            set { this.next = value; }
        }
        /// <summary>
        /// 当前节点的上一个节点
        /// </summary>
        Node<T> prev;
        /// <summary>
        /// 上一个节点
        /// </summary>
        public Node<T> Prev
        {
            get { return prev; }
            set { prev = value; }
        }
        /// <summary>
        /// 无参构造：数据为默认值，下一个节点为null，上一个节点也为null
        /// </summary>
        public Node()
        {
            this.data = default(T);
            this.next = null;
            this.prev = null;
        }
        /// <summary>
        /// 构造方法：数据为传过来的t，下一个节点为null，上一个节点也为null
        /// </summary>
        /// <param name="t">传入的元素值</param>
        public Node(T t)
        {
            this.data = t;
            this.next = null;
            this.prev = null;
        }
        /// <summary>
        /// 构造方法：数据为t，下一个节点为node
        /// </summary>
        /// <param name="t">传入的元素值</param>
        /// <param name="next">上一个节点</param>
        /// <param name="prev">下一个节点</param>
        public Node(T t, Node<T> next, Node<T> prev)
        {
            this.data = t;
            this.next = next;
            this.prev = prev;
        }

        /// <summary>
        /// 此方法在调试过程中使用，可以删掉
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            T p = this.prev == null ? default(T) : this.prev.data;
            T n = this.next == null ? default(T) : this.next.data;
            string s = string.Format("Data:{0},Prev:{1},Next:{2}", data, p, n);
            return s;
        }
    }
    /// <summary>
    /// 双向链表接口
    /// </summary>
    /// <typeparam name="T">链表中元素的类型</typeparam>
    public interface ILinkList<T>
    {
        void AddFirst(T t);
        void AddLast(T t);
        void Clear();
        int Count { get; }
        Node<T> Head { get; set; }
        Node<T> Tail { get; set; }
        bool IsEmpty { get; }
        void RemoveFirst();
        void RemoveLast();
        void Remove(Node<T> t);
        void Remove(T t);
    }


    /// <summary>
    /// 双向链表操作类
    /// </summary>
    /// <typeparam name="T">链表中元素的类型</typeparam>
    public class LinkList<T> : ILinkList<T>
    {
        static private LogImplement log = LogFactory.getLogger(typeof(LinkList<T>));
        /// <summary>
        /// 链表头节点
        /// </summary>
        Node<T> head;
        Dictionary<T, Node<T>> dics;//保存每个实体对应的节点，减少索引次数

        public LinkList()
        {
            dics = new Dictionary<T, Node<T>>();
        }
        /// <summary>
        /// 链表头节点
        /// </summary>
        public Node<T> Head
        {
            get { return head; }
            set { head = value; }
        }
        /// <summary>
        /// 链表尾节点
        /// </summary>
        Node<T> tail;
        /// <summary>
        /// 链表尾节点
        /// </summary>
        public Node<T> Tail
        {
            get { return tail; }
            set { tail = value; }
        }
        /// <summary>
        /// 链表大小
        /// </summary>
        int size = 0;

        /// <summary>
        /// 添加节点到链表的开头
        /// </summary>
        /// <param name="t">要添加的数据</param>
        public void AddFirst(T t)
        {
            Node<T> node = new Node<T>(t);
            if (dics.ContainsKey(t))
            {
                log.log("AddFirst出错");
            }
            dics[t] = node;
            //如果头为null
            if (head == null)
            {
                //把头节点设置为node
                head = node;
                //因为是空链表，所以头尾一致
                tail = node;
                //大小加一
                size++;
                return;
            }
            //原来头节点的上一个为新节点
            head.Prev = node;
            //新节点的下一个为原来的头节点
            node.Next = head;
            //新头节点为新节点
            head = node;
            //大小加一
            size++;
        }

        /// <summary>
        /// 添加节点到链表的末尾
        /// </summary>
        /// <param name="t">要添加的数据</param>
        public void AddLast(T t)
        {
            Node<T> node = new Node<T>(t);
            if (dics.ContainsKey(t))
            {
                log.log("AddLast出错");
            }
            dics[t] = node;
            //如果头为null
            if (head == null)
            {
                //把头节点设置为node
                head = node;
                //因为是空链表，所以头尾一致
                tail = node;
                //大小加一
                size++;
                return;
            }
            //将原尾节点的下一个设置为新节点
            tail.Next = node;
            //将新节点的上一个设置为原尾节点
            node.Prev = tail;
            //将尾节点重新设置为新节点
            tail = node;
            //大小加一
            size++;
        }

        /// <summary>
        /// 在 current之前插入t
        /// </summary>
        /// <param name="a"></param>
        /// <param name="t"></param>
        public void AddBefore(Node<T> current, T t)
        {
            Node<T> node = new Node<T>(t);
            if (dics.ContainsKey(t))
            {
                log.log("AddBefore出错");
            }
            dics[t] = node;
            //此处非常重要，特别要注意先后次序
            //当前节点的上一个的下一个设置为新节点
            if (current.Prev != null)
            {
                current.Prev.Next = node;
            }
            //新节点的上一个设置为当前节点的上一个
            node.Prev = current.Prev;
            //新节点的下一个设置为当前节点l
            node.Next = current;
            //当前节点的上一个设置为新节点
            current.Prev = node;
            if (current == head)
            {
                head = node;
            }
            //大小加一
            size++;
        }
        /// <summary>
        /// 在 current之后插入t
        /// </summary>
        /// <param name="a"></param>
        /// <param name="t"></param>
        public void AddAfter(Node<T> current, T t)
        {
            Node<T> node = new Node<T>(t);
            if (dics.ContainsKey(t))
            {
                log.log("AddAfter出错");
            }
            dics[t] = node;
            //此处非常重要，特别要注意先后次序
            //当前节点的上一个的下一个设置为新节点
            if (current.Next != null)
            {
                current.Next.Prev = node;
            }
            //新节点的上一个设置为当前节点的上一个
            node.Next = current.Next;
            //新节点的下一个设置为当前节点
            node.Prev = current;
            //当前节点的上一个设置为新节点
            current.Next = node;
            if (tail == current)
            {
                tail = node;
            }
            //大小加一
            size++;
        }

        /// <summary>
        /// 移除头节点
        /// </summary>
        public void RemoveFirst()
        {
            //链表头节点是空的
            if (IsEmpty)
            {
                throw new Exception("链表是空的。");
            }
            //如果size为1，那就是清空链表。
            if (size == 1)
            {
                Clear();
                return;
            }

            //将头节点设为原头结点的下一个节点，就是下一个节点上移
            dics.Remove(head.Data);
            head = head.Next;
            //处理上一步遗留问题，原来的第二个节点的上一个是头结点，现在第二个要变成头节点，那要把它的Prev设为null才能成为头节点
            head.Prev = null;
            //大小减一
            size--;
        }
        /// <summary>
        /// 移除尾节点
        /// </summary>
        public void RemoveLast()
        {
            //链表头节点是空的
            if (IsEmpty)
            {
                throw new Exception("链表是空的。");
            }
            //如果size为1，那就是清空链表。
            if (size == 1)
            {
                Clear();
                return;
            }
            dics.Remove(tail.Data);
            //尾节点设置为倒数第二个节点
            tail = tail.Prev;
            //将新尾节点的Next设为null，表示它是新的尾节点
            tail.Next = null;
            //大小减一
            size--;
        }
        public void Remove(Node<T> current)
        {
            if (current == null)
            {
                return;
            }
            dics.Remove(current.Data);
            //当前节点的上一个的Next设置为当前节点的Next
            if (current.Prev != null)
            {
                current.Prev.Next = current.Next;
            }
            //当前节点的下一个的Prev设置为当前节点的Prev
            if (current.Next != null)
            {
                current.Next.Prev = current.Prev;

            }
            if (current == tail)
            {
                tail = tail.Prev;
            }
            if (current == head)
            {
                head = head.Next;
            }
            //大小减一
            size--;
        }
        public void Remove(T t)
        {
            if (dics.ContainsKey(t))
            {
                Remove(dics[t]);
            }
            else
            {
                //AOI3<T>.log.Error("Remove出错");
            }

        }
        public Node<T> Find(T t)
        {
            if (dics.ContainsKey(t))
            {
                return dics[t];
            }
            return null;
            /*
            Node<T> current = head;
            while (current!=null)
            {
                if (current.Data.Equals(t)) { 
                    return current;
                }
                current = current.Next;
            }
            return null;*/
        }
        /// <summary>
        /// 判断链表是否是空的
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return head == null;
            }
        }
        /// <summary>
        /// 链表中元素的个数
        /// </summary>
        public int Count
        {
            get
            {
                return size;
            }
        }
        /// <summary>
        /// 清除链表中的数据
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            dics.Clear();
            size = 0;
        }
        public override string ToString()
        {
            Node<T> t = head;
            string s = "";
            while (t != null)
            {
                s += t.Data + ",";
                t = t.Next;
            }
            return s;
        }
    }
}
