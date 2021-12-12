using System;

namespace VigenerCipher
{
    class Vigener
    {
        private static string alphabetRus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private static string alphabetRusUpper = alphabetRus.ToUpper();

        private static int FindTargetIndex(char ch, char key)
        {
            int keyShift = alphabetRus.IndexOf(char.ToLower(key));
            if (keyShift != -1)
                return (alphabetRus.IndexOf(char.ToLower(ch)) + keyShift) % 33;
            else
                return alphabetRus.IndexOf(ch);
        }

        public static string Encrypt(string text, string key)
        {
            string result = "";
            for (int i = 0, j = 0; i < text.Length; i++)
            {
                if (alphabetRus.Contains(text[i]))
                {
                    result += alphabetRus[FindTargetIndex(text[i], key[j++ % key.Length])];
                }
                else if (alphabetRusUpper.Contains(text[i]))
                {
                    result += alphabetRusUpper[FindTargetIndex(text[i], key[j++ % key.Length])];
                }
                else
                {
                    result += text[i].ToString();
                }
            }

            return result;
        }

        public static string Decrypt(string text, string key)
        {
            key = key.ToLower();
            string modKey = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (alphabetRus.Contains(key[i]))
                {
                    int temp = alphabetRus.IndexOf(key[i]) - 1;
                    int index = temp < 0 ? temp + 33 : temp;
                    modKey += alphabetRus[32 - index];

                }
                else
                    modKey += key[i];
            }

            return Encrypt(text, modKey);
        }
    }
}
