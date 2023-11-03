using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public GameObject panelGameOver;
    public GameObject panelTimeCountDown;
    public GameObject panelPause;
    public GameObject panelWinner;
    public GameObject bubbleChat;
    public List<Image> imageItems;
    public List<GameObject> gameObjectItems;
    // Start is called before the first frame update
    void Awake()
    {
        mergeListItem();
    }
    void Start()
    {
        isShowGameOver(false);
        isShowPause(false);
        bubbleChat.SetActive(false);
    }
    //khoi tao Item va Listitem
    public class Item
    {
        public int idItem { get; set; }
        public Image imgItem { get; set; }
    }
    List<Item> listItem = new List<Item>();

    //cho imageItem va Id vao listItem
    void mergeListItem()
    {
        for(int i = 0; i < gameObjectItems.Count; i++)
        {
            listItem.Add(new Item() { 
                idItem=gameObjectItems[i].gameObject.GetInstanceID(),
                imgItem = imageItems[i]
            });
        }
    }
    public List<Item> getListIten()
    {
        return listItem;
    }
    public void isShowGameOver(bool state)
    {
        panelGameOver.SetActive(state);
    }
    public void isShowTimeCountDown(bool state)
    {
        panelTimeCountDown.SetActive(state);
    }
    public void isShowPause(bool state)
    {
        panelPause.SetActive(state);
    }
    public void isShowWinner(bool state)
    {
        panelWinner.SetActive(state);
    }
    //Ham de active ImageItem tren Canvas
    public void itemActive(int idItem)
    {
        foreach (Item item in listItem)
        {
            if(idItem == item.idItem)
            {
                item.imgItem.color = new Color(item.imgItem.color.r, item.imgItem.color.g, item.imgItem.color.b, 1f);
            }
        } 
    }

    public void FadeOutBubbleChat()
    {
        StartCoroutine(FadeOutBubbleChatCR());
    }
    IEnumerator FadeOutBubbleChatCR()
    {
        bubbleChat.SetActive(true);
        yield return new WaitForSeconds(3);
        bubbleChat.SetActive(false);
    }
}
