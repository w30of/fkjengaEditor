using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour {

	void OnMouseDrag ()
	{
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 11);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		transform.parent.transform.position = new Vector3 (Mathf.Round(objPosition.x), Mathf.Round(objPosition.y), transform.parent.transform.position.z);
	}

	void OnMouseUp ()
	{
		Vector3 pos = transform.position;
		if (pos.x >= 3 && pos.y <= -3) {
			transform.parent.GetComponent<LineControl> ().RemoveBlock ();
		}
	}

	public void RotateParentBlock ()
	{
		transform.parent.GetComponent<LineControl> ().RotateBlock ();
	}
}
