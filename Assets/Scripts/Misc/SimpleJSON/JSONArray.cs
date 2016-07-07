using System.Collections;
using System.Collections.Generic;

namespace SimpleJSON {
    public class JSONArray : JSONNode, IEnumerable {
        private readonly List<JSONNode> _list = new List<JSONNode>();

        public override JSONNode this[int aIndex] {
            get {
                if (aIndex < 0 || aIndex >= _list.Count)
                    return new JSONLazyCreator(this);
                return _list[aIndex];
            }
            set {
                if (aIndex < 0 || aIndex >= _list.Count)
                    _list.Add(value);
                else
                    _list[aIndex] = value;
            }
        }

        public override JSONNode this[string aKey] {
            get { return new JSONLazyCreator(this); }
            set { _list.Add(value); }
        }

        public override int Count {
            get { return _list.Count; }
        }

        public override void Add(string aKey, JSONNode aItem) {
            _list.Add(aItem);
        }

        public override JSONNode Remove(int aIndex) {
            if (aIndex < 0 || aIndex >= _list.Count)
                return null;
            JSONNode tmp = _list[aIndex];
            _list.RemoveAt(aIndex);
            return tmp;
        }

        public override JSONNode Remove(JSONNode aNode) {
            _list.Remove(aNode);
            return aNode;
        }

        public override IEnumerable<JSONNode> Children {
            get {
                return _list;
            }
        }

        public IEnumerator GetEnumerator() {
            return _list.GetEnumerator();
        }

        public override string ToString() {
            var result = "[ ";
            foreach (JSONNode n in _list) {
                if (result.Length > 2)
                    result += ", ";
                result += n.ToString();
            }
            result += " ]";
            return result;
        }

        public override string ToString(string aPrefix) {
            string result = "[ ";
            foreach (JSONNode n in _list) {
                if (result.Length > 3)
                    result += ", ";
                result += "\n" + aPrefix + "   ";
                result += n.ToString(aPrefix + "   ");
            }
            result += "\n" + aPrefix + "]";
            return result;
        }

        public override string ToJSON(int prefix) {
            string s = new string(' ', (prefix + 1) * 2);
            string ret = "[ ";
            foreach (JSONNode n in _list) {
                if (ret.Length > 3)
                    ret += ", ";
                ret += "\n" + s;
                ret += n.ToJSON(prefix + 1);

            }
            ret += "\n" + s + "]";
            return ret;
        }

        public override void Serialize(System.IO.BinaryWriter aWriter) {
            aWriter.Write((byte)JSONBinaryTag.Array);
            aWriter.Write(_list.Count);
            foreach (JSONNode t in _list) {
                t.Serialize(aWriter);
            }
        }
    }
}