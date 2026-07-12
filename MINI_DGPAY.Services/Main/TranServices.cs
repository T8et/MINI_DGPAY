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

        private ResultModel resmdl;
        public TranServices(AppDBContext _db, ResultModel resmdl = null)
        {
            db = _db;
            this.resmdl = resmdl;
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

        public async Task<CommonResponse<BtTransaction>> MakeDeposit(string sdr, decimal amt, string remk)
        {
            var sdresp = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == sdr);
            if (sdresp == null) return resmdl.trresponse = CommonResponse<BtTransaction>.NotFound();
            if (!string.IsNullOrEmpty(sdr) && amt < 0 && !string.IsNullOrEmpty(remk))
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError("","Filled All required Fields");
            }
            BtTransaction tran = new BtTransaction()
            {
                TranId = await GenerateTransactionId(),
                TranDate = DateTime.Now,
                TranSender = sdr,
                TranRecver = sdr,
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
                return resmdl.trresponse = CommonResponse<BtTransaction>.Success();
            }
            catch (Exception)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.SystemError();
            }
        }

        public async Task<CommonResponse<BtTransaction>> MakeWithdrawl(string sdr, decimal amt, string remk, int pin = 0)
        {
            var sdresp = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == sdr);
            if (sdresp == null) return resmdl.trresponse = CommonResponse<BtTransaction>.NotFound();
            if (!string.IsNullOrEmpty(sdr) && amt < 0 && !string.IsNullOrEmpty(remk) && pin == 0)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError("","Filled All required Fields");
            }

            if(sdresp.UserPin != pin)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError("","Invalid PIN");
            }

            BtTransaction tran = new BtTransaction()
            {
                TranId = await GenerateTransactionId(),
                TranDate = DateTime.Now,
                TranSender = sdr,
                TranRecver = sdr,
                TranAmount = amt,
                TranRemk = remk,
                TranType = "Withdrawal",
                TranStatus = 1
            };

            try
            {
                var bal = sdresp.UserBalance - amt;
                sdresp.UserBalance = bal;
                db.Entry(sdresp).State = EntityState.Modified;
                await db.SaveChangesAsync();

                await db.BtTransactions.AddAsync(tran);
                await db.SaveChangesAsync();
                return resmdl.trresponse = CommonResponse<BtTransaction>.Success();
            }
            catch (Exception)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.SystemError();
            }
        }

        public async Task<CommonResponse<BtTransaction>> MakeTransfer(string sdr, string rcr, decimal amt, string remk, int pin = 0)
        {
            if (!string.IsNullOrEmpty(sdr) && !string.IsNullOrEmpty(rcr) && amt < 0 && !string.IsNullOrEmpty(remk) && pin == 0)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError();
            }

            if(sdr == rcr)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError("","Sender and Receiver cannot be the same");
            }

            var sdresp = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == sdr);
            if (sdresp == null) 
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.NotFound("","Account Not Exists");
            }

            var rcresp = await db.BtAccounts.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == rcr);
            if (rcresp == null)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.NotFound("","Account Not Exists");
            }

            if (sdresp.UserPin != pin)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError("","Invalid PIN");
            }

            if (sdresp.UserBalance < amt)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.ValidationError("","Insufficient Balance");
            }

            BtTransaction tran = new BtTransaction()
            {
                TranId = await GenerateTransactionId(),
                TranDate = DateTime.Now,
                TranSender = sdr,
                TranRecver = rcr,
                TranAmount = amt,
                TranRemk = remk,
                TranType = "Transfer",
                TranStatus = 1
            };

            try
            {
                var senderBalance = sdresp.UserBalance - amt;
                sdresp.UserBalance = senderBalance;
                db.Entry(sdresp).State = EntityState.Modified;

                var receiverBalance = rcresp.UserBalance + amt;
                rcresp.UserBalance = receiverBalance;
                db.Entry(rcresp).State = EntityState.Modified;
                await db.SaveChangesAsync();

                await db.BtTransactions.AddAsync(tran);
                await db.SaveChangesAsync();
                return resmdl.trresponse = CommonResponse<BtTransaction>.Success();
            }
            catch (Exception)
            {
                return resmdl.trresponse = CommonResponse<BtTransaction>.SystemError();
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
