using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dual_Display : MonoBehaviour {


		[SerializeField, Range( 1, 8 )]
		private int m_useDisplayCount   = 2;

		private void Awake()
		{
			int count   = Mathf.Min( Display.displays.Length, m_useDisplayCount );

			for( int i = 0; i < count; ++i )
			{
				Display.displays[ i ].Activate();
			}
		}
}
