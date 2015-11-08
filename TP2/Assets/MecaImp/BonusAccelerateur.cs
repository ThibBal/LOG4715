using UnityEngine;
using System.Collections;

public class BonusAccelerateur : MonoBehaviour {
	[SerializeField] private float VitesseBonus = 30.0f;
	[SerializeField] private float Duree = 1.5f;
	
	void OnCollisionEnter(Collision collision)
	{
		Rigidbody car = collision.gameObject.GetComponent<Rigidbody>();
		StartCoroutine("BonusCoroutine", car);
	}
	
	IEnumerator BonusCoroutine(Rigidbody car)
	{
		float time = 0f;
		while(time < Duree)
		{
			time += Time.fixedDeltaTime;
			car.AddForce(car.rotation * new Vector3(0.0f, 0.0f, VitesseBonus));
			yield return new WaitForFixedUpdate();
		}
	}
}