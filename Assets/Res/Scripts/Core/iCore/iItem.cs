using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public interface iItem
{
    
    void UseItem();
    void UnUseItem();
    int GetItemID();
    string GetItemName();
    Vector3 GetStartLocalPosition();
    Quaternion GetStartLocalRotation();
    GameObject GetObject();
    Sprite GetIcon();
    void DropToGround();
    int GetPriceValue();
    

}
