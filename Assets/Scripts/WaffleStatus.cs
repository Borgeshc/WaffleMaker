using UnityEngine;
using System.Collections;

public class WaffleStatus : MonoBehaviour {

    public enum Status
    {
        Cooked,
        Overcooked,
        Undercooked,
    }

    //[HideInInspector]
    public Status currentStatus = Status.Undercooked;
    [HideInInspector]
    public bool hasSyrup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}