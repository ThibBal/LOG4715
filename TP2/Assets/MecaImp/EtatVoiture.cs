using UnityEngine;
using System.Collections;

public class EtatVoiture :	MonoBehaviour {

	public CarController car ;
	private float boost = 50;
	public int PVmax = 50;
	public int PVvoiture = 50;

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
			boost -= 1f;
			car.Boost = true;
		} else {
			boost += 0.1f;
			car.Boost = false;
		}
	}

	public void changerPV(int PV){
		if (this.PVvoiture + PV < this.PVmax) {
			this.PVvoiture += PV;
		} else {
			this.PVvoiture = this.PVmax;
		}

		if (this.PVvoiture <= 0) {
			car.modifierVitesseMax(car.MaxSpeed/10);
		} else if (this.PVvoiture < this.PVmax/5) {
			car.modifierVitesseMax(car.MaxSpeed/4);
		} else if (this.PVvoiture < this.PVmax/2) {
			car.modifierVitesseMax(car.MaxSpeed/2);
		}
	}


}