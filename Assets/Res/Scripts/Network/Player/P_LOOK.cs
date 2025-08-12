using UnityEngine;
using UnityEngine.UI;

public class P_LOOK : MonoBehaviour
{
    private Transform playerTransform;
    private Transform cameraTransform;
    P_STATE p;

    [SerializeField]
    private float sensitivity = 10f; // Чувствительность мыши
    private float xRotation = 0f;           // Угол поворота по оси X
    private float yRotation = 0f;           // Угол поворота по оси Y
    private float currentXRotation = 0f;    // Текущий угол поворота по оси X
    private float currentYRotation = 0f;    // Текущий угол поворота по оси Y
    public float rotationSmoothTime = 0.02f;// Время сглаживания поворота
    private float velocityX = 0f;           // Скорость изменения угла по оси X
    private float velocityY = 0f;           // Скорость изменения угла по оси Y

    public Slider SensSlider;

    void Start()
    {
        SensSlider.value = sensitivity;
        p = GetComponent<P_STATE>();
        playerTransform = transform;
        cameraTransform = p.Camera.transform;
    }

    void Update()
    {
        RotateView();
    }

    void RotateView()
    {
        // Получаем ввод от мыши
        float mouseX = p.LookInput.x * Time.deltaTime * sensitivity;
        float mouseY = p.LookInput.y * Time.deltaTime * sensitivity;

        // Обновляем углы поворота
        xRotation += mouseY;
        yRotation += mouseX;

        // Ограничиваем угол наклона вверх/вниз
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref velocityX, rotationSmoothTime);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref velocityY, rotationSmoothTime);

        cameraTransform.localRotation = Quaternion.Euler(-currentXRotation, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
    public void SetSens()
    {
        sensitivity = SensSlider.value;
    }
}