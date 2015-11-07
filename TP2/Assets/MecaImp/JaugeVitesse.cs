using UnityEngine;
using UnityEngine.UI;

public class JaugeVitesse : MonoBehaviour {
	
	public Texture2D cadranVitesse ;
	public Texture2D aiguilleVitesse ;
	CarController carController;
	
	float vitesse;
	float vitesseMax;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("Joueur 1");
		carController = player.GetComponent<CarController> ();
		vitesseMax = carController.MaxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		vitesse = carController.CurrentSpeed;
	}
	
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