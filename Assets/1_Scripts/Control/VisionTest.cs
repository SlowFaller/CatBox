using UnityEngine;

namespace LD47.Control
{
    public class VisionTest : MonoBehaviour
    {    
        Vector3[] vertices = new Vector3[4];
        Vector3[] frustrumWorldPoints = new Vector3[4];
        Vector2[] UV = new Vector2[4];
        int[] triangles = new int[6];

        [SerializeField] [Range(-1f, 1f)] float frustrumHeight = 0.4f;
        [SerializeField] [Range(0.5f, 5f)] float frustrumLength = 1.0f;
        [SerializeField] [Range(10f, 180f)] float frustrumHAngle = 15f;
        [SerializeField] [Range(10f, 180f)] float frustrumVAngle = 15f;
        [SerializeField] [Range(0.1f, 2f)]float frustrumBackHeight = 1.0f;
        [SerializeField] [Range(0.1f, 2f)] float frustrumBackWidth = 1.0f;
        Vector3 center = Vector3.zero;

        GameObject FrustrumPoint;
        GameObject FrustrumTR;
        GameObject FrustrumBR;
        GameObject FrustrumBL;
        GameObject FrustrumTL;
        GameObject Cone;
        GameObject BackPlane;
        Vector3 forward = Vector3.zero;

        void Start()
        {
            Cone = new GameObject("Cone");
            BackPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);

            //forward = transform.forward;
            FrustrumPoint = GameObject.Find("FrustrumCenter");
            FrustrumTR = GameObject.Find("FrustrumTopRight");
            FrustrumBR = GameObject.Find("FrustrumBotRight");
            FrustrumBL = GameObject.Find("FrustrumBotLeft");
            FrustrumTL = GameObject.Find("FrustrumTopLeft");

            Mesh mesh = new Mesh();
            Cone.AddComponent<MeshFilter>();
            Cone.AddComponent<MeshRenderer>();
            Cone.AddComponent<MeshCollider>();
            Cone.GetComponent<MeshFilter>().mesh = mesh;

            Cone.transform.position = transform.position;
            BackPlane.transform.position = transform.position;
            CreateFrustrumVertices();
            CalculateTriangles();
            mesh.vertices = vertices;
            mesh.uv = UV;
            mesh.triangles = triangles;
        }

        // Update is called once per frame
        void Update()
        {
            CreateFrustrumVertices();
            Cone.GetComponent<MeshFilter>().mesh.vertices = vertices;
            FrustrumPoint.transform.position = center;
            FrustrumTR.transform.position = frustrumWorldPoints[0];
            FrustrumBR.transform.position = frustrumWorldPoints[1];
            FrustrumBL.transform.position = frustrumWorldPoints[2];
            FrustrumTL.transform.position = frustrumWorldPoints[3];
            BackPlane.transform.forward = -transform.forward;
            BackPlane.transform.position = transform.position;
            
            //Cone.transform.position = center;
            Cone.transform.position = transform.position;
            Cone.transform.forward = transform.forward;
        }

        // calculates the vertices
        void CreateFrustrumVertices()
        {
            center = new Vector3(transform.position.x, transform.position.y + frustrumHeight, transform.position.z);
            float halfX = frustrumBackWidth / 2;
            float halfY = frustrumBackHeight / 2;
            frustrumWorldPoints[0] = new Vector3(center.x + halfX, center.y + halfY, center.z);
            frustrumWorldPoints[1] = new Vector3(center.x + halfX, center.y - halfY, center.z);
            frustrumWorldPoints[2] = new Vector3(center.x - halfX, center.y - halfY, center.z);
            frustrumWorldPoints[3] = new Vector3(center.x - halfX, center.y + halfY, center.z);

            for(int i = 0; i < 4; i++)
            {
                vertices[i] = Cone.transform.InverseTransformPoint(frustrumWorldPoints[i]);
            }

            /*for(int i = 0; i < 4; i++)
            {
                Vector3 dir = vertices[i] - center;
                dir = Quaternion.Euler(transform.forward) * dir;
                vertices[i] = dir + center;
            } */
        }

        void CalculateTriangles()
        {
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;
        }

        Vector3 ConvertAngleToVector(float angle)
        {
            float radian = angle * (Mathf.PI / 180f);    
            return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
        }
    }
}
