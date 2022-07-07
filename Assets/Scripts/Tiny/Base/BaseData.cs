using System;
using System.Text;

namespace TinyFramework
{
    /// <summary>
    /// 基础数据类
    /// </summary>
    public abstract class BaseData
    {
        /// <summary>
        /// 获取字节数组长度
        /// </summary>
        /// <returns>字节数组长度</returns>
        public abstract int GetBytesNum();

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <returns>字节数组</returns>
        public abstract byte[] Writing();

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="beginIndex">数据起始位置</param>
        /// <returns>字节数组长度</returns>
        public abstract int Reading(byte[] bytes, int beginIndex = 0);

        protected virtual void WriteInt(byte[] bytes, int value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(int);
        }

        protected virtual void WriteLong(byte[] bytes, long value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(long);
        }

        protected virtual void ReadDouble(byte[] bytes, double value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(double);
        }

        protected virtual void WriteFloat(byte[] bytes, float value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(float);
        }

        protected virtual void WriteShort(byte[] bytes, short value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(short);
        }

        protected virtual void WriteBool(byte[] bytes, bool value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(bool);
        }

        protected virtual void WriteString(byte[] bytes, string value, ref int index)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(value);
            WriteInt(bytes, strBytes.Length, ref index);
            strBytes.CopyTo(bytes, index);
            index += strBytes.Length;
        }

        protected virtual void WriteData(byte[] bytes, BaseData value, ref int index)
        {
            value.Writing().CopyTo(bytes, index);
            index += value.GetBytesNum();
        }

        protected virtual int ReadInt(byte[] bytes, ref int index)
        {
            int value = BitConverter.ToInt32(bytes, index);
            index += sizeof(int);
            return value;
        }

        protected virtual long ReadLong(byte[] bytes, ref int index)
        {
            long value = BitConverter.ToInt64(bytes, index);
            index += sizeof(long);
            return value;
        }

        protected virtual double ReadDouble(byte[] bytes, ref int index)
        {
            double value = BitConverter.ToDouble(bytes, index);
            index += sizeof(double);
            return value;
        }

        protected virtual float ReadFloat(byte[] bytes, ref int index)
        {
            float value = BitConverter.ToSingle(bytes, index);
            index += sizeof(float);
            return value;
        }

        protected virtual short ReadShort(byte[] bytes, ref int index)
        {
            short value = BitConverter.ToInt16(bytes, index);
            index += sizeof(short);
            return value;
        }

        protected virtual bool ReadBool(byte[] bytes, ref int index)
        {
            bool value = BitConverter.ToBoolean(bytes, index);
            index += sizeof(bool);
            return value;
        }

        protected virtual string ReadString(byte[] bytes, ref int index)
        {
            int length = BitConverter.ToInt32(bytes, index);
            index += sizeof(int);
            string value = Encoding.UTF8.GetString(bytes, index, length);
            index += length;
            return value;
        }

        protected virtual T ReadData<T>(byte[] bytes, ref int index) where T: BaseData, new()
        {
            T value = new T();
            index += value.Reading(bytes, index);
            return value;
        }

    }
}

