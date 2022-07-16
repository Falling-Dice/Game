
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSide
{
	public Vector3 Rotation { get; private set; }
	public int Range { get; private set; }
	public float Size { get; private set; }
	public CharacterSide(Vector3 rotation, int range, float size)
	{
		Rotation = rotation;
		Range = range;
		Size = size;
	}


	public static CharacterSide One = new(new Vector3(0, 0, -90), 1, 1f);
	public static CharacterSide Two = new(new Vector3(0, 0, -180), 2, 1.2f);
	public static CharacterSide Three = new(new Vector3(0, 0, -270), 3, 1.4f);
	public static CharacterSide Four = new(new Vector3(0, 0, -360), 4, 1.6f);
	public static CharacterSide Five = new(new Vector3(-90, 0, 0), 5, 1.8f);
	public static CharacterSide Six = new(new Vector3(90, 0, 0), 6, 2);

	public static List<CharacterSide> All = new List<CharacterSide>(){
		One,
		Two,
		Three,
		Four,
		Five,
		Six
	};
}