using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

	public GameObject[] buildings;
	private BuildingPlacement buildingPlacement;

    // Start is called before the first frame update
    void Start() {
    	buildingPlacement = GetComponent<BuildingPlacement>();
    }

    Rect GetRect(int i)
    {
    	return new Rect(Screen.width/20,Screen.height/15 + Screen.height/12 * i,100,30);
    }

    // Update is called once per frame
    void Update() {

    	if(Input.GetMouseButtonDown(0))
    	{
    		Debug.Log(Input.mousePosition);

    		var position = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

    		Debug.Log(position);
		for (int i = 0; i <buildings.Length; i ++) 
		{
    		var rect = GetRect(i);

    			Debug.Log(rect);
    		if (rect.Contains(position))
    		{
    			Debug.Log("button down");
				buildingPlacement.SetItem(buildings[i]);
				return;
			}
    	}
    }
    }

	void OnGUI() { 
		for (int i = 0; i <buildings.Length; i ++) {
			GUI.Button(GetRect(i), buildings[i].name);
		}
    }
}
