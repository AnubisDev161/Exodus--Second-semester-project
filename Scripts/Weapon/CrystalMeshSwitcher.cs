using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
public class CrystalMeshSwitcher : MonoBehaviour
{
    [SerializeField] private Mesh crackedMesh;
    private MeshFilter meshFilter;
    private Mesh defaultMesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        defaultMesh = meshFilter.mesh;
    }

    public void SwitchMesh()
    {
        if (meshFilter.mesh == defaultMesh)
        {
            meshFilter.mesh = crackedMesh;
        }
        else
        {
            meshFilter.mesh = defaultMesh;
        }
    }
}
