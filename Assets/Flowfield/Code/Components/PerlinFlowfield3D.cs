using UnityEngine;

public class PerlinFlowfield3D : Flowfield3DBase
{
	public bool DrawDebug = true;
	public float Frequency = 1f;
	public Vector3 Movement = Vector3.forward;
	public Vector3 Offset = Vector3.forward;
	private Vector3 movementOffset;
	private Vector3 finalOffset;

	private void Update ()
	{
		movementOffset += Movement * Time.deltaTime;
		finalOffset = movementOffset + Offset;

		var field = Data.Field;

		var xCount = Data.Field.GetLength (0);
		var yCount = Data.Field.GetLength (1);
		var zCount = Data.Field.GetLength (2);

		var scaledXFrequency = Frequency / xCount;
		var scaledYFrequency = Frequency / yCount;
		var scaledZFrequency = Frequency / zCount;

		for (int x = 0; x < xCount; x++)
		{
			for (int y = 0; y < yCount; y++)
			{
				for (int z = 0; z < zCount; z++)
				{
					var scaledX = finalOffset.x + x * scaledXFrequency;
					var scaledY = finalOffset.y + y * scaledYFrequency;
					var scaledZ = finalOffset.z + z * scaledZFrequency;

					var xAmount = Mathf.Rad2Deg * (Mathf.PerlinNoise (scaledX, scaledY) * (Mathf.PI * 2f));
					var yAmount = Mathf.Rad2Deg * (Mathf.PerlinNoise (scaledY, scaledZ) * (Mathf.PI * 2f));
					var zAmount = Mathf.Rad2Deg * (Mathf.PerlinNoise (scaledZ, scaledX) * (Mathf.PI * 2f));

					var direction = Quaternion.Euler (xAmount, yAmount, zAmount) * Vector3.forward;

					field[x, y, z] = direction;
				}
			}
		}

		Data.SetAll (field);

		if (DrawDebug)
			Draw ();
	}
}