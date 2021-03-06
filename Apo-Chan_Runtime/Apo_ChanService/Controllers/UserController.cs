﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Apo_ChanService.DataObjects;
using Apo_ChanService.Models;

namespace Apo_ChanService.Controllers
{
    public class UserController : BaseController<UserItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            Apo_ChanContext context = new Apo_ChanContext();
            DomainManager = new EntityDomainManager<UserItem>(context, Request);
        }

        // GET tables/user
        [HttpGet]
        public IQueryable<UserItem> GetAllUserItems()
        {
            UserItem item = this.getObjByQuery<UserItem>();
            var q = Query();
            //TODO:Now Only Filter Id, ProviderType and UserProviderId.
            if (item != null)
            {
                if (!string.IsNullOrWhiteSpace(item.Id))
                {
                    q = q.Where(x => item.Id == x.Id);
                }
                if (item.ProviderType.HasValue)
                {
                    q = q.Where(x => item.ProviderType == x.ProviderType);
                }
                if (!string.IsNullOrWhiteSpace(item.UserProviderId))
                {
                    q = q.Where(x => item.UserProviderId == x.UserProviderId);
                }
            }
            return q;
        }

        // GET tables/user/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [HttpGet]
        public SingleResult<UserItem> GetUserItem(string id)
        {
            return Lookup(id);
        }

        // POST tables/user
        [HttpPost]
        public async Task<IHttpActionResult> PostUserItem(UserItem item)
        {
            UserItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // PATCH tables/user/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [HttpPatch]
        public Task<UserItem> PatchUserItem(string id, Delta<UserItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        //// DELETE tables/user/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public Task DeleteUserItem(string id)
        //{
        //    return DeleteAsync(id);
        //}
    }
}