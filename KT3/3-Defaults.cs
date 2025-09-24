namespace _25_KT3
{
    public class GenerlizedClass<T>
    {
        public T Value { get; private set; }
        public GenerlizedClass(T value)
        {
            Value = value;
        }
        public void Reset()
        {
            if (default(T) != null)
            {
                Value = default;
            }
        }
    }
}
