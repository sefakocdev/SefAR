using UnityEngine;

public class RainbowLight : MonoBehaviour
{
    public Light directionalLight; // Sahnedeki directional light
    public float colorChangeSpeed = 1f; // Renk değiştirme hızı

    private float hueValue = 0f;

    void Start()
    {
        if (directionalLight == null)
        {
            // Eğer atama yapılmadıysa, otomatik olarak sahnedeki ışığı bulur
            directionalLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        // Hue değerini zamanla arttır
        hueValue += Time.deltaTime * colorChangeSpeed;
        if (hueValue > 1f)
        {
            hueValue = 0f;
        }

        // HSV değerlerini RGB'ye çevir ve directional light'ın rengini ayarla
        Color newColor = Color.HSVToRGB(hueValue, 1f, 1f);
        directionalLight.color = newColor;
    }
}
