using System;
using System.Linq;
using System.IO;
public class ArraySet : ISetInt
{
    private int[] _item;
    private int _size;
    public ArraySet()
    {
        _item = new int[16];
        _size = 0;
    }

    public int Count {get {return _size;} }

    public bool Add(int value)
    {
        bool Check = this.Contains(value);
        if(Check)
        {
            return false;
        }
        if(_size == _item.Length)
        {
            Array.Resize(ref _item, _size * 2);
        }
        _item[_size] = value;
        _size++;
        return true;
    }
    public bool Remove(int value)
    {
        int index = this.FindIndex(value);
        if(index == -1)
        {
            return false;
        }
        for(int i = index; i < _size - 1; i++)
        {
            _item[i] = _item[i + 1];
        }
        this._size--;
        return true;
    }

    public void Clear()
    {
        _size = 0;
    }

    public bool Contains(int value)
    {
        int index = this.FindIndex(value);
        return index >= 0;
    }
    private int FindIndex(int value)
    {
        for(int i = 0; i < _size; i++)
        {
            if(_item[i] == value)
            {
                return i;
            }
        }
        return -1;
    }

    public void CopyTo(int[] array)
    {
        Array.Copy(_item, array, _size);
        if(array.Length <= _size)
        {
            throw new System.ArgumentException("Array is too small.");
        }
    }

    bool ISetInt.Overlaps(ISetInt other)
    {
        for(int i = 0; i < _size; i++)
        {
            if(other.Contains(_item[i]))
            {
                return true;
            }
        }
        return false;
    }

    void ISetInt.SymmetricExceptWith(ISetInt other)
    {
        int[] arr = new int[16];
        other.CopyTo(arr);
        var numbersList = arr.ToList();
        for(int i = 0; i < _size; i++)
        {
            bool check = other.Contains(_item[i]);
            if(check)
            {
                numbersList.Remove(_item[i]);
                this.Remove(_item[i]);
            }
        }
        arr = numbersList.ToArray();
        for(int j = 0; j < arr.Length - 1; j++)
        {
            if(arr[j] == 0)
            {
                continue;
            }
            this.Add(arr[j]);
        }
    }
    ISetInt ISetInt.ReadSet(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        string number = "";
        while (true)
        {
            number = reader.ReadLine();
            if(number == null)
            {
                break;
            }
        
            int numb = int.Parse(number);
            this.Add(numb);
        }
        reader.Close();
        return this;
    }
    void ISetInt.WriteSet(string filePath, ISetInt set)
    {
        StreamWriter writer = new StreamWriter(filePath);
        int[] arr = new int[30];
        set.CopyTo(arr);
        for(int i = 0; i < arr.Length - 1; i++)
        {
            if(arr[i] == 0)
            {
                continue;
            }
            writer.WriteLine(arr[i]);
        }
        
        writer.Close();
    }
}
