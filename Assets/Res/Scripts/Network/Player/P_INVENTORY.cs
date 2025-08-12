using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Unity.VisualScripting;
using UnityEngine;

public class P_INVENTORY : MonoBehaviour
{
    //public List<iItem> currentInventory;
    public GameObject[] currentInventoryItem;
    public List<UI_item> UI_Items;
    public int maxItems = 3;
    public bool InventoryFull;
    public int currentInventoryItemIndex; //0-1-2
    public RectTransform Frame;
    public Transform PlayerInventory;
    public Transform DropItemPosition;
    public Sprite EmptyIcon;
    public AudioSource AS;
    public AudioClip TakeItemSound;
        public AudioClip _collectedSound;
    iItem currentItem;
    public GameObject FullInventory;
    void Start()
    {

        //currentInventory = new List<iItem>(maxItems);
        currentInventoryItem = new GameObject[3];
        SelectInventoryItem(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentInventoryItem[currentInventoryItemIndex] != null)
            {
                DropItem(currentInventoryItemIndex);
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards 
        {
            currentInventoryItemIndex++;

            if (currentInventoryItemIndex == maxItems) currentInventoryItemIndex = 0;
            SelectInventoryItem(currentInventoryItemIndex);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentInventoryItemIndex--;
            if (currentInventoryItemIndex == -1) currentInventoryItemIndex = maxItems - 1;
            SelectInventoryItem(currentInventoryItemIndex);
        }
        if (Input.GetMouseButton(0))
        {
            if (currentInventoryItem[currentInventoryItemIndex] != null)
            {
                if (currentItem == null)
                {
                    currentItem = currentInventoryItem[currentInventoryItemIndex].GetComponent<iItem>();
                }
                else
                {
                    currentItem.UseItem();
                }



            }

        }
        else
        {
            if (currentItem != null)
            {
                currentItem.UnUseItem();
                currentItem = null;
            }
        }
    }
    public void SelectInventoryItem(int index)
    {
        for (int i = 0; i < currentInventoryItem.Count(); i++)
        {
            if (currentInventoryItem[i] != null)
                currentInventoryItem[i].SetActive(false);
        }

        //currentInventoryItem[index].SetActive(true);

        Frame.parent = UI_Items[index].transform;
        Frame.anchoredPosition = new Vector3(0, 0, 0);

        if (currentInventoryItem[index] != null)
        {

            currentInventoryItem[index].SetActive(true);
        }

    }
    public void TakeItem(iItem item)
    {
        if (InventoryFull)
        {

            Debug.Log("Инвентарь полный");

            return;
        }
        else
        {

            if (currentInventoryItem[currentInventoryItemIndex] != null)
            {
                Debug.Log("Ячейка занята");
                return;
            }
            currentInventoryItem[currentInventoryItemIndex] = item.GetObject();
            item.GetObject().transform.parent = PlayerInventory;
            item.GetObject().SetActive(false);
            //item.GetObject().GetComponent<Rigidbody>().isKinematic = true;
            item.GetObject().GetComponent<Collider>().enabled = false;
            item.GetObject().transform.localPosition = item.GetStartLocalPosition();
            item.GetObject().transform.localRotation = item.GetStartLocalRotation();
            item.GetObject().layer = 0;
            item.GetObject().SetActive(true);
            UI_Items[currentInventoryItemIndex].ItemIcon.sprite = item.GetIcon();
            //AS.PlayOneShot(TakeItemSound);
            //SelectInventoryItem(currentInventoryItemIndex);




        }


    }
    public void DropItem(int index)
    {

        currentInventoryItem[index].gameObject.SetActive(true);
        //currentInventoryItem[index].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        currentInventoryItem[index].gameObject.GetComponent<Collider>().enabled = true;
        currentInventoryItem[index].layer = 8;
        currentInventoryItem[index].transform.parent = null;
        //currentInventoryItem[index].transform.position = DropItemPosition.position;

        UI_Items[index].ItemIcon.sprite = EmptyIcon;
        currentInventoryItem[index].gameObject.GetComponent<iItem>().DropToGround();

        currentInventoryItem[index] = null;
    }
    public void TakeToTrunk()
    {

        if (currentInventoryItem[currentInventoryItemIndex] != null)
        {
            UI_Items[currentInventoryItemIndex].ItemIcon.sprite = EmptyIcon;
            CoreController.instance.AddCollectedCount(currentInventoryItem[currentInventoryItemIndex].gameObject.GetComponent<iItem>().GetPriceValue());
            Destroy(currentInventoryItem[currentInventoryItemIndex].gameObject);

            currentInventoryItem[currentInventoryItemIndex] = null;
            AS.PlayOneShot(_collectedSound);
        }


    }

    public int GetCurrentInvenoryItemID()
    {
        if (currentInventoryItem[currentInventoryItemIndex] != null)
        {
            Debug.Log("GET Component");
            return currentInventoryItem[currentInventoryItemIndex].GetComponent<iItem>().GetItemID();

        }
        else
        {
            return 0;
        }

    }
    public iItem GetCurrentInventoryItem()
    {
        if (currentInventoryItem[currentInventoryItemIndex] != null)
        {
            return currentInventoryItem[currentInventoryItemIndex].GetComponent<iItem>();
        }
        else
        {
            return null;
        }

    }


}
