using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingOrder : MonoBehaviour {

	public string sortingLayer;
	public int orderInLayer;

	void Awake () {
		//SetSortingLayer ();
	}
 
	[ContextMenu ("Update sorting layer settings")]
	void UpdateSortingLayerSettings () {
		//SetSortingLayer ();
	}
 
	private void SetSortingLayer () {
        MeshRenderer[] floor = GetComponentsInChildren<MeshRenderer>();
        Debug.Log(floor.Length);
        Debug.Log(floor[0]);
        Debug.Log(floor[50]);
        int i = 0;
        foreach (MeshRenderer rend in floor)
        {
            rend.sortingLayerName = sortingLayer;
            rend.sortingOrder = orderInLayer;
            Debug.Log(floor[i].sortingLayerName);
        }
	}
}
