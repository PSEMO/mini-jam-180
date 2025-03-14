using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Transform PlayerTransform;

    [SerializeField] float MinPosLimitX = -2;
    [SerializeField] float MaxPosLimitX = 2;
    //for example make it 0.5 for cam to be cam.pos.x = 1 when player.pos.x = 2
    [SerializeField] float XPosFollowerMultiplier = 0.5f;

    private void Awake()
    {
        PlayerTransform = Player.transform;
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(PlayerTransform.position.x * XPosFollowerMultiplier, MinPosLimitX, MaxPosLimitX), PlayerTransform.position.y, transform.position.z);
    }
}
