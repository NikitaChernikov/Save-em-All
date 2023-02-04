using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Text[] priceTexts;
    [SerializeField] public Skin[] skins;
    [SerializeField] private Button[] skinImage;
    [SerializeField] private Text moneyText;
    [SerializeField] private Vector3 scaledImage = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private Vector3 normalImage = new Vector3(1f, 1f, 1f);

    private void Start()
    {
        for (int i = 0; i < priceTexts.Length; i++)
        {
            if (PlayerPrefs.HasKey(skins[i].name) || skins[i].price == 0)
            {
                priceTexts[i].text = "";
                continue;
            }
            //if (skins[i].price == 0)
            //{
            //    priceTexts[i].text = "";
            //    continue;
            //}
            priceTexts[i].text = skins[i].price.ToString();
        }
        if (PlayerPrefs.HasKey("ActiveSkin"))
        {
            skinImage[PlayerPrefs.GetInt("ActiveSkin")].transform.localScale = scaledImage;
        }
        else
        {
            skinImage[0].transform.localScale = scaledImage;
        }
    }

    public void ChangeSkin(int currentSkin, int newSkin)
    {
        skinImage[currentSkin].transform.localScale = normalImage;
        skinImage[newSkin].transform.localScale = scaledImage;
    }

    public void ChangeMoneyText()
    {
        moneyText.text = "GOLD: " + PlayerPrefs.GetInt("Money");
    }

    public void BuySkin(int skinIndex)
    {
        moneyText.text = "GOLD: " + PlayerPrefs.GetInt("Money");
        priceTexts[skinIndex].text = "";
    }
}
