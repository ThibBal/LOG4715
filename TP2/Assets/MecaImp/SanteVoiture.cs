using UnityEngine;
using UnityEngine.UI;

public class SanteVoiture : MonoBehaviour {
	
	public Text sante;

	//Affichage de la santé du joueur
	void Update ()
	{
		GameObject player = GameObject.Find("Joueur 1");
		EtatVoiture etatVoiture = player.GetComponent<EtatVoiture>();
		sante.text = "PV : " + etatVoiture.PVvoiture.ToString () + "/" + etatVoiture.PVmax.ToString ();				
	}
}