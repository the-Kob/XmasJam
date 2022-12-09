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

    // chance to spawn collectibles in section serialized in inspector
    [Range(0, 100)]
    public int chanceToSpawnCollectible = 50;
    void Start()
    {
        // generate first three sections
        GenerateSection(3);
    }

    // Update is called once per frame
    void Update()
    {
        // check if section is out of the screen and destroy it
        CheckSections();
        // if we only have three section left, generate new section
        if (sections.Count < 3)
        {
            GenerateSection(1);
        }

    }

    // generate one section
    public void GenerateSection(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            // get random section from avalibleSections list
            int randomIndex = Random.Range(0, avalibleSections.Count);
            // instantiate section
            GameObject newSection = Instantiate(avalibleSections[randomIndex], transform);
            //get lenght of section
            float sectionLength = newSection.transform.localScale.x;
            // set position of new section next to the last section on the list, if there is no sections on the list, set position to 0
            if (sections.Count > 0)
                newSection.transform.position = new Vector3(sections[sections.Count - 1].transform.position.x + sectionLength, 0, 0);
            else
                newSection.transform.position = new Vector3(0, 0, 0);
            // Get children of new section with tag "SpawnPoint"
            var collectiblesSpawnPoints = GetComponentsInChildren(newSection.transform, "SpawnPoint");
            // loop through all children and randomly spawn collectibles with chance = chanceToSpawnCollectible
            foreach (Transform collectiblesSpawnPoint in collectiblesSpawnPoints)
            {
                int randomCollectibleIndex = Random.Range(0, avalibleCollectibles.Count);
                int randomChance = Random.Range(0, 100);
                if (randomChance < chanceToSpawnCollectible)
                {
                    GameObject newCollectible = Instantiate(avalibleCollectibles[randomCollectibleIndex], collectiblesSpawnPoint.position, Quaternion.identity);
                    newCollectible.transform.parent = collectiblesSpawnPoint;
                }
            }
            // add new section to sections list
            sections.Add(newSection);
        }

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

    // return all children with tag
    public Transform[] GetComponentsInChildren(Transform parent, string tag)
    {
        List<Transform> childrenWithTag = new List<Transform>();
        foreach (Transform child in parent)
        {
            if (child.tag == tag)
            {
                childrenWithTag.Add(child);
            }
        }
        return childrenWithTag.ToArray();
    }



}
