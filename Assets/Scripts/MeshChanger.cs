using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    [SerializeField] private Skin[] skins;

    private MeshFilter playerMesh;
    private MeshRenderer playerRenderer;

    //public static int meshNum = 0;

    private void Awake()
    {
        playerMesh = GetComponent<MeshFilter>();
        playerRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("ActiveSkin"))
        {
            ChangeSkin(PlayerPrefs.GetInt("ActiveSkin"));
        }
        else
        {
            ChangeSkin(0);
        }
    }

    public void ChangeSkin(int skinIndex)
    {
        playerMesh.mesh = skins[skinIndex].mesh;
        playerRenderer.material = skins[skinIndex].material;
    }
}
