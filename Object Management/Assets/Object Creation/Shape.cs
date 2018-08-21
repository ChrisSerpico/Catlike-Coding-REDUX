using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : PersistableObject {

    MeshRenderer meshRenderer; 

    static MaterialPropertyBlock sharedPropertyBlock; 
    static int colorPropertyId = Shader.PropertyToID("_Color"); 

    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>(); 
    }

    private int shapeId = int.MinValue; 
   
    public int ShapeId {
        get {
            return shapeId; 
        }
        set {
            if (shapeId == int.MinValue && value != int.MinValue) {
                shapeId = value; 
            }
            else {
                Debug.LogError("Not allowed to change shapeId."); 
            }
        }
    }

    public int MaterialId { get; private set; } 

    public void SetMaterial(Material material, int materialId) {
        meshRenderer.material = material; 
        MaterialId = materialId; 
    }

    private Color color; 

    public void SetColor(Color color) {
        this.color = color; 
        if (sharedPropertyBlock == null) {
            sharedPropertyBlock = new MaterialPropertyBlock(); 
        }
        sharedPropertyBlock.SetColor(colorPropertyId, color); 
        meshRenderer.SetPropertyBlock(sharedPropertyBlock); 
    }

    public override void Save(GameDataWriter writer) {
        base.Save(writer);
        writer.Write(color); 
    }

    public override void Load(GameDataReader reader) {
        base.Load(reader); 
        SetColor(reader.version > 1 ? reader.ReadColor() : Color.white); 
    }
}
