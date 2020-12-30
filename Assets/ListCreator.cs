using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCreator : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    public int numberOfItems = 0;

    public string[] itemNames = null;
    public string[] itemPrefs = null;
    public Sprite[] itemImages = null;

    // Start is called before the first frame update
    void Start()
    {
        content.sizeDelta = new Vector2(0, numberOfItems * 150);

        for(int i = 0; i < numberOfItems; i++)
        {
            float spawnY = i * 150;
            //newSpawn Position
            Vector3 pos = new Vector3(spawnPoint.position.x, -spawnY, spawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, spawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(spawnPoint, false);
            //get ItemDetails Component
            ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
            //set name
            itemDetails.text.text = itemNames[i];


            itemDetails.challengePref = itemPrefs[i];

            bool completedChallenge = PlayerPrefs.GetInt(itemDetails.challengePref) == 1;
            itemDetails.image.sprite = completedChallenge ? itemImages[1] : itemImages[0];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
