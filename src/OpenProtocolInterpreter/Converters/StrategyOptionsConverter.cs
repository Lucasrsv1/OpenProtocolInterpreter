﻿using OpenProtocolInterpreter.Tightening;

namespace OpenProtocolInterpreter.Converters
{
    public class StrategyOptionsConverter : BitConverter, IValueConverter<StrategyOptions>
    {
        private readonly IValueConverter<byte[]> _byteArrayConverter;

        public StrategyOptionsConverter(IValueConverter<byte[]> byteArrayConverter)
        {
            _byteArrayConverter = byteArrayConverter;
        }

        public StrategyOptions Convert(string value)
        {
            var bytes = _byteArrayConverter.Convert(value);
            return ConvertFromBytes(bytes);
        }

        public string Convert(StrategyOptions value)
        {
            byte[] bytes = ConvertToBytes(value);
            return _byteArrayConverter.Convert(bytes);
        }

        public string Convert(char paddingChar, int size, DataField.PaddingOrientations orientation, StrategyOptions value) => Convert(value);

        public StrategyOptions ConvertFromBytes(byte[] value)
        {
            return new StrategyOptions()
            {
                //Byte 0
                Torque = GetBit(value[0], 1),
                Angle = GetBit(value[0], 2),
                Batch = GetBit(value[0], 3),
                PvtMonitoring = GetBit(value[0], 4),
                PvtCompensate = GetBit(value[0], 5),
                Selftap = GetBit(value[0], 6),
                Rundown = GetBit(value[0], 7),
                CM = GetBit(value[0], 8),
                //Byte 1
                DsControl = GetBit(value[1], 1),
                ClickWrench = GetBit(value[1], 2),
                RbwMonitoring = GetBit(value[1], 3)
            };
        }

        public byte[] ConvertToBytes(StrategyOptions value)
        {
            byte[] bytes = new byte[10];
            bytes[0] = SetByte(new bool[]
            {
                value.Torque,
                value.Angle,
                value.Batch,
                value.PvtMonitoring,
                value.PvtCompensate,
                value.Selftap,
                value.Rundown,
                value.CM
            });
            bytes[1] = SetByte(new bool[]
            {
                 value.DsControl,
                 value.ClickWrench,
                 value.RbwMonitoring,
                 false,
                 false,
                 false,
                 false,
                 false
            });

            bytes[2] = bytes[3] = bytes[4] = bytes[5] = bytes[6] = bytes[7] = bytes[8] = bytes[9] = 0;
            return bytes;
        }

        public byte[] ConvertToBytes(char paddingChar, int size, DataField.PaddingOrientations orientation, StrategyOptions value) => ConvertToBytes(value);
    }
}
