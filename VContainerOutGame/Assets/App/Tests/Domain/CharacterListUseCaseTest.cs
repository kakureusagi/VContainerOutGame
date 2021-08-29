using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Domain.Character;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace App.Domain.Tests
{
	public class CharacterListUseCaseTest
	{
		class MockRepository : ICharacterRepository
		{
			CharacterEntity[] entities;

			public MockRepository(CharacterEntity[] entities)
			{
				this.entities = entities;
			}

			public async UniTask<IReadOnlyList<CharacterEntity>> GetOwnedCharacters()
			{
				await UniTask.Delay(1);
				return entities;
			}

			public async UniTask SellCharacters(IEnumerable<CharacterEntity> characters)
			{
				entities = entities
					.Where(entity => characters.All(c => c.Id != entity.Id))
					.ToArray();
				await UniTask.Delay(1);
			}
		}

		class MockDialogHelper : ICharacterListDialogHelper
		{
			public bool HasConfirmed { get; private set; }
			bool result = true;

			public void SetResult(bool result)
			{
				this.result = result;
			}

			public async UniTask<bool> ConfirmSale()
			{
				await UniTask.Delay(1);
				HasConfirmed = true;
				return result;
			}
		}


		CharacterPriceCalculator calculator;
		CharacterEntity[] entities;
		MockRepository repository;
		MockDialogHelper dialogHelper;

		[SetUp]
		public void SetUp()
		{
			calculator = new CharacterPriceCalculator();
			entities = new[]
			{
				new CharacterEntity(1, "name_1", CharacterRarity.Common, 1, 10, 100),
				new CharacterEntity(2, "name_2", CharacterRarity.Rare, 2, 20, 200),
				new CharacterEntity(3, "name_3", CharacterRarity.Epic, 3, 30, 300),
				new CharacterEntity(4, "name_4", CharacterRarity.Legendary, 4, 40, 400),
			};
			repository = new MockRepository(entities);
			dialogHelper = new MockDialogHelper();
		}

		[UnityTest]
		public IEnumerator 初期化できる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = new CharacterListUseCase(calculator, repository, null);
				await useCase.Prepare();
				Assert.That(useCase.Characters.Value.Count, Is.EqualTo(4));
				Assert.That(useCase.TotalSellPrice.Value, Is.Zero);
				Assert.That(useCase.CanSell.Value, Is.False);
				Assert.That(useCase.SelectedCharacters.Count, Is.Zero);
			});
		}

		[UnityTest]
		public IEnumerator 所持キャラクターが0でも初期化できる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = new CharacterListUseCase(calculator, new MockRepository(new CharacterEntity[0]), new MockDialogHelper());
				await useCase.Prepare();

				Assert.That(useCase.Characters.Value.Count, Is.Zero);
				Assert.That(useCase.TotalSellPrice.Value, Is.Zero);
				Assert.That(useCase.CanSell.Value, Is.False);
				Assert.That(useCase.SelectedCharacters.Count, Is.Zero);
			});
		}

		[UnityTest]
		public IEnumerator キャラクターを選択できる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				useCase.Select(entities[0]);

				Assert.That(useCase.TotalSellPrice.Value, Is.GreaterThan(0));
				Assert.That(useCase.CanSell.Value, Is.True);
				Assert.That(useCase.SelectedCharacters.Count, Is.EqualTo(1));
			});
		}

		[UnityTest]
		public IEnumerator キャラクターを選択した後に選択を外せる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				useCase.Select(entities[0]);
				useCase.Unselect(entities[0]);

				Assert.That(useCase.TotalSellPrice.Value, Is.Zero);
				Assert.That(useCase.CanSell.Value, Is.False);
				Assert.That(useCase.SelectedCharacters.Count, Is.Zero);
			});
		}

		[UnityTest]
		public IEnumerator キャラクターを売却できる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				useCase.Select(entities[0]);
				var result = await useCase.SellAsync();

				Assert.That(result, Is.True);
				Assert.That(useCase.TotalSellPrice.Value, Is.Zero);
				Assert.That(useCase.CanSell.Value, Is.False);
				Assert.That(useCase.SelectedCharacters.Count, Is.Zero);
				Assert.That(useCase.Characters.Value.Count, Is.EqualTo(entities.Length - 1));
				Assert.That(dialogHelper.HasConfirmed, Is.True);
			});
		}

		[UnityTest]
		public IEnumerator キャラクターの売却をキャンセルできる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				dialogHelper.SetResult(false);
				var useCase = await CreateUseCaseAsync();
				useCase.Select(entities[0]);
				var result = await useCase.SellAsync();

				Assert.That(result, Is.False);
				Assert.That(useCase.TotalSellPrice.Value, Is.GreaterThan(0));
				Assert.That(useCase.CanSell.Value, Is.True);
				Assert.That(useCase.SelectedCharacters.Count, Is.EqualTo(1));
				Assert.That(dialogHelper.HasConfirmed, Is.True);
			});
		}

		[UnityTest]
		public IEnumerator 全てのキャラクターを売却できる()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				foreach (var entity in entities)
				{
					useCase.Select(entity);
				}

				var result = await useCase.SellAsync();

				Assert.That(result, Is.True);
				Assert.That(useCase.TotalSellPrice.Value, Is.Zero);
				Assert.That(useCase.CanSell.Value, Is.False);
				Assert.That(useCase.SelectedCharacters.Count, Is.Zero);
				Assert.That(useCase.Characters.Value.Count, Is.Zero);
				Assert.That(dialogHelper.HasConfirmed, Is.True);
			});
		}

		[UnityTest]
		public IEnumerator 存在しないキャラクターを選択すると例外をthrow()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				var entity = new CharacterEntity(999, "name_999", CharacterRarity.Common, 1, 2, 3);

				Assert.That(() => useCase.Select(entity), Throws.InvalidOperationException);
			});
		}

		[UnityTest]
		public IEnumerator 同じキャラクターを選択しても何も起きない()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				useCase.Select(entities[0]);

				var beforePrice = useCase.TotalSellPrice.Value;
				var beforeCanSell = useCase.CanSell.Value;
				var beforeSelectedCount = useCase.SelectedCharacters.Count;
				useCase.Select(entities[0]);

				Assert.That(beforePrice, Is.EqualTo(useCase.TotalSellPrice.Value));
				Assert.That(beforeCanSell, Is.EqualTo(useCase.CanSell.Value));
				Assert.That(beforeSelectedCount, Is.EqualTo(useCase.SelectedCharacters.Count));
			});
		}

		[UnityTest]
		public IEnumerator 存在しないキャラクターで選択を外そうとすると例外をthrow()
		{
			yield return UniTask.ToCoroutine(async () =>
			{
				var useCase = await CreateUseCaseAsync();
				var entity = new CharacterEntity(999, "name_999", CharacterRarity.Common, 1, 2, 3);

				Assert.That(() => useCase.Unselect(entity), Throws.InvalidOperationException);
			});
		}

		private async UniTask<CharacterListUseCase> CreateUseCaseAsync()
		{
			var useCase = new CharacterListUseCase(calculator, repository, dialogHelper);
			await useCase.Prepare();
			return useCase;
		}
	}
}