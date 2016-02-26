using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace SalonNamespace
{
  public class HomeModule : NancyModule
  {

    public HomeModule()
    {
      Get["/"] =_=> {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/Stylist/new"] =_=> {
        return View["createStylist.cshtml"];
      };

      Post["/Stylist/new"] =_=> {
        Stylist newStylist = new Stylist(Request.Form["stylistName"]);
        newStylist.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };
    }

  }

}
