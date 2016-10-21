using UnityEngine;
using System.Collections;

public class snomanMove : MonoBehaviour {
	Vector3 velocity = Vector3.zero;

	public float forwardSpeed;
	public Vector3 scaleNum;




	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame

	void Update () {
		velocity.y = forwardSpeed;
		transform.position += velocity * Time.deltaTime;
		transform.localScale += scaleNum;



	} 




}
