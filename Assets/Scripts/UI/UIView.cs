using UnityAsync;
using UnityEngine;

public class UIView : MonoBehaviour
{
	public virtual void Initialize() { }

	private bool _isInvalid = false;

	public bool Visible
	{
		get { return gameObject.activeInHierarchy; }
		protected set { gameObject.SetActive(value); }
	}

	public virtual void Init() { }

	public virtual void Show()
	{
		Visible = true;
	}
	public virtual void Hide()
	{
		Visible = false;
	}
	public void Toggle()
    {
		if (Visible)
			Hide();
		else
			Show();
    }

	// Current state of UI is invalid
	public async void Invalidate(bool immediate = false)
	{
		if (immediate)
			Revalidate();
		else
		{
			if (!_isInvalid)
			{
				_isInvalid = true;
				await Await.NextLateUpdate();
				Revalidate();
			}
		}
	}

	protected virtual void OnInvalidated() { }

	private void Revalidate()
	{
		OnInvalidated();
		_isInvalid = false;
	}

	protected void DestroyChildren()
	{
		for (int i = 0; i < transform.childCount; i++)
			Destroy(transform.GetChild(i).gameObject);
	}
}
