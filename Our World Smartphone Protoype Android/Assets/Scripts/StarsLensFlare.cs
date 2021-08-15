using UnityEngine;
using UnityEngine.UI;

public class StarsLensFlare : MonoBehaviour
{
    private Image image;
    private float x;
    private float y;
    private float a;
    private float b;
    private float t;
    private float ra;
    private float ri;
    private float s;
    private float r;
    private bool swaped;

    private void Start()
    {
        image = GetComponent<Image>();
        GetRandom();
    }

    private void GetRandom()
    {
        swaped = false;
        do
        {
            x = Random.Range(-950f, 950f);
            y = Random.Range(-500f, 500f);
        } while (x > 0 && y < 240);
        s = Random.Range(0.07f, 0.1f);
        a = 0.1f;
        b = 1;
        ra = Random.Range(10, 200);
        ri = 0;
        t = 0;
        r = Random.Range(0.2f, 2);
        gameObject.transform.localPosition = new Vector3(x, y, 0);
    }

    private void Update()
    {
        image.color = new Color (1, 1, 1, Mathf.Lerp(a, b, t));
        gameObject.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(ri, ra, t));
        t += s * Time.deltaTime;

        if (image.color.a == b)
            if (!swaped)
            {
                float c = a;
                a = b;
                b = c;
                t = 0;
                ri = ra;
                ra = 0;
                swaped = true;
            }
            else
                GetRandom();

    }
}
