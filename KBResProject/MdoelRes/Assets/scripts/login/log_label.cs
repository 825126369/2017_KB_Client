using UnityEngine;
using System.Collections;

public class log_label : MonoBehaviour {

	public static UILabel obj;
	
	// Use this for initialization
	void Start () {
		obj = GetComponent<UILabel>();
		obj.color = UnityEngine.Color.green;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
