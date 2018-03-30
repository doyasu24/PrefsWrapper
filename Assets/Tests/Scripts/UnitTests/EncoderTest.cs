using RuntimeUnitTestToolkit;
using UnityEngine;
using System;

namespace PrefsWrapper.Encoders
{
    public class EncoderTest
    {
        public void IntEncode()
        {
            var encoder = new IntEncoder();
            var bytes = encoder.Encode(1);
            bytes.IsCollection<byte>(0x01, 0x00, 0x00, 0x00);
            encoder.Decode(bytes).Is(1);
        }

        public void FloatEncode()
        {
            var encoder = new FloatEncoder();
            var bytes = encoder.Encode(1f);
            bytes.IsCollection<byte>(0x00, 0x00, 0x80, 0x3f);
            encoder.Decode(bytes).Is(1f);
        }

        public void StringEncode()
        {
            var encoder = new StringEncoder();
            var bytes = encoder.Encode("test");
            bytes.IsCollection<byte>(0x74, 0x65, 0x73, 0x74);
            encoder.Decode(bytes).Is("test");
        }

        public void BoolEncode()
        {
            var encoder = new BoolEncoder();
            var bytes = encoder.Encode(true);
            bytes.IsCollection<byte>(0x01);
            encoder.Decode(bytes).IsTrue();
        }

        public void NopEncode()
        {
            var encoder = new NopEncoder();
            var bytes = encoder.Encode(new byte[] { 0x01 });
            bytes.IsCollection<byte>(0x01);
            encoder.Decode(bytes).IsCollection<byte>(0x01);
        }

        public void EnumEncode()
        {
            var encoder = new EnumEncoder<TestType>();
            var bytes = encoder.Encode(TestType.Fuga);
            bytes.IsCollection<byte>(0x46, 0x75, 0x67, 0x61);
            encoder.Decode(bytes).Is(TestType.Fuga);
        }

        public enum TestType { Hoge, Fuga, Piyo }

        public void JsonEncode()
        {
            var encoder = new JsonEncoder<Vector3>();
            var bytes = encoder.Encode(Vector3.up);
            bytes.IsCollection<byte>(
                0x7B, 0x22, 0x78, 0x22, 0x3A, 0x30, 0x2E, 0x30, 0x2C,
                0x22, 0x79, 0x22, 0x3A, 0x31, 0x2E, 0x30, 0x2C, 0x22,
                0x7A, 0x22, 0x3A, 0x30, 0x2E, 0x30, 0x7D
            );
            encoder.Decode(bytes).Is(Vector3.up);
        }

        public void AesEncode()
        {
            var encoder = new AesEncoder("password", "salt-1234567890");
            var bytes = encoder.Encode(new byte[] { 0x01 });
            bytes.IsCollection<byte>(
                0xE4, 0x09, 0xA3, 0xCE, 0xCC, 0x35, 0x68, 0x6C, 0x55,
                0xEF, 0x00, 0xFF, 0x85, 0xD7, 0xD6, 0x14
            );
            encoder.Decode(bytes).IsCollection<byte>(0x01);
        }

        public void DateTimeEncode()
        {
            var encoder = new DateTimeEncoder();
            var date = new DateTime(2000, 1, 1, 0, 0, 0);
            var bytes = encoder.Encode(date);
            bytes.IsCollection<byte>(
                0x00, 0x40, 0xE4, 0x47, 0x02, 0x22, 0xC1, 0x08
            );
            encoder.Decode(bytes).Is(date);
        }

        public void TimeSpanEncode()
        {
            var encoder = new TimeSpanEncoder();
            var date = TimeSpan.FromSeconds(1);
            var bytes = encoder.Encode(date);
            bytes.IsCollection<byte>(
                0x80, 0x96, 0x98, 0x00, 0x00, 0x00, 0x00, 0x00
            );
            encoder.Decode(bytes).Is(date);
        }

        public void CombinedEncode()
        {
            var intEncoder = new IntEncoder();
            var aesEncoder = new AesEncoder("password", "salt-1234567890");
            var combinedEncoder = new CombinedEncoder<int>(intEncoder, aesEncoder);
            var combinedBytes = combinedEncoder.Encode(1);

            var aesDecode = aesEncoder.Decode(combinedBytes);
            var intDecode = intEncoder.Decode(aesDecode);
            intDecode.Is(1);
        }
    }
}