using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RedCarpet
{
    public class Object
    {
        public MapObject mpobj = new MapObject();
        public List<MapObject> mobjs = new List<MapObject>();

        public class MapObject
        {
            public string objectID;
            public string Layer;
            public int priority;
            public string unitConfigName;
            public string modelName;
            public Vector3 position;
            public Vector3 rotation;
            public Vector3 scale;
            public List<Vector3> vertices = new List<Vector3>();
            public Vector3 bbMin;
            public Vector3 bbMax;

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
