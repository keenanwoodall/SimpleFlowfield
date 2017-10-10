using UnityEngine;

public class Flowfield3DData
{
	public float Scale = 0.1f;
	public Vector3[,,] Field { get; private set; }

	public Flowfield3DData (Vector3[,,] field)
	{
		Field = field;
	}
	public Flowfield3DData (int xCount, int yCount, int zCount)
	{
		Field = new Vector3[xCount, yCount, zCount];
	}

	public Vector3 GetFieldIndexPosition (int x, int y, int z)
	{
		return new Vector3 (x, y, z) * Scale;
	}

	public Vector3 GetNearestFieldIndex (Vector3 position)
	{
		var xIndex = (int)(Mathf.Clamp (position.x / Scale, 0, Field.GetLength (0) - 1));
		var yIndex = (int)(Mathf.Clamp (position.y / Scale, 0, Field.GetLength (1) - 1));
		var zIndex = (int)(Mathf.Clamp (position.z / Scale, 0, Field.GetLength (2) - 1));
		return Field[xIndex, yIndex, zIndex];
	}

	public bool IsPointOutsideBounds (Vector3 point)
	{
		if (point.x > Field.GetLength (0) * Scale || point.x < 0f)
			return true;
		if (point.y > Field.GetLength (1) * Scale || point.y < 0f)
			return true;
		if (point.z > Field.GetLength (2) * Scale || point.z < 0f)
			return true;
		return false;
	}

	public Vector3 LoopPoint (Vector3 point)
	{
		if (point.x > Field.GetLength (0) * Scale)
			point.x = 0f;
		else if (point.x < 0f)
			point.x = Field.GetLength (0) * Scale;

		if (point.y > Field.GetLength (1) * Scale)
			point.y = 0f;
		else if (point.y < 0f)
			point.y = Field.GetLength (1) * Scale;

		if (point.z > Field.GetLength (2) * Scale)
			point.z = 0f;
		else if (point.z < 0f)
			point.z = Field.GetLength (1) * Scale;

		return point;
	}

	public void SetAll (Vector3[,,] field)
	{
		Field = field;
	}

	public void SetAllDirections (Vector3 direction)
	{
		for (int x = 0; x < Field.GetLength (0); x++)
		{
			for (int y = 0; y < Field.GetLength (1); y++)
			{
				for (int z = 0; z < Field.GetLength (2); z++)
				{
					Field[x, y, z] = direction;
				}
			}
		}
	}
}