using System.Linq;
using UnityEngine;

public class CharacterColorControl : MonoBehaviour
{
    [SerializeField] private bool _autoFindRenderers = true;
    [SerializeField] private Renderer[] _manualRenderers;

    private ColorController[] _colorControllers;

    private void Start()
    {
        var renderers = _manualRenderers;
        if (_autoFindRenderers)
            renderers = GetComponentsInChildren<Renderer>();
        if (_manualRenderers == null) return;

        _colorControllers = renderers.Select(r => new ColorController(r)).ToArray();
    }

    public void Reset()
    {
        foreach (var controller in _colorControllers)
            controller.Color = controller.OriginalColor;
    }

    public void SetColor(Color color)
    {
        foreach (var controller in _colorControllers)
            controller.Color = color;
    }

    class ColorController
    {
        private const string COLOR_PROP = "_BaseColor";

        public Renderer Renderer { get; private set; }
        public Color OriginalColor { get; private set; }
        public Color Color
        {
            get => _currentColor;
            set
            {
                _currentColor = value;
                Renderer.material.SetColor(COLOR_PROP, value);
            }
        }

        private Color _currentColor;

        public ColorController(Renderer renderer)
        {
            Renderer = renderer;
            OriginalColor = renderer.material.GetColor(COLOR_PROP);
            _currentColor = OriginalColor;
        }
    }
}
