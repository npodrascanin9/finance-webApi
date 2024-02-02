namespace FinanceAPI.Shared.Extensions
{
    public static class ValidationExtensions
    {
        public static bool HasText(this string text)
        {
            return !text.IsEmpty();
        }

        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
    }
}
