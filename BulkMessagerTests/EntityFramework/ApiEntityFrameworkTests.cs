using BulkMessager.Data;
using BulkMessager.Data.Entities;
using BulkMessager.Data.Repositories;
using BulkMessager.Utils;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BulkMessagerTests.EntityFramework {

    public class ApiEntityFrameworkTests {

        private readonly IApplicationLogger<Message> _logger;
        private readonly IRepository<Message, long> _repo;
        private readonly IMessageRepository _messageRepoInt;

        public ApiEntityFrameworkTests() {
             _logger = A.Fake<IApplicationLogger<Message>>();
            _repo = A.Fake<IRepository<Message, long>>(); 
            _messageRepoInt = A.Fake<IMessageRepository>();
        }

        [Theory]
        [InlineData(1)]
        public async Task MessageRepository_FindByIdAsync_ReturnsMessage(long id) {

            #region Arrange

             var context = await GetDatabaseContext();
             var repository = new MessageRepository(context, _logger);

            #endregion

            #region Act

            var result = await repository.FindByIdAsync(id);
            var resultId = result.Id;
            #endregion

            #region Assert
            
            result.Should().NotBeNull();
            result.Should().BeOfType<Message>();
            resultId.Should().Be(1);
            #endregion
        }

        [Theory]
        [InlineData(0)]
        public async Task MessageRepository_FindByIdAsync_ReturnsNull(long id) {

            #region Arrange
             var context = await GetDatabaseContext();
             var repository = new MessageRepository(context, _logger);

            #endregion

            #region Act
            var result = await repository.FindByIdAsync(id);
            #endregion

            #region Assert
            
            result.Should().BeNull();
            #endregion
        }

        [Fact]
        public void MessageRepository_GetAllAsync_ReturnsMessages() {

        }

        [Theory]
        [InlineData(1)]
        public async Task MessageRepository_ExistsAsync_ReturnsTrue(long id) {
            
            #region Arrange
             var context = await GetDatabaseContext();
             var repository = new MessageRepository(context, _logger);

            #endregion

            #region Act
            var result = await repository.ExistsAsync(m => m.Id == id);
            #endregion

            #region Assert
            
            result.Should().BeTrue();
            #endregion
        }

        [Theory]
        [InlineData(0)]
        public async Task MessageRepository_ExistsAsync_ReturnsFalse(long id) {
            
            #region Arrange
             var context = await GetDatabaseContext();
             var repository = new MessageRepository(context, _logger);

            #endregion

            #region Act
            var result = await repository.ExistsAsync(m => m.Id == id);
            #endregion

            #region Assert
            
            result.Should().BeFalse();
            #endregion
        }

        /// <summary>
        /// Create an in-memory database for unit testing
        /// </summary>
        /// <returns></returns>
        private async Task<DataContext> GetDatabaseContext() {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString())
                .Options;

            //..create context
            var dbContext = new DataContext(options);

            //..add messages
            if(!await dbContext.Messages.AnyAsync()) {
                for(var i = 0; i< 10; i++) {
                    dbContext.Messages.AddRange(
                        new Message[]{
                            new(){
                                Text = "Many returns on this day for you. Hapy birth day",
                                Status = Status.Running, 
                                RunFrom = new DateTime(2024,01,01),
                                RunTo = new DateTime(2024,12,31),
                                Interval = Interval.Daily,
                                IntervalStatus = IntervalStatus.None,
                                IsApproved = true, 
                                NextSendDate = null, 
                                AddedBy = "Macjohnan",
                                AddedOn = DateTime.UtcNow,
                                LastModifiedBy = null, 
                                LastModifiedOn = null
                            },
                            new(){
                                Text = "We love to share with you all during this holy month of Ramadan. May Allah bless you all greatly",
                                Status = Status.New, 
                                RunFrom = new DateTime(2024,04,16),
                                RunTo = new DateTime(2024,04,16),
                                Interval = Interval.Daily,
                                IntervalStatus = IntervalStatus.None,
                                IsApproved = true, 
                                NextSendDate = null, 
                                AddedBy = "Macjohnan",
                                AddedOn = DateTime.UtcNow,
                                LastModifiedBy = null, 
                                LastModifiedOn = null
                            },
                            new() {
                                Text = "With love through the festive season. Merry Christmas",
                                Status = Status.New, 
                                RunFrom = new DateTime(2024,12,01),
                                RunTo = new DateTime(2024,12,01).AddDays(35),
                                Interval = Interval.Weekly,
                                IntervalStatus = IntervalStatus.None,
                                IsApproved = true, 
                                NextSendDate = null, 
                                AddedBy = "Macjohnan",
                                AddedOn = DateTime.UtcNow,
                                LastModifiedBy = null, 
                                LastModifiedOn = null
                            },
                            new(){
                                Text = "May your candles burn bright in this comming year. Happy New Year",
                                Status = Status.New, 
                                RunFrom = new DateTime(2024,12,20),
                                RunTo = new DateTime(2024,12,20).AddDays(15),
                                Interval = Interval.Weekly,
                                IntervalStatus = IntervalStatus.None,
                                IsApproved = true, 
                                NextSendDate = null, 
                                AddedBy = "Macjohnan",
                                AddedOn = DateTime.UtcNow,
                                LastModifiedBy = null, 
                                LastModifiedOn = null
                            }
                        }
                    );

                    await dbContext.SaveChangesAsync();
                }
            }

            return dbContext;
        }
    }
}
