using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarUserControlMP : MonoBehaviour
{
	private CarController car;  // the car controller we want to use
	private bool boost = false;
	public bool saut = false;

	[SerializeField]
	private string vertical = "Vertical";

	[SerializeField]
	private string horizontal = "Horizontal";

	private string roll = "Roll";
	
	void Awake ()
	{
		// get the car controller
		car = GetComponent<CarController>();
	}
	
	void Update() {
		if (!saut) {
			saut = Input.GetKey (KeyCode.Space);
		}
		boost = Input.GetKey(KeyCode.H);		
	}

	void FixedUpdate()
	{
		// pass the input to the car!
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis(horizontal);
		float v = CrossPlatformInput.GetAxis(vertical);
		float r = CrossPlatformInput.GetAxis(roll);

		car.GetComponent<EtatVoiture>().UtilisationBoost(boost);

		if(saut){
			car.Sauter();
			saut = false;
		}

		#else
		float h = Input.GetAxis(horizontal);
		float v = Input.GetAxis(vertical);
		#endif
		car.controleAérien (h, v, r);
		car.Move(h,v);

		//HashSet<string> voituresEnFrolageCurrentFrame = new HashSet<string>();
		


	}
}
