using System;

namespace SimpleJSON {
    public sealed class JSONData : JSONNode {
        private string _data;


        public override string Value {
            get { return _data; }
            set {
                _data = value;
                Tag = JSONBinaryTag.Value;
            }
        }

        public JSONData(string aData) {
            _data = aData;
            Tag = JSONBinaryTag.Value;
        }

        public JSONData(float aData) {
            AsFloat = aData;
        }

        public JSONData(double aData) {
            AsDouble = aData;
        }

        public JSONData(bool aData) {
            AsBool = aData;
        }

        public JSONData(int aData) {
            AsInt = aData;
        }

        public override string ToString() {
            return "\"" + Escape(_data) + "\"";
        }

        public override string ToString(string aPrefix) {
            return "\"" + Escape(_data) + "\"";
        }

        public override string ToJSON(int prefix) {
            switch (Tag) {
                case JSONBinaryTag.DoubleValue:
                case JSONBinaryTag.FloatValue:
                case JSONBinaryTag.IntValue:
                    return _data;
                case JSONBinaryTag.Value:
                    return string.Format("\"{0}\"", Escape(_data));
                default:
                    throw new NotSupportedException("This shouldn't be here: " + Tag);
            }
        }

        public override void Serialize(System.IO.BinaryWriter aWriter) {
            var tmp = new JSONData("") { AsInt = AsInt };

            if (tmp._data == _data) {
                aWriter.Write((byte)JSONBinaryTag.IntValue);
                aWriter.Write(AsInt);
                return;
            }
            tmp.AsFloat = AsFloat;
            if (tmp._data == _data) {
                aWriter.Write((byte)JSONBinaryTag.FloatValue);
                aWriter.Write(AsFloat);
                return;
            }
            tmp.AsDouble = AsDouble;
            if (tmp._data == _data) {
                aWriter.Write((byte)JSONBinaryTag.DoubleValue);
                aWriter.Write(AsDouble);
                return;
            }

            tmp.AsBool = AsBool;
            if (tmp._data == _data) {
                aWriter.Write((byte)JSONBinaryTag.BoolValue);
                aWriter.Write(AsBool);
                return;
            }
            aWriter.Write((byte)JSONBinaryTag.Value);
            aWriter.Write(_data);
        }
    }
}