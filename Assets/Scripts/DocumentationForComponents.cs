using UnityEngine;
/// <summary>
///  A class used for maintaining per-object documentation
/// </summary>
public class DocumentationForComponents : MonoBehaviour
{
    [TextArea(3, 8)]
    [SerializeField] string Notes;
}
