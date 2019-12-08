﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : Singleton<Toolbox>
{
    // Used to track any components added at runtime.
    private HashSet<Component> components = new HashSet<Component>();

    // Prevent constructor use.
    protected Toolbox() { }

    public int Count => components.Count;
    public bool Add(Component component) => components.Add(component);
    public bool Contains(Component component) => components.Contains(component);
    public HashSet<Component>.Enumerator GetEnumerator() => components.GetEnumerator();
    public bool Remove(Component component) => components.Remove(component);
    public int RemoveWhere(Predicate<Component> predicate) => components.RemoveWhere(predicate);

    // Return if the component is found and output the value
    public bool TryGetValue<T>(out T value) where T : Component
    {
        foreach(var element in components)
        {
            if(element is T)
            {
                value = element as T;
                return true;
            }
        }

        value = null;
        return false;
    }
}
