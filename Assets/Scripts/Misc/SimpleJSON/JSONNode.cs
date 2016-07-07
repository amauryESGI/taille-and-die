using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SimpleJSON {
    public abstract class JSONNode {
        #region common interface

        public virtual void Add(string aKey, JSONNode aItem) { }

        [NotNull]
        public virtual JSONNode this[int aIndex] {
            get { return null; }
            set { if (value == null) throw new ArgumentNullException("value"); }
        }

        [NotNull]
        public virtual JSONNode this[string aKey] {
            get { return null; }
            set { if (value == null) throw new ArgumentNullException("value"); }
        }

        [NotNull]
        public virtual string Value {
            get { return ""; }
            set { if (value == null) throw new ArgumentNullException("value"); }
        }

        public virtual int Count {
            get { return 0; }
        }

        public virtual void Add(JSONNode aItem) {
            Add("", aItem);
        }

        public virtual JSONNode Remove(string aKey) {
            return null;
        }

        public virtual JSONNode Remove(int aIndex) {
            return null;
        }

        public virtual JSONNode Remove(JSONNode aNode) {
            return aNode;
        }

        public virtual IEnumerable<JSONNode> Children {
            get { yield break; }
        }

        public IEnumerable<JSONNode> DeepChildren {
            get {
                return Children.SelectMany(c => c.DeepChildren);
            }
        }

        public override string ToString() {
            return "JSONNode";
        }

        public virtual string ToString(string aPrefix) {
            return "JSONNode";
        }

        public abstract string ToJSON(int prefix);

        #endregion common interface

        #region typecasting properties

        public virtual JSONBinaryTag Tag { get; set; }

        public virtual int AsInt {
            get {
                int v;
                return int.TryParse(Value, out v) ? v : 0;
            }
            set {
                Value = value.ToString();
                Tag = JSONBinaryTag.IntValue;
            }
        }

        public virtual float AsFloat {
            get {
                float v;
                return float.TryParse(Value, out v) ? v : 0.0f;
            }
            set {
                Value = value.ToString();
                Tag = JSONBinaryTag.FloatValue;
            }
        }

        public virtual double AsDouble {
            get {
                double v;
                return double.TryParse(Value, out v) ? v : 0.0;
            }
            set {
                Value = value.ToString();
                Tag = JSONBinaryTag.DoubleValue;

            }
        }

        public virtual bool AsBool {
            get {
                bool v;
                if (bool.TryParse(Value, out v))
                    return v;
                return !string.IsNullOrEmpty(Value);
            }
            set {
                Value = (value) ? "true" : "false";
                Tag = JSONBinaryTag.BoolValue;

            }
        }

        public virtual JSONArray AsArray {
            get { return this as JSONArray; }
        }

        public virtual JSONClass AsObject {
            get { return this as JSONClass; }
        }


        #endregion typecasting properties

        #region operators

        public static implicit operator JSONNode(string s) {
            return new JSONData(s);
        }

        public static implicit operator string (JSONNode d) {
            return (d == null) ? null : d.Value;
        }

        public static bool operator ==(JSONNode a, object b) {
            if (b == null && a is JSONLazyCreator)
                return true;
            return ReferenceEquals(a, b);
        }

        public static bool operator !=(JSONNode a, object b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }


        #endregion operators

        internal static string Escape(string aText) {
            string result = "";
            foreach (char c in aText) {
                switch (c) {
                    case '\\':
                        result += "\\\\";
                        break;
                    case '\"':
                        result += "\\\"";
                        break;
                    case '\n':
                        result += "\\n";
                        break;
                    case '\r':
                        result += "\\r";
                        break;
                    case '\t':
                        result += "\\t";
                        break;
                    case '\b':
                        result += "\\b";
                        break;
                    case '\f':
                        result += "\\f";
                        break;
                    default:
                        result += c;
                        break;
                }
            }
            return result;
        }

        static JSONData Numberize(string token) {
            bool flag;
            int integer;
            double real;

            if (int.TryParse(token, out integer)) {
                return new JSONData(integer);
            }

            if (double.TryParse(token, out real)) {
                return new JSONData(real);
            }

            if (bool.TryParse(token, out flag)) {
                return new JSONData(flag);
            }

            throw new NotImplementedException(token);
        }

        static void AddElement(JSONNode ctx, string token, string tokenName, bool tokenIsString) {
            if (tokenIsString) {
                if (ctx is JSONArray)
                    ctx.Add(token);
                else
                    ctx.Add(tokenName, token); // assume dictionary/object
            } else {
                JSONData number = Numberize(token);
                if (ctx is JSONArray)
                    ctx.Add(number);
                else
                    ctx.Add(tokenName, number);

            }
        }

        public static JSONNode Parse(string aJSON) {
            var stack = new Stack<JSONNode>();
            JSONNode ctx = null;
            var i = 0;
            var token = "";
            var tokenName = "";
            var quoteMode = false;
            var tokenIsString = false;
            while (i < aJSON.Length) {
                switch (aJSON[i]) {
                    case '{':
                        if (quoteMode) {
                            token += aJSON[i];
                            break;
                        }
                        stack.Push(new JSONClass());
                        if (ctx != null) {
                            tokenName = tokenName.Trim();
                            if (ctx is JSONArray)
                                ctx.Add(stack.Peek());
                            else if (tokenName != "")
                                ctx.Add(tokenName, stack.Peek());
                        }
                        tokenName = "";
                        token = "";
                        ctx = stack.Peek();
                        break;

                    case '[':
                        if (quoteMode) {
                            token += aJSON[i];
                            break;
                        }

                        stack.Push(new JSONArray());
                        if (ctx != null) {
                            tokenName = tokenName.Trim();

                            if (ctx is JSONArray)
                                ctx.Add(stack.Peek());
                            else if (tokenName != "")
                                ctx.Add(tokenName, stack.Peek());
                        }
                        tokenName = "";
                        token = "";
                        ctx = stack.Peek();
                        break;

                    case '}':
                    case ']':
                        if (quoteMode) {
                            token += aJSON[i];
                            break;
                        }
                        if (stack.Count == 0)
                            throw new Exception("JSON Parse: Too many closing brackets");

                        stack.Pop();
                        if (token != "") {
                            tokenName = tokenName.Trim();
                            /*
							if (ctx is JSONArray)
								ctx.Add (Token);
							else if (TokenName != "")
								ctx.Add (TokenName, Token);
								*/
                            AddElement(ctx, token, tokenName, tokenIsString);
                            tokenIsString = false;
                        }
                        tokenName = "";
                        token = "";
                        if (stack.Count > 0)
                            ctx = stack.Peek();
                        break;

                    case ':':
                        if (quoteMode) {
                            token += aJSON[i];
                            break;
                        }
                        tokenName = token;
                        token = "";
                        tokenIsString = false;
                        break;

                    case '"':
                        quoteMode ^= true;
                        tokenIsString = true;
                        break;

                    case ',':
                        if (quoteMode) {
                            token += aJSON[i];
                            break;
                        }
                        if (token != "") {
                            AddElement(ctx, token, tokenName, tokenIsString);
                        }
                        tokenName = "";
                        token = "";
                        tokenIsString = false;
                        break;

                    case '\r':
                    case '\n':
                        break;

                    case ' ':
                    case '\t':
                        if (quoteMode)
                            token += aJSON[i];
                        break;

                    case '\\':
                        ++i;
                        if (quoteMode) {
                            char c = aJSON[i];
                            switch (c) {
                                case 't':
                                    token += '\t';
                                    break;
                                case 'r':
                                    token += '\r';
                                    break;
                                case 'n':
                                    token += '\n';
                                    break;
                                case 'b':
                                    token += '\b';
                                    break;
                                case 'f':
                                    token += '\f';
                                    break;
                                case 'u':
                                    {
                                        string s = aJSON.Substring(i + 1, 4);
                                        token += (char)int.Parse(
                                            s,
                                            System.Globalization.NumberStyles.AllowHexSpecifier);
                                        i += 4;
                                        break;
                                    }
                                default:
                                    token += c;
                                    break;
                            }
                        }
                        break;

                    default:
                        token += aJSON[i];
                        break;
                }
                ++i;
            }
            if (quoteMode) {
                throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
            }
            return ctx;
        }

        public virtual void Serialize(System.IO.BinaryWriter aWriter) { }

        public void SaveToStream(System.IO.Stream aData) {
            var w = new System.IO.BinaryWriter(aData);
            Serialize(w);
        }

#if USE_SharpZipLib
		public void SaveToCompressedStream(System.IO.Stream aData)
		{
			using (var gzipOut = new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(aData))
			{
				gzipOut.IsStreamOwner = false;
				SaveToStream(gzipOut);
				gzipOut.Close();
			}
		}
 
		public void SaveToCompressedFile(string aFileName)
		{
 
#if USE_FileIO
			System.IO.Directory.CreateDirectory((new System.IO.FileInfo(aFileName)).Directory.FullName);
			using(var F = System.IO.File.OpenWrite(aFileName))
			{
				SaveToCompressedStream(F);
			}
 
#else
			throw new Exception("Can't use File IO stuff in webplayer");
#endif
		}
		public string SaveToCompressedBase64()
		{
			using (var stream = new System.IO.MemoryStream())
			{
				SaveToCompressedStream(stream);
				stream.Position = 0;
				return System.Convert.ToBase64String(stream.ToArray());
			}
		}
 
#else
        public void SaveToCompressedStream(System.IO.Stream aData) {
            throw new Exception(
                "Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
        }

        public void SaveToCompressedFile(string aFileName) {
            throw new Exception(
                "Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
        }

        public string SaveToCompressedBase64() {
            throw new Exception(
                "Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
        }
#endif

        public void SaveToFile(string aFileName) {
#if USE_FileIO
            System.IO.Directory.CreateDirectory((new System.IO.FileInfo(aFileName)).Directory.FullName);
            using (var F = System.IO.File.OpenWrite(aFileName)) {
                SaveToStream(F);
            }
#else
            throw new Exception("Can't use File IO stuff in webplayer");
#endif
        }

        public string SaveToBase64() {
            using (var stream = new System.IO.MemoryStream()) {
                SaveToStream(stream);
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static JSONNode Deserialize(System.IO.BinaryReader aReader) {
            JSONBinaryTag type = (JSONBinaryTag)aReader.ReadByte();
            switch (type) {
                case JSONBinaryTag.Array:
                    {
                        int count = aReader.ReadInt32();
                        JSONArray tmp = new JSONArray();
                        for (int i = 0; i < count; i++)
                            tmp.Add(Deserialize(aReader));
                        return tmp;
                    }
                case JSONBinaryTag.Class:
                    {
                        int count = aReader.ReadInt32();
                        JSONClass tmp = new JSONClass();
                        for (int i = 0; i < count; i++) {
                            string key = aReader.ReadString();
                            var val = Deserialize(aReader);
                            tmp.Add(key, val);
                        }
                        return tmp;
                    }
                case JSONBinaryTag.Value:
                    {
                        return new JSONData(aReader.ReadString());
                    }
                case JSONBinaryTag.IntValue:
                    {
                        return new JSONData(aReader.ReadInt32());
                    }
                case JSONBinaryTag.DoubleValue:
                    {
                        return new JSONData(aReader.ReadDouble());
                    }
                case JSONBinaryTag.BoolValue:
                    {
                        return new JSONData(aReader.ReadBoolean());
                    }
                case JSONBinaryTag.FloatValue:
                    {
                        return new JSONData(aReader.ReadSingle());
                    }

                default:
                    {
                        throw new Exception("Error deserializing JSON. Unknown tag: " + type);
                    }
            }
        }

#if USE_SharpZipLib
		public static JSONNode LoadFromCompressedStream(System.IO.Stream aData)
		{
			var zin = new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(aData);
			return LoadFromStream(zin);
		}
		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
#if USE_FileIO
			using(var F = System.IO.File.OpenRead(aFileName))
			{
				return LoadFromCompressedStream(F);
			}
#else
			throw new Exception("Can't use File IO stuff in webplayer");
#endif
		}
		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			var tmp = System.Convert.FromBase64String(aBase64);
			var stream = new System.IO.MemoryStream(tmp);
			stream.Position = 0;
			return LoadFromCompressedStream(stream);
		}
#else
        public static JSONNode LoadFromCompressedFile(string aFileName) {
            throw new Exception(
                "Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
        }

        public static JSONNode LoadFromCompressedStream(System.IO.Stream aData) {
            throw new Exception(
                "Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
        }

        public static JSONNode LoadFromCompressedBase64(string aBase64) {
            throw new Exception(
                "Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
        }
#endif

        public static JSONNode LoadFromStream(System.IO.Stream aData) {
            using (var r = new System.IO.BinaryReader(aData)) {
                return Deserialize(r);
            }
        }

        public static JSONNode LoadFromFile(string aFileName) {
#if USE_FileIO
            using (var F = System.IO.File.OpenRead(aFileName)) {
                return LoadFromStream(F);
            }
#else
            throw new Exception("Can't use File IO stuff in webplayer");
#endif
        }

        public static JSONNode LoadFromBase64(string aBase64) {
            byte[] tmp = Convert.FromBase64String(aBase64);
            var stream = new System.IO.MemoryStream(tmp) { Position = 0 };
            return LoadFromStream(stream);
        }
    }
}