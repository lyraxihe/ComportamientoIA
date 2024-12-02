using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class VisionCone : MonoBehaviour
{
    
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example
    public int VisionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    MeshCollider MeshCollider_;

    // Conevision colors
    public Color PatrolColor = new Color(1, 1, 1, 0.7f);
    public Color ChaseColor = new Color(1, 0, 0, 0.7f);
    public Color SeekColor = new Color(1, 1, 0, 0.7f);

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
        MeshCollider_ = gameObject.GetComponent<MeshCollider>();
        InvokeRepeating("DrawVisionCone", 0.1f, 0.1f);
        InvokeRepeating("DetectPlayer", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //DrawVisionCone();//calling the vision cone function everyframe just so the cone is updated every frame
        //DetectPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            transform.GetComponent<MeshRenderer>().material.color = ChaseColor;
            GetComponentInParent<EnemyBehaviour>().state = 1;
        }
    }

    void DetectPlayer()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = transform.forward;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, VisionRange))
        {
            if (hitInfo.collider.gameObject.layer == playerLayer)
                print("Player");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
            transform.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
            }


            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
        MeshCollider_.sharedMesh = MeshFilter_.mesh;
    }

}
