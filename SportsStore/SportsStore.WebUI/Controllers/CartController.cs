using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        // This is how we get the data from the database.
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc){
            repository  = repo;
            orderProcessor = proc;
        }
        

        // ??
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {
 //               Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        

        // This is the action method to add a product to the cart.
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null){
                // GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        // ????
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null){
                // GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
         }


        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }


        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippinfDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry you cart is empty!");
            }

            if (ModelState.IsValid)
            {
               
                orderProcessor.ProcessOrder(cart, shippinfDetails);
                cart.Clear();
                return View("Completed");
            } else {
                return View(shippinfDetails);
            }

        }


    }
}