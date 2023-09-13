namespace OOP.DesignPatterns
{
    /**
     * The Iterator design pattern provides a way to access the elements of an aggregate (tổng hợp) object sequentially(tuần tự) without exposing 
     * its underlying (adj) ['ʌndə,laiiη] cơ bản representation.
     * 
     * Frequency (N - Tính thường xuyên) of use: high (Tần suất sử dụng)
     */
    /**
     * The classes and objects participating in this pattern include:
        1. Iterator  (AbstractIterator)
        defines an interface for accessing and traversing elements. (duyệt từng phần tử)
        2. ConcreteIterator  (Iterator)
        implements the Iterator interface.
        keeps track of the current position in the traversal of the aggregate. (theo dõi vị trí hiện tại trong quá trình truyền tải)
        3. Aggregate  (AbstractCollection)
        defines an interface for creating an Iterator object
        4. ConcreteAggregate  (Collection)
        implements the Iterator creation interface to return an instance of the proper ['prɔpə] (adj - đúng, thích hợp) ConcreteIterator
     */
    public interface IIterator
    {
        Iterator CreateIterator();
    }

    public class Item
    {
        string name;
        public Item(string name)
        {
            this.name = name;
        }

        public string Name { 
            get { return name;  }
        }
    }

    public class Iterator: IIterator 
    {
        List<Item> items = new List<Item>();
        public Iterator CreateIterator()
        {
            return new Iterator();
        }

        public int Count
        {
            get { return items.Count;  }
        }
    }
}
