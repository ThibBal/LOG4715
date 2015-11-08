using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	
	public Text score;

	void Update ()
	{
		GameObject player = GameObject.Find("Joueur 1");
		EtatVoiture etatVoiture = player.GetComponent<EtatVoiture>();
		score.text = "Score : " + etatVoiture.Score.ToString ();				
	}
}