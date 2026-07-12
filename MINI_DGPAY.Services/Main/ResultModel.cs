using MINI_DGPAY.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINI_DGPAY.Services.Main
{
    public class ResultModel
    {
        public CommonResponse<BtAccount>? acresponse { get; set; }

        public CommonResponse<BtTransaction>? trresponse { get; set; }
    }
}
