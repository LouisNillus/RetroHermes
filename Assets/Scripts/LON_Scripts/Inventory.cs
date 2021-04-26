using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public GameObject slotTemplate;
    [Range(0,200)]
    public int yOffset; //In pixels
    [Range(0,200)]
    public int xOffset; //In pixels
    [Range(-1000,1000)]
    public int yMargin; //In pixels
    [Range(-1000,1000)]
    public int xMargin; //In pixels

    public int rows;
    public int columns;


    // Start is called before the first frame update
    void Start()
    {
        InitSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSlots()
    {
        Vector3[] vecs = new Vector3[rows * columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                vecs[i * j].y = i * -(slotTemplate.GetComponent<RectTransform>().rect.height + yOffset) + yMargin;
                vecs[i * j].x = j * (slotTemplate.GetComponent<RectTransform>().rect.width + xOffset) + xMargin;
                GameObject go = Instantiate(slotTemplate, vecs[i*j], Quaternion.identity);
                slots.Add(go.GetComponent<Slot>());
                go.transform.SetParent(this.transform, false);
            }
        }
    }
}
