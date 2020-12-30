using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FaceSpawner : MonoBehaviour
{
    public int index;
    public GameObject parent;
    public GameObject[] faces;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Score-" + index) > 0)
        {
            int faceIndex = PlayerPrefs.GetInt("Score-" + index + "-Face");
            GameObject newOb = Instantiate(faces[faceIndex], transform.position, Quaternion.identity);
            newOb.transform.SetParent(parent.transform, false);
            newOb.transform.position = transform.position;
        }
    }    // Update is called once per frame
    void Update()
    {

    }
}