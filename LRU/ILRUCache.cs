namespace LRU
{
    public interface ILRUCache
    {
        string Display();
        object Get(int key);
        int GetCapacity();
        void Put(int key, object value);
    }
}