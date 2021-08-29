using App.Domain;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Presentation
{
	public abstract class DialogViewBase : MonoBehaviour, IDialogAnimator
	{
		static readonly int OpenAnimationHash = Animator.StringToHash("Open");
		static readonly int CloseAnimationHash = Animator.StringToHash("Close");

		[SerializeField]
		Animator animator = default;

		[SerializeField]
		CanvasGroup canvasGroup = default;

		readonly UniTaskCompletionSource openCompletionSource = new UniTaskCompletionSource();
		readonly UniTaskCompletionSource closeCompletionSource = new UniTaskCompletionSource();

		private void Awake()
		{
			animator.enabled = false;
			canvasGroup.alpha = 0;
		}

		private void Start()
		{
			OpenAsync().Forget();
		}

		private async UniTask OpenAsync()
		{
			animator.enabled = true;
			animator.Play(OpenAnimationHash);
			await openCompletionSource.Task;
		}

		public async UniTask CloseAsync()
		{
			animator.Play(CloseAnimationHash);
			await closeCompletionSource.Task;
			animator.enabled = false;
			Destroy(gameObject);
		}

		public void OnDialogOpenAnimationFinish()
		{
			openCompletionSource.TrySetResult();
		}

		private void OnDialogCloseAnimationFinish()
		{
			closeCompletionSource.TrySetResult();
		}


#if UNITY_EDITOR
		void OnValidate()
		{
			if (animator == null)
			{
				animator = GetComponent<Animator>();
				if (animator == null)
				{
					animator = gameObject.AddComponent<Animator>();
				}
			}

			if (canvasGroup == null)
			{
				canvasGroup = GetComponent<CanvasGroup>();
				if (canvasGroup == null)
				{
					canvasGroup = gameObject.AddComponent<CanvasGroup>();
				}
			}
		}
#endif
	}
}