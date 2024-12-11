
using ComportamientoIA.Runtime.Managers;
using ComportamientoIA.Runtime.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class VisionCone : MonoBehaviour
{
    
    public Material  VisionConeMaterial;
    public float     VisionRange;
    public float     VisionAngle;
    public float     PatrolVisionRange;
    public float     PatrolVisionAngle;
    public float     VisionRadAngle;
    public LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example
    public int       VisionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be

    // Conevision colors
    public Color PatrolColor = new Color(1, 1, 1, 0.7f);
    public Color ChaseColor  = new Color(1, 0, 0, 0.7f);
    public Color SeekColor   = new Color(1, 1, 0, 0.7f);

    public LayerMask playerLayer;

    public  MeshRenderer   _MeshRenderer;

    private Mesh           VisionConeMesh;
    private MeshFilter     _MeshFilter;
    private MeshCollider   _MeshCollider;
    private EnemyBehaviour _controller;

    void Start()
    {
        _controller = GetComponentInParent<EnemyBehaviour>();
        
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;

        _MeshFilter    =  transform.AddComponent<MeshFilter>();
        VisionConeMesh =  new Mesh();
        VisionRadAngle =  VisionAngle * Mathf.Deg2Rad;
        _MeshCollider  =  this.GetComponent<MeshCollider>();
        _MeshRenderer  =  this.GetComponent<MeshRenderer>();

        InvokeRepeating(nameof(DrawVisionCone), 0.1f, 0.2f);
    }

    void DrawVisionCone()
    {
        int[]     triangles     = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices      = new Vector3[VisionConeResolution + 1];
        Vertices[0]             = Vector3.zero;
        float     Currentangle  = -VisionRadAngle / 2;
        float     angleIcrement = VisionRadAngle / (VisionConeResolution - 1);
        float     Sine;
        float     Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine   = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);

            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward      = (Vector3.forward * Cosine) + (Vector3.right * Sine);

            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
                Vertices[i + 1] = VertForward * hit.distance;
            else
                Vertices[i + 1] = VertForward * VisionRange;

            Currentangle += angleIcrement;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i]     = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        VisionConeMesh.Clear();

        VisionConeMesh.vertices  = Vertices;
        VisionConeMesh.triangles = triangles;
        _MeshFilter.mesh         = VisionConeMesh;
        _MeshCollider.sharedMesh = _MeshFilter.mesh;
    }

    public void changeColorTo(Color color)
    {
        _MeshRenderer.material.color = color;
    }

    public void ChaseSeekStateRange()
    {
        VisionRange = PatrolVisionRange;/** 2;*/
        VisionAngle = PatrolVisionAngle;/** 2;*/
    }

    public void PatrolStateRange()
    {
        VisionRange = PatrolVisionRange;
        VisionAngle = PatrolVisionAngle;
    }
}

