using AutoMapper;
using InvoiceAPI.BusinessLogic.Interfaces;
using InvoiceAPI.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAPI.Controllers
{
    [ApiController]
    [Route("/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Invoice>>> GetInvoices()
        {
            var all = await _invoiceService.GetAll();

            return Ok(all);
        }

        /// <summary>
        /// Add an invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateInvoice(Invoice invoice)
        {
            try
            {
                if (invoice == null)
                {
                    return BadRequest("Invoice cannot be null");
                }

                await _invoiceService.Add(invoice);

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}/{locationId}")]
        public async Task<IActionResult> DeleteInvoice(int id, int locationId)
        {
            try
            {
                var success = await _invoiceService.Delete(id, locationId);
                if (!success)
                {
                    return NotFound();
                }
                return Ok("Invoice deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Update the invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpPut("{invoiceId}/{locationId}")]
        public async Task<IActionResult> UpdateInvoice(int invoiceId, int locationId, Invoice invoice)
        {
            try
            {
                var success = await _invoiceService.Update(invoiceId, locationId, invoice);
                if (!success)
                {
                    return NotFound();
                }
                return Ok("Invoice updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
