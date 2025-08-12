
using UnityEngine;
using System.Collections;

public class Grab_1 : MonoBehaviour, iItem
{
    public int _id;
    public int _averagePriceValue;
    public string _name;
    public Sprite _icon;
    public Vector3 _handLocalPosition;
    public Quaternion _handLocalRotation;



    public float itemVerticalOffset;
    public Quaternion _GroundDropRotation;

    private Vector3 targetFloorPosition;
    private Vector3 startDropPosition;
    float DropTime;
    bool isDropping;
    AudioSource _audioSource;
    public AudioClip _dropSound;


    public void UseItem()
    {

    }

    public void UnUseItem()
    {


    }


    public int GetItemID()
    {
        return _id;
    }
    public string GetItemName()
    {
        return _name;
    }
    public Vector3 GetStartLocalPosition()
    {
        return _handLocalPosition;
    }
    public GameObject GetObject()
    {
        return this.gameObject;
    }
    public Sprite GetIcon()
    {
        return _icon;
    }
    public Quaternion GetStartLocalRotation()
    {
        return _handLocalRotation;
    }
    public int GetPriceValue()
    {

        return _averagePriceValue;
    }

    public void DropToGround()
    {

        if (Physics.Raycast(base.transform.position, Vector3.down, out var hitInfo, 80f))
        {
            targetFloorPosition = hitInfo.point + itemVerticalOffset * Vector3.up;
            /* startDropPosition = base.transform.parent.InverseTransformPoint(startDropPosition);
             if (base.transform.parent != null)
             {
                 targetFloorPosition = base.transform.parent.InverseTransformPoint(targetFloorPosition);
             }*/
            startDropPosition = base.transform.position;
            Debug.Log($"Target Floor Position: {targetFloorPosition}");
            isDropping = true;

        }
    }
    public void StartDropToGround()
    {

        if (Physics.Raycast(base.transform.position, Vector3.down, out var hitInfo, 80f))
        {
            targetFloorPosition = hitInfo.point + itemVerticalOffset * Vector3.up;
            base.transform.position = targetFloorPosition;
            base.transform.eulerAngles = new Vector3(0,Random.Range(0,359),0);
        }
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _averagePriceValue = SetAverangeValue();
        CoreController.instance.AddPriceCount(_averagePriceValue);

        // Добавить правильный дроп спавн
        StartDropToGround();

    }
    public int SetAverangeValue()
    {
        float x = Random.Range(_averagePriceValue * 0.7f, _averagePriceValue * 1.3f);
        _averagePriceValue = Mathf.RoundToInt(x);
        return _averagePriceValue;
    }
    void Update()
    {
        if (isDropping)
        {
            base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, targetFloorPosition, Time.deltaTime * 15);
            base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, _GroundDropRotation, Time.deltaTime * 25);
            if (Vector3.Distance(base.transform.localPosition, targetFloorPosition) < 0.1f)
            {
                PlayDropSound();
                isDropping = false;
            }
        }


    }
    public void PlayDropSound()
    {
        _audioSource.PlayOneShot(_dropSound);
    }
}

