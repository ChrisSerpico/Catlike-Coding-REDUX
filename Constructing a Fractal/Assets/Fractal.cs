using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

	public Mesh[] meshes; 
	public Material material; 

	public int maxDepth; 

	private int depth; 

	public float childScale; 

	private Material[] materials; 

	[Range(0, 1)] // added since we don't want ranges outside of these values 
	public float spawnProbability; 

	public float maxRotationSpeed; 
	private float rotationSpeed; 

	public float maxTwist; 

	private void InitializeMaterials() {
		materials = new Material[maxDepth + 1]; 
		for (int i = 0; i <= maxDepth; i++) {
			float t = (float) i / maxDepth; 
			t *= t; 
			materials[i] = new Material(material); 
			// Changed from original for truly random colors 
			materials[i].color = Color.Lerp(Color.white, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), t); 
		}
	}

	// Use this for initialization
	void Start () {
		if (materials == null) {
			InitializeMaterials(); 
		}
		gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)]; 
		gameObject.AddComponent<MeshRenderer>().material = materials[depth]; 
		rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed); 
		transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f); 
		if (depth < maxDepth) {
			StartCoroutine(CreateChildren()); 
		}
	}

	void Update() {
		transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); 
	}

	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f), 
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
	}; 

	private IEnumerator CreateChildren() {
		for (int i = 0; i < childDirections.Length; i++) {
			if (Random.value < spawnProbability) {
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f)); 
				new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
			}
		}
	}

	private void Initialize(Fractal parent, int childIndex) {
		meshes = parent.meshes; 
		materials = parent.materials; 
		material = parent.material; 
		maxDepth = parent.maxDepth; 
		depth = parent.depth + 1; 
		childScale = parent.childScale; 
		spawnProbability = parent.spawnProbability; 
		maxRotationSpeed = parent.maxRotationSpeed; 
		maxTwist = parent.maxTwist; 
		transform.parent = parent.transform; 
		transform.localScale = Vector3.one * childScale; 
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale); 
		transform.localRotation = childOrientations[childIndex]; 
	}
}
