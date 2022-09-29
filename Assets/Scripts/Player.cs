using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveSpeed = 1;
    Vector3 targetPos = new Vector3();

    void Start()
    {

    }

    void Update()
    {
        if (this.targetPos != this.transform.position)
            this._moveTo();
    }

    public void moveToDirect(Vector3 pos)
    {
        targetPos = pos;
        this.transform.position = pos;
    }

    public void moveTo(Vector3 pos)
    {
        targetPos = pos;
    }

    void _moveTo()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, this.moveSpeed * Time.deltaTime);
    }
}
