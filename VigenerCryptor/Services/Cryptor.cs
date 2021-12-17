using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VigenerCryptor.Services
{
    public class Cryptor : ICrypting
    {
        public static Dictionary<string, string> alphabets = new Dictionary<string, string>()
        {
            { "ru", "абвгдеёжзийклмнопрстуфхцчшщъыьэюя"},
            { "eng", "abcdefghijklmnopqrstuvwxyz" }
        };
        
        private string Alphabet { get; set; }
        private string AlphabetUpper { get; set; }

        public Cryptor(string alphabet)
        {
            Alphabet = alphabet;
            AlphabetUpper = alphabet.ToUpper();
            
        }

        private int FindTargetIndex(char ch, char key)
        {
            int keyShift = Alphabet.IndexOf(char.ToLower(key));
            if (keyShift != -1)
                return (Alphabet.IndexOf(char.ToLower(ch)) + keyShift) % Alphabet.Length;
            else
                return Alphabet.IndexOf(ch);
        }

        public async Task<string> Encrypt(string text, string key)
        {
            string result = "";

            await Task.Run(() =>
            {
                for (int i = 0, j = 0; i < text.Length; i++)
                {
                    if (Alphabet.Contains(text[i]))
                    {
                        result += Alphabet[FindTargetIndex(text[i], key[j++ % key.Length])];
                    }
                    else if (AlphabetUpper.Contains(text[i]))
                    {
                        result += AlphabetUpper[FindTargetIndex(text[i], key[j++ % key.Length])];
                    }
                    else
                    {
                        result += text[i].ToString();
                    }
                }
            });

            return result;
        }
        
        public async Task<string> Decrypt(string text, string key)
        {
            key = key.ToLower();
            string modKey = "";
            
            await Task.Run(() =>
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (Alphabet.Contains(key[i]))
                    {
                        int temp = Alphabet.IndexOf(key[i]) - 1;
                        int index = temp < 0 ? temp + Alphabet.Length : temp;
                        modKey += Alphabet[(Alphabet.Length - 1) - index];
                    }
                }
            });

            return await Encrypt(text, modKey);
        }
    }
}
