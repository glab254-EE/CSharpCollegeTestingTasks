namespace _25_KT3
{
    public class Book<T>
    {
        public T ID { get; private set; }
        public string BookName { get; private set; }
        public string AuthorName { get; private set; }
        public int PagesCount { get; private set; }
        public override string ToString()
        {
            return $"{ID} {BookName}, {AuthorName}, {PagesCount} pages";
        }
        public Book(T id, string bookName, string authorName, int pagesCount)
        {
            ID = id;
            BookName = bookName;
            AuthorName = authorName;
            PagesCount = pagesCount;
        }
    }
}
