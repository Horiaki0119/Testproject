using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMove : MonoBehaviour {

    [SerializeField]
    private GameObject SlashTrail;

    bool create = false;

    List<GameObject> slashImage = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector3 screenPosition;
            if (!create)
            {
                SlashTrail.GetComponent<TrailRenderer>().Clear();
                screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
                SlashTrail.transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
                //複製
                GameObject sl = Instantiate(SlashTrail);

                slashImage.Add(sl);

                create = true;
            }
            //SlashTrail.SetActive(true);
            screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
            slashImage[slashImage.Count-1].transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
        }
        else
        {
            for(int i = 0; i < slashImage.Count; i++)
            {
                slashImage[i].GetComponent<TrailRenderer>().Clear();
                slashImage[i].SetActive(false);
                //Destroy(slashImage[i]);
            }
            create = false;
            //SlashTrail.GetComponent<TrailRenderer>().Clear();
            //StartCoroutine(ResetTrail(SlashTrail.GetComponent<TrailRenderer>()));
            //SlashTrail.SetActive(false);
        }
    }

    /// <summary>
    /// Coroutine to reset a trail renderer trail
    /// </summary>
    /// <param name="trail"></param>
    /// <returns></returns>
    static IEnumerator ResetTrail(TrailRenderer trail)
    {
        var trailTime = trail.time;
        trail.time = 0;
        yield return 0;
        trail.time = trailTime;
    }
}
