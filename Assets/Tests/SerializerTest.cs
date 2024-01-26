using System;
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
        public void SerializeBinary()
        {
            var key = "test-binary";
            var value = new byte[] { 0x00, 0x01 };
            var serializer = new BinaryPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), "AAE=");
        }

        [Test]
        public void SerializeJson()
        {
            var key = "test-json";
            var value = Vector3.up;
            var serializer = new JsonPrefSerializer<Vector3>();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), JsonUtility.ToJson(Vector3.up));
        }

        [Test]
        public void SerializeEnum()
        {
            var key = "test-enum";
            var value = TestType.Fuga;
            var serializer = new EnumPrefSerializer<TestType>();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), TestType.Fuga.ToString());
        }

        public enum TestType
        {
            Hoge,
            Fuga,
            Piyo
        }

        [Test]
        public void SerializeDateTime()
        {
            var key = "test-datetime";
            var value = DateTime.Now;
            var serializer = new DateTimePrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), value.ToBinary().ToString());
        }

        [Test]
        public void SerializeTimeSpan()
        {
            var key = "test-timespan";
            var value = TimeSpan.FromSeconds(1);
            var serializer = new TimeSpanPrefSerializer();
            serializer.Serialize(key, value);
            Assert.AreEqual(serializer.Deserialize(key), value);
            Assert.IsTrue(PlayerPrefs.HasKey(key));
            Assert.AreEqual(PlayerPrefs.GetString(key), value.Ticks.ToString());
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