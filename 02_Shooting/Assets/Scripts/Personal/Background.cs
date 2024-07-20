using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Background : MonoBehaviour
{
    float elapsedTime = 0.0f;

    SpriteRenderer sr;

    Color orgColor;

    Color newColor;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        orgColor = sr.material.color;

        StartCoroutine(FlashBackground());
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        transform.position = new Vector3(Mathf.Sin(elapsedTime) / 10.0f, Mathf.Cos(elapsedTime) / 10.0f, 0);

        
    }

    IEnumerator FlashBackground()
    {
        while (true)
        {
            float _coef = 0.05f;

            yield return new WaitForSeconds(5.0f);

            for (int i = 0; i < 40; i++)
            {
                newColor = orgColor;
                newColor.r = orgColor.r + i * _coef;
                newColor.g = orgColor.g + i * _coef;
                newColor.b = orgColor.b + i * _coef;
                sr.material.color = newColor;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(1.0f);

            for (int i = 40; i > 0; i--)
            {
                newColor.r = orgColor.r + i * _coef;
                newColor.g = orgColor.g + i * _coef;
                newColor.b = orgColor.b + i * _coef;
                sr.material.color = newColor;
                yield return new WaitForSeconds(0.03f);
            }

            sr.material.color = orgColor;
        }
    }
}
