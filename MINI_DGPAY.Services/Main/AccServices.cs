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
        private ResultModel resmdl;
        public AccServices(AppDBContext _db, ResultModel resmdl)
        {
            this.db = _db;
            this.resmdl = resmdl;
        }

        public async Task<List<BtAccount>> GetAll()
        {
            var response = await db.BtAccounts.AsNoTracking().ToListAsync();
            return response;
        }

        public async Task<CommonResponse<BtAccount>> GetById(string id)
        {
            var response = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
            if (response == null)
            {
                return resmdl.acresponse = CommonResponse<BtAccount>.NotFound();
            }
            return resmdl.acresponse = CommonResponse<BtAccount>.Success();
        }

        public async Task<CommonResponse<BtAccount>> Create(BtAccount account)
        {
            await db.BtAccounts.AddAsync(account);
            await db.SaveChangesAsync();
            return resmdl.acresponse = CommonResponse<BtAccount>.Success();
        }

        public async Task<CommonResponse<BtAccount>> Update(string code, string username, string moddate, string modby)
        {
            var existingAccount = await db.BtAccounts.FirstOrDefaultAsync(x => x.UserId == code);
            if (existingAccount == null)
            {
                return resmdl.acresponse = CommonResponse<BtAccount>.NotFound("","Account Not Exists");
            }

            if(username != null) existingAccount.UserName = username;

            if (moddate != null) existingAccount.ModifyDate = DateTime.UtcNow;

            if (modby != null) existingAccount.ModifyBy = modby;

            try
            {
                await db.SaveChangesAsync();
                return resmdl.acresponse = CommonResponse<BtAccount>.Success();
            }
            catch (Exception)
            {
                return resmdl.acresponse = CommonResponse<BtAccount>.SystemError();
            }
        }
    }
}
