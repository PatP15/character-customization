using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizable : MonoBehaviour
{
    IDictionary<string, int> partIndex = new Dictionary<string, int>(){
        {"hat", 0 },
        {"hair", 0 },
        {"armor", 0},
        {"weapon", 0},
        {"model", 0 },
        {"animation", 0 },

    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DecreaseIndex(string key)
    {
        Debug.Log(partIndex[key]);
    }
    public void IncreaseIndex(string key)
    {

    }
}
