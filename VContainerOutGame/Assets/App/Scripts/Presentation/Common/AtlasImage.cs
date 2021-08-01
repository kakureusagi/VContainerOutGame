using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace App.Presentation
{

	/// <summary>
	/// SpriteAtlasをいい感じに扱うコンポーネント
	/// </summary>
	public class AtlasImage : Image
	{
		[SerializeField]
		SpriteAtlas atlas = default;


		public void UpdateSprite(string spriteName)
		{
			sprite = atlas.GetSprite(spriteName);
			SetNativeSize();
		}


#if UNITY_EDITOR
		[CustomEditor(typeof(AtlasImage))]
		public class AtlasImageEditor : ImageEditor
		{
			SerializedProperty atlasProperty;

			protected override void OnEnable()
			{
				base.OnEnable();
				var image = target as AtlasImage;
				atlasProperty = serializedObject.FindProperty(nameof(image.atlas));
			}

			public override void OnInspectorGUI()
			{
				serializedObject.Update();
				EditorGUILayout.PropertyField(atlasProperty);
				serializedObject.ApplyModifiedProperties();
				
				base.OnInspectorGUI();
				
				
			}
		}
#endif
	}

}