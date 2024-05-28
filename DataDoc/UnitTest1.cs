namespace DataDoc
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arange 
            int valueA = 1;
            int valueB = 2;
            int valueExpected = 3;

            //Act

            int result = valueA + valueB;


            //Assert
            Assert.Equal(valueExpected, result);
        }
    }
}