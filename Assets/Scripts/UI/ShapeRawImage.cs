// 图片变形
// 参考 https://blog.csdn.net/linxinfa/article/details/123378696
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ShapeRawImage : RawImage
{
    // 倾斜偏移
    [SerializeField]
    public float offset;

    // 每次offset更改时调用
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        UIVertex vertex = new UIVertex();
        toFill.PopulateUIVertex(ref vertex, 1);
        vertex.position += Vector3.right * offset;
        toFill.SetUIVertex(vertex, 1);

        vertex = new UIVertex();
        toFill.PopulateUIVertex(ref vertex, 2);
        vertex.position += Vector3.right * offset;
        toFill.SetUIVertex(vertex, 2);
    }
}
