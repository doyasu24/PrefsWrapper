using System;
using NUnit.Framework;
using PrefsWrapper.Serializers;
using UnityEngine;

namespace Tests
{
    public class StringBasePrefSerializerTest
    {
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

        public enum TestType
        {
            Hoge,
            Fuga,
            Piyo
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
    }
}
