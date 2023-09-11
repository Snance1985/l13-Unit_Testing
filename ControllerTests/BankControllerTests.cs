using BankAccountAPI.Controllers;
using BankAccountAPI.Models;
using BankAccountAPI.Repositories;
using NSubstitute;
using Xunit;

namespace BankTests.ControllerTests;

public class BankControllerTests
{
    [Fact]
    public void CreateNewAccount_WhenInvalidAccountDetails_ReturnsNull()
    {
        // Arrange
        var mockRepository = Substitute.For<IBankAccountRepository>();
        var controller = new BankController(mockRepository);
        var newAccount = new CheckingAccount
        {
            Name = "My Checking Account",
            Balance = 100
        };

        // Act
        var response = controller.CreateNewAccount(newAccount);

        // Assert
        Assert.Equal(null, response);
    }

    [Fact]
    public void CreateNewAccount_WhenValidRequest_CallsRepositoryAndReturnsAccount()
    {
        // Arrange
        var newAccount = new CheckingAccount
        {
            AccountNumber = 12345,
            Name = "My Checking Account",
            Balance = 100
        };

        var mockRepository = Substitute.For<IBankAccountRepository>();
        mockRepository.CreateAccount(newAccount)
            .Returns(newAccount);

        var controller = new BankController(mockRepository);

        // Act
        var response = controller.CreateNewAccount(newAccount);

        // Assert
        mockRepository.Received().CreateAccount(Arg.Is<CheckingAccount>(
            x => x.AccountNumber == 12345
        ));
        Assert.Equal(newAccount, response);

    }

    [Fact]
    public void GetAccountDetails_WhenInvalidAccountNumber_ReturnsNull()
    {
        // Arrange
        var mockRepository = Substitute.For<IBankAccountRepository>();
        var controller = new BankController(mockRepository);

        // Act
        var response = controller.GetAccountDetails(-1);

        // Assert
        Assert.Equal(null, response);
    }

    [Fact]
    public void GetAccountDetails_WhenValidNumber_CallsRepositoryAndReturnsAccount()
    {
        // Arrange
        var accountNum = 1234567;
        var accountDetails = new CheckingAccount
        {
            AccountNumber = accountNum,
            Name = "My Checking Account",
            Balance = 1005
        };

        var mockRepository = Substitute.For<IBankAccountRepository>();
        mockRepository.GetBankAccount(accountNum).Returns(accountDetails);

        var controller = new BankController(mockRepository);

        // Act
        var response = controller.GetAccountDetails(accountNum);

        // Assert
        mockRepository.Received().GetBankAccount(accountNum);
        Assert.Equal(accountDetails, response);
    }


}

