using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class ConvertToRegularMesh : MonoBehaviour {


    [ContextMenu("Конвертирай SkinnedMeshRenderer-a в regular mesh")]
    //Чрез този ред добавям метод Convert в dropdown-а, който се появява при натискане на бутона Options (зъбното колело вдясно на всеки компонент)
    void Convert()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
        // mesh filter-a взема mesh-a от skinnedMeshRenderer (който е направен в Blender).

        meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;
        // mesh renderer-a взема материалите от skinnedMeshRenderer (цветове, текстури и т.н.)

        DestroyImmediate(skinnedMeshRenderer);
        DestroyImmediate(this);
        // Накрая SkinnedMeshRenderer-a и самият script се премахват от gameobject-a.
    }
}
