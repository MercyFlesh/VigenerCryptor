using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenerCryptor.Services;
using System.Reflection;
using System.Threading.Tasks;

namespace VigenerCryptorTests
{
    [TestClass]
    public class CryptorTest
    {
        public static string alphabetRus = Cryptor.alphabets["ru"];
        [TestMethod]
        public void TestFindTargetIndex_CorrectKeySymbol()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);
            
            int index = (int)findTargetInfo.Invoke(new Cryptor(Cryptor.alphabets["ru"]), new object[] { 'а', 'д'});
            int target = 4;

            string errorMessage = $"Incorrect value: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public void TestFindTargetIndex_InorrectKeySymbol()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);

            int index = (int)findTargetInfo.Invoke(new Cryptor(alphabetRus), new object[] { 'е', 'w' });
            int target = 5;

            string errorMessage = $"Incorrect value whith not included key in alphabet: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public void TestFindTargetIndex_OutOfRangeAlphabet()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);
            
            int index = (int)findTargetInfo.Invoke(new Cryptor(alphabetRus), new object[] { 'е', 'ю' });
            int target = 3;

            string errorMessage = $"Incorrect value when out of range alphabet index: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public void TestFindTargetIndex_CustomAlphabett()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);

            int index = (int)findTargetInfo.Invoke(new Cryptor("aбвж*%lmn"), new object[] { 'в', '*' });
            int target = 6;

            string errorMessage = $"Incorrect value whith custom alphabet: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public async Task TestEncrypt_CorrectRuTextAndKey()
        {
            string testText = "ладно";
            string testKey = "абоба";
            string target = "лбтоо";

            string result = await new Cryptor(alphabetRus).Encrypt(testText, testKey);

            //string errorMessage = $"Wrong value: {} != {}";
            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestEncrypt_KeyLowerText()
        {
            string testText = "какой-то текст, который шифруется";
            string testKey = "абоба";
            string target = "кбщпй-тп бёксу, щптосйк шихяфеттн";

            string result = await new Cryptor(alphabetRus).Encrypt(testText, testKey);

            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestEncrypt_TextWithUppercase()
        {
            string testText = "Какой-то тЕкст, Который шифруется";
            string testKey = "абоба";
            string target = "Кбщпй-тп бЁксу, Щптосйк шихяфеттн";

            string result = await new Cryptor(alphabetRus).Encrypt(testText, testKey);

            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestDecrypt_Example()
        {
            string testEncryptedText = "кбщпй-тп бёксу, щптосйк дещчхруёбтя";
            string testKey = "абоба";
            string target = "какой-то текст, который дешифруется";

            string result = await new Cryptor(alphabetRus).Decrypt(testEncryptedText, testKey);

            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestDecrypt_TextWithUpperCase()
        {
            string testEncryptedText = "Кбщпй-тп Бёксу, Щптосйк дещчхруёбтя";
            string testKey = "абоба";
            string target = "Какой-то Текст, Который дешифруется";

            string result = await new Cryptor(alphabetRus).Decrypt(testEncryptedText, testKey);

            Assert.AreEqual(result, target);
        }
    }
}
