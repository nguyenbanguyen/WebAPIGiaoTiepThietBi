using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestJWTIn.NetCoreApi.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TestJWTIn.NetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Contact")]
    public class ContactController : Controller
    {
        private readonly PeopleDbContext _context;

        public ContactController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/Contact
        [HttpGet("get")]
        public IEnumerable<Contact> GetContact()
        {
            return _context.Contact;
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] string id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contact
        [HttpPost("post")]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }
        [HttpPost("RegisterToAnotherApi")]
        public async Task<bool> SendRegisterToApiAsyn(string username, string email,string password)
        {
            using (var httpClient = new HttpClient(new HttpClientHandler()))
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/account/register");
                request.Headers.Authorization = new AuthenticationHeaderValue(  "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", "yourusername", "yourpwd"))));

                var json = JsonConvert.SerializeObject(new { Email = email, Username = username, Password = password });
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var resp = await httpClient.SendAsync(request);

                return resp.IsSuccessStatusCode;
            }
            //List<Post> posts = null;
            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri("http://jsonplaceholder.typicode.com/posts/")
            //};

            //var response = await client.GetAsync("");
            //var stream = await response.Content.ReadAsStreamAsync();
            //var serializer = new DataContractJsonSerializer(typeof(List<Post>));
            //posts = (List<Post>)serializer.ReadObject(stream);
        }
        private bool ContactExists(string id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}