using TMPro;
using UnityAsync;
using UnityEngine;

public class HitText : MonoBehaviour
{
    public Vector3 Velcocity { get; set; }
    public string Text { get { return _textMesh.text; } set { _textMesh.text = value; } }
    public Color Color { get { return _textMesh.color; } set { _textMesh.color = value; } }
    public float FontSize { get { return _textMesh.fontSize; } set { _textMesh.fontSize = value; } }

    private TextMeshPro _textMesh;

    public static async void Create(string text, Vector3 position, Vector3 velocity, Color color)
    {
        var hitText = HitTextManager.Pool.GetFreeObject();
        hitText.Text = text;
        hitText.transform.position = position;
        hitText.Velcocity = velocity;
        hitText.Color = color;
        await Await.Seconds(HitTextManager.HIT_TEXT_LIFE_TIME);
        HitTextManager.Pool.ReturnToPool(hitText);
    }

    public void Init()
    {
        _textMesh = GetComponent<TextMeshPro>();
        _textMesh.sortingOrder = 50;
    }

    public void UpdateHitText()
    {
        UpdatePosition();
        LookAtCamera();
    }

    private void UpdatePosition()
        => transform.position += Camera.main.transform.localToWorldMatrix.MultiplyVector(Velcocity * Time.deltaTime);
    private void LookAtCamera()
        => transform.rotation = Camera.main.transform.rotation;
}
