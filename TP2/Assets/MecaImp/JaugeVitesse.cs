using UnityEngine;
using UnityEngine.UI;

public class JaugeVitesse : MonoBehaviour {
	
	public Texture2D cadranVitesse ;
	public Texture2D aiguilleVitesse ;
	CarController carController;
	
	float vitesse;
	float vitesseMax;

	void Start () {
		GameObject player = GameObject.Find("Joueur 1");
		carController = player.GetComponent<CarController> ();
		vitesseMax = carController.MaxSpeed;
	}

	void Update () {
		vitesse = carController.CurrentSpeed;
	}

	//Affichage du cadran avec une aiguille pour le compteur vitesse. 
	//Source: https://www.youtube.com/watch?v=UbzbYDhJQRQ
	void OnGUI() {
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height-150,100,50),cadranVitesse);
		float spFactor = vitesse / vitesseMax;
		float rotationAngle ;
		
		if (vitesse >= 0){
			rotationAngle = Mathf.Lerp(0,180,spFactor);
		}
		else {
			rotationAngle = Mathf.Lerp(0,180,-spFactor);
		}
		GUIUtility.RotateAroundPivot(rotationAngle,new Vector2(Screen.width-100,Screen.height-100));
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height-150,100,100),aiguilleVitesse);
		
	}
}