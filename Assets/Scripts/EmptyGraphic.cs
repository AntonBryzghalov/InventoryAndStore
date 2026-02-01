using UnityEngine;
using UnityEngine.UI;

namespace CWTL.Code.UI.Components
{
    /// <summary>
    /// Provides a Graphic clickable component, which doesn't have a mesh and doesn't cause overdraws (like a transparent images do)
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class EmptyGraphic : Graphic
    {
        protected override void OnPopulateMesh (VertexHelper vh)
        {
            vh.Clear ();
        }
    }
}
