using UnityEngine;
using System.Collections;

public class EtatVoiture :	MonoBehaviour {

	public CarController car ;
	[SerializeField] private float boost = 50f;
	[SerializeField] private float boostMax = 50f;
	[SerializeField] public int PVmax = 50;
	public int PVvoiture = 50;
	public int Score = 0;


	// Boost de la voiture
	public float Boost {
		get {
			return this.boost;
		}
		set {
			boost = value;
		}
	}

	void Start () {
		car = GetComponent<CarController> ();
	}

	public void UtilisationBoost(bool BoutonBoost) {
		if (BoutonBoost && boost > 0) {
			// Utilisation du boost
			boost -= 1f;
			car.Boost = true;
		} else if(boost < boostMax) {
			// Recharge du boost
			boost += 0.1f;
			car.Boost = false;
		}
	}

	// Application des dégats ou ajout des points de vie
	public void changerPV(int PV){
		if (this.PVvoiture + PV < this.PVmax) {
			this.PVvoiture += PV;
			if (this.PVvoiture < 0){
				this.PVvoiture = 0;
			}
		} else {
			this.PVvoiture = this.PVmax;
		}

		// Rétroacion des dégats sur la vitesse maximale de la voiture
		if (this.PVvoiture <= 0) {
			car.modifierVitesseMax(car.MaxSpeed/1.5f);
		} else if (this.PVvoiture < this.PVmax/5) {
			car.modifierVitesseMax(car.MaxSpeed/1.3f);
		} else if (this.PVvoiture < this.PVmax/2) {
			car.modifierVitesseMax(car.MaxSpeed/1.15f);
		}
	}

	// Modification du score
	public void changerScore(int points){
		this.Score += points;
		if (this.Score < 0){
			this.Score = 0;
		}
	}
}