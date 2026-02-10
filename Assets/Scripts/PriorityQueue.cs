using UnityEngine;
using System;
using System.Collections.Generic;

public class PriorityQueue<TElement, TPriority>
{
    private List<(TElement element, TPriority priority)> heap = new();
    private Comparer<TPriority> comparer = Comparer<TPriority>.Default;

    public int Count => heap.Count;

    // 加入
    public void Enqueue(TElement element, TPriority priority)
    {
        heap.Add((element, priority));
        HeapifyUp(heap.Count - 1);
    }

    // 取出最小 priority
    public TElement Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue empty");

        var root = heap[0].element;

        var last = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        if (heap.Count > 0)
        {
            heap[0] = last;
            HeapifyDown(0);
        }

        return root;
    }

    // 查看最小但不取出
    public TElement Peek()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue empty");

        return heap[0].element;
    }

    public TPriority PeekPriority()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue empty");

        return heap[0].priority;
    }

    private void HeapifyUp(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;

            if (comparer.Compare(heap[i].priority, heap[parent].priority) >= 0)
                break;

            (heap[i], heap[parent]) = (heap[parent], heap[i]);
            i = parent;
        }
    }

    private void HeapifyDown(int i)
    {
        int last = heap.Count - 1;

        while (true)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            int smallest = i;

            if (left <= last &&
                comparer.Compare(heap[left].priority, heap[smallest].priority) < 0)
                smallest = left;

            if (right <= last &&
                comparer.Compare(heap[right].priority, heap[smallest].priority) < 0)
                smallest = right;

            if (smallest == i)
                break;

            (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
            i = smallest;
        }
    }
}
