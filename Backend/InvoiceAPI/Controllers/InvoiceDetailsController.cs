using InvoiceAPI.BusinessLogic.Implementation;
using InvoiceAPI.BusinessLogic.Interfaces;
using InvoiceAPI.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAPI.Controllers
{
    [ApiController]
    [Route("/invoiceDetails")]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly IInvoiceDetailsService _invoiceDetailsService;
        public InvoiceDetailsController(IInvoiceDetailsService invoiceDetailServicee)
        {
            _invoiceDetailsService = invoiceDetailServicee;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDetails>>> GetInvoiceDetails()
        {
            var all = await _invoiceDetailsService.GetAll();

            return Ok(all);
        }

        /// <summary>
        /// Add a product
        /// </summary>
        /// <param name="invoiceDetails"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateInvoice(InvoiceDetails invoiceDetails)
        {
            try
            {
                if (invoiceDetails == null)
                {
                    return BadRequest("Invoice details cannot be null");
                }

                await _invoiceDetailsService.Add(invoiceDetails);

                return Ok(invoiceDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update the details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceDetails details)
        {
            try
            {
                var success = await _invoiceDetailsService.Update(id, details);
                if (!success)
                {
                    return NotFound();
                }
                return Ok("Details updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
