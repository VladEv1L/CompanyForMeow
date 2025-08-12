using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class P_INTERACTION : MonoBehaviour
{
    [Header("Interaction settings")]
    public float maxDistance = 2;
    public LayerMask longIntLayers;
    public LayerMask shortIntLayers;
    public LayerMask itemLayers;

    public GameObject knobLong;
    public GameObject knobShort;
    public GameObject knobItem;

    public Image fillInteratable;
    public P_INVENTORY inventory;

    private iLongInteractable[] currentLongInts;
    private iShortInteractable[] currentShortInts;
    private iItem currentItem;

    void Update()
    {
        CheckLongInteraction();
        CheckShortInteraction();
        CheckItemInteraction();
    }

    private void CheckLongInteraction()
    {
        if (TryGetLongInteraction(out var hit))
        {
            if (!knobLong.activeSelf) knobLong.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                fillInteratable.fillAmount += 0.01f;
                if (fillInteratable.fillAmount >= 1)
                {
                    currentLongInts = hit.collider.GetComponents<iLongInteractable>();
                    foreach (var interactable in currentLongInts)
                    {
                        interactable.Interact();
                    }
                    fillInteratable.fillAmount = 0;
                }
            }
            else
            {
                fillInteratable.fillAmount = 0;
            }
        }
        else
        {
            currentLongInts = null;
            if (knobLong.activeSelf) knobLong.SetActive(false);
            fillInteratable.fillAmount = 0;
        }
    }

    private bool TryGetLongInteraction(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, longIntLayers);
    }

    private void CheckShortInteraction()
    {
        if (TryGetShortInteraction(out var hit))
        {
            if (!knobShort.activeSelf) knobShort.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentShortInts = hit.collider.GetComponents<iShortInteractable>();
                foreach (var interactable in currentShortInts)
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            currentShortInts = null;
            if (knobShort.activeSelf) knobShort.SetActive(false);
        }
    }

    private bool TryGetShortInteraction(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, shortIntLayers);
    }

    private void CheckItemInteraction()
    {
        if (TryGetItemInteraction(out var hit))
        {
            if (!knobItem.activeSelf) knobItem.SetActive(true);

            if (currentItem == null)
            {
                currentItem = hit.collider.GetComponent<iItem>();
                Debug.Log("item show");
                //itemNameTMP.text = $"[{currentItem.GetItemName()}]";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.TakeItem(currentItem);
                currentItem = null;
                Debug.Log("Take Test");
            }
        }
        else
        {
            if (knobItem.activeSelf) knobItem.SetActive(false);
            if (currentItem != null) currentItem = null;
            //itemNameTMP.text = "";
        }
    }

    private bool TryGetItemInteraction(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, itemLayers);
    }
}