using UnityEngine;
using UnityEngine.UI;

public class JaugeBoost : MonoBehaviour
{
	public Texture2D cadranBoost ;
	public Texture2D aiguilleBoost ;
	EtatVoiture etat;
	float boost;
	float boostMax;

	void Start () {
		GameObject player = GameObject.Find("Joueur 1");
		etat = player.GetComponent<EtatVoiture> ();
		boostMax = etat.Boost;
	}

	void Update () {
		boost = etat.Boost;
	}

	//Affichage de l'aiguille de la nitro, sur le meme cadran que la vitesse mais la rotation est inversée.
	void OnGUI() {
		//GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height-70,100,50),cadranBoost);
		float spFactor = boost / boostMax;
		float rotationAngle ;

		rotationAngle = Mathf.Lerp(0,180,spFactor);

		GUIUtility.RotateAroundPivot(rotationAngle,new Vector2(Screen.width-100,Screen.height- 100));
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height- 150,100,100),aiguilleBoost);
		
	}
}