using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	public Animator _animator;
	public GameObject[] CheckPoint;
	public string NameVariableWalk;
	public float MinDistanceToPoint;
	public float SpeedRotate; 
	public int walk, target;

	void Start () {
		target = Random.Range(0, CheckPoint.Length);
	}
	

	void Update () {
	   
		if(Vector3.Distance(transform.position, CheckPoint[target].transform.position) > MinDistanceToPoint)
		{
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(CheckPoint[target].transform.position.x, transform.position.y, CheckPoint[target].transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z)), SpeedRotate * Time.deltaTime);	                                   
		}
		else
		{
			target = Random.Range(0, CheckPoint.Length);
		}

	}

	void FixedUpdate () {
		_animator.SetInteger(NameVariableWalk, walk);
	}
}
