using System.IO; 
using UnityEngine;

public class GameDataReader {

	// Changed from a property to avoid having to use version 6+ of c# 
	public readonly int version; 
	
	private BinaryReader reader;

	public GameDataReader(BinaryReader reader, int version) {
		this.reader = reader; 
		this.version = version; 
	}

	public float ReadFloat() {
		return reader.ReadSingle(); 
	}

	public int ReadInt() {
		return reader.ReadInt32(); 
	}

	public Quaternion ReadQuaternion() {
		Quaternion value; 
		value.x = reader.ReadSingle();
		value.y = reader.ReadSingle();
		value.z = reader.ReadSingle();
		value.w = reader.ReadSingle(); 
		return value; 
	}

	public Vector3 ReadVector3() {
		Vector3 value; 
		value.x = reader.ReadSingle();
		value.y = reader.ReadSingle();
		value.z = reader.ReadSingle();
		return value; 
	}

	public Color ReadColor() {
		Color value; 
		value.r = reader.ReadSingle(); 
		value.g = reader.ReadSingle(); 
		value.b = reader.ReadSingle(); 
		value.a = reader.ReadSingle(); 
		return value; 
	}
}
