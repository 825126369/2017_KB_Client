using UnityEngine;
using System.Collections;

public class register_commit_btn : MonoBehaviour {

	public static UIImageButton button;
	
	void Awake ()   
	{
		button = GetComponent<UIImageButton>();
		NGUITools.SetActive(button.gameObject, false);
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onClick()
	{
		
	}
}
