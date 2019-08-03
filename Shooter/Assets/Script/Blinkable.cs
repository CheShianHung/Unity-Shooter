using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinkable : MonoBehaviour
{
	public float blinkingTimer = 0.0f;
	public float blinkingDuration = 0.1f;
	public bool startBlinking = false;
	public int blinkTime = 1;

	private int blinkCount = 0;

	public virtual void Update()
	{
		if (startBlinking == true)
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
				
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				blinkCount++;
				if (blinkCount >= blinkTime) {
					startBlinking = false;
					blinkCount = 0;
					blinkingTimer = 0.0f;
				}
			}
		}
	}
}
