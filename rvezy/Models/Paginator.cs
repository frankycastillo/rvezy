namespace rvezy.Models
{
    public interface IPaginator
    {
        int Page { get; set; }

        int Size { get; set; }

        int Index { get; }
    }

    public class Paginator : IPaginator
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public int Index => (Page - 1) * Size;
    }
}