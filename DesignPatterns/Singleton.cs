namespace OOP.DesignPatterns
{
    /* sealed: niêm phong
    * class: not enable inherit
    * method: not enable override
    */
    public sealed class Singleton
    {
        /*
         * - A singleton is like a deck a cards - 1 bộ bài (handy - tiện dụng, compact - gọn nhẹ)
         * -  only one instance of a class is to be created
         * - It representing a single DB connection shared by multiple objects or reading a configuration file.
         */
        private static Singleton? singleton = null;
        // Object for lock functionality
        private static readonly object singletonLock = new object();

        // Private constructor ensures instances can't be made elsewhere
        private Singleton() { }

        // The SingleInstance method here acts like the gatekeeper
        // only accessible via its very own property SingleInstance.
        public static Singleton SingleInstance
        {
            get
            {
                // Khóa object singletonLock đến khi hoàn thành câu lệnh các thread khác sẽ phải đợi khi giải phóng ms được xài
                // keeping sure it is safely locked and thread-safe
                lock (singletonLock)
                {
                    if(singleton == null)
                    {
                        singleton = new Singleton();
                    }
                    return singleton;
                }
            }
        }
    }
}
