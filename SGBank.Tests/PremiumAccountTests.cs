using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    class PremiumAccountTests
    {   //check actual balance 
        [TestCase("99999", "Premium Account", 1000, AccountType.Premium, -1600, 1000, false)]
        [TestCase("99999", "Premium Account", 1000, AccountType.Free, -100, 1000, false)]
        [TestCase("99999", "Premium Account", 1000, AccountType.Premium, 100, 1000, false)]
        [TestCase("99999", "Premium Account", 1000, AccountType.Premium, -100, 900, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Name = name;
            account.Type = accountType;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }


        [TestCase("99999", "Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, -100, false)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, 250, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Name = name;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);

        }
    }
}
