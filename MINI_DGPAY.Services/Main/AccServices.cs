using Microsoft.EntityFrameworkCore;
using MINI_DGPAY.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_DGPAY.Services.Main
{
    public class AccServices
    {
        private AppDBContext db;
        public AccServices(AppDBContext _db)
        {
            this.db = _db;
        }

        public async Task<List<BtAccount>> GetAll()
        {
            var response = await db.BtAccounts.AsNoTracking().ToListAsync();
            return response;
        }

        public async Task<BtAccount> GetById(string id)
        {
            var response = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
            if (response == null)
            {
                throw new Exception("Account not found");
            }
            return response;
        }

        public async Task<BtAccount> Create(BtAccount account)
        {
            await db.BtAccounts.AddAsync(account);
            await db.SaveChangesAsync();
            return account;
        }

        public async Task<BtAccount> Update(BtAccount account)
        {
            var existingAccount = await db.BtAccounts.FirstOrDefaultAsync(x => x.UserId == account.UserId);
            if (existingAccount == null)
            {
                throw new Exception("Account not found");
            }
            existingAccount.UserBalance = account.UserBalance;
            existingAccount.ModifyDate = DateTime.UtcNow;
            existingAccount.ModifyBy = account.ModifyBy;
            await db.SaveChangesAsync();
            return existingAccount;
        }
    }
}
