using System.Text;

namespace PUB.Domain.Entities.Validations.Docs
{
    public static class Utils
    {
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = new StringBuilder();
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber.Append(s);
                }
            }
            return onlyNumber.ToString().Trim();
        }
    }
}
