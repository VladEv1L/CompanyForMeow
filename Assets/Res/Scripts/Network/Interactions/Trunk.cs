using UnityEngine;

public class Trunk : MonoBehaviour, iShortInteractable
{
    public P_INVENTORY inventory;

    public void Interact()
    {
        inventory.TakeToTrunk();
        CoreController.instance.UpdateCollectedTMp();

    }


}
