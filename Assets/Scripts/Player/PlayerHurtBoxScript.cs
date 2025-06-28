using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBoxScript : MonoBehaviour
{
	#region SINGLETON
	public static PlayerHurtBoxScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	private PlayerControlScript _pcs;
	
	private void Start()
	{
		_pcs = PlayerControlScript.me;
	}
	private void Update()
	{
		transform.position = _pcs.transform.position;
	}
	public void GetHit_byEnemy(int hitAmount)
	{
		if (!_pcs.invincible)
		{
			_pcs.hp -= hitAmount;
			_pcs.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			var ps = Instantiate(_pcs.PS_blood);
			ps.transform.position = transform.position;
			AbilityManagerScript.onPlayerHitByEnemy?.Invoke();
		}
	}
}
