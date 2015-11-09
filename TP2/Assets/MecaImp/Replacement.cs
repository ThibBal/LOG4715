using UnityEngine;
using System.Collections;

public class Replacement : MonoBehaviour {
	
	public string tag = "Player";
	
	public Transform replacementPoint;

	void OnTriggerEnter(Collider collider) {
		Transform car = collider.transform.parent.parent;
		if (car.tag == tag) {
			car.position = replacementPoint.position;
			car.rotation = replacementPoint.rotation;
			car.rigidbody.velocity = Vector3.zero;
			car.GetComponent<EtatVoiture>().changerScore(-300);
			car.GetComponent<EtatVoiture>().changerPV(-5);
		}
		//StartCoroutine(ReplacementCoroutine ());
	}

//	IEnumerator ReplacementCoroutine(){
//		int timer = 0;
//		while (timer<4) {
//			timer++;
//			yield return new WaitForSeconds(1.0f);
//		}
//
//	}
	
}