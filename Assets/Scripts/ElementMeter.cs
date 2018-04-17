using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ElementWar
{
	public class ElementMeter : MonoBehaviour
	{
		[SerializeField]
		Color[] elementColor;

		[SerializeField]
		Image baseImage;

		[SerializeField]
		Image[] images;

		public void ShowElements(Element[] elements)
		{
			for (int i = 0; i < images.Length; i++)
			{
				Destroy(images[i].gameObject);
			}
			images = new Image[elements.Length];

			for (int i = 0; i < elements.Length; i++)
			{
				images[i] = Instantiate(baseImage.gameObject).GetComponent<Image>();
				images[i].transform.SetParent(this.transform);
				images[i].transform.localPosition = Vector3.zero;
				switch (elements[i])
				{
					case Element.Air:
						images[i].color = elementColor[0];
						break;
					case Element.Fire:
						images[i].color = elementColor[1];
						break;
					case Element.Earth:
						images[i].color = elementColor[2];
						break;
					case Element.Water:
						images[i].color = elementColor[3];
						break;
				}
			}
		}
	}
}