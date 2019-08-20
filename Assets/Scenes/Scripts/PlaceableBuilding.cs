﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

	[HideInInspector]
	public List<Collider> colliders = new List<Collider>();
	private bool isSelected;
	public int Amount;
	public string Resource;


	void OnGUI() {
		if (isSelected) {
			GUI.Button(new Rect(100, 200, 100, 30), name);
		}
	}

    void OnTriggerEnter(Collider c) {
    	if (c.CompareTag("Building")) {
    		colliders.Add(c);
    	}
    }

    void OnTriggerExit(Collider c) {
    	if (c.CompareTag("Building")) {
    		colliders.Remove(c);
    	}
    }

    public void SetSelected(bool s) {
    	isSelected = s;
    }

}
