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

        public async Task<string> Update(string code, string username, string moddate, string modby)
        {
            string msg = "";
            var existingAccount = await db.BtAccounts.FirstOrDefaultAsync(x => x.UserId == code);
            if (existingAccount == null)
            {
                msg = "Account not found";
                return msg;
            }

            if(username != null) existingAccount.UserName = username;

            if (moddate != null) existingAccount.ModifyDate = DateTime.UtcNow;

            if (modby != null) existingAccount.ModifyBy = modby;

            try
            {
                await db.SaveChangesAsync();
                msg = "Account updated successfully";
            }
            catch (Exception)
            {
                msg = "Error occurred while updating account";
            }
            return msg;
        }
    }
}
