using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.Shapes;

public class VisionCone : MonoBehaviour
{
    [SerializeField] UnityEvent<IDetectable> targetInSight; // returns null if player is out of sight

    [SerializeField] Material VisionConeMaterial;

    [SerializeField] float VisionRange;
    [SerializeField] float VisionAngleInDegrees;
    
    private float visionAngle;
    private bool hadTarget;

    [SerializeField] LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example
    [SerializeField] int VisionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;
    //Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
    //for the ones that you dont understand dont worry, just follow along
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        visionAngle = VisionAngleInDegrees * Mathf.Deg2Rad;
      
    }


    void Update()
    {
        DrawVisionCone();//calling the vision cone function everyframe just so the cone is updated every frame
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] vertecies = new Vector3[VisionConeResolution + 1];
        vertecies[0] = Vector3.zero;
        float currentAngle = -visionAngle / 2;
        float angleIcrement = visionAngle / (VisionConeResolution - 1);
     

        UpdateRaycasts(currentAngle, angleIcrement, vertecies);

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = vertecies;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }

    private void UpdateRaycasts(float currentAngle, float angleIncrement, Vector3[] vertecies)
    {
        float sine;
        float cosine;
        bool targetInRange = false;
        IDetectable detectableComponent = null;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            sine = Mathf.Sin(currentAngle);
            cosine = Mathf.Cos(currentAngle);
            Vector3 RaycastDirection = (transform.forward * cosine) + (transform.right * sine);
            Vector3 VertForward = (Vector3.forward * cosine) + (Vector3.right * sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                vertecies[i + 1] = VertForward * hit.distance;
                detectableComponent = CheckHit(hit);
                targetInRange = true;
            }
            else
            {
                vertecies[i + 1] = VertForward * VisionRange;

            }

            currentAngle += angleIncrement;
        }
        
        if (targetInRange)
        {
            targetInSight?.Invoke(detectableComponent);

        }
        else
        {
            targetInSight?.Invoke(null);
        }
    }

    private IDetectable CheckHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<IDetectable>(out IDetectable detectableComponent))
        {
            return detectableComponent;
        }
        return null;
    }

    public void DisableDetection()
    {
        gameObject.SetActive(false);
    }

}



