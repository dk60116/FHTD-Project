using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{
    public Transform IconPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 1; i <= 245; i++)
        {
            var go = Instantiate(IconPrefab, transform, false);
            go.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Equip_" + i.ToString().PadLeft(3, '0'));
        }
    }
}
