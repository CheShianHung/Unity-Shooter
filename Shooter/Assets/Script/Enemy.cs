using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float blinkingTimer = 0.0f;
	public float blinkingDuration = 0.1f;
	public bool startBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
		if(startBlinking == true)
		{ 
			StartBlinking();
		}
    }

	private void StartBlinking()
	{
		blinkingTimer += Time.deltaTime;
		if(blinkingTimer >= blinkingDuration)
		{
			blinkingTimer = 0.0f;
			if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
			} else {
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
				startBlinking = false;
			}
		}
	}
}
