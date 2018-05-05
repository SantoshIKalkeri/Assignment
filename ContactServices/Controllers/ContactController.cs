using ContactRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class ContactController : ApiController
    {
        private ContactRepository m_contactRepository;

        public ContactController()
        {
            m_contactRepository = new ContactRepository();
        }

        /// <summary>
        /// Gets the contact list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContactModel> Get()
        {
            try
            {
                IEnumerable<ContactModel> contactList = m_contactRepository.GetContactList();
                return contactList;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(
                    HttpStatusCode.NotFound, "Issue in fetching contact list"));
            }
        }

        /// <summary>
        /// Edit/update existing record. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public HttpResponseMessage Put(int id, [FromBody]ContactModel model)
        {
            try
            {
                m_contactRepository.UpdateContact(id, model);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Error in updating contact"));
            }
        }

        /// <summary>
        /// Add a new record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public HttpResponseMessage Post([FromBody]ContactModel model)
        {
            try
            {
                m_contactRepository.AddContact(model);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Error in adding contact"));
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                m_contactRepository.DeleteContact(id);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "Error in adding contact"));
            }
        }
    }
}