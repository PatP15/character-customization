using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customizable : MonoBehaviour
{
    IDictionary<string, int> partIndex = new Dictionary<string, int>(){
        {"hat", 0 },
        {"clothes", 0},
        {"item", 0 },
        {"skin", 0 },
        {"hair", 0 },
        {"animation", 0}
    };

    IDictionary<string, GameObject[]> partList = new Dictionary<string, GameObject[]>();
    IDictionary<string, Color32[]> colorList = new Dictionary<string, Color32[]>();

    [SerializeField] private GameObject[] hatModels;
    [SerializeField] private GameObject[] clothesModels;
    [SerializeField] private GameObject[] itemModels;
    [SerializeField] private Color32[] skinColors;
    [SerializeField] private Color32[] hairColors;
    [SerializeField] private string[] animations;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hair;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private GameObject slider;

    [SerializeField] private Transform headAnchor;
    [SerializeField] private Transform handAnchor;
   
    private GameObject curHat;
    private GameObject curClothes;
    private GameObject curItem;

    private Animator anim;


    private void Start()
    {
        partList.Add("hat", hatModels);
        partList.Add("clothes", clothesModels);
        partList.Add("item", itemModels);
        colorList.Add("skin", skinColors);
        colorList.Add("hair", hairColors);
        anim = characterModel.GetComponent<Animator>();

    }


    public void IncreaseIndex(string key)
    {
        if (colorList.ContainsKey(key))
        {
            if (partIndex[key] < colorList[key].Length - 1)
                partIndex[key]++;
            else
                partIndex[key] = 0;
        }
        else if(partList.ContainsKey(key))
        {
            if (partIndex[key] < partList[key].Length - 1)
                partIndex[key]++;
            else
                partIndex[key] = 0;
        }
        else
        {
            if (partIndex[key] < animations.Length - 1)
                partIndex[key]++;
            else
                partIndex[key] = 0;
        }
        
        UpdateModel(key, partIndex[key]);
    }

    public void DecreaseIndex(string key)
    {
        if (colorList.ContainsKey(key))
        {
            if (partIndex[key] > 0)
                partIndex[key]--;
            else
                partIndex[key] = colorList[key].Length - 1;
            //Debug.Log(partIndex[key]);
        }
        else if (partList.ContainsKey(key))
        {
            if (partIndex[key] > 0)
                partIndex[key]--;
            else
                partIndex[key] = partList[key].Length - 1;
        }
        else
        {
            if (partIndex[key] > 0)
                partIndex[key]--;
            else
                partIndex[key] = animations.Length - 1;
        }
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
            case "hair":
                foreach(Transform child in hair.transform)
                {
                    child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color = colorList[key][partIndex[key]];
                }
                break;
            case "skin":
                foreach (Transform child in body.transform)
                {
                    child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color = colorList[key][partIndex[key]];
                }
                //Debug.Log("update skin");
                break;
            case "animation":
                //anim.Play(animations[partIndex[key]]);
                anim.Play(animations[partIndex[key]], -1, 0f);
                break;
        }
        
    }

    public void Rotate()
    {
        float sliderValue = slider.GetComponent<Slider>().value;
        characterModel.transform.rotation = Quaternion.Euler(0, sliderValue * 360, 0);
    }
}
