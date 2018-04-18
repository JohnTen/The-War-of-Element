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
		ElementIcon baseIcon;

		[SerializeField]
		ElementIcon[] icons;


		public void ShowElements(Element element)
		{
			DisposeAllIcon();
			icons = new ElementIcon[1];
			icons[0] = Instantiate(baseIcon.gameObject).GetComponent<ElementIcon>();
			icons[0].transform.SetParent(this.transform);
			icons[0].transform.localPosition = Vector3.zero;
			icons[0].transform.localRotation = Quaternion.identity;
			icons[0].Element = element;
		}

		public void ShowElements(Element[] elements)
		{
			DisposeAllIcon();
			icons = new ElementIcon[elements.Length];


			for (int i = 0; i < elements.Length; i++)
			{
				icons[i] = Instantiate(baseIcon.gameObject).GetComponent<ElementIcon>();
				icons[i].transform.SetParent(this.transform);
				icons[i].transform.localPosition = Vector3.zero;
				icons[i].transform.localRotation = Quaternion.identity;
				icons[i].Element = elements[i];
			}
		}

		void DisposeAllIcon()
		{
			for (int i = 0; i < icons.Length; i++)
			{
				Destroy(icons[i].gameObject);
			}
		}
	}
}