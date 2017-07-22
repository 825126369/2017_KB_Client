using UnityEngine;
using System.Collections;

public class curravatar_shadow : MonoBehaviour {

	public float shadowHeight = 0.01f;
	public float shadowOpacity = 0.6f;
	public float shadowSize = 1.0f ;
	public Transform shadowPosition = null;

	private float initHeight;
	private float currentHeight;
	
	void Awake ()   
	{
	}
	
	// Use this for initialization
	void Start () {
		initHeight = shadowPosition.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3(shadowPosition.transform.position.x, shadowHeight, shadowPosition.transform.position.z);
		
		currentHeight = shadowPosition.transform.position.y;
		float shadowRate = (initHeight/currentHeight);
		
		transform.localScale = new Vector3(1.0f,1.0f,1.0f) * shadowSize + new Vector3(1.0f,1.0f,1.0f) * (shadowRate * 0.2f);
		Color c = GetComponent<Renderer>().material.color;
		c.a = shadowOpacity + (shadowRate * 0.1f) ; 
		GetComponent<Renderer>().material.color = c;
	}
}
