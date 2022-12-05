using UnityEngine;

public class Shop : MonoBehaviour
{
    private ShopUI shopUI;

    //public static int activeSkin;

    private void Awake()
    {
        shopUI = GetComponent<ShopUI>();
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
            if (PlayerPrefs.GetInt("Money") > shopUI.skins[skinIndex].price)
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
        //activeSkin = skinIndex;
        PlayerPrefs.SetInt("ActiveSkin", skinIndex);
    }
}
