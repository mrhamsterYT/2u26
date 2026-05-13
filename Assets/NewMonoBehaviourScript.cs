using UnityEngine;

using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    public float damageRadius = 0.5f; // Радиус вмятины
    public float damageDepth = 0.2f;   // Глубина вмятины
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        modifiedVertices = (Vector3[])originalVertices.Clone();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Считаем силу удара
        if (collision.relativeVelocity.magnitude > 2f)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                DeformMesh(contact.point);
            }
        }
    }

    void DeformMesh(Vector3 contactPoint)
    {
        // Переводим точку удара в локальные координаты объекта
        contactPoint = transform.InverseTransformPoint(contactPoint);

        for (int i = 0; i < modifiedVertices.Length; i++)
        {
            float distance = Vector3.Distance(contactPoint, modifiedVertices[i]);
            if (distance < damageRadius)
            {
                // Смещаем вершину внутрь объекта
                Vector3 direction = (modifiedVertices[i] - contactPoint).normalized;
                modifiedVertices[i] -= direction * (damageRadius - distance) * damageDepth;
            }
        }

        // Обновляем сетку
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();

        // Обновляем коллайдер, чтобы он тоже стал «мятым» (ресурсозатратно!)
        if (GetComponent<MeshCollider>())
            GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}