using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
	public float radius = 1f;
	public float lifetime = 3f;
	public int perFrame = 3;
	public Boid boid;
	public Flowfield3DBase target;

	private IEnumerator Start ()
	{
		while (true)
		{
			for (int i = 0; i < perFrame; i++)
			{
				var newBoid = Instantiate (boid, transform.position + Random.insideUnitSphere * radius, Quaternion.identity);
				newBoid.Target = target;
			}
			yield return null;
		}
	}

	private void OnDrawGizmosSelected ()
	{
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}