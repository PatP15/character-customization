using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizable : MonoBehaviour
{
    IDictionary<string, int> partIndex = new Dictionary<string, int>(){
        {"hat", 0 },
        {"clothes", 0},
        {"item", 0 },
    };

    IDictionary<string, GameObject[]> partList = new Dictionary<string, GameObject[]>();

    [SerializeField] private GameObject[] hatModels;
    [SerializeField] private GameObject[] clothesModels;
    [SerializeField] private GameObject[] itemModels;


    [SerializeField] private SkinnedMeshRenderer skinRenderer;
    [SerializeField] private Transform headAnchor;
    [SerializeField] private Transform handAnchor;
   
    private GameObject curHat;
    private GameObject curClothes;
    private GameObject curItem;

    private void Start()
    {
        partList.Add("hat", hatModels);
        partList.Add("clothes", clothesModels);
        partList.Add("item", itemModels);
    }


    public void IncreaseIndex(string key)
    {
        if(partIndex[key] < partList[key].Length - 1)
            partIndex[key]++;
        else
            partIndex[key] = 0;
        UpdateModel(key, partIndex[key]);
    }

    public void DecreaseIndex(string key)
    {
        if (partIndex[key] > 0)
            partIndex[key]--;
        else
            partIndex[key] = 0;
        //Debug.Log(partIndex[key]);
        UpdateModel(key, partIndex[key]);

    }
    
    public void UpdateModel(string key, int index)
    {
        switch (key) {
            case "hat":
                if(curHat != null)
                {
                    curHat.SetActive(false);
                }
                curHat = partList[key][partIndex[key]];
                curHat.SetActive(true);
                if(curHat.GetComponent<SkinnedMeshRenderer>() == null)
                    curHat.transform.SetParent(headAnchor);
                break;
            case "clothes":
                if (curClothes != null)
                {
                    curClothes.SetActive(false);
                }
                curClothes = partList[key][partIndex[key]];
                curClothes.SetActive(true);
                
                break;
            case "item":
                if (curItem != null)
                {
                    curItem.SetActive(false);
                }
                curItem = partList[key][partIndex[key]];
                curItem.SetActive(true);
                curItem.transform.SetParent(handAnchor);

                break;
        }
        
    }
}
