using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> avalibleSections = new List<GameObject>();

    // shop section
    public GameObject shopSection;

    // sections list
    public List<GameObject> sections = new List<GameObject>();

    // old sections list
    public List<GameObject> oldSections = new List<GameObject>();

    public List<GameObject> avalibleCollectibles = new List<GameObject>();

    public bool reseting = false;

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
        if (sections.Count > 2) CheckSections();
        // if we only have three section left, generate new section
        if (sections.Count < 3 && !reseting)
        {
            GenerateSection(1);
        }

    }

    // generate one section
    public void GenerateSection(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject newSection;
            // set position of new section next to the last section on the list, if there is no sections on the list, set position to 0
            if (sections.Count > 0)
            {
                // get random section from avalibleSections list
                int randomIndex = Random.Range(0, avalibleSections.Count);
                // instantiate section
                newSection = Instantiate(avalibleSections[randomIndex], transform);
                // find child object of sections[sections.Count - 1] with name "Ground"
                var ground = sections[sections.Count - 1].transform.Find("Ground");
                // get sprite renderer of ground
                var groundSpriteRenderer = ground.GetComponent<SpriteRenderer>();
                // get size of ground sprite
                var groundSize = groundSpriteRenderer.sprite.bounds.size;
                // set position of new section
                newSection.transform.position = new Vector3(sections[sections.Count - 1].transform.position.x + groundSize.x, 0, 0);
                // Get children of new section with tag "SpawnPoint"
                var collectiblesSpawnPoint = GetComponentsInChildren(newSection.transform, "SpawnPoint");
                var collectiblesSpawnPoints = GetComponentsInChildren(collectiblesSpawnPoint[1].transform, "SpawnPoint");
                // loop through all children and randomly spawn collectibles with chance = chanceToSpawnCollectible
                foreach (var spawnPoint in collectiblesSpawnPoints)
                {
                    if (Random.Range(0, 100) < chanceToSpawnCollectible)
                    {
                        // get random collectible from avalibleCollectibles list
                        int randomCollectibleIndex = Random.Range(0, avalibleCollectibles.Count);
                        // instantiate collectible
                        GameObject newCollectible = Instantiate(avalibleCollectibles[randomCollectibleIndex], spawnPoint.position, Quaternion.identity, spawnPoint);
                    }
                }
            }
            else
            {
                newSection = Instantiate(shopSection, transform);
                newSection.transform.position = new Vector3(0, 0, 0);

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
        // if section is out of camera view, move it to old sections list
        if (sectionPosition + sectionSize + 10 < cameraPosition - cameraViewSize)
        {
            oldSections.Add(sections[0]);
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

    // Generate section at 0,0,0
    public void GenerateSectionAtZero()
    {
        GameObject newSection = Instantiate(shopSection, transform);
        //get lenght of section
        float sectionLength = newSection.transform.localScale.x;
        // set position of new section next to the last section on the list, if there is no sections on the list, set position to 0
        newSection.transform.position = new Vector3(0, 0, 0);
        // Get children of new section with tag "SpawnPoint"
        var collectiblesSpawnPoints = GetComponentsInChildren(newSection.transform, "SpawnPoint");
        // move all sections to the old sections list and clear sections list
        foreach (GameObject section in sections)
        {
            oldSections.Add(section);
        }
        sections.Clear();
        // add new section to sections list
        sections.Add(newSection);
        // create 2 new sections
        /*GenerateSection(2);
        // delete the first 2 sectiosn of old list
        Destroy(oldSections[0]);
        Destroy(oldSections[1]);
        oldSections.RemoveAt(0);
        oldSections.RemoveAt(0);*/
    }

    // destroy all sections
    public void DestroyAllSections()
    {
        foreach (GameObject section in oldSections)
        {
            Destroy(section);
        }
        oldSections.Clear();
    }



}
