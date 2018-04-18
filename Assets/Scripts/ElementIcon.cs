using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ElementWar
{
	public class ElementIcon : MonoBehaviour, IElementalObject
	{
		[SerializeField]
		Image image;

		[SerializeField]
		Color[] colorsForElements;

		[SerializeField]
		Element element;
		public Element Element
		{
			get { return element; }
			set
			{
				element = value;
				switch (element)
				{
					case Element.Air:
						image.color = colorsForElements[0];
						break;
					case Element.Fire:
						image.color = colorsForElements[1];
						break;
					case Element.Earth:
						image.color = colorsForElements[2];
						break;
					case Element.Water:
						image.color = colorsForElements[3];
						break;
				}
			}
		}

		private void Start()
		{
			Element = Element;
		}
	}
}

