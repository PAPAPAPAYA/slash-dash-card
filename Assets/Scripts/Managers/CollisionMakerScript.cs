using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

public class CollisionMakerScript : MonoBehaviour
{
	#region SINGLETON
	public static CollisionMakerScript me;
	void Awake()
	{
		me = this;
	}
	#endregion
	public GameObject explosionCollider_prefab;
	public ObjectPool<GameObject> explosionCollider_pool;
	
	void Start()
	{
		explosionCollider_pool = new ObjectPool<GameObject>(
		InstantiateExplosionCollider,
		explosionCollider => explosionCollider.SetActive(true),
		explosionCollider => explosionCollider.SetActive(false),
		explosionCollider => Destroy(explosionCollider),
		true,
		20,
		100
		);
	}
	
	private GameObject InstantiateExplosionCollider()
	{
		GameObject colliderToMake = Instantiate(explosionCollider_prefab);
		colliderToMake.transform.SetParent(gameObject.transform);
		return colliderToMake;
	}
}
