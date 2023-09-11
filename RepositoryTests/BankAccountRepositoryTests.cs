using System;
using Xunit;
using BankAccountAPI.Models;
using BankAccountAPI.Repositories;


namespace BankTests.RepositoryTests;

public class BankAccountRepositoryTests
{
    [Fact]
    public void CreateAccount_WhenInvalidAccountNumber_ThrowsException()
    {
        // Arrange
        var repo = new BankAccountRepository();

        var newAccount = new CheckingAccount
        {
            AccountNumber = -123,
            Name = "My Account",
            Balance = 100
        };

        // Act & Assert
        Assert.Throws<Exception>(() => repo.CreateAccount(newAccount));

    }

    [Fact]
    public void CreateAccount_WhenInvalidAccountName_ThrowsException()
    {
        // Arrange
        var repo = new BankAccountRepository();

        var newAccount = new CheckingAccount
        {
            AccountNumber = 12345,
            Name = "",
            Balance = 100
        };

        // Act & Assert
        Assert.Throws<Exception>(() => repo.CreateAccount(newAccount));
    }

    [Fact]
    public void CreateAccount_WhenValidAccount_AddsToBankAccounts()
    {
        // Arrange
        var repo = new BankAccountRepository();

        var newAccount = new CheckingAccount
        {
            AccountNumber = 12345,
            Name = "My Account",
            Balance = 100
        };

        // Act
        repo.CreateAccount(newAccount);
        var account = repo.GetBankAccount(12345);

        // Assert
        Assert.Equal(newAccount, account);
    }

    [Fact]
    public void CreateAccount_WhenValidAccount_ReturnsAddedAccount()
    {
        // Arrange
        var repo = new BankAccountRepository();

        var newAccount = new CheckingAccount
        {
            AccountNumber = 12345,
            Name = "My Account",
            Balance = 100
        };

        // Act
        var result = repo.CreateAccount(newAccount);

        // Assert
        Assert.Equal(newAccount, result);
    }

    [Fact]
    public void GetBankAccount_WhenNoAccount_ReturnsNull()
    {
        // Arrange
        var repo = new BankAccountRepository();

        // Act
        var result = repo.GetBankAccount(4321);

        // Assert
        Assert.Equal(null, result);
    }

    [Fact]
    public void GetBankAccount_WhenAccountExists_ReturnsAccount()
    {
        // Arrange
        var repo = new BankAccountRepository();
        repo.CreateAccount(new CheckingAccount { AccountNumber = 123, Name = "Account 1" });
        repo.CreateAccount(new CheckingAccount { AccountNumber = 456, Name = "Account 2" });
        repo.CreateAccount(new CheckingAccount { AccountNumber = 789, Name = "Account 3" });

        // Act
        var result = repo.GetBankAccount(456);

        // Assert
        Assert.Equal(456, result.AccountNumber);
        Assert.Equal("Account 2", result.Name);
    }

    [Fact]
    public void Deposit_WhenInvalidAccountNumber_ThrowsException()
    {
        // Arrange
        var repo = new BankAccountRepository();
        var accountNumber = 0;
        var balance = 500;

        // Act & Assert
        Assert.Throws<Exception>(() => repo.Deposit(accountNumber, balance));
    }

    [Fact]
    public void Deposit_WhenNegativeBalance_ThrowsException()
    {
        // Arrange
        var repo = new BankAccountRepository();
        var accountNumber = 123;
        var balance = -50;

        // Act & Assert
        Assert.Throws<Exception>(() => repo.Deposit(accountNumber, balance));
    }

    [Fact]
    public void Deposit_WhenAccountDoesNotExist_ThrowsException()
    {
        // Arrange
        var repo = new BankAccountRepository();
        var accountNumber = 123;
        var balance = 50;

        // Act & Assert
        Assert.Throws<Exception>(() => repo.Deposit(accountNumber, balance));
    }

    [Fact]
    public void Deposit_WhenValidInput_UpdatesAndReturnsBalance()
    {
        // Arrange
        var repo = new BankAccountRepository();

        var accountNumber = 123;
        var deposit = 25;

        repo.CreateAccount(new CheckingAccount
        {
            AccountNumber = accountNumber,
            Balance = 100,
            Name = "Test"
        });

        // Act
        var updatedBalance = repo.Deposit(accountNumber, deposit);

        // Assert
        Assert.Equal(125, updatedBalance);
    }

}

