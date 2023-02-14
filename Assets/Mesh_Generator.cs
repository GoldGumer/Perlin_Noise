using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mesh_Generator : MonoBehaviour
{
    [SerializeField] Vector2Int fieldSize;

    Mesh field;

    Vector3[] vertices;
    int[] trianglesList;

    // Start is called before the first frame update
    void Start()
    {
        field = new Mesh();
        GetComponent<MeshFilter>().mesh = field;

        CreateVertexList();
    }

    void CreateVertexList()
    {
        vertices = new Vector3[(fieldSize.x + 1) * (fieldSize.y + 1)];
        for (int i = 0; i < (fieldSize.y + 1); i++)
        {
            for (int j = 0; j < (fieldSize.x + 1); j++)
            {
                vertices[j + i * (fieldSize.x + 1)] = new Vector3(i, 0, j);
            }
        }

        vertices[5] = new Vector3(vertices[5].x, 3, vertices[5].z);

        trianglesList = new int[fieldSize.x * fieldSize.y * 2 * 3];
        for (int i = 0; i < fieldSize.y; i++)
        {
            for (int j = 0; j < fieldSize.x; j++)
            {
                int offset = fieldSize.x + 1;
                int indexOffset = j + (i * offset);
                int arrayOffset = j * 6 + i * 6 * fieldSize.x;

                trianglesList[arrayOffset] = indexOffset + offset;
                trianglesList[arrayOffset + 1] = indexOffset;
                trianglesList[arrayOffset + 2] = indexOffset + 1;

                trianglesList[arrayOffset + 3] = indexOffset + offset + 1;
                trianglesList[arrayOffset + 4] = indexOffset + offset;
                trianglesList[arrayOffset + 5] = indexOffset + 1;
            }
        }
    }

    void UpdateMesh()
    {
        field.Clear();
        field.vertices = vertices;
        field.triangles = trianglesList;
    }

    void OnDrawGizmos()
    {
        if (vertices == null) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = new Color(i * 0.1f, i * 0.1f, i * 0.1f);
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMesh();
    }
}
