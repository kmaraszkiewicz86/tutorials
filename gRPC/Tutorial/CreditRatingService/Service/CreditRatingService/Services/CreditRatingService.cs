using CreditRatingShared;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditRatingService.Services
{
    public class CreditRatingService: CreditRatingCheck.CreditRatingCheckBase
    {
        private readonly ILogger<CreditRatingService> _logger;
        private static readonly Dictionary<string, Int32> customerTrustedCredit = new Dictionary<string, Int32>()
        {
            {"id0201", 10000},
            {"id0417", 5000},
            {"id0306", 15000}
        };

        public CreditRatingService(ILogger<CreditRatingService> logger)
        {
            _logger = logger;
        }

        public override Task<CreditReply> CheckCreditRequest(CreditRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreditReply
            {
                IsAcepted = IsEligibleForCredit(request.CustomerId, request.Credit)
            });
        }

        private bool IsEligibleForCredit(string customerId, Int32 credit)
        {
            bool isEligible = false;

            if (customerTrustedCredit.TryGetValue(customerId, out Int32 maxCredit))
            {
                isEligible = credit <= maxCredit;
            }

            return isEligible;
        }
    }
}
