using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScanController : MonoBehaviour
{
    public RectTransform ScanCanvas;
    public Camera PlayerCamera;
    public GameObject ScanUI_Prefab;

    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.layer == 8)
    {
        iItem item = other.GetComponent<iItem>();
        if (item != null)
        {
            GameObject inst = Instantiate(ScanUI_Prefab, ScanCanvas);
            ScanElementUI scanElement = inst.GetComponent<ScanElementUI>();
            
            if (scanElement != null)
            {
                scanElement.main_cam = PlayerCamera;
                scanElement.root_rect = ScanCanvas;
                scanElement.target = other.transform;
                
                string itemName = item.GetItemName();
                int priceValue = item.GetPriceValue();
                
                scanElement.txt_descr.text = itemName;
                scanElement.txt_value.text = $"Стоимость: {priceValue}";
            }
        }
    }
}
}
