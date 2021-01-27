using UnityEngine;

public class BasicAttachmentPoint
{
    public Transform Root { get; private set; }

    public BasicAttachmentPoint(Transform root)
    {
        Root = root;
    }

    public virtual void Clear(bool destroy = false)
    {
        foreach (Transform child in Root)
        {
            if (destroy)
                GameObject.Destroy(child.gameObject);
            else
            {
                child.SetParent(null);
                child.gameObject.SetActive(false);
            }
        }
    }

    public virtual void Attach(Transform transform)
    {
        if (transform == null) return;
        transform.SetParent(Root);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.gameObject.SetActive(true);
    }
}