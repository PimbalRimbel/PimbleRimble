
    public interface IPool
    {
        public void Get(IPooledObject aux);
        public void Release(IPooledObject obj);
    }
