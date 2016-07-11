namespace SimpleJSON {
    internal class JSONLazyCreator : JSONNode {
        private JSONNode _node;
        private readonly string _key;

        public JSONLazyCreator(JSONNode aNode) {
            _node = aNode;
            _key = null;
        }

        public JSONLazyCreator(JSONNode aNode, string aKey) {
            _node = aNode;
            _key = aKey;
        }

        private void Set(JSONNode aVal) {
            if (_key == null) {
                _node.Add(aVal);
            } else {
                _node.Add(_key, aVal);
            }
            _node = null; // Be GC friendly.
        }

        public override JSONNode this[int aIndex] {
            get { return new JSONLazyCreator(this); }
            set {
                var tmp = new JSONArray();
                tmp.Add(value);
                Set(tmp);
            }
        }

        public override JSONNode this[string aKey] {
            get { return new JSONLazyCreator(this, aKey); }
            set {
                var tmp = new JSONClass { { aKey, value } };
                Set(tmp);
            }
        }

        public override void Add(JSONNode aItem) {
            var tmp = new JSONArray();
            tmp.Add(aItem);
            Set(tmp);
        }

        public override void Add(string aKey, JSONNode aItem) {
            var tmp = new JSONClass { { aKey, aItem } };
            Set(tmp);
        }

        public static bool operator ==(JSONLazyCreator a, object b) {
            return b == null || ReferenceEquals(a, b);
        }

        public static bool operator !=(JSONLazyCreator a, object b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            return obj == null || ReferenceEquals(this, obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return "";
        }

        public override string ToString(string aPrefix) {
            return "";
        }

        public override string ToJSON(int prefix) {
            return "";
        }

        public override int AsInt {
            get {
                JSONData tmp = new JSONData(0);
                Set(tmp);
                return 0;
            }
            set {
                JSONData tmp = new JSONData(value);
                Set(tmp);
            }
        }

        public override float AsFloat {
            get {
                JSONData tmp = new JSONData(0.0f);
                Set(tmp);
                return 0.0f;
            }
            set {
                JSONData tmp = new JSONData(value);
                Set(tmp);
            }
        }

        public override double AsDouble {
            get {
                JSONData tmp = new JSONData(0.0);
                Set(tmp);
                return 0.0;
            }
            set {
                JSONData tmp = new JSONData(value);
                Set(tmp);
            }
        }

        public override bool AsBool {
            get {
                JSONData tmp = new JSONData(false);
                Set(tmp);
                return false;
            }
            set {
                JSONData tmp = new JSONData(value);
                Set(tmp);
            }
        }

        public override JSONArray AsArray {
            get {
                JSONArray tmp = new JSONArray();
                Set(tmp);
                return tmp;
            }
        }

        public override JSONClass AsObject {
            get {
                var tmp = new JSONClass();
                Set(tmp);
                return tmp;
            }
        }
    }
}