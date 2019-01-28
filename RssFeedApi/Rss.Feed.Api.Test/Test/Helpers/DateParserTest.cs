using Rss.Feed.Api.Helpers;
using System;
using Xunit;

namespace Rss.Feed.Api.Test.Test.Helpers
{
    public class DateParserTest
    {
        private readonly IDateParser _sut;
        public DateParserTest()
        {
            _sut= new DateParser();
        }

        [Fact]
        public void ParseDate_WhenCalled_ReturnValidDateTime()
        {
            //Arrange
            var date = "Sat, 26 Jan 2019 18:23:09 GMT";

            //Act
            var result = _sut.ParseDate(date);

            //Assert
            Assert.IsType<DateTime>(result);
        }

        [Fact]
        public void ParseDate_WithInvalidDate_ReturnDateTimeMinValue()
        {
            //Arrange
            var date = "Not a valid date to convert";

            //Act
            var result = _sut.ParseDate(date);

            //Assert
            Assert.Equal(DateTime.MinValue, result);
        }
    }
}
