using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketItem : MonoBehaviour
{
    public int itemId;
    public int wearId;
    public int price;
    public Button buyButton, equipButton, unequipButton;

    public Text priceText;

    public GameObject itemPrefab;

    public bool HasItem()
    {
        bool hasItem=PlayerPrefs.GetInt("item" + itemId.ToString()) != 0;
        return hasItem;
    }


    public bool isEquipped()
    {
        bool equippedItem = PlayerPrefs.GetInt("item" + itemId.ToString()) != 2;
        return equippedItem;
    }

    public void InitializeItem()
    {
        priceText.text = price.ToString();
        if (HasItem())
        { 

            buyButton.gameObject.SetActive(false);
            if (isEquipped())
            {
                EquipItem();
            }
            else
            {
                equipButton.gameObject.SetActive(true);
            }
        }
        else
        {
            buyButton.gameObject.SetActive(true);

        }

    }

    public void BuyItem()
    {
        if (!HasItem())
        {
            int money = PlayerPrefs.GetInt("money");
            if (money >= price)
            {
                karakterkontrol.Current.itemaudioSource.PlayOneShot(karakterkontrol.Current.buyAudioClip, 01f);
                LevelController.Current.GiveMoneyToPlayer(-price);
                PlayerPrefs.SetInt("item" + itemId.ToString(), 1);
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
            }

                

            }
        }
    


    public void EquipItem()
    {
        UnequipItem();
        MarketController.Current.equippedItems[wearId] = Instantiate(itemPrefab, karakterkontrol.Current.wearSpots[wearId].transform).GetComponent<Item>();
        MarketController.Current.equippedItems[wearId].itemId = itemId;
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(true);
        PlayerPrefs.SetInt("item" + itemId.ToString(), 2);
        




    }

    public void UnequipItem()
    {
        Item equippedItem = MarketController.Current.equippedItems[wearId];
        if (equippedItem != null)
        {
            MarketItem marketItem = MarketController.Current.items[equippedItem.itemId];
            PlayerPrefs.SetInt("item" + marketItem.itemId, 1);
            marketItem.equipButton.gameObject.SetActive(true);
            marketItem.unequipButton.gameObject.SetActive(false);
            Destroy(equippedItem.gameObject);
            
        }


    }

    public void EquipItemButton()
    {
        karakterkontrol.Current.itemaudioSource.PlayOneShot(karakterkontrol.Current.equipItemAudioClip, 0.1f);
        EquipItem();
    }

    public void UnequipItemButton()
    {
        karakterkontrol.Current.itemaudioSource.PlayOneShot(karakterkontrol.Current.unequipItemAudioClip, 0.1f);
        UnequipItem();
    }
}
