
using UnityEngine;

public class CharacterSide
{
	public Vector3 Rotation { get; private set; }
	public int Range { get; private set; }
	public CharacterSide(Vector3 rotation, int range)
	{
		Rotation = rotation;
		Range = range;
	}


	public static CharacterSide One = new(new Vector3(0, 0, -90), 1);
	public static CharacterSide Two = new(new Vector3(0, 0, -180), 2);
	public static CharacterSide Three = new(new Vector3(0, 0, -270), 3);
	public static CharacterSide Four = new(new Vector3(0, 0, -360), 4);
	public static CharacterSide Five = new(new Vector3(-90, 0, 0), 5);
	public static CharacterSide Six = new(new Vector3(90, 0, 0), 6);
}