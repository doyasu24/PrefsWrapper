using NUnit.Framework;
using PrefsWrapper.Encoders;
using UnityEngine;

namespace PrefsWrapper.Serializers
{
    public class SerializerTest
    {
        [Test]
        public void SerializeString()
        {
            var key = "test-string";
            var value = "test";
            var serializer = new StringPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), value);
        }

        [Test]
        public void SerializeInt()
        {
            var key = "test-int";
            var value = 1;
            var serializer = new IntPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetInt(key), value);
        }

        [Test]
        public void SerializeFloat()
        {
            var key = "test-float";
            var value = 1f;
            var serializer = new FloatPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetFloat(key), value);
        }

        [Test]
        public void EncodeSerialize()
        {
            var key = "test-encode";
            var value = 1;
            var intEncoder = new IntEncoder();
            var serializer = new EncodeSerializer<int>(new BinaryPrefSerializer(), intEncoder);
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), "AQAAAA==");
        }
    }
}
