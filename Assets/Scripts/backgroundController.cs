using UnityEngine;

public class backgroundController : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] float backgroundHeight = 10;
    static int backgroundCount = 3;
    //add 3 as child objects
    Transform[] backgrounds = new Transform[backgroundCount];

    void Start()
    {
        for (int i = 0; i < backgroundCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
            backgrounds[i].transform.position = new Vector3 (0, -1 * i * backgroundHeight, 0);
        }
    }

    void Update()
    {
        // Check if the player has moved past the lowest background
        if (Player.position.y < backgrounds[backgroundCount - 1].position.y)
        {
            transform.position += new Vector3(0, -1 * (backgroundCount - 1) * backgroundHeight, 0);
        }
        else if (Player.position.y > backgrounds[0].position.y)
        {
            transform.position += new Vector3(0, +1 * (backgroundCount - 1) * backgroundHeight, 0);
        }
    }
}
