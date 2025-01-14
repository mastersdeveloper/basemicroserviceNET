﻿using Microsoft.AspNetCore.Mvc;
using BASE.MICRONET.Account.DTOs;
using BASE.MICRONET.Account.Service;
using System.Linq;
using BASE.MICRONET.Cross.Metric.Registry;

namespace BASE.MICRONET.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMetricsRegistry _metricsRegistry;

        public AccountController(IAccountService accountService, IMetricsRegistry metricsRegistry)
        {
            _accountService = accountService;
            _metricsRegistry = metricsRegistry;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _metricsRegistry.IncrementFindQuery();
            return Ok(_accountService.GetAll());
        }

        [HttpPost("Deposit")]
        public IActionResult Deposit([FromBody] AccountRequest request)
        {
            var result = _accountService.GetAll().Where(x => x.IdAccount == request.IdAccount).FirstOrDefault();
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount + request.Amount,
                Customer = result.Customer
            };
            _accountService.Deposit(account);
            return Ok();
        }

        [HttpPost("Withdrawal")]
        public IActionResult Withdrawal([FromBody] AccountRequest request)
        {
            var result = _accountService.GetAll().Where(x => x.IdAccount == request.IdAccount).FirstOrDefault();
            if (result.TotalAmount < request.Amount)
            {
                return BadRequest(new { message = "The indicated amount cannot be withdrawal" });
            }
            Models.Account account = new Models.Account()
            {
                IdAccount = request.IdAccount,
                IdCustomer = result.IdCustomer,
                TotalAmount = result.TotalAmount - request.Amount,
                Customer = result.Customer
            };
            _accountService.Withdrawal(account);
            return Ok();
        }
    }
}
