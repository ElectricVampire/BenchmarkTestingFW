using SpeedTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkDotNetCollections
{
    [SpeedTestClass]
    class AddAndInsertTestClass
    {
        Random rand = null;
        List<int> testList = null;
        Dictionary<int, int> testDic = null;
        private const int COLLECTION_SIZE = 1000000;

        #region LIST_METHODS
        #region SPEED_TEST_METHOD        
        [SpeedTestMethod(1, "Here we have added 1000000 elements in list by using \".Add\" function" , "Add_ListInitialization", "ListReset")]
        public void AddList()
        {
            for (int i = 0; i < COLLECTION_SIZE; i++)
            {
                testList.Add(rand.Next(1, 1001));
            }
        }

        [SpeedTestMethod(10000, "This test is to perform 10000 insert operation in list using \".Insert\" function", "Insert_ListInitialization", "ListReset")]
        public void InsertList()
        {
            testList.Insert(rand.Next(0, COLLECTION_SIZE), 1000);
        }

        #endregion

        #region INIT_METHODS
        public void Add_ListInitialization()
        {
            testList = new List<int>();
            rand = new Random();
        }
        public void Insert_ListInitialization()
        {
            Add_ListInitialization();
            AddList();
        }
        #endregion

        #region RESET_METHODS
        public void ListReset()
        {
            testList = null;
            rand = null;
        }
        #endregion
        #endregion

        #region DICTIONARY_METHODS
        #region SPEED_TEST_METHOD
        [SpeedTestMethod(1, "This test is to add 1000000 elements in Dictionary using \".Add\" function", "Add_DicInitialization", "DicReset")]
        public void AddDic()
        {
            for (int i = 0; i < COLLECTION_SIZE; i++)
            {
                testDic.Add(i, rand.Next(0, 1000));
            }
        }

        [SpeedTestMethod(10000, "This test is to perform 10000 insert operation in dictionary using \".Insert\" function", "Insert_DicInitialization", "DicReset")]
        public void InsertDic()
        {
            testDic[rand.Next(0, COLLECTION_SIZE)] = 1000;
        }
        #endregion

        #region INIT_METHODS
        public void Add_DicInitialization()
        {
            testDic = new Dictionary<int, int>();
            rand = new Random();
        }

        public void Insert_DicInitialization()
        {
            Add_DicInitialization();
            AddDic();
        }
        #endregion

        #region RESET_METHODS
        public void DicReset()
        {
            testDic = null;
            rand = null;
        }
        #endregion
        #endregion         
    }
}
