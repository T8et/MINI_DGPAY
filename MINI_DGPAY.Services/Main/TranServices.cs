using Azure;
using Microsoft.EntityFrameworkCore;
using MINI_DGPAY.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_DGPAY.Services.Main
{
    public class TranServices
    {
        private AppDBContext db;
        public TranServices(AppDBContext _db)
        {
            db = _db;
        }

        public async Task<List<BtTransaction>> GetAll()
        {
            var response = await db.BtTransactions.AsNoTracking().ToListAsync();
            return response;
        }

        public async Task<BtTransaction> GetById(string code)
        {
            var response = await db.BtTransactions.AsNoTracking().FirstOrDefaultAsync(x => x.TranId == code);
            if (response == null)
            {
                return new BtTransaction()
                {
                    TranId = "NotFound",
                    TranDate = null,
                    TranSender = null,
                    TranRecver = null,
                    TranAmount = null,
                    TranRemk = null,
                    TranType = null,
                    TranStatus = null
                };
            }
            return response;
        }

        public async Task<string> MakeDeposit(string sdr, string rcr, decimal amt, string remk)
        {
            var sdresp = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == sdr);
            if (sdresp == null) return "Sender Not Found";
            if (!string.IsNullOrEmpty(sdr) && !string.IsNullOrEmpty(rcr) && amt < 0 && !string.IsNullOrEmpty(remk))
            {
                return "Please provide all required information";
            }
            BtTransaction tran = new BtTransaction()
            {
                TranId = await GenerateTransactionId(),
                TranDate = DateTime.Now,
                TranSender = sdr,
                TranRecver = rcr,
                TranAmount = amt,
                TranRemk = remk,
                TranType = "Deposit",
                TranStatus = 1
            };

            try
            {
                var bal = sdresp.UserBalance + amt;
                sdresp.UserBalance = bal;
                db.Entry(sdresp).State = EntityState.Modified;
                await db.SaveChangesAsync();

                await db.BtTransactions.AddAsync(tran);
                await db.SaveChangesAsync();
                return "Deposit Successful";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> GenerateTransactionId()
        {
            var lastId = db.BtTransactions
                            .AsNoTracking()
                            .OrderByDescending(t => Convert.ToInt32(t.TranId.Substring(2)))
                            .Select(t => t.TranId)
                            .FirstOrDefault();

            if (string.IsNullOrEmpty(lastId))
                return "TR0000001"; 

            var numberPart = int.Parse(lastId.Substring(2));
            numberPart++;

            return $"TR{numberPart:D7}";
        }
    }
}
