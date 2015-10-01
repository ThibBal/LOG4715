# Platformer2D
Labs from LOG4715 module at EPM

Lab 1 :
1. Déplacement aérien 
2. Modulation de la hauteur du saut
  IEnumerator JumpRoutine()
	{
		float timer = 0;
		
		while(CrossPlatformInput.GetButton("Jump") && timer < timeJump)
		{
			float proportionCompleted = timer / timeJump;
			Vector2 thisFrameJumpVector = Vector2.Lerp(new Vector2(0f, currentJumpForce), Vector2.zero, proportionCompleted);
			rigidbody2D.AddForce(thisFrameJumpVector);
			timer += Time.deltaTime;
			yield return null;
		}
	}

3. Saut multiple
  if (jump) {
			if (currentJump +1 < maxJump) {
				jumpable = true;
				//currentJumpForce = jumpAbsorber*currentJumpForce;
			} else if (currentJump + 1 == maxJump) {
				jumpable = false;
				jetpack = true;
			} else {
				jumpable = false;
			}
		}

4. Saut mural
  
5. Jetpack
  
6. Indicateur de la hauteur maximale du saut simple
  void Awake()
	{
  // Ajouter
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		if (showMaxHeight) {
			Debug.DrawLine (new Vector3(player.position.x, player.position.y, 0), new Vector3 (player.position.x, player.position.y+currentJumpForce/20, 0), Color.blue);
		}
	}
  
7. Caméra

8. Ajustements
