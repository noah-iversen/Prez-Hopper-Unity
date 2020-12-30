using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialText : MonoBehaviour
{
    private bool fadingIn = true; public GameObject tutorialParent;
    // Start is called before the first frame update
    void Start()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        InvokeRepeating("RepeatingFade", 0, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameSystem.Instance.gameScreen.SetActive(true);
            CancelInvoke("RepeatingFade");
            tutorialParent.SetActive(false);
        }
    }
    private void RepeatingFade()
    {
        StartCoroutine(FadeTo()); fadingIn = !fadingIn;
    }
    IEnumerator FadeTo()
    {
        float aValue = fadingIn ? 1.0f : 0.0f;
        Color color = GetComponent<SpriteRenderer>().color;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 4.0f)
        {
            color.a = Mathf.Lerp(color.a, aValue, t);
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForEndOfFrame();
        }
    }
}