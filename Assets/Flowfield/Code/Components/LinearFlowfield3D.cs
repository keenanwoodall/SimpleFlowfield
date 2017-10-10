using UnityEngine;

public class LinearFlowfield3D : Flowfield3DBase
{
	public Vector3 Direction;

	private void Update ()
	{
		Data.SetAllDirections (Direction);
		Draw ();
	}
}