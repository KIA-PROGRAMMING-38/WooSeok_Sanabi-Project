using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IObjectPool<T> where T : class
{
    T GetFromPool();

    void ReturnToPool(T element);

    void ClearPool();

}

