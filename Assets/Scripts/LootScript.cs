using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }
    public List<DropCurrency> LootTable = new List<DropCurrency>();
    public int dropChance;
    int i, j;

    public void calculateLoot()
    {
        int cal_drop = Random.Range(0, 101);
        
        if (cal_drop > dropChance)
        {
            Debug.Log("No Loot for me");
            return;
        }
        if(cal_drop <= dropChance)
        {
            int itemWeight = 0;
            for(i = 0;i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].dropRarity;
            }
            Debug.Log("ItemWeight = " + itemWeight);

            int randomValue = Random.Range(0, itemWeight);
            for(j = 0; j<LootTable.Count; j++)
            {
                if(randomValue <= LootTable[j].dropRarity)
                {
                    Instantiate(LootTable[j].item, transform.position, Quaternion.identity);
                    return;
                }
                randomValue -= LootTable[j].dropRarity;
                Debug.Log("Random Value decreased" + randomValue);
            }
        }
    }
}
