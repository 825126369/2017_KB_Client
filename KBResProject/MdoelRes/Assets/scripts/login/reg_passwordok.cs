using UnityEngine;
using System.Collections;

public class reg_passwordok : MonoBehaviour {

	public static UIInput input;
	
	void Awake ()   
	{
		input = GetComponent<UIInput>();
		NGUITools.SetActive(input.gameObject, false);
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
