using System;
using System.Collections.Generic;
using System.Linq;

public class CircularList<T>
{
    private List<T> list = new();
    private int currentIndex = 1;

    public void Add(T item)
    {
        list.Add(item);
    }

    public T Find(Predicate<T> match)
    {
        return list.Find(match);
    }

    public bool All(Func<T, bool> predicate)
    {
        return list.All(predicate);
    }

    public List<T> Where(Func<T, bool> predicate)
    {
        return list.Where(predicate).ToList();
    }

    public void ForEach(Action<T> action)
    {
        list.ForEach(action);
    }

    public T GetNext()
    {
        if (list.Count == 0)
        {
            return default;
        }
        T item = list[currentIndex];
        currentIndex = (currentIndex + 1) % list.Count;
        return item;
    }

    public int Count() { return list.Count; }

    public void Remove(T item)
    {
        list.Remove(item);
    }

    public void Clear()
    {
        this.list.Clear();
        currentIndex = 0;
    }

    public List<T> GetList() { return list; }
}
