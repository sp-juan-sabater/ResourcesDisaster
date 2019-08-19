using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;


public class BuildingPlacement : MonoBehaviour {

	private PlaceableBuilding placeableBuilding;
	private Transform currentBuilding;
	private bool hasPlaced;

	public LayerMask buildingsMask;

	private PlaceableBuilding placeableBuildingOld;
    // Start is called before the first frame update
    void Start() {
        
    }

    Vector3 GetPosition()
    {

	        	Vector3 m = Input.mousePosition;
	        	m = new Vector3(m.x,m.y,transform.position.y);
	        	Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);
	        	return p;
    }

    // Update is called once per frame
    void Update() {
			if (Input.GetMouseButtonDown(0))
        	{
        		Debug.Log("click");
        		RaycastHit hit = new RaycastHit();

        		var p = GetPosition();
        		Ray ray = new Ray(new Vector3(p.x,8,p.z), Vector3.down);
        		if (Physics.Raycast(ray, out hit,Mathf.Infinity,buildingsMask))
        		{
        			hit.collider.gameObject.GetComponent<PlaceableBuilding>().SetSelected(true);
        			placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding>();

        		}
        		else 
        		{ 
        			if (placeableBuildingOld != null)
        			 {
        				placeableBuildingOld.SetSelected(false);
        			}
        		}

        		return;
        	}

        	var position = Vector3.zero;

        	if (currentBuilding != null)
        		position = currentBuilding.position;

        	if (Input.GetMouseButton(0))
        	{
	        	Vector3 p = GetPosition();
	        	position = new Vector3(p.x,0,p.z);
      		}

       		if (currentBuilding != null && !hasPlaced) 
       		{

	        	currentBuilding.position = position;

	        	if (Input.GetMouseButtonUp(0)) 
	        	{
	        		if (IsLegalPosition() == false)
	        		{
	        			FsmVariables.GlobalVariables.GetFsmInt(placeableBuilding.Resource).Value += placeableBuilding.Amount;

	        			GameObject.Destroy(currentBuilding.gameObject);
					}
				    	
			    	hasPlaced = false;
			    	currentBuilding = null;
			    	placeableBuilding = null;
        		}
	        }
    }

    // // Update is called once per frame
    // void Update() {



    //     	Vector3 m = Input.mousePosition;
    //     	m = new Vector3(m.x,m.y,transform.position.y);
    //     	Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);

    //     if (currentBuilding != null && !hasPlaced) {

    //     	currentBuilding.position = new Vector3(p.x,0,p.z);

    //     	if (Input.GetMouseButtonDown(0)) 
    //     	{
    //     		if (IsLegalPosition()) {

    //     			hasPlaced = true;
    //     		}
    //     	}
    //     }
    //     else {
    //     	if (Input.GetMouseButtonDown(0))
    //     	{
    //     		Debug.Log("click");
    //     		RaycastHit hit = new RaycastHit();
    //     		Ray ray = new Ray(new Vector3(p.x,8,p.z), Vector3.down);
    //     		if (Physics.Raycast(ray, out hit,Mathf.Infinity,buildingsMask))
    //     		{
    //     			hit.collider.gameObject.GetComponent<PlaceableBuilding>().SetSelected(true);
    //     			placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding>();

    //     		}
    //     		else 
    //     		{ 
    //     			if (placeableBuildingOld != null) {
    //     				placeableBuildingOld.SetSelected(false);
    //     			}
    //     		}
    //     	}
    //     }
    // }

    bool IsLegalPosition() {
    	if (placeableBuilding.colliders.Count > 0) {
    		return false;
    	}
    	return true;

    }

    public void SetItem(GameObject b) {

    	Debug.Log("Set item");
    	var pb = b.GetComponent<PlaceableBuilding>();
    	if (pb == null)
    		return;
    	
    	var CurrentAmount = FsmVariables.GlobalVariables.GetFsmInt(pb.Resource).Value;
    	if(CurrentAmount<pb.Amount)
    		return;

    	hasPlaced = false;
    	currentBuilding = ((GameObject)Instantiate(b)).transform;
    	placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
    }
}
