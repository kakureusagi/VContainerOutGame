using App.Presentation.Character;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace App.EntryPoint
{

	public class CharacterListEntryPoint : MonoBehaviour
	{
		CharacterListView view;

		[Inject]
		public void Constructor(CharacterListView view)
		{
			this.view = view;
		}

		async UniTaskVoid Start()
		{
			await view.Prepare();
		}

	}

}