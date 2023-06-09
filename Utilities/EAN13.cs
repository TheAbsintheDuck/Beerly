namespace Backend_Task03.Utilities
{
    public class EAN13
    {

        public static string GenerateEAN13()
        {
            string productCode = GenerateUniqueProductCode();
            string ean13 = productCode;
            int checkDigit = CalculateEAN13CheckDigit(ean13);
            return ean13 + checkDigit.ToString();
        }

        private static string GenerateUniqueProductCode()
        {
            var random = new Random();
            var productCode = new char[12];

            for (int i = 0; i < productCode.Length; i++)
            {
                productCode[i] = (char)('0' + random.Next(0, 10));
            }

            return new string(productCode);
        }


        private static int CalculateEAN13CheckDigit(string ean13)
        {
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                int digit = int.Parse(ean13[i].ToString());

                if (i % 2 == 0)
                {
                    sum += digit;
                }
                else
                {
                    sum += digit * 3;
                }
            }

            int checkDigit = 10 - (sum % 10);
            return checkDigit == 10 ? 0 : checkDigit;
        }
    }
}