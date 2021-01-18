using UnityEngine;

public class CharacterAttachmentPoints : MonoBehaviour
{
    public AttachPoint LeftHand => _leftHand;
    public AttachPoint RightHand => _rightHand;

    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;

    private AttachPoint _leftHand;
    private AttachPoint _rightHand;

    private void Awake()
    {
        _leftHand = new AttachPoint(_leftHandTransform);
        _rightHand = new AttachPoint(_rightHandTransform);
    }

    public class AttachPoint
    {
        public Transform Transform { get; private set; }
        public Item Item { get; private set; }

        public AttachPoint(Transform transform)
        {
            Transform = transform;
        }

        public void Clear(bool destroy = false)
        {
            Item = null;
            foreach (Transform child in Transform)
            {
                if (destroy)
                    Destroy(child.gameObject);
                else
                {
                    child.SetParent(null);
                    child.gameObject.SetActive(false);
                }
            }
        }
        public void Equip(Transform transform)
        {
            transform.SetParent(Transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}