using UnityEngine;
using UnityEngine.UI;

public class JaugeBoost : MonoBehaviour
{
	public Texture2D cadranBoost ;
	public Texture2D aiguilleBoost ;
//	CarController carController;
	EtatVoiture etat;
	float boost;
	float boostMax;

	void Start () {
		GameObject player = GameObject.Find("Joueur 1");
//		carController = player.GetComponent<CarController> ();
		etat = player.GetComponent<EtatVoiture> ();
		boostMax = etat.Boost;
	}

	void Update () {
		boost = etat.Boost;
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height-70,100,50),cadranBoost);
		float spFactor = boost / boostMax;
		float rotationAngle ;
		
		//if (boost >= 0){
			rotationAngle = Mathf.Lerp(0,180,spFactor);
		//}
		//else {
		//	rotationAngle = Mathf.Lerp(0,180,-spFactor);
		//}
		GUIUtility.RotateAroundPivot(rotationAngle,new Vector2(Screen.width-100,Screen.height- 100));
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height- 150,100,100),aiguilleBoost);
		
	}
}