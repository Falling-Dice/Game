
using System.Collections.Generic;
using UnityEngine;

public class CharacterSide
{
	public Vector3 Rotation { get; private set; }
	public int Range { get; private set; }
	public float Size { get; private set; }
	public float RotationSpeed { get; private set; }
	public CharacterSide(Vector3 rotation, int range, float size, float rotationSpeed)
	{
		Rotation = rotation;
		Range = range;
		Size = size;
		RotationSpeed = rotationSpeed;
	}


	public static CharacterSide One = new(new Vector3(0, 0, -90), 2, 1f, 5f);
	public static CharacterSide Two = new(new Vector3(0, 0, -180), 3, 1.2f, 3f);
	public static CharacterSide Three = new(new Vector3(0, 0, -270), 4, 1.4f, 2f);
	public static CharacterSide Four = new(new Vector3(0, 0, -360), 5, 1.6f, 1.5f);
	public static CharacterSide Five = new(new Vector3(-90, 0, 0), 6, 1.8f, 1f);
	public static CharacterSide Six = new(new Vector3(90, 0, 0), 7, 2, .8f);

	public static List<CharacterSide> All = new List<CharacterSide>(){
		One,
		Two,
		Three,
		Four,
		Five,
		Six
	};
}