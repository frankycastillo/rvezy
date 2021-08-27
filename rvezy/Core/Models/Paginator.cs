namespace rvezy.Core.Models
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

        public static Paginator Single() => new Paginator { Page = 1, Size = 1 };
        public static Paginator Small() => new Paginator {Page = 1, Size = 100};
        public static Paginator Medium() => new Paginator {Page = 1, Size = 1000};
        public static Paginator All() => new Paginator {Page = 1, Size = 1000000};
    }
}