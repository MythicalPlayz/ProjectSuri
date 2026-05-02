using UnityEngine;

public class FrozenFood : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum FrozenType // Changed from private to public to fix CS0052
    {
        Fries,
        Chicken,
    }

    public FrozenType type;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
