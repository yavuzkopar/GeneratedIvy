using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IvyGenerator : MonoBehaviour
{
    int segment = 6;
    int length = 0;
    private Mesh mesh;
    [SerializeField] Transform centerPoint;
    int boyut;
    float amount = 2 * Mathf.PI / 6;
    
    public float radius = 1;
    List<Vector3> centers = new List<Vector3>();
    [SerializeField] GameObject flower;
    [SerializeField] GameObject branch;
    [SerializeField] Transform head;
    private void Awake()
    {
        mesh = new Mesh();
        //   backVertices= new Vector3[segment];
        amount = 2 * Mathf.PI / segment;
        mesh.MarkDynamic();
    }
    float timer;
    float flowerTime;
    float branchTime;
    private void Update()
    {
        if (radius <= 0) return;
        flowerTime += Time.deltaTime;
        timer += Time.deltaTime;
        branchTime+= Time.deltaTime;
        if (timer > 0.05f && radius > 0)
        {
            //  circleDatas.Add(new CircleData());
            centers.Add(centerPoint.position);
            for (int i = 0; i < segment; i++)
            {
                DrawForward();
            }
            radius -= 0.001f;
            head.localScale = Vector3.one * radius;
            timer = 0;
            length++;
        }
        if(flowerTime >= 1)
        {
            float randoFloat = Random.Range(0f, 100f);
            Vector3 randomPos = centerPoint.TransformPoint(new Vector3(Mathf.Sin(randoFloat), 0, Mathf.Cos(randoFloat)) * radius);
            GameObject go = Instantiate(flower,randomPos,Quaternion.identity);
            go.transform.forward = centerPoint.position - randomPos;
            flowerTime = 0;
        }
        //if (branchTime > 5)
        //{
        //    GenerateBranch();
        //    branchTime = 0;
        //}
     //   SetCircles();
        //  if (Input.GetKeyDown(KeyCode.A))

    }
    void Start()
    {
        DrawMesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    private void DrawMesh()
    {
        Vector3[] vertices = new Vector3[segment];
        float amount = 2 * Mathf.PI / segment;
        mesh.vertices.CopyTo(vertices, 0);

        for (int i = 0; i < segment; i++)
        {
            int index = i;
            vertices[index] = centerPoint.position;
        }
        //    length++;
        mesh.vertices = vertices;
    }
    int ind = 0;
    private void DrawForward()
    {
        DrawForward(ind);
        ind++;
    }
    int flowerTimer;
    private void DrawForward(int j)
    {
        float amount = 2 * Mathf.PI / segment;
        Vector3[] vertices = new Vector3[mesh.vertices.Length + 1];
        Vector2[] uvs = new Vector2[mesh.uv.Length + 2];
        int[] triangles = new int[mesh.triangles.Length + 3];

        mesh.vertices.CopyTo(vertices, 0);
        mesh.uv.CopyTo(uvs, 0);
        mesh.triangles.CopyTo(triangles, 0);

        int vIndex = vertices.Length - 7 - ind;
        int vIndex0 = vIndex;
        int vIndex2 = vIndex + 6;



        Vector3 nextVertex1 = centerPoint.TransformPoint(new Vector3(Mathf.Sin(j * amount), 0, Mathf.Cos(j * amount)) * radius);
        vertices[vIndex2] = nextVertex1;
        //  circleDatas[length].circlePoints.Add(nextVertex1);
        nextVertex1 = centerPoint.TransformPoint(new Vector3(Mathf.Sin((1 + j) * amount), 0, Mathf.Cos((j + 1) * amount)) * radius);
        int tIndex = triangles.Length - 3;
        
        //     circleDatas[length].circlePoints.Add(nextVertex1);
        triangles[tIndex] = vIndex0;
        triangles[tIndex + 1] = vIndex0 + 1;
        triangles[tIndex + 2] = vIndex0 + 6;

        mesh.vertices = vertices;
        mesh.triangles = triangles;



        vertices = new Vector3[mesh.vertices.Length + 1];
        uvs = new Vector2[mesh.uv.Length + 2];
        triangles = new int[mesh.triangles.Length + 3];
        mesh.vertices.CopyTo(vertices, 0);
        mesh.uv.CopyTo(uvs, 0);
        mesh.triangles.CopyTo(triangles, 0);

        vIndex = vertices.Length - 7 - ind;
        vIndex0 = vIndex;
        vIndex2 = vIndex + 6;
        vertices[vIndex2] = nextVertex1;

        tIndex = triangles.Length - 3;

        triangles[tIndex] = vIndex0;
        triangles[tIndex + 1] = vIndex0 + 6;
        triangles[tIndex + 2] = vIndex0 + 5;
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
    void SetCircles()
    {
        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        Vector2[] uvs = new Vector2[mesh.uv.Length];
        int[] triangles = new int[mesh.triangles.Length];

        mesh.vertices.CopyTo(vertices, 0);
        mesh.uv.CopyTo(uvs, 0);
        mesh.triangles.CopyTo(triangles, 0);
        int index = 5;
        int index2 = 0;
        for (int i = 6; i < vertices.Length; i += 12)
        {

            for (int j = 0; j < 12; j++)
            {
                index++;
                Vector3 dir = vertices[index] - centers[index2];
                dir.y = centers[index2].y;

                vertices[index] += dir.normalized * Time.deltaTime/10;
                Debug.DrawRay(vertices[index], dir);
            }
            index2++;
            //Vector3 vvertNormal= transform.TransformDirection(vertices[i]);
            //  vertices[i] = vvertNormal*2;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
    void GenerateBranch()
    {
        GameObject go = Instantiate(branch);
        
        go.GetComponent<IvyGenerator>().radius= radius;
        go.GetComponent<IvyGenerator>().centerPoint.position = centerPoint.position;
    }
}
public class CircleData
{
    public List<Vector3> circlePoints;

    public CircleData()
    {
        this.circlePoints = new List<Vector3>();
    }
}
