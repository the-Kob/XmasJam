using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> avalibleSections = new List<GameObject>();

    // sections list
    public List<GameObject> sections = new List<GameObject>();
    // Start is called before the first frame update

    public List<GameObject> avalibleCollectibles = new List<GameObject>();
    void Start()
    {
        GenerateMap();
        // call move sections every second
    }

    // Update is called once per frame
    void Update()
    {
        // check if section is out of the screen and destroy it
        CheckSections();
        // if we only have three section left, generate new section
        if (sections.Count < 3)
        {
            GenerateSection();
        }

    }

    public void GenerateMap()
    {
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = Random.Range(0, avalibleSections.Count);
            GameObject newSection = Instantiate(avalibleSections[randomIndex], transform);
            //get lenght of section
            float sectionLength = newSection.transform.localScale.x;
            newSection.transform.position = new Vector3(sectionLength * i, 0, 0);
            sections.Add(newSection);

            // Get CollectiblesSpawnPoints chilren from section
            Transform[] collectiblesSpawnPoints = newSection.GetComponentsInChildren<Transform>();
            // loop through all children and randomly spawn collectibles with 50% chance
            foreach (Transform collectiblesSpawnPoint in collectiblesSpawnPoints)
            {
                int randomCollectibleIndex = Random.Range(0, avalibleCollectibles.Count);
                int randomChance = Random.Range(0, 10);
                if (randomChance == 0)
                {
                    GameObject newCollectible = Instantiate(avalibleCollectibles[randomCollectibleIndex], collectiblesSpawnPoint.position, Quaternion.identity);
                    newCollectible.transform.parent = collectiblesSpawnPoint;
                }
            }
        }
    }

    // generate one section
    public void GenerateSection()
    {
        int randomIndex = Random.Range(0, avalibleSections.Count);
        GameObject newSection = Instantiate(avalibleSections[randomIndex], transform);
        //get lenght of section
        float sectionLength = newSection.transform.localScale.x;
        // set position of new section next to the last section on the list
        newSection.transform.position = new Vector3(sections[sections.Count - 1].transform.position.x + sectionLength, 0, 0);
        sections.Add(newSection);
    }

    // check if the first section is out of camera view (the camera moves with the player) and destroy it
    public void CheckSections()
    {
        // get camera position
        float cameraPosition = Camera.main.transform.position.x;
        // get camera view size
        float cameraViewSize = Camera.main.orthographicSize * 2;
        // get section position
        float sectionPosition = sections[0].transform.position.x;
        // get section size
        float sectionSize = sections[0].transform.localScale.x;
        // if section is out of camera view, destroy it
        if (sectionPosition + sectionSize < cameraPosition - cameraViewSize)
        {
            Destroy(sections[0]);
            sections.RemoveAt(0);
        }
    }



}
