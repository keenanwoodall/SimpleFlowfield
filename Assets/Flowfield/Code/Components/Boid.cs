using UnityEngine;

public class Boid : MonoBehaviour
{
	public float MaxSpeed = 1f;
	public float MaxForce = 4f;
	public Flowfield3DBase Target;

	public Vector3 velocity { get; private set; }
	public Vector3 acceleration { get; private set; }

	private void Update ()
	{
		Follow (Target);

		velocity += acceleration;
		velocity = Vector3.ClampMagnitude (velocity, MaxSpeed);
		transform.position += velocity * Time.deltaTime;
		acceleration = Vector3.zero;

		if (Target.Data.IsPointOutsideBounds (transform.position))
			Destroy (gameObject);
	}

	private void AddForce (Vector3 force)
	{
		acceleration += force;
	}

	public void Seek (Vector3 target)
	{
		var goal = (target - transform.position).normalized * MaxSpeed;
		var steer = goal - velocity;
	}

	public void Follow (Flowfield3DBase target)
	{
		var goal = target.Data.GetNearestFieldIndex (transform.position);
		goal *= MaxSpeed;

		var steer = goal - velocity;
		steer = Vector3.ClampMagnitude (steer, MaxForce);

		AddForce (steer);
	}
}