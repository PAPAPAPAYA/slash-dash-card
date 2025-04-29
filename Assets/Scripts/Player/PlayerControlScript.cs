using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
	#region SINGLETON
	public static PlayerControlScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	public float chargeMax;
	public float chargeCurrent;
	#region REFS
	private InputManagerScript inputMan;
	private Rigidbody2D rb;
	private StateController sc;
	#endregion
	public bool chargeCompleted = false;
	public GameObject actionPrefab;
	private ActionPrefabScript aps; //action prefab script
	public List<GameObject> actionColliders;
	public float movingVelocityShreshold;
	public bool moving;
	private float ogDrag;
	public float brakeSpeed;
	public int hp;
	public int ammo; // when have ammo, dash=slash; when don't dashing into enemies deal damage to player and knockback
	public int ammo_max;
	public bool reloaded; // only when ammo reaches ammo_max, then it's reloaded
	public float reloadDuration;
	public float reloadTimer;
	public GameObject PS_blood;
	public bool invincible = false;
	public float loseInvincibilitySpd;
	public GameObject knockBackArea;
	public float speedLimit;
	private Vector2 currentPos;
	
	private void Start()
	{
		inputMan = InputManagerScript.me; // get input manager
		rb = GetComponent<Rigidbody2D>(); // get rigidbody
		sc = StateController.me; // get state controller
		
		aps = actionPrefab.GetComponent<ActionPrefabScript>(); // get action prefab script
		ogDrag = rb.drag; // record original drag
		reloadTimer = reloadDuration; // initialize reload timer
		reloaded = true;
		ammo_max = CardManager.me.hand.Count;
		ammo = ammo_max;
	}
	private void Update()
	{
		// track charge progress
		chargeCurrent = Mathf.Clamp(inputMan.mouseDragDuration, 0, chargeMax);
		// check if charge complete
		chargeCompleted = chargeCurrent >= chargeMax;
		if (moving)
		{
			if (Vector2.Distance(currentPos, transform.position) >= AbilityManagerScript.me.spike_dist)
			{
				currentPos = transform.position;
				AbilityManagerScript.whenSlashing?.Invoke(transform.position);
			}
		}
		//Reload_old();
		// check if moving
		moving = rb.velocity.magnitude > movingVelocityShreshold;
	}
	private void FixedUpdate()
	{
		// swap physics material (should be called when upgrade is applied)
		GetComponent<Rigidbody2D>().sharedMaterial = AbilityManagerScript.me.wallBounce ? AbilityManagerScript.me.phyMat_Bounce : AbilityManagerScript.me.phyMat_noBounce;
		// face moving direction
		if (moving)
		{
			Vector2 movingDir = GetComponent<Rigidbody2D>().velocity;
			transform.up = (Vector3)movingDir;
		}
		// brake
		if (moving && inputMan.mouseDown)
		{
			rb.drag += brakeSpeed * Time.deltaTime;
			// destroy action collider
			foreach (GameObject collider in actionColliders)
			{
				Destroy(collider);
			}
			actionColliders.Clear();
			invincible = false;
		}
		else
		{
			rb.drag = ogDrag;
		}
		// set velocity to zero if not enough speed
		if (rb.velocity.magnitude < movingVelocityShreshold)
		{
			rb.velocity = Vector3.zero;
		}
		// destroy action collider when no speed
		if (rb.velocity.magnitude < loseInvincibilitySpd)
		{
			foreach (GameObject collider in actionColliders)
			{
				Destroy(collider);
			}
			actionColliders.Clear();
			invincible = false;
		}
		// drift
		//rb.velocity = Vector2.Lerp(rb.velocity, transform.up*rb.velocity.magnitude, 2f * Time.deltaTime);
		//Debug.DrawRay(transform.position, transform.up * rb.velocity.magnitude, Color.magenta);
		//Debug.DrawRay(transform.position, rb.velocity, Color.green);

		// rotate
		if (inputMan.mouseDown)
		{
			transform.rotation = Quaternion.Euler(0, 0, -UtilityFuncManagerScript.me.ConvertV2ToAngle(inputMan.mouseDragDir));
		}

		rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedLimit);
	}
	public void Slash(Vector2 dir)
	{
		// get dmg from card
		
		// get force amount from action prefab
		float force = aps.slashForce;
		// rotate
		transform.rotation = Quaternion.Euler(0, 0, -UtilityFuncManagerScript.me.ConvertV2ToAngle(dir));
		// move
		rb.AddForce(dir * force, ForceMode2D.Impulse);
		// spawn action collider
		 StartCoroutine(SpawnActionCollider(aps.precastDuration));
		// change state
		sc.ChangeState(sc.slashingState);
		// update moving asap
		moving = rb.velocity.magnitude > movingVelocityShreshold;
	}
	public void Dash(Vector2 dir)
	{
		float force = aps.dashForce;
		// rotate
		transform.rotation = Quaternion.Euler(0, 0, -UtilityFuncManagerScript.me.ConvertV2ToAngle(dir));
		// move
		rb.AddForce(dir * force, ForceMode2D.Impulse);
		// change state
		sc.ChangeState(sc.dashingState);
		// update moving asap
		moving = rb.velocity.magnitude > movingVelocityShreshold;
	}
	IEnumerator SpawnActionCollider(float precastDur)
	{
		yield return new WaitForSeconds(precastDur);
		// instantiate collider to attack
		//actionCollider= Instantiate(aps.colliderPrefab, transform.position, transform.rotation, transform);
		
		invincible = true;
	}
	IEnumerator DestroyActionCollider(float actionDur, GameObject collider)
	{
		yield return new WaitForSeconds(actionDur);
		Destroy(collider);
		invincible = false;
	}
	public void GetHit(int hitAmount)
	{
		if (!invincible)
		{
			hp -= hitAmount;
			GameObject ps = Instantiate(PS_blood);
			ps.transform.position = transform.position;
			AbilityManagerScript.onPlayerHit?.Invoke();
		}
	}
}