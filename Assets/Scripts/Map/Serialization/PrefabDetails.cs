using System.Xml.Serialization;
using UnityEngine;

namespace Serialization {
    public class PrefabDetails {
        [XmlAttribute("Name")]
        public string OriginPrefab { get; set; }

        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlIgnore]
        public Vector3 Pos {
            get { return new Vector3(PosX, PosY, PosZ); }
            set {
                PosX = value.x;
                PosY = value.y;
                PosZ = value.z;
            }
        }

        [XmlAttribute("PosX")]
        public float PosX { get; set; }

        [XmlAttribute("PosY")]
        public float PosY { get; set; }

        [XmlAttribute("PosZ")]
        public float PosZ { get; set; }
    }
}
