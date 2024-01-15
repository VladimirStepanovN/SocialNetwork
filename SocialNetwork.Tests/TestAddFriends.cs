using NUnit.Framework;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class TestAddFriends
    {
        private User _user1;
        private UserService _userService = new();

        [Test]
        public void AddFriend_MustBeSuccess()
        {
            _userService.Register(new UserRegistrationData()
            {
                FirstName = "test1",
                LastName = "testTest1",
                Email = "test1@gmail.ru",
                Password = "11111111"
            });

            _userService.Register(new UserRegistrationData()
            {
                FirstName = "test2",
                LastName = "testTest2",
                Email = "test2@gmail.ru",
                Password = "11111111"
            });

            _user1 = _userService.FindByEmail("test1@gmail.ru");

            UserAddingFriendData userAddingFriendData = new()
            {
                FriendEmail = "test2@gmail.ru",
                UserId = _user1.Id
            };

            _userService.AddFriend(userAddingFriendData);

            var _user1Friends = _userService.GetFriendsByUserId(_user1.Id);

            Assert.That(_user1Friends.Where(c => c.Email.Equals("test2@gmail.ru")).Count() == 1, Is.True);
        }

        [Test]
        public void AddFriend_MustBeUserNotFoundException()
        {
            UserAddingFriendData userAddingFriendData = new()
            {
                FriendEmail = "test3@gmail.ru",
                UserId = _user1.Id
            };
            Assert.Throws<UserNotFoundException>(() => _userService.AddFriend(userAddingFriendData));
        }
    }
}