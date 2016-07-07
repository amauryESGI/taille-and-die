#if !UNITY_WEBPLAYER
#define USE_FileIO
#endif

namespace SimpleJSON {
    public static class JSON {
        public static JSONNode Parse(string aJSON) {
            return JSONNode.Parse(aJSON);
        }
    }
}