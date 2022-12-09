using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> avalibleSections = new List<GameObject>();

    // sections list
    public List<GameObject> sections = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        // call move sections every second
        InvokeRepeating("MoveSections", 1, 0.1f);
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

    // check if the first section is out of the screen and destroy it
    public void CheckSections()
    {
        if (sections[0].transform.position.x < ((sections[0].transform.localScale.x * -1)))
        {
            Destroy(sections[0]);
            sections.RemoveAt(0);
        }
    }

    //move sections to the left
    public void MoveSections()
    {
        //get all sections
        //move all sections to the left
        foreach (GameObject section in sections)
        {
            section.transform.position += new Vector3(-1, 0, 0);
        }
    }

}
