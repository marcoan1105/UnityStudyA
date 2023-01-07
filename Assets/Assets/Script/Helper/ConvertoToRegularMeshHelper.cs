using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertoToRegularMeshHelper : MonoBehaviour {
    [ContextMenu("Convert to regular mesh")]
    void Convert()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
        meshRenderer.sharedMaterial = skinnedMeshRenderer.sharedMaterial;

        DestroyImmediate(skinnedMeshRenderer);
        DestroyImmediate(this);
    }
}
