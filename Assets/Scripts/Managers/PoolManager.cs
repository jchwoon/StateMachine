using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Main
{
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Queue<Poolable> _poolQueue = new Queue<Poolable>();

        public void Init(GameObject original, int count)
        {
            Original = original;

            Root = new GameObject().transform;

            Root.name = $"{original.name}_Root"; 

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        private Poolable Create()
        {
            GameObject go = Instantiate<GameObject>(Original);
            go.name = Original.name;

            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable == null)
            {
                poolable = go.AddComponent<Poolable>();
            }
            return poolable;
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null) return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);

            _poolQueue.Enqueue(poolable);
        }

        public Poolable Ppp(Transform parentTransform)
        {
            Poolable poolable;
            if (_poolQueue.Count == 0)
            {
                poolable = Create();
            }
            else
            {
                poolable= _poolQueue.Dequeue();
            }

            poolable.gameObject.SetActive(true);
            poolable.transform.parent = parentTransform;

            return poolable;
        }
    }
    private Transform _root;

    public void Initialize()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            DontDestroyOnLoad(_root);
        }
    }

    public void Push(Poolable poolable)
    {

    }

    public void Pop()
    {

    }
}
