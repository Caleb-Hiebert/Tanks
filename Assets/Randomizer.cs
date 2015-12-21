using UnityEngine;
using System.Collections;

public class Randomizer : MonoBehaviour {

    public float zMin, zMax;

    void Start()
    {
        Arrange();
    }

    void OnDrawGizmosSelected()
    {
        Arrange();
    }

	void Arrange () {
        foreach (var item in transform.GetComponentsInChildren<Transform>()) {
            item.position = item.position.Z(0);

            if(item != transform)
            {
                item.Translate(0, 0, Random.Range(zMin, zMax));
            }
        }
	}
}
