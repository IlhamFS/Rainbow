using UnityEngine;
using System.Collections;

public class BoundaryScript : MonoBehaviour {

	void OnTriggerExit2D(Collider2D coll) {
		Destroy (coll.gameObject);
	}
}
