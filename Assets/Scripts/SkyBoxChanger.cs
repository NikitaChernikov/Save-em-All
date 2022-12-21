using UnityEditor;
using UnityEngine;

public class SkyBoxChanger : MonoBehaviour
{
    [SerializeField] private Material[] skyBoxes;

    private void Start()
    {
        Material currentMaterial = skyBoxes[Random.Range(0, skyBoxes.Length)];
        RenderSettings.skybox = currentMaterial;
        
    }
}
