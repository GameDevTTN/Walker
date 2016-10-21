using UnityEngine;
using System.Collections;

public class walkAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		randomPoint = new Vector3(0f, transform.position.y, 0f);
		InvokeRepeating("updateImage", 0.5f, 0.1f);
	}

	// Update is called once per frame
	void Update () {

	}

	public Material[] images;
	private int lastImage = 0;
	private Vector3 randomPoint;
	void updateImage()
	{
		while (Vector3.Distance(transform.position, randomPoint) < 0.1f)
		{
			randomPoint = new Vector3(Random.Range(-2f, 2f), transform.position.y, Random.Range(-2f, 2f));
		}
		Vector3 move = randomPoint - transform.position;
		move = move.normalized * 0.1f;
		transform.Translate(move,Space.World);
		lastImage = 1 - lastImage;
		GetComponent<MeshRenderer>().material = images[lastImage];
	}
}
