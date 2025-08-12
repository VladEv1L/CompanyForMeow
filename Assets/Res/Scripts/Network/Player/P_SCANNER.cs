using UnityEngine;

public class P_SCANNER : MonoBehaviour
{
    public Transform scanModel;
    public bool isScanning;
    PlayerAudioController pac;
    public AudioClip SonarSound;
    public Animator ScannerAniamtor;
    void Start()
    {
        pac = GetComponent<PlayerAudioController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Scaneer test");
            if (isScanning)
            {
                return;
            }
            ScannerAniamtor.Play("Scanning");
            StartScan();
        }
        if (isScanning)
        {
            
            scanModel.transform.localScale = Vector3.Lerp(scanModel.transform.localScale, new Vector3(3000, 3000, 3000), Time.deltaTime * 0.7f);
            if (scanModel.transform.localScale.x > 2900)
            {
                isScanning = false;
                scanModel.transform.localScale = new Vector3(0, 0, 0);
            }
        }

    }
    public void StartScan()
    {
        pac.PlayOne(SonarSound);
        isScanning = true;
    }
}
