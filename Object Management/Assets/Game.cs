﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : PersistableObject {

	private const int saveVersion = 1; 

	public ShapeFactory shapeFactory; 

	public PersistentStorage storage; 

	public KeyCode createKey = KeyCode.C; 
	public KeyCode newGameKey = KeyCode.N; 
	public KeyCode saveKey = KeyCode.S; 
	public KeyCode loadKey = KeyCode.L; 

	private List<Shape> shapes; 

	void Awake() {
		shapes = new List<Shape>(); 
	}
	
	void Update() {
		if (Input.GetKeyDown(createKey)) {
			CreateObject(); 
		}
		else if (Input.GetKeyDown(newGameKey)) { 
			BeginNewGame(); 
		}
		else if (Input.GetKeyDown(saveKey)) {
			storage.Save(this); 
		}
		else if (Input.GetKeyDown(loadKey)) {
			BeginNewGame();
			storage.Load(this); 
		}
	}

	void CreateObject() {
		Shape instance = shapeFactory.GetRandom(); 
		Transform t = instance.transform; 
		t.localPosition = Random.insideUnitSphere * 5f; 
		t.localRotation = Random.rotation; 
		t.localScale = Vector3.one * Random.Range(0.1f, 1f); 
		shapes.Add(instance); 
	}

	void BeginNewGame() {
		for (int i = 0; i < shapes.Count; i++) {
			Destroy(shapes[i].gameObject); 
		}
		shapes.Clear(); 
	}

	public override void Save(GameDataWriter writer) {
		writer.Write(-saveVersion); 
		writer.Write(shapes.Count); 
		for (int i = 0; i < shapes.Count; i++) {
			writer.Write(shapes[i].ShapeId); 
			writer.Write(shapes[i].MaterialId); 
			shapes[i].Save(writer); 
		}
	}

	public override void Load(GameDataReader reader) {
		int version = -reader.ReadInt(); 
		if (version > saveVersion) {
			Debug.LogError("Unsupported future save version " + version); 
			return; 
		}
		int count = version <= 0 ? -version : reader.ReadInt(); 
		for (int i = 0; i < count; i++) {
			int shapeId = version > 0 ? reader.ReadInt() : 0; 
			int materialId = version > 0 ? reader.ReadInt() : 0; 
			Shape instance = shapeFactory.Get(shapeId, materialId); 
			instance.Load(reader); 
			shapes.Add(instance); 
		}
	}
}
