    public interface ISetInt
    {
        int Count{get;}
        bool Add(int value);
        bool Remove(int value);
        bool Contains(int value);
        void Clear();
        void CopyTo(int[] array);
        bool Overlaps(ISetInt other);
        void SymmetricExceptWith(ISetInt other);
        ISetInt ReadSet(string filePath);
        void WriteSet(string filePath, ISetInt set);
    }

