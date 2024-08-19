namespace Comabit.DL.Test
{
    using Comabit.DL.DBDal;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        public DataStorageImplementation DataStorage
        {
            get
            {
                return new DataStorageImplementation();
            }
        }

        [Test]
        public async Task CreateBuyerTestAsync()
        {
            var newbuyerTest = new BuyerTest()
            {
                Text = "Walter Test",
                CreateUserId = "4dc810aa-fadc-4e16-b150-cfc0d1cc5991",
                CreateTimestamp = DateTime.Now,
                LastChangeUserId = "4dc810aa-fadc-4e16-b150-cfc0d1cc5991",
                LastChangeTimestamp = DateTime.Now,
            };
            var result = await this.DataStorage.CreateBuyerTestAsync(newbuyerTest);
            var retrieveBuyerTest = await this.DataStorage.RetrieveBuyerTestByIdAsync(result);
            Assert.NotNull(retrieveBuyerTest);
            Assert.AreEqual(newbuyerTest.Text, retrieveBuyerTest.Text);
            retrieveBuyerTest.Text = "Walter Test Aenderung";
            retrieveBuyerTest.LastChangeUserId = "f9f05efa-c892-4963-9f6f-b9f34c3f8f73";
            retrieveBuyerTest.LastChangeTimestamp = DateTime.Now;
            await this.DataStorage.UpdateBuyerTestAsync(retrieveBuyerTest);
            var updatedBuyerTest = await this.DataStorage.RetrieveBuyerTestByIdAsync(retrieveBuyerTest.BuyerTestId);
            Assert.NotNull(updatedBuyerTest);
            Assert.AreEqual(retrieveBuyerTest, updatedBuyerTest);
        }
    }
}