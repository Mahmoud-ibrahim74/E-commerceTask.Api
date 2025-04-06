namespace E_commerceTask.Shared.Helpers
{
    public class NumberHelper
    {
        /// <summary>
        /// Formats the view count as a short, human-readable string.
        /// For example:
        /// - 999 will be displayed as "999"
        /// - 1,500 will be displayed as "1.5K"
        /// - 1,000,000 will be displayed as "1M"
        /// 
        /// Values greater than or equal to 1,000 will be shortened with "K" (thousands),
        /// and values greater than or equal to 1,000,000 will be shortened with "M" (millions).
        /// </summary>
        /// <param name="viewCount">The total number of views to be formatted.</param>
        /// <returns>A formatted string representing the number of views.</returns>
        public static string FormatViewCount(int viewCount)
        {
            if (viewCount >= 1000000)
            {
                return (viewCount / 1000000D).ToString("0.#") + "M"; // Millions
            }
            else if (viewCount >= 1000)
            {
                return (viewCount / 1000D).ToString("0.#") + "K"; // Thousands
            }
            else
            {
                return viewCount.ToString(); // Less than 1000, show the exact number
            }
        }
    }
}
