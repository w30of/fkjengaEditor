using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public GameObject blockContainer;
	public Text genText;
	public InputField LevelInput;
	public InputField IndexInput;

	void Update ()
	{
		if (Input.GetMouseButtonUp (1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitObj;
			if (Physics.Raycast (ray, out hitObj)) {
				(hitObj.transform.gameObject as GameObject).GetComponent<BlockControl> ().RotateParentBlock ();
			}
		}
	}
		
	// Button functions...
	public void createBlock (string name)
	{
		instantiateBlock (name);
	}
		
	public void saveCurrent ()
	{
		if (!checkInput ())
			return;

		// Delete saved file, I will create a new one
		FileManager.DeleteFile (Application.persistentDataPath, getSaveFileName());

		// Combine block datas
		string data = "";
		int childCount = blockContainer.transform.childCount;
		for (int i = 0; i < childCount; ++i) {
			Transform t = blockContainer.transform.GetChild (i);
			LineControl l = t.gameObject.GetComponent<LineControl>();
			data += l.PrefabName + ",";
			data += t.position.x + "," + t.position.y + "," + t.position.z + ",";
			data += l.RotateCount;

			if (i < childCount - 1)
				data += "\n";
		}
		genText.text = "File has save at:\n"+Application.persistentDataPath+getSaveFileName();
		FileManager.CreateFile (Application.persistentDataPath, getSaveFileName(), data);
	}

	public void LoadBlock ()
	{
		if (!checkInput ())
			return;

		// Remove all block in the container
		foreach (Transform child in blockContainer.transform) {
			GameObject.Destroy(child.gameObject);
		}

		ArrayList list = FileManager.LoadFile (Application.persistentDataPath, getSaveFileName());
		if (list != null) {
			foreach (string line in list) {
				createBlockFromStringParam (line);
			}
		} else {
			genText.text = "File : " + getSaveFileName () + " not found!";
		}
	}





	// Aux functions...
	private GameObject instantiateBlock (string name)
	{
		GameObject newBlock = (GameObject)Instantiate (Resources.Load("Prefabs/"+name), new Vector3(0, 4, 0), transform.rotation);
		newBlock.transform.parent = blockContainer.transform;
		newBlock.GetComponent<LineControl> ().PrefabName = name;
		return newBlock;
	}

	private string getSaveFileName ()
	{
		return "block_data" + LevelInput.text + "-" + IndexInput.text + ".txt";
	}

	private bool checkInput ()
	{
		int level = -1;
		if (!Int32.TryParse (LevelInput.text, out level)) {
			genText.text = "Save failed, make sure your 'level' is correct.";
			return false;
		}
		int index = -1;
		if (!Int32.TryParse (IndexInput.text, out index)) {
			genText.text = "Save failed, make sure your 'index' is correct.";
			return false;
		}
		return true;
	}

	private void createBlockFromStringParam (String param)
	{
		String[] arrParam = param.Split (',');
		if (arrParam.Length == 5) {
			// Instantiate a block
			GameObject obj = instantiateBlock (arrParam[0]);

			// Set the block position
			obj.transform.position = new Vector3 (float.Parse(arrParam[1]),float.Parse(arrParam[2]),float.Parse(arrParam[3]));

			// Set the block rotation
			int rotateCount = int.Parse(arrParam[4]);
			obj.transform.Rotate (new Vector3(0, 0, 90.0f*rotateCount));
			LineControl comp = obj.GetComponent<LineControl> ();
			comp.RotateCount = rotateCount;
//			obj.transform.rotation = new Quaternion (float.Parse (arrParam [4]), float.Parse (arrParam [5]), float.Parse (arrParam [6]), float.Parse (arrParam [7]));
		} else {
			genText.text = "File format invalid!";
		}
	}
}
