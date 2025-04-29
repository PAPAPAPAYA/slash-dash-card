using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedIndicatorScript : MonoBehaviour
{
      public float showTime;
      public float showTimeLeft;
      private bool showed;
      private void Update()
      {
	    if (PlayerControlScript.me.chargeCompleted &&
		  !showed)
	    {
		  showTimeLeft = showTime;
	    }
	    else if (InputManagerScript.me.mouseDragDuration <= 0)
	    {
		  showed = false;
	    }
	    if (showTimeLeft > 0)
	    {
		  // show
		  GetComponent<SpriteRenderer>().enabled = true;
		  showed = true;
		  showTimeLeft -= Time.deltaTime;
	    }
	    else
	    {
		  showTimeLeft = 0;
		  // hide
		  GetComponent<SpriteRenderer>().enabled = false;
	    }
      }
}
