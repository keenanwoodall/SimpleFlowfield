using UnityEngine;

public class Flowfield3DBase : MonoBehaviour
{
	public int XCount = 20;
	public int YCount = 20;
	public int ZCount = 20;
	public float Scale = 1f;

	public Flowfield3DData Data { get; protected set; }

	private void Awake ()
	{
		Data = new Flowfield3DData (XCount, YCount, ZCount);
		Data.Scale = Scale;
	}

	private void OnDrawGizmosSelected ()
	{
		Gizmos.DrawWireCube (new Vector3 (XCount, YCount, ZCount) * 0.5f * Scale, new Vector3 (XCount, YCount, ZCount) * Scale);
	}

	public void Draw ()
	{
		for (int x = 0; x < Data.Field.GetLength (0); x++)
		{
			for (int y = 0; y < Data.Field.GetLength (1); y++)
			{
				for (int z = 0; z < Data.Field.GetLength (2); z++)
				{
					var origin = Data.GetFieldIndexPosition (x, y, z);
					var direction = Data.Field[x, y, z];

					Debug.DrawRay (origin, direction * Data.Scale, new Color (direction.x, direction.y, direction.z));
				}
			}
		}
	}
}