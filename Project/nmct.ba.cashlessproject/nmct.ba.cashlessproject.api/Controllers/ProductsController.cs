using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products
        public List<Products> Get()
        {
            return ProductenDA.GetProducts();
        }

        //verwijderen
        public HttpStatusCode Delete(int id)
        {
            ProductenDA.DeleteProduct(id);
            return HttpStatusCode.OK;
        }

        public Products Post(Products product)
        {
            int id = ProductenDA.InsertProduct(product);
            product.Id = id;
            return product;
        }

        public HttpStatusCode Put(long id, Products prod)
        {
            ProductenDA.UpdateProduct(id, prod);
            return HttpStatusCode.OK;
        }
        


    }
}