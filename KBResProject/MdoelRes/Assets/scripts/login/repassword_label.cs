using UnityEngine;
using System.Collections;

public class repassword_label : MonoBehaviour {

	public static UILabel obj;
	
	void Awake ()   
	{
		obj = GetComponent<UILabel>();
		NGUITools.SetActive(obj.gameObject, false);
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
