using UnityEngine;
using System.Collections;

public class IndicationVirage : MonoBehaviour 
{
	[SerializeField]
	private GUIText fleche;

	void Update () {
		fleche.text = virageSuivant();
	}
	
	public string virageSuivant() {
		var x = transform.position.x;
		var z = transform.position.z;

		// Chaque paramètre x et z passé dans la condition délimite une zone du terrain dans laquelle le texte indiquant le prochain virage apparaitra.
		//1er virage
		if (x > -20 && x < 40 && z > 120 && z < 180) {
			return "<--";
		}
		//2ème virage
		else if(x > -230 && x < -140 && z > 30 && z < 100){
			return "-->";
		}
		//3ème virage
		else if(x > -330 && x < -280 && z > 50 && z < 125){
			return "<--";
		}
		//4ème  virage
		else if(x > -600 && x < -350 && z > -30 && z < 50){
			return "<--";
		}
		//5ème  virage
		else if(x > -390 && x < -280 && z > -100 && z < 0){
			return "-->";
		}
		//6ème virage
		else if(x > -260 && x < -210 && z > -130 && z < -50){
			return "<--";
		}
		//7ème  virage
		else if(x > -200 && x < -100 && z > -180 && z < -80){
			return "-->";
		}
		//8ème virage
		else if(x > -85 && x < -30 && z > -110 && z < -40){
			return "<--";
		}
		return "";
	}
}