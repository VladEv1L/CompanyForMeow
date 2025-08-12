using TMPro;
using UnityEngine;

public class ScanElementUI : MonoBehaviour
{
    public Transform target;
    public RectTransform root_rect;
    public Camera main_cam;
    public TMP_Text txt_descr;
    public TMP_Text txt_value;
    public RectTransform BackGroundImages;
    void Start()
    {
        Destroy(this.gameObject, 3);
    }


    void Update()
    {
        Vector3 worldPosition = target.transform.position;
        Vector3 viewportPosition = main_cam.WorldToViewportPoint(worldPosition);
        viewportPosition.y += 0.05f;
        if (Vector3.Dot(worldPosition, viewportPosition) < 0)
        {
            //BackGroundImages.gameObject.transform.gameObject.SetActive(false);

        }
        Vector3 screenPosition = new Vector3(
            ((viewportPosition.x * root_rect.sizeDelta.x) - (root_rect.sizeDelta.x * 0.5f)),
            ((viewportPosition.y * root_rect.sizeDelta.y) - (root_rect.sizeDelta.y * 0.5f)),
            0
        );
        BackGroundImages.anchoredPosition = screenPosition;


    }
}
