using UnityEngine;
using System.Collections;

public class moveVertex : MonoBehaviour {
	public Vector3[] vertices;
	Vector3 goal;
	Mesh mesh;
	void Start(){
		mesh = GetComponent<MeshFilter>().mesh;
	}
	void Update() {
		goal = transform.parent.GetComponent<MoveTo>().goal.position;

		 vertices = mesh.vertices;
		Vector3 V1 = transform.TransformPoint(vertices[2]);
		V1 = goal;
		vertices[2] = transform.InverseTransformPoint(V1);
		Vector3 V2 = transform.TransformPoint(vertices[5]);
		V2 = goal;
		vertices[5] = transform.InverseTransformPoint(V2);
		Vector3 V3 = transform.TransformPoint(vertices[8]);
		V3 = goal;
		vertices[8] = transform.InverseTransformPoint(V3);
		Vector3 V4 = transform.TransformPoint(vertices[11]);
		V4 = goal;
		vertices[11] = transform.InverseTransformPoint(V4);
		mesh.vertices = vertices;
	}
}

