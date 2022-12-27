using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public enum AimTarget
    {
        apple,
        shopKeep
    }

    [SerializeField] private AimTarget aimTarget;

    private string aimTag;

    private float rotationSpeed = 60f;
    private float arrowTime = 10f;

    [SerializeField] private int maxUses = 5;
    [SerializeField] private int used = 0;

    private Transform target;

    delegate bool Targeting(Transform transform, GameObject oltarget, GameObject newtarget);
    Targeting shouldTarget;

    private void Start()
    {
        shouldTarget = TargetClosest;
        switch (aimTarget)
        {
            case AimTarget.apple:
                aimTag = Tags.OBJNAME_APPLE;
                break;
            case AimTarget.shopKeep:
                aimTag = Tags.OBJNAME_SHOPKEEPER;
                break;
        }
    }

    private void OnEnable()
    {
        if (used <= maxUses)
        {
            StartCoroutine(UseArrow());
        }
    }

    private void Update()
    {
        if (target)
        {
            Aim(target.position);
        }
    }

    private void Aim(Vector3 position)
    {
        Vector3 dif = position - transform.position;
        Quaternion qua = Quaternion.LookRotation(Vector3.forward, dif);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qua, Time.deltaTime * rotationSpeed);
        dif.z = 0;
        dif.Normalize();
    }
    private void OnTriggerStay2D(Collider2D target)
    {
        if (this.target == target.transform || !target.name.Contains(aimTag)) return;
        if (this.target)
        {
            if (shouldTarget(transform, this.target.gameObject, target.gameObject)) this.target = target.transform;
            return;
        }
        this.target = target.transform;
    }

    private static bool TargetClosest(Transform transform, GameObject oldtarget, GameObject newtarget)
    {
        return Vector3.Distance(oldtarget.transform.position, transform.position) > Vector3.Distance(newtarget.transform.position, transform.position);
    }

    private IEnumerator UseArrow()
    {
        used++;
        yield return new WaitForSeconds(arrowTime);
        gameObject.SetActive(false);
    }
}
