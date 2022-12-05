using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Text[] priceTexts;
    [SerializeField] public Skin[] skins;
    [SerializeField] private Text moneyText;

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
    }

    public void BuySkin(int skinIndex)
    {
        moneyText.text = "MONEY: " + PlayerPrefs.GetInt("Money");
        priceTexts[skinIndex].text = "";
    }
}
