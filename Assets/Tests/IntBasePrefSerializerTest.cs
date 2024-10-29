using NUnit.Framework;
using PrefsWrapper.Serializers;
using UnityEngine;

namespace Tests
{
    public class IntBasePrefSerializerTest
    {
        [Test]
        public void SerializeBool()
        {
            var key = "test-bool";
            var value = true;
            var serializer = new BoolPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), 1);
        }

        [Test]
        public void SerializeByte()
        {
            var key = "test-byte";
            var value = (byte)1;
            var serializer = new BytePrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), 1);
        }

        [Test]
        public void SerializeSByte()
        {
            var key = "test-sbyte";
            var value = (sbyte)1;
            var serializer = new SBytePrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), 1);
        }

        [Test]
        public void SerializeChar()
        {
            var key = "test-char";
            var value = 'a';
            var serializer = new CharPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), 97);
        }

        [Test]
        public void SerializeShort()
        {
            var key = "test-short";
            var value = (short)1;
            var serializer = new ShortPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), 1);
        }

        [Test]
        public void SerializeUShort()
        {
            var key = "test-ushort";
            var value = (ushort)1;
            var serializer = new UShortPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), 1);
        }
    }
}
