using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdoToFood.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EdoToFood.Pages.Restaurant
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;
        [BindProperty]
        public Core.Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cusines { get; set; }
        public EditModel(IRestaurantData restaurantData,
                            IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.restaurantData = restaurantData;

        }
        public IActionResult OnGet(int?restaurantId)
        {
            Cusines = htmlHelper.GetEnumSelectList<Core.Restaurant.CusineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);

            }
            else
            {
                Restaurant = new Core.Restaurant();
            }
            
            if (Restaurant == null)
            {
                return RedirectToAction("./NotFound");
            }

            return Page();
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cusines = htmlHelper.GetEnumSelectList<Core.Restaurant.CusineType>();

                return Page();


            }

            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
            }
            else
            {
                restaurantData.Add(Restaurant);
            }
            
            restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

        }
    }
}
