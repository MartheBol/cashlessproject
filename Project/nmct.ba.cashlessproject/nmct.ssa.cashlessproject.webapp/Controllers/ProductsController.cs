using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products
        public List<Products> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return ProductenDA.GetProducts(p.Claims);
        }

        //verwijderen
        public HttpStatusCode Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            ProductenDA.DeleteProduct(id, p.Claims);
            return HttpStatusCode.OK;
        }

        public Products Post(Products product)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = ProductenDA.InsertProduct(product,p.Claims);
            product.Id = id;
            return product;
        }

        public HttpStatusCode Put(long id, Products prod)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            ProductenDA.UpdateProduct(id, prod, p.Claims);
            return HttpStatusCode.OK;
        }



    }
}