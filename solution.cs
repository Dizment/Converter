using System;
using System.Globalization;
using System.Text;

namespace NumeralSystems
{
    public static class Converter
    {
        public static string GetPositiveOctal(this int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("exception", nameof(number));
            }

            StringBuilder str = new StringBuilder();

            while (number != 0)
            {
                str.Append(number % 8);
                number = number / 8;
            }

            for (int i = 0; i < str.Length / 2; i++)
            {
                char tmp = str[i];
                str[i] = str[str.Length - i - 1];
                str[str.Length - i - 1] = tmp;
            }

            if (string.IsNullOrEmpty(str.ToString()))
            {
                return "0";
            }

            return str.ToString();
        }

        public static string GetPositiveDecimal(this int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("exception", nameof(number));
            }

            return number.ToString(CultureInfo.GetCultureInfo("en-US"));
        }

        public static string GetPositiveHex(this int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("exception", nameof(number));
            }

            StringBuilder str = new StringBuilder();
            char[] hex = "0123456789ABCDEF".ToCharArray();
            while (number != 0)
            {
                for (int i = 0; i < hex.Length; i++)
                {
                    if (number % 16 == i)
                    {
                        str.Append(hex[i]);
                        break;
                    }
                }

                number = number / 16;
            }

            for (int i = 0; i < str.Length / 2; i++)
            {
                char tmp = str[i];
                str[i] = str[str.Length - i - 1];
                str[str.Length - i - 1] = tmp;
            }

            if (string.IsNullOrEmpty(str.ToString()))
            {
                return "0";
            }

            return str.ToString();
        }

        public static string GetPositiveRadix(this int number, int radix)
        {
            if ((radix != 10 && radix != 16 && radix != 8) || number < 0)
            {
                throw new ArgumentException("exception", nameof(number));
            }

            switch (radix)
            {
                case 10:
                    return GetPositiveDecimal(number);
                case 8:
                    return GetPositiveOctal(number);
                case 16:
                    return GetPositiveHex(number);
                default:
                    return string.Empty;
            }
        }

        public static string GetRadix(this int number, int radix)
        {
            if (radix != 10 && radix != 16 && radix != 8)
            {
                throw new ArgumentException("exception", nameof(number));
            }

            if (number < 0)
            {
                StringBuilder bin = new StringBuilder(GetBinary(number));
                StringBuilder sum = new StringBuilder();

                int sumConv = 0;
                int k = 0;

                for (int i = bin.Length - 1; i >= 0; i--)
                {
                    if (bin[i] == '1')
                    {
                        sumConv = sumConv + (int)Math.Pow(2, k);
                    }

                    k++;

                    if ((k == 3 && radix == 8) || (k == 4 && radix == 16))
                    {
                        sum.Append(GetPositiveRadix(sumConv, radix));
                        sumConv = 0;
                        k = 0;
                    }

                    if (i == 1 && radix == 8)
                    {
                        sumConv = 0;
                        k = 0;
                        string qwerty = "0" + bin[0] + bin[1];

                        for (int j = qwerty.Length - 1; j >= 0; j--)
                        {
                            if (qwerty[j] == '1')
                            {
                                sumConv = sumConv + (int)Math.Pow(2, k);
                            }

                            k++;
                        }

                        sum.Append(GetPositiveRadix(sumConv, radix));
                        break;
                    }
                }

                for (int i = 0; i < sum.Length / 2; i++)
                {
                    char tmp = sum[i];
                    sum[i] = sum[sum.Length - i - 1];
                    sum[sum.Length - i - 1] = tmp;
                }

                return sum.ToString();
            }
            else
            {
                return GetPositiveRadix(number, radix);
            }
        }

        public static string GetBinary(int number)
        {
            StringBuilder str = new StringBuilder();

            while (number != 0)
            {
                str.Append(Math.Abs(number % 2));
                number = number / 2;
            }

            while (str.Length != 32)
            {
                str.Append(0);
            }

            str[str.Length - 1] = '1';

            for (int i = 0; i < str.Length / 2; i++)
            {
                char tmp = str[i];
                str[i] = str[str.Length - i - 1];
                str[str.Length - i - 1] = tmp;
            }

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '1')
                {
                    str[i] = '0';
                }
                else
                {
                    str[i] = '1';
                }
            }

            for (int i = str.Length - 1; i > 0; i--)
            {
                if (str[i] == '0')
                {
                    str[i] = '1';
                    break;
                }

                str[i] = '0';
            }

            return str.ToString();
        }
    }
}
