using LRU;

namespace Cache.nUnitTests
{
    [TestClass]
    public class LRUCacheTests
    {
        private ILRUCache testCache;
        private int cacheSize = 3;

        private Dictionary<int, dynamic> testDictionary = new Dictionary<int, dynamic>()
        {
            {1, 1},
            {3, "string" },
            {6, 99.99 },
            {9, 333 }

        };

        [TestInitialize]
        public void TestInitialize()
        {
            testCache = LRUCache.GetInstance(cacheSize);
        }

        [TestMethod]
        public void Step00_Get_AnEmptyCacheShouldReturnMinusOne_True()
        {
            Assert.AreEqual(-1, testCache.Get(1));
        }     

        [TestMethod]
        public void Step01_GetCapacity_IntendedCapacityIsSet_True()
        {
            Assert.AreEqual(cacheSize, testCache.GetCapacity());
        }

        [TestMethod]
        public void Step02_GetCapacity_NewCapacityHasNotBeSet_True()
        {
            int newCapacity = cacheSize + 10;
            //second attempt
            testCache = LRUCache.GetInstance(newCapacity);
            Assert.AreNotEqual(newCapacity, testCache.GetCapacity());
        }

        [TestMethod]
        public void Step03_Display_CacheIsInExpectedOrder_True()
        {
            InsertValueIntoTestCache(1);
            InsertValueIntoTestCache(3);
            InsertValueIntoTestCache(6);

            string expectedResult = "6 3 1";

            Assert.AreEqual(expectedResult, testCache.Display());
        }

        [TestMethod]
        public void Step04_Display_WhenCacheIsFullRemovedLeastRecentlyUsed_True()
        {
            InsertValueIntoTestCache(9);

            string expectedResult = "9 6 3";

            Assert.AreEqual(expectedResult, testCache.Display());
        }

        [TestMethod]
        public void Step05_Display_CachedDataMovesToTopOfCacheOnceUsed_True()
        {
            InsertValueIntoTestCache(1);

            string expectedResult = "1 9 6";

            Assert.AreEqual(expectedResult, testCache.Display());
        }

        [TestMethod]
        public void Step06_Get_StringShouldBeReturned_True()
        {
            dynamic cacheResult = testCache.Get(9);

            Assert.AreEqual(testDictionary[9], cacheResult);
        }

        [TestMethod]
        public void Step07_Display_DataInMiddleOfCacheShouldGoToTheTop_True()
        {
            string expectedResult = "9 1 6";
            Assert.AreEqual(expectedResult, testCache.Display());
        }

        [TestMethod]
        public void Step08_Get_DoubleShouldBeReturned_True()
        {
            dynamic cacheResult = testCache.Get(6);

            Assert.AreEqual(testDictionary[6], cacheResult);
        }

        [TestMethod]
        public void Step09_Display_DataAtBottomOfCacheShouldGoToTheTop_True()
        {
            string expectedResult = "6 9 1";
            Assert.AreEqual(expectedResult, testCache.Display());
        }

        //Helper function to make it easier to insert into cache for testing
        private void InsertValueIntoTestCache(int key)
        {
            testCache.Put(key, testDictionary[key]);
        }
    }
}