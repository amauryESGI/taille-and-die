using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Serialization {
    public class SaveLevel : MonoBehaviour {
        [SerializeField] private GameObject _gameObjectSerialized;

        public void Save(string fileName) {
            string filePath = CreatePathXml(fileName);

            Debug.Log("Save!");
            
            var prefabList = new List<PrefabDetails>(_gameObjectSerialized.transform.childCount);
            var id = 0;
            foreach (Transform child in _gameObjectSerialized.transform) {
                Debug.Log(string.Format("[{0}] {1} : {2}", id, child.name, child.position));
                prefabList.Add(new PrefabDetails {
                    OriginPrefab = child.name.Split(' ')[0],
                    Id = id,
                    Pos = child.position
                });

                id++;
            }

            var serializer = new XmlSerializer(typeof(List<PrefabDetails>));
            using (TextWriter sw = new StreamWriter(filePath))
                serializer.Serialize(sw, prefabList);
        }

        public void Load(string fileName) {
            string filePath = CreatePathXml(fileName);

            if (!File.Exists(filePath)) {
                Debug.Log(filePath);
                throw new System.NotImplementedException(); // TODO ADD 
            }

            Debug.Log("Load!");
            List<PrefabDetails> XmlData;

            var deserializer = new XmlSerializer(typeof(List<PrefabDetails>));
            using (TextReader sr = new StreamReader(filePath)) {
                XmlData = deserializer.Deserialize(sr) as List<PrefabDetails>;
            }

            foreach (PrefabDetails objDetails in XmlData) {
                Debug.Log(string.Format("[{0}] {1} {2} : {3}", objDetails.Id, objDetails.OriginPrefab, objDetails.Pos, Resources.Load("Prefabs/" + objDetails.OriginPrefab)));
                var obj = Instantiate(Resources.Load("Prefabs/" + objDetails.OriginPrefab)) as GameObject;
                obj.transform.position = objDetails.Pos;
                obj.transform.parent = _gameObjectSerialized.transform;
            }
        }

        private static string CreatePathXml(string fileName) {
            return SaveConfig.MapPath + "/" + fileName + ".xml";
        }
    }
}