using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeHelper : MonoBehaviour
{
   /// <summary>
    /// C#获取一个类在其所在的程序集中的所有子类
    /// </summary>
    /// <param name="parentType">给定的类型</param>
    /// <returns>所有子类的名称</returns>
    public static List<string> GetSubClassNames(Type parentType)
    {
        var subTypeList = new List<Type>();
        var assembly = parentType.Assembly;//获取当前父类所在的程序集
        var assemblyAllTypes = assembly.GetTypes();//获取该程序集中的所有类型
        foreach (var itemType in assemblyAllTypes)//遍历所有类型进行查找
        {
            var baseType = itemType.BaseType;//获取元素类型的基类
            if (baseType != null)//如果有基类
            {
                if (baseType.Name == parentType.Name)//如果基类就是给定的父类
                {
                    subTypeList.Add(itemType);//加入子类表中
                }
            }
        }
        return subTypeList.Select(item => item.Name).ToList();//获取所有子类类型的名称
    }

    /// <summary>
    /// C#获取一个类在其所在的程序集中的指定名的子类
    /// </summary>
    /// <param name="parentType">给定的类型</param>
    /// <returns>子类的Type</returns>
    public static Type GetSubClassType(Type parentType, string subTypeName)
    {
        var assembly = parentType.Assembly;//获取当前父类所在的程序集
        var assemblyAllTypes = assembly.GetTypes();//获取该程序集中的所有类型
        foreach (var itemType in assemblyAllTypes)//遍历所有类型进行查找
        {
            var baseType = itemType.BaseType;//获取元素类型的基类
            if (baseType != null)//如果有基类
            {
                if (baseType.Name == parentType.Name)//如果基类就是给定的父类
                {
                    if (itemType.Name == subTypeName)
                    {
                        return itemType;
                    }
                }
            }
        }
        return null;
    }
}
