using UnityEngine;
using System.Collections;

public class Spherize : MonoBehaviour
{

    public float radius;
    public Material myMat;

    void Start()
    {
        Vector3[] positionArr = {
             new Vector3(0,0.5f,0),
             new Vector3(0,-0.5f,0),
             new Vector3(0,0,0.5f),
             new Vector3(0,0,-0.5f),
             new Vector3(0.5f,0,0),
             new Vector3(-0.5f,0,0)
         };

        Vector3[] rotationArr = {
             Vector3.zero,
             new Vector3(180,0,0),
             new Vector3(90,0,0),
             new Vector3(-90,0,0),
             new Vector3(0,0,-90),
             new Vector3(0,0,90)
         };

        for (int p = 0; p < 6; p++)
        {
            //make 6 planes for the sphere in this loop
            GameObject newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            newPlane.transform.SetParent(transform);
            newPlane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            newPlane.transform.position = positionArr[p];
            newPlane.transform.eulerAngles = rotationArr[p];
            Vector3[] vertices = newPlane.GetComponent<MeshFilter>().mesh.vertices;

            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = newPlane.transform.InverseTransformPoint(newPlane.transform.TransformPoint(vertices[i]).normalized * this.radius);
            }

            newPlane.GetComponent<MeshFilter>().mesh.vertices = vertices;
            newPlane.GetComponent<MeshCollider>().sharedMesh = newPlane.GetComponent<MeshFilter>().mesh;
            newPlane.GetComponent<MeshCollider>().convex = true;
            newPlane.GetComponent<MeshFilter>().mesh.RecalculateNormals();
            newPlane.GetComponent<MeshFilter>().mesh.RecalculateBounds();

            //set the material
            newPlane.GetComponent<Renderer>().material = myMat;
            newPlane.GetComponent<MeshRenderer>().enabled = false;

            //Add cloth componants for physics
            newPlane.AddComponent<Cloth>();

            ClothSkinningCoefficient[] newConstraints;
            newConstraints = newPlane.GetComponent<Cloth>().coefficients;

            newConstraints[0].maxDistance = 2.5f;
            newConstraints[1].maxDistance = 2.5f;
            newConstraints[2].maxDistance = 2.5f;
            newConstraints[3].maxDistance = 2.5f;
            newConstraints[4].maxDistance = 2.5f;
            newConstraints[5].maxDistance = 2.5f;
            newConstraints[6].maxDistance = 2.5f;
            newConstraints[7].maxDistance = 2.5f;
            newConstraints[8].maxDistance = 2.5f;
            newConstraints[9].maxDistance = 2.5f;
            newConstraints[10].maxDistance = 2.5f;

            newConstraints[11].maxDistance = 2.5f;
            newConstraints[21].maxDistance = 2.5f;
            newConstraints[22].maxDistance = 2.5f;
            newConstraints[32].maxDistance = 2.5f;
            newConstraints[33].maxDistance = 2.5f;
            newConstraints[43].maxDistance = 2.5f;
            newConstraints[44].maxDistance = 2.5f;
            newConstraints[54].maxDistance = 2.5f;
            newConstraints[55].maxDistance = 2.5f;
            newConstraints[65].maxDistance = 2.5f;
            newConstraints[66].maxDistance = 2.5f;
            newConstraints[76].maxDistance = 2.5f;
            newConstraints[77].maxDistance = 2.5f;
            newConstraints[87].maxDistance = 2.5f;
            newConstraints[88].maxDistance = 2.5f;
            newConstraints[98].maxDistance = 2.5f;
            newConstraints[99].maxDistance = 2.5f;
            newConstraints[109].maxDistance = 2.5f;

            newConstraints[110].maxDistance = 2.5f;
            newConstraints[111].maxDistance = 2.5f;
            newConstraints[112].maxDistance = 2.5f;
            newConstraints[113].maxDistance = 2.5f;
            newConstraints[114].maxDistance = 2.5f;
            newConstraints[115].maxDistance = 2.5f;
            newConstraints[116].maxDistance = 2.5f;
            newConstraints[117].maxDistance = 2.5f;
            newConstraints[118].maxDistance = 2.5f;
            newConstraints[119].maxDistance = 2.5f;
            newConstraints[120].maxDistance = 2.5f;

            newPlane.GetComponent<Cloth>().coefficients = newConstraints;

            //define cloth parametres
            newPlane.GetComponent<Cloth>().stretchingStiffness = 0.07f;
            newPlane.GetComponent<Cloth>().useGravity = false;
            newPlane.GetComponent<Cloth>().worldAccelerationScale = 0.7f;

            //create timed wave
            newPlane.AddComponent<WaveController>();
            //set up parameters
            newPlane.GetComponent<WaveController>().height = 2.0f;
            newPlane.GetComponent<WaveController>().time = 4.0f;
        }
    }
}