using UnityEngine;

[System.Serializable]
public class Skin
{
    [Header("Level Scene")]
    public Mesh mesh;
    public Material material;

    [Header("Menu Scene")]
    public int price;
    public string name;
}