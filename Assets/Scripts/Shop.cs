using UnityEngine;

public class Shop : MonoBehaviour
{
    private ShopUI shopUI;

    private int currentSkin;

    private void Awake()
    {
        shopUI = GetComponent<ShopUI>();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("ActiveSkin"))
        {
            currentSkin = PlayerPrefs.GetInt("ActiveSkin");
        }
    }

    public void BuySkin(int skinIndex)
    {
        if (PlayerPrefs.HasKey(shopUI.skins[skinIndex].name))
        {
            SetActiveSkin(skinIndex);
            return;
        }
        if (PlayerPrefs.HasKey("Money"))
        {
            if (PlayerPrefs.GetInt("Money") >= shopUI.skins[skinIndex].price)
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - shopUI.skins[skinIndex].price);
                shopUI.BuySkin(skinIndex);
                PlayerPrefs.SetInt(shopUI.skins[skinIndex].name, 1);
                SetActiveSkin(skinIndex);
            }
        }
    }

    public void SetActiveSkin(int skinIndex)
    {
        shopUI.ChangeSkin(currentSkin, skinIndex);
        currentSkin = skinIndex;
        PlayerPrefs.SetInt("ActiveSkin", skinIndex);
    }
}
