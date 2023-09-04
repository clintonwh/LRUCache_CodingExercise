using LRU;

namespace Cache.nUnitTests
{
    [TestClass]
    public class LRUCacheTests
    {
        private ILRUCache testCache;
        private int cacheSize = 3;
        private StringWriter stringWriter;

        private Dictionary<int, dynamic> testDictionary = new Dictionary<int, dynamic>()
        {
            {1, true}, {3, "string" }, {6, 99.99 }, {9, -33 }
        };

        [TestInitialize]
        public void TestInitialize()
        {
            testCache = LRUCache.GetInstance(cacheSize);
        }

        [TestMethod]
        public void Step00_Get_AnEmptyCacheShouldReturnNull_True()
        {
            Assert.IsNull(testCache.Get(1));
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
            
            testCache = LRUCache.GetInstance(newCapacity);

            Assert.AreNotEqual(newCapacity, testCache.GetCapacity());
        }

        [TestMethod]
        public void Step03_Display_CacheIsInExpectedOrder_True()
        {
            string expectedResult = "6 3 1";

            InsertValueIntoTestCache(1);
            InsertValueIntoTestCache(3);
            InsertValueIntoTestCache(6);      

            Assert.AreEqual(expectedResult, testCache.Display());
        }

        [TestMethod]
        public void Step04_Display_WhenCacheIsFullRemovedLeastRecentlyUsed_True()
        {
            string expectedResult = "9 6 3";
         
            InsertValueIntoTestCache(9);

            Assert.AreEqual(expectedResult, testCache.Display());
        }

        [TestMethod]
        public void Step05_Display_CachedDataMovesToTopOfCacheOnceUsed_True()
        {
            string expectedResult = "1 9 6";

            InsertValueIntoTestCache(1);

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

        [TestMethod]
        public void Step10_Get_ReturnNullIfValueIsNotInFullCache_True()
        {
            Assert.IsNull(testCache.Get(3));
        }

        [TestMethod]
        public void Step12_ConsoleShouldReturnKeyAndValueWhenRemovingDataFromCacheBool_True()
        {
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            int dictionaryIndex = 1;

            InsertValueIntoTestCache(3); //order is now "3 6 9"

            var output = stringWriter.ToString();
            Assert.AreEqual($"Removed From Cache - Key: {dictionaryIndex}, Value: {testDictionary[dictionaryIndex]}\r\n", output);
        }

        [TestMethod]
        public void Step13_ConsoleShouldReturnKeyAndValueWhenRemovingDataFromCacheInteger_True()
        {
            stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            int dictionaryIndex = 9;

            InsertValueIntoTestCache(1); //order is now "1 3 6"

            var output = stringWriter.ToString();
            Assert.AreEqual($"Removed From Cache - Key: {dictionaryIndex}, Value: {testDictionary[dictionaryIndex]}\r\n", output);
        }

        //Helper function to make it easier to insert into cache for testing
        private void InsertValueIntoTestCache(int key)
        {
            testCache.Put(key, testDictionary[key]);
        }
    }
}