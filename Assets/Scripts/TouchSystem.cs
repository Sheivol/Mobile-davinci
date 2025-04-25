using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> _spherePrefabs = new List<GameObject>();
    [SerializeField] LayerMask _floorMask;
    void Update()
    {
        if (Input.touchCount == 0) return;
        
        Touch firstTouch = Input.touches[0];

        if (firstTouch.phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(firstTouch.position);
            RaycastHit hit;

            if(Physics.Raycast(touchRay, out hit, 100000, _floorMask, QueryTriggerInteraction.Ignore))
            {
                int random = Random.Range(0, _spherePrefabs.Count);

                GameObject spawedObject = Instantiate(_spherePrefabs[random].gameObject);
                spawedObject.transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z);
            }
        }
    }
}
