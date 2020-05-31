namespace ProductService.Infrastructure.Helpers
{
    public static class ArrayDeconstructExtensions
    {
        public static void Deconstruct<T>(this T[] items, out T? item0, out T? item1) where T : class
        {
            item0 = items.Length > 0 ? items[0] : null;
            item1 = items.Length > 1 ? items[1] : null;
        }
    }
}
