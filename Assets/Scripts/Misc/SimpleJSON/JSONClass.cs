using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleJSON {
    public class JSONClass : JSONNode, IEnumerable {
        private readonly Dictionary<string, JSONNode> _dict = new Dictionary<string, JSONNode>();

        public override JSONNode this[string aKey] {
            get {
                return _dict.ContainsKey(aKey) ? _dict[aKey] : new JSONLazyCreator(this, aKey);
            }
            set {
                if (_dict.ContainsKey(aKey))
                    _dict[aKey] = value;
                else
                    _dict.Add(aKey, value);
            }
        }

        public override JSONNode this[int aIndex] {
            get {
                if (aIndex < 0 || aIndex >= _dict.Count)
                    return null;
                return _dict.ElementAt(aIndex).Value;
            }
            set {
                if (aIndex < 0 || aIndex >= _dict.Count)
                    return;
                string key = _dict.ElementAt(aIndex).Key;
                _dict[key] = value;
            }
        }

        public override int Count {
            get { return _dict.Count; }
        }


        public override void Add(string aKey, JSONNode aItem) {
            if (!string.IsNullOrEmpty(aKey)) {
                if (_dict.ContainsKey(aKey))
                    _dict[aKey] = aItem;
                else
                    _dict.Add(aKey, aItem);
            } else
                _dict.Add(Guid.NewGuid().ToString(), aItem);
        }

        public override JSONNode Remove(string aKey) {
            if (!_dict.ContainsKey(aKey))
                return null;
            JSONNode tmp = _dict[aKey];
            _dict.Remove(aKey);
            return tmp;
        }

        public override JSONNode Remove(int aIndex) {
            if (aIndex < 0 || aIndex >= _dict.Count)
                return null;
            var item = _dict.ElementAt(aIndex);
            _dict.Remove(item.Key);
            return item.Value;
        }

        public override JSONNode Remove(JSONNode aNode) {
            try {
                KeyValuePair<string, JSONNode> item = _dict.First(k => k.Value == aNode);
                _dict.Remove(item.Key);
                return aNode;
            } catch {
                return null;
            }
        }

        public override IEnumerable<JSONNode> Children {
            get {
                return _dict.Select(n => n.Value);
            }
        }

        public IEnumerator GetEnumerator() {
            return _dict.GetEnumerator();
        }

        public override string ToString() {
            var result = "{";
            foreach (KeyValuePair<string, JSONNode> n in _dict) {
                if (result.Length > 2)
                    result += ", ";
                result += "\"" + Escape(n.Key) + "\":" + n.Value;
            }
            result += "}";
            return result;
        }

        public override string ToString(string aPrefix) {
            string result = "{ ";
            foreach (KeyValuePair<string, JSONNode> n in _dict) {
                if (result.Length > 3)
                    result += ", ";
                result += "\n" + aPrefix + "   ";
                result += "\"" + Escape(n.Key) + "\" : " + n.Value.ToString(aPrefix + "   ");
            }
            result += "\n" + aPrefix + "}";
            return result;
        }

        public override string ToJSON(int prefix) {
            string s = new string(' ', (prefix + 1) * 2);
            string ret = "{ ";
            foreach (KeyValuePair<string, JSONNode> n in _dict) {
                if (ret.Length > 3)
                    ret += ", ";
                ret += "\n" + s;
                ret += string.Format("\"{0}\": {1}", n.Key, n.Value.ToJSON(prefix + 1));
            }
            ret += "\n" + s + "}";
            return ret;
        }

        public override void Serialize(System.IO.BinaryWriter aWriter) {
            aWriter.Write((byte)JSONBinaryTag.Class);
            aWriter.Write(_dict.Count);
            foreach (string k in _dict.Keys) {
                aWriter.Write(k);
                _dict[k].Serialize(aWriter);
            }
        }
    }
}