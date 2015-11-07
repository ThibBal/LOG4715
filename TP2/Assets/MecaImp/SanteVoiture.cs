using UnityEngine;
using UnityEngine.UI;

public class SanteVoiture : MonoBehaviour {
	
	public Text text;
	
	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.Find("Joueur 1");
		if (player!=null)
		{
			EtatVoiture etatVoiture = player.GetComponent<EtatVoiture>();
			if (etatVoiture!=null)
			{
				text.text = "PV : " + etatVoiture.PVvoiture.ToString () + "/" + etatVoiture.PVmax.ToString ();				
			}
		}
	}
}