using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;
using static RedCarpet.PropertyGridTypes;

namespace RedCarpet
{
    public class Object
    {
        public List<MapObject> mobjs = new List<MapObject>();

        public class MapObject
        {
            public string objectID
            {
                get { return AllProperties["Id"]; }
                set { AllProperties["Id"] = value; }
            }

            public string modelName
            {
                get { return AllProperties["ModelName"]; }
                set { AllProperties["ModelName"] = value; }
            }

            public string Layer
            {
                get { return AllProperties["LayerConfigName"]; }
                set { AllProperties["LayerConfigName"] = value; }
            }

            public string unitConfigName
            {
                get { return AllProperties["UnitConfigName"]; }
                set { AllProperties["UnitConfigName"] = value; }
            }

            [TypeConverter(typeof(Vector3Converter))]
            public Vector3 position
            {
                get { return new Vector3(AllProperties["Translate"]["X"], AllProperties["Translate"]["Y"], AllProperties["Translate"]["Z"]); }
                set
                {
                    AllProperties["Translate"]["X"] = value.X;
                    AllProperties["Translate"]["Y"] = value.Y;
                    AllProperties["Translate"]["Z"] = value.Z;
                }
            }

            [TypeConverter(typeof(Vector3Converter))]
            public Vector3 rotation
            {
                get { return new Vector3(AllProperties["Rotate"]["X"], AllProperties["Rotate"]["Y"], AllProperties["Rotate"]["Z"]); }
                set
                {
                    AllProperties["Rotate"]["X"] = value.X;
                    AllProperties["Rotate"]["Y"] = value.Y;
                    AllProperties["Rotate"]["Z"] = value.Z;
                }
            }

            [TypeConverter(typeof(Vector3Converter))]
            public Vector3 scale
            {
                get { return new Vector3(AllProperties["Scale"]["X"], AllProperties["Scale"]["Y"], AllProperties["Scale"]["Z"]); }
                set
                {
                    AllProperties["Scale"]["X"] = value.X;
                    AllProperties["Scale"]["Y"] = value.Y;
                    AllProperties["Scale"]["Z"] = value.Z;
                }
            }

            /*public string Template
            {
                get { return AllProperties; }
                set { AllProperties = value; }
            }*/

            public int priority;
            public List<Vector3> vertices = new List<Vector3>();
            public Vector3 bbMin;
            public Vector3 bbMax;

            public Dictionary<string, dynamic> AllProperties;

            public MapObject(dynamic _obj)
            {
                if (!(_obj is Dictionary<string, dynamic>)) throw new Exception("Game object node not supported");
                AllProperties = _obj;
            }

            public Vector3 calcBBMin()
            {
                Vector3 minTemp = new Vector3();
                List<Vector3> verts = vertices;
                for (int i = 0; i < verts.Count; i++)
                {
                    if (verts[i].X < minTemp.X) minTemp.X = verts[i].X;
                    if (verts[i].Y < minTemp.Y) minTemp.Y = verts[i].Y;
                    if (verts[i].Z < minTemp.Z) minTemp.Z = verts[i].Z;
                }
                bbMin = minTemp;
                return minTemp;
            }

            public Vector3 calcBBMax()
            {
                Vector3 maxTemp = new Vector3();
                List<Vector3> verts = vertices;
                for (int i = 0; i < verts.Count; i++)
                {
                    if (verts[i].X > maxTemp.X) maxTemp.X = verts[i].X;
                    if (verts[i].Y > maxTemp.Y) maxTemp.Y = verts[i].Y;
                    if (verts[i].Z > maxTemp.Z) maxTemp.Z = verts[i].Z;
                }
                bbMax = maxTemp;
                return maxTemp;
            }
        }
    }
}
