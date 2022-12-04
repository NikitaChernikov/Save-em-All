using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    [SerializeField] private Skin[] skins;

    private MeshFilter playerMesh;
    private MeshCollider playerCollider;
    private MeshRenderer playerRenderer;

    [SerializeField] private int meshNum = 0;

    private void Awake()
    {
        //playerCollider = GetComponent<MeshCollider>();
        playerMesh = GetComponent<MeshFilter>();
        playerRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //playerCollider.sharedMesh = skins[meshNum].mesh;
        playerMesh.mesh = skins[meshNum].mesh;
        playerRenderer.material = skins[meshNum].material;
    }
}


[System.Serializable]
public class Skin
{
    public Mesh mesh;
    public Material material;
}
