using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour {

	public Color color;

	private string prefabName;
	public string PrefabName {
		get {
			return prefabName;
		}
		set {
			prefabName = value;
		}
	}

	private int rotateCount = 0;
	public int RotateCount {
		get {
			return rotateCount;
		}
		set {
			rotateCount = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer> ();

		foreach (Renderer r in renderers) {
			r.material.color = color;
		}
	}

	public void RotateBlock ()
	{
		transform.Rotate (0, 0, 90.0f);
		rotateCount++;
		if (rotateCount > 3) {
			rotateCount = 0;
		}
	}

	public void RemoveBlock ()
	{
		Debug.Log ("is ok!");
		Destroy (gameObject);
	}
}
