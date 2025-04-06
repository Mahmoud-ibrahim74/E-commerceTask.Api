using System.Text;

namespace E_commerceTask.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string GenerateUsernameFromEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Invalid email format", nameof(email));

            var firstSideofEmail = email.Split('@')[0];
            var username = firstSideofEmail + "_" + new Random().Next(1, 1000);
            return username;
        }
        /// <summary>
        /// Generates a random alphanumeric code of specified length.
        /// </summary>
        /// <param name="length">The length of the code to generate.</param>
        /// <returns>A random alphanumeric code as a string.</returns>
        public static string GenerateRandomCode(this string value, int length)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder code = new();
            Random random = new();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characters.Length);
                code.Append(characters[index]);
            }
            return code.ToString();
        }
        public static string ConvertQuestionOptions(this string[] options)
        {
            string result = "[" + string.Join(", ", options.Select(n => $"\"{n}\"")) + "]";
            return result;
        }
    }
}
