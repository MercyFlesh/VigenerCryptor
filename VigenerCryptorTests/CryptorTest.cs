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
            
            int index = (int)findTargetInfo.Invoke(new Cryptor(Cryptor.alphabets["ru"]), new object[] { '�', '�'});
            int target = 4;

            string errorMessage = $"Incorrect value: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public void TestFindTargetIndex_InorrectKeySymbol()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);

            int index = (int)findTargetInfo.Invoke(new Cryptor(alphabetRus), new object[] { '�', 'w' });
            int target = 5;

            string errorMessage = $"Incorrect value whith not included key in alphabet: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public void TestFindTargetIndex_OutOfRangeAlphabet()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);
            
            int index = (int)findTargetInfo.Invoke(new Cryptor(alphabetRus), new object[] { '�', '�' });
            int target = 3;

            string errorMessage = $"Incorrect value when out of range alphabet index: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public void TestFindTargetIndex_CustomAlphabett()
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo findTargetInfo = typeof(Cryptor).GetMethod("FindTargetIndex", bindingAttr);

            int index = (int)findTargetInfo.Invoke(new Cryptor("a���*%lmn"), new object[] { '�', '*' });
            int target = 6;

            string errorMessage = $"Incorrect value whith custom alphabet: {alphabetRus[index]} != {alphabetRus[target]}";
            Assert.AreEqual(index, target, errorMessage);
        }

        [TestMethod]
        public async Task TestEncrypt_CorrectRuTextAndKey()
        {
            string testText = "�����";
            string testKey = "�����";
            string target = "�����";

            string result = await new Cryptor(alphabetRus).Encrypt(testText, testKey);

            //string errorMessage = $"Wrong value: {} != {}";
            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestEncrypt_KeyLowerText()
        {
            string testText = "�����-�� �����, ������� ���������";
            string testKey = "�����";
            string target = "�����-�� ����, ������� ���������";

            string result = await new Cryptor(alphabetRus).Encrypt(testText, testKey);

            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestEncrypt_TextWithUppercase()
        {
            string testText = "�����-�� �����, ������� ���������";
            string testKey = "�����";
            string target = "�����-�� ����, ������� ���������";

            string result = await new Cryptor(alphabetRus).Encrypt(testText, testKey);

            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestDecrypt_Example()
        {
            string testEncryptedText = "�����-�� ����, ������� ����������";
            string testKey = "�����";
            string target = "�����-�� �����, ������� �����������";

            string result = await new Cryptor(alphabetRus).Decrypt(testEncryptedText, testKey);

            Assert.AreEqual(result, target);
        }

        [TestMethod]
        public async Task TestDecrypt_TextWithUpperCase()
        {
            string testEncryptedText = "�����-�� �����, ������� ����������";
            string testKey = "�����";
            string target = "�����-�� �����, ������� �����������";

            string result = await new Cryptor(alphabetRus).Decrypt(testEncryptedText, testKey);

            Assert.AreEqual(result, target);
        }
    }
}
